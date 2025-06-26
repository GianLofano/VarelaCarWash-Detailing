namespace VarelaCarWash.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime AppointmentDate { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int ServiceId { get; set; }
        public Service Service { get; set; }
    }
}
