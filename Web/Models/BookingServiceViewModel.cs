namespace Web.Models
{
    public class BookingServiceViewModel
    {
        public Guid ServiceId { get; set; }
        public Guid TherapistId { get; set; }
        public string SelectedDate { get; set; }
        public string SelectedTime { get; set; }
    }
}
