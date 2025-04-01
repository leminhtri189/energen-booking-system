using BusinessLogicLayer.Services.Implementation;
using BusinessObject.Entities;

namespace Web.Models
{
    public class BookingViewModel
    {
        public Service Service { get; set; } // Giả sử đây là model cho dịch vụ
        public ICollection<Therapist> Therapists { get; set; } // Danh sách chuyên viên
    }
}
