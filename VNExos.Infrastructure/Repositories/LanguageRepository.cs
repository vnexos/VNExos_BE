using Microsoft.EntityFrameworkCore;
using VNExos.Domain.Entities;
using VNExos.Domain.Presistence;
using VNExos.Domain.Repositories;

namespace VNExos.Infrastructure.Repositories;

internal class LanguageRepository(VNExosContext context) : ALanguageRepository(context)
{
    public override async Task<Language?> GetByCode(string code)
    {
        return await dbSet.Where(r => r.Code == code).FirstOrDefaultAsync();
    }
}
