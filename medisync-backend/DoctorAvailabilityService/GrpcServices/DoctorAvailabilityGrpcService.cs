using DoctorAvailabilityService.Interfaces;
using Grpc.Core;
using Shared.Protos.DoctorAvailability;

namespace DoctorAvailabilityService.GrpcServices
{
    public class DoctorAvailabilityGrpcService:DoctorAvailabilityCheckService.DoctorAvailabilityCheckServiceBase
    {
        private readonly IDoctorAvailabilityService _service;

        public DoctorAvailabilityGrpcService(IDoctorAvailabilityService service)
        {
            _service = service;
        }

        public override async Task<DoctorAvailabilityResponse>
            IsDoctorAvailable(
                DoctorAvailabilityRequest request,
                ServerCallContext context)
        {
            var available = await _service.IsDoctorAvailableAsync(
                Guid.Parse(request.DoctorId),
                DateTime.Parse(request.StartTime),
                DateTime.Parse(request.EndTime));

            return new DoctorAvailabilityResponse
            {
                IsAvailable = available
            };
        }
    }
}
