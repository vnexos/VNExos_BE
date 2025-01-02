using VNExos.Domain.Common;
using VNExos.Domain.Entities;
using VNExos.Domain.Presistence;

namespace VNExos.Domain.Repositories;

public abstract class ATranslationRepository : ACommonRepository<Translation>
{
    public ATranslationRepository(VNExosContext context) : base(context)
    {
    }

    public abstract Task<ICollection<Translation>> CreateTranslations(ICollection<Translation> translations);
}
