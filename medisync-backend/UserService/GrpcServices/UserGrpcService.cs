using UserService.Interfaces;
using Shared.Protos.User;
using Grpc.Core;

namespace UserService.GrpcServices
{
    public class UserGrpcService : DoctorUserService.DoctorUserServiceBase
    {
        private readonly IUserService _userService;

        public UserGrpcService(IUserService userService)
        {
            _userService = userService;
        }

        public override async Task<DoctorListResponse> GetDoctorsBySpeciality(
            SpecialityRequest request, ServerCallContext context)
        {
            var doctors = await _userService
                .GetDoctorsBySpecialityAsync(request.Speciality);

            var response = new DoctorListResponse();
            response.Doctors.AddRange(
                doctors.Select(d => new DoctorDto { DoctorId = d.ToString() })
            );

            return response;
        }
    }

}
