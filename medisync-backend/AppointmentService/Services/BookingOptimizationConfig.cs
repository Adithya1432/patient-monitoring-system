namespace AppointmentService.Services
{
    public class BookingOptimizationConfig
    {
        public int DefaultAppointmentDurationMinutes { get; set; } = 30;
        public int AppointmentBufferMinutes { get; set; }
        public int MaxAppointmentsPerDay { get; set; }
        public int MaxDailyWorkingHours { get; set; }
        public int MinHoursBeforeBooking { get; set; }
        public int MaxDaysInAdvanceBooking { get; set; }
        public string DoctorAssignmentStrategy { get; set; } = "LEAST_WORKLOAD";
    }

}
