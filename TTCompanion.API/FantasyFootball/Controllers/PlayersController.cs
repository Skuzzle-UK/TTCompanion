﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TTCompanion.API.FantasyFootball.Models.Player;
using TTCompanion.API.FantasyFootball.Services;
using TTCompanion.API.FantasyFootball.Services.Player;
using TTCompanion.API.FantasyFootball.Services.Race;

namespace TTCompanion.API.FantasyFootball.Controllers
{
    [ApiController]
    [Authorize]
    [ApiVersion("1.0")]
    [Route("ttcompanion/api/v{version:apiVersion}/fantasyfootball")]
    public class PlayersController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IRaceRepository _raceRepository;
        private readonly IMapper _mapper;
        const int maxPlayersPageSize = 100;

        public PlayersController(IRepository repository, IPlayerRepository playerRepository, IRaceRepository raceRepository, IMapper mapper)
        {
            _repository = repository;
            _playerRepository = playerRepository;
            _raceRepository = raceRepository;
            _mapper = mapper;
        }

        [HttpGet("players", Name = "Get Players")]
        public async Task<ActionResult<IEnumerable<PlayerDto>>> GetPlayersAsync(int? raceId, string? name, string? searchQuery, bool withSkills = false, int pageSize = 30, int pageNumber = 1)
        {
            if(pageSize > maxPlayersPageSize)
            {
                pageSize = maxPlayersPageSize;
            }

            if (raceId != null && !await _raceRepository.RaceExistsAsync(raceId!.Value))
            {
                return NotFound($"raceId: {raceId} does not exist.");
            }

            var (players, paginationMetadata) = await _playerRepository.GetPlayersAsync(raceId, name, searchQuery, withSkills, pageSize, pageNumber);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

            if (withSkills)
            {
                return Ok(_mapper.Map<IEnumerable<PlayerDto>>(players));
            }

            return Ok(_mapper.Map<IEnumerable<PlayerOnlyDto>>(players));
        }

        [HttpGet("players/{playerId}", Name = "Get Player By Id")]
        public async Task<ActionResult<PlayerDto>> GetPlayerByIdAsync(int playerId, bool? withSkills = false)
        {
            if (!await _playerRepository.PlayerExistsAsync(playerId))
            {
                return NotFound($"playerId: {playerId} does not exist.");
            }

            var player = await _playerRepository.GetPlayerByIdAsync(playerId, withSkills!.Value);

            return Ok(_mapper.Map<PlayerDto>(player));
        }

        [HttpPost("players", Name = "Create Player")]
        public async Task<ActionResult<PlayerDto>> CreatePlayer(int raceId, PlayerDto player)
        {
            if (!await _raceRepository.RaceExistsAsync(raceId))
            {
                return NotFound();
            }

            var finalPlayer = _mapper.Map<Entities.Player>(player);

            await _playerRepository.AddPlayerForRaceAsync(raceId, finalPlayer);

            await _repository.SaveChangesAsync();

            var createdPlayerToReturn = _mapper.Map<PlayerDto>(finalPlayer);

            return CreatedAtRoute("Get Player By Id",
                new
                {
                    raceId = raceId,
                    playerId = createdPlayerToReturn.Id
                },
                createdPlayerToReturn);
        }

        [HttpPost("players/{playerId}", Name = "Update Player")]
        public async Task<ActionResult> UpdatePlayer(int playerId, PlayerForUpdateDto player)
        {
            var playerEntity = await _playerRepository.GetPlayerByIdAsync(playerId);
            if(playerEntity == null)
            {
                return NotFound($"playerId: {playerId} does not exist.");
            }

            if (!playerEntity.Modifiable)
            {
                return Unauthorized($"playerId: {playerId} can not be modified.");
            }

            _mapper.Map(player, playerEntity);
            await _repository.SaveChangesAsync();
            
            return NoContent();
        }

        [HttpPatch("players/{playerId}", Name = "Partially Update Player")]
        public async Task<ActionResult> PartiallyUpdatePlayer(int playerId, JsonPatchDocument<PlayerForUpdateDto> patchDocument)
        {
            var playerEntity = await _playerRepository.GetPlayerByIdAsync(playerId);
            if (playerEntity == null)
            {
                return NotFound($"playerId: {playerId} does not exist.");
            }

            if (!playerEntity.Modifiable)
            {
                return Unauthorized($"playerId: {playerId} can not be modified.");
            }

            var playerToPatch = _mapper.Map<PlayerForUpdateDto>(playerEntity);
            
            patchDocument.ApplyTo(playerToPatch, ModelState);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(!TryValidateModel(playerToPatch))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(playerToPatch, playerEntity);

            await _repository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("players/{playerId}", Name = "Delete Player")]
        public async Task<ActionResult> DeletePlayer(int playerId)
        {

            var playerEntity = await _playerRepository.GetPlayerByIdAsync(playerId);
            if (playerEntity == null)
            {
                return NotFound($"playerId: {playerId} does not exist.");
            }
            if(!playerEntity.Modifiable)
            {
                return Unauthorized($"playerId: {playerId} can not be modified.");
            }

            _playerRepository.DeletePlayer(playerEntity);
            await _repository.SaveChangesAsync();

            return NoContent();
        }
    }
}
