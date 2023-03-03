﻿using Microsoft.EntityFrameworkCore;
using TTCompanion.API.FantasyFootball.DBContexts;

namespace TTCompanion.API.FantasyFootball.Services.Skill
{
    public class SkillRepository : ISkillRepository
    {
        private readonly DBContext _context;

        public SkillRepository(DBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Entities.Skill>> GetSkillsAsync(int? playerId, string? name, string? searchQuery, int pageNumber = 1, int pageSize = 30)
        {
            var collection = _context.Skills as IQueryable<Entities.Skill>;

            if (!string.IsNullOrEmpty(name))
            {
                name = name.Trim();
                collection = collection.Where(s => s.Name == name);
            }

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                searchQuery = $"%{searchQuery}%";
                collection = collection
                    .Where(s => EF.Functions.Like(s.Name, searchQuery));
            }

            if (playerId != null)
            {
                collection = collection
                .Where(s => s.Players.Any(p => p.Id == playerId));
            }

            return await collection
                .OrderBy(s => s.Name)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Entities.Skill?> GetSkillByIdAsync(int skillId)
        {
            return await _context.Skills
                .Where(s => s.Id == skillId)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> SkillExistsAsync(int skillId)
        {
            return await _context.Skills.AnyAsync(s => s.Id == skillId);
        }

        public Task AddSkillForPlayerAsync(int playerId, Entities.Skill skill)
        {
            throw new NotImplementedException();
        }

        public void DeleteSkill(Entities.Skill skill)
        {
            _context.Skills.Remove(skill);
        }
    }
}
