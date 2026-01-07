namespace AppointmentService.DTOs
{
    public class BookAppointmentResponseDto
    {
        public Guid AppointmentId { get; set; }
        public Guid DoctorId { get; set; }
        public DateTime ScheduledStartTime { get; set; }
        public DateTime ScheduledEndTime { get; set; }
        public string Status { get; set; } = null!;
    }

}
