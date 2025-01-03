using Microsoft.EntityFrameworkCore;
using VNExos.Domain.Entities;
using VNExos.Domain.Presistence;
using VNExos.Domain.Repositories;

namespace VNExos.Infrastructure.Repositories;

internal class TranslationRepository(VNExosContext context) : ATranslationRepository(context)
{
    public override async Task<ICollection<Translation>> CreateTranslations(ICollection<Translation> translations)
    {
        await dbSet.AddRangeAsync(translations);
        await context.SaveChangesAsync();
        return translations;
    }

    public override async Task<Translation?> UpdateTranslationsByCodeAndOrigin(string code, string origin, Translation translation)
    {
        var existingEntity = await dbSet
            .Where(t => t.Origin == origin)
            .Include(t => t.Language)
            .Where(t => t.Language!.Code == code)
            .FirstOrDefaultAsync();
        var res = await UpdateFromEntity(existingEntity!, translation);
        return res;
    }
}
