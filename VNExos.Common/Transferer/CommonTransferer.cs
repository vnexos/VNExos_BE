using MediatR;
using VNExos.Common.DataTransferObject;

namespace VNExos.Common.Transferer;

public class CommonTransferer<TDto> : IRequest<TDto>
    where TDto : CommonDTO
{
    public virtual Guid Id { get; set; }
}
