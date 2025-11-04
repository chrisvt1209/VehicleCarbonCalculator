using Grpc.Core;
using RdwData.proto;
using static RdwData.proto.RdwDataService;

namespace Rdw.Service.Services;

public class RdwDataService(
    ILogger<RdwDataService> logger,
    EmissionService service) : RdwDataServiceBase
{

    public override async Task<RdwEmissionReply> GetShiftEmission(RdwEmissionRequest request, ServerCallContext context)
    {
        logger.LogDebug($"{nameof(GetShiftEmission)} called with request {{@ServiceRequest}}", request);

        var result = await service.CalculateEmissionAsync(request.LicensePlate, request.TotalDistanceKm, context.CancellationToken);

        if (result is null)
        {
            return new RdwEmissionReply();
        }

        return new RdwEmissionReply()
        {
            TotalEmissionKg = (double)result
        };
    }
}
