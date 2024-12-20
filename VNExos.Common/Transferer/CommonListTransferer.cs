﻿using MediatR;
using VNExos.Common.DataTransferObject;

namespace VNExos.Common.Transferer;

public class CommonListTransferer<TDto> : IRequest<ICollection<TDto>>
    where TDto : CommonDto
{
    public virtual string? SearchPhase { get; set; }
}
