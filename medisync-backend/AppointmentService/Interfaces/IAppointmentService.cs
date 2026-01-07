using AppointmentService.DTOs;

namespace AppointmentService.Interfaces
{
    public interface IAppointmentService
    {
        Task<BookAppointmentResponseDto> BookAsync(BookAppointmentRequestDto request);
    }
}
