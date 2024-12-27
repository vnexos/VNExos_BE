using VNExos.Domain.Common;
using VNExos.Domain.Entities;
using VNExos.Domain.Presistence;

namespace VNExos.Domain.Repositories;

public abstract class ALanguageRepository : ACommonRepository<Language>
{
    protected ALanguageRepository(VNExosContext context) : base(context)
    {
    }

    public abstract Task<Language?> GetByCode(string code);
}
