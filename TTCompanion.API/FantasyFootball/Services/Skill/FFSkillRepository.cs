using Microsoft.EntityFrameworkCore;
using TTCompanion.API.FantasyFootball.DBContexts;
using TTCompanion.API.FantasyFootball.Entities;

namespace TTCompanion.API.FantasyFootball.Services.Skill
{
    public class FFSkillRepository : IFFSkillRepository
    {
        private readonly FFContext _context;

        public FFSkillRepository(FFContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<FFSkill>> GetSkillsAsync()
        {
            return await _context.Skills
                .OrderBy(s => s.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<FFSkill>> GetSkillsForPlayerAsync(int playerId)
        {
            return await _context.Skills
                .OrderBy(s => s.Name)
                .Where(s => s.PlayerId == playerId)
                .ToListAsync();
        }

        public async Task<FFSkill?> GetSkillByIdAsync(int skillId)
        {
            return await _context.Skills
                .Where(s => s.Id == skillId)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> SkillExistsAsync(int skillId)
        {
            return await _context.Skills.AnyAsync(s => s.Id == skillId);
        }

        public Task AddSkillForPlayerAsync(int playerId, FFSkill skill)
        {
            throw new NotImplementedException();
        }

        public void DeleteSkill(FFSkill skill)
        {
            _context.Skills.Remove(skill);
        }
    }
}
