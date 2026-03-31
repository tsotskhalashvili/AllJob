using AllJob.Application.Interfaces.Repositories;
using AllJob.Domain.Entities.Candidate;
using AllJob.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AllJob.Persistence.Repositories;

public class CandidateRepository(AppDbContext context)
    : GenericRepository<CandidateProfile>(context), ICandidateRepository
{
    public async Task<CandidateProfile?> GetCandidateWithDetailsAsync(Guid userId)
        => await _dbSet
            .AsNoTracking()
            .Include(c => c.Address)
            .Include(c => c.Skills)
                .ThenInclude(cs => cs.Skill)
            .Include(c => c.Experiences)
            .Include(c => c.Educations)
            .FirstOrDefaultAsync(c => c.UserId == userId);
}