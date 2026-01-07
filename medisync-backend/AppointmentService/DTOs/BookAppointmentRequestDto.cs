namespace AppointmentService.DTOs
{
    public class BookAppointmentRequestDto
    {
        public Guid PatientId { get; set; }
        public string Speciality { get; set; } = null!;
        public DateOnly PreferredDate { get; set; }
        public TimeSpan PreferredTime { get; set; }
    }

}
