using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNExos.Domain.Common;
using VNExos.Domain.Entities;
using VNExos.Domain.Presistence;

namespace VNExos.Domain.Repositories;

public abstract class ALanguageRepository : ACommonRepository<Language>
{
    protected ALanguageRepository(VNExosContext context) : base(context)
    {
    }
}
