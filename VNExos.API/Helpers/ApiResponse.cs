using Microsoft.AspNetCore.Mvc;

namespace VNExos.API.Helpers;

public class ApiResponse<TKey> : IActionResult
{
    private int Status;
    private TKey? Data;

    public ApiResponse(int Status, TKey? Data)
    {
        this.Status = Status;
        this.Data = Data;
    }

    public static ApiResponse<TKey> CreateOk(TKey? data)
    {
        return new ApiResponse<TKey>(200, data);
    }

    public static ApiResponse<TKey> CreateBadRequest(TKey? data)
    {
        return new ApiResponse<TKey>(400, data);
    }

    public static ApiResponse<TKey> CreateInternalError(TKey? data)
    {
        return new ApiResponse<TKey>(500, data);
    }

    public async Task ExecuteResultAsync(ActionContext context)
    {
        var objectResult = new ObjectResult(this)
        {
            StatusCode = Status,
            Value = new
            {
                Status,
                Result = Data
            },
        };
        await objectResult.ExecuteResultAsync(context);
    }
}
