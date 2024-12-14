using VNExos.Domain.Presistence;
using VNExos.Domain.Repositories;

namespace VNExos.Infrastructure.Repositories;

internal class LanguageRepository(VNExosContext context) : ALanguageRepository(context)
{
}
