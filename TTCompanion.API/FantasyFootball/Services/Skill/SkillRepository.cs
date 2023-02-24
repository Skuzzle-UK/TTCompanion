﻿using Microsoft.EntityFrameworkCore;
using TTCompanion.API.FantasyFootball.DBContexts;
using TTCompanion.API.FantasyFootball.Entities;

namespace TTCompanion.API.FantasyFootball.Services.Skill
{
    public class SkillRepository : ISkillRepository
    {
        private readonly DBContext _context;

        public SkillRepository(DBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Entities.Skill>> GetSkillsAsync()
        {
            return await _context.Skills
                .OrderBy(s => s.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Entities.Skill>> GetSkillsForPlayerAsync(int playerId)
        {
            return await _context.Skills
                .OrderBy(s => s.Name)
                .Where(s => s.PlayerId == playerId)
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