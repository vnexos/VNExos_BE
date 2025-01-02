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
}
