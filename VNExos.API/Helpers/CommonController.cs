using MediatR;
using Microsoft.AspNetCore.Mvc;
using VNExos.Common.DataTransferObject;
using VNExos.Common.Transferer;

namespace VNExos.API.Helpers;

public class CommonController<TDto>(IMediator mediator) : ControllerBase
    where TDto : CommonDto
{
    protected async Task<IActionResult> Execute<TRequest>(TRequest request)
        where TRequest : CommonTransferer<TDto>
    {
        try
        {
            var res = await mediator.Send(request);
            return ApiResponse<object>.CreateOk(res);
        } catch (Exception ex)
        {
            return ApiResponse<string>.CreateInternalError(ex.Message);
        }
    }

    protected async Task<IActionResult> Execute(CommonListTransferer<TDto> request)
    {
        try
        {
            var res = await mediator.Send(request);
            return ApiResponse<object>.CreateOk(res);
        }
        catch (Exception ex)
        {
            return ApiResponse<string>.CreateInternalError(ex.Message);
        }
    }
}
