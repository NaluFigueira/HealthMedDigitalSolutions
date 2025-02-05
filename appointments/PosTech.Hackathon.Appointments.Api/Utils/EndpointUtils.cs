using System.Net;

namespace PosTech.Hackathon.Appointments.Api.Utils;

public class EndpointUtils
{
    public static async Task<IResult> CallUseCase(Func<Task<IResult>> func)
    {
        try
        {
            return await func();
        }
        catch (Exception ex)
        {
            return Results.Problem(statusCode: (int?)HttpStatusCode.InternalServerError, detail: ex.Message);
        }
    }
}
