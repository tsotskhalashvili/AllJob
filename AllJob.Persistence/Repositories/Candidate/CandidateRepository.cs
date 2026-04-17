using AllJob.Application.Interfaces.Repositories.Candidate;
using AllJob.Domain.Entities.Candidate;
using AllJob.Persistence.Context;
using AllJob.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace AllJob.Persistence.Repositories.Candidate;

public class CandidateRepository(AppDbContext context)
    : GenericRepository<CandidateProfile>(context), ICandidateRepository
{
    public async Task<CandidateProfile?> GetByIdWithDetailsAsync(Guid candidateId)
      => await _dbSet
        .AsNoTracking()
        .Include(c => c.Address)
        .Include(c => c.Skills)
          .ThenInclude(cs => cs.Skill)
        .Include(c => c.Experiences)
        .Include(c => c.Educations)
        .FirstOrDefaultAsync(c => c.Id == candidateId);

    public async Task<CandidateProfile?> GetCandidateWithDetailsAsync(Guid userId)
        => await _dbSet
            .AsNoTracking()
            .Include(c => c.Address)
            .Include(c => c.Skills)
                .ThenInclude(cs => cs.Skill)
            .Include(c => c.Experiences)
            .Include(c => c.Educations)
            .FirstOrDefaultAsync(c => c.UserId == userId);

    public async Task<IReadOnlyList<CandidateProfile>> GetOpenToWorkAsync()
     => await _dbSet
         .AsNoTracking()
         .Include(c => c.User)
         .Include(c => c.Skills)
             .ThenInclude(cs => cs.Skill)
         .Where(c => c.IsOpenToWork)
         .ToListAsync();
}