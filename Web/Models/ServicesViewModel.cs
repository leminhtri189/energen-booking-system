using BusinessObject.Entities;
using BusinessObject.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DataAccessLayer.Commons;

namespace Web.Models
{
    public class ServiceViewModel
    {
        [Required(ErrorMessage = "Service Name is required.")]
        [StringLength(100, ErrorMessage = "Service Name cannot be longer than 100 characters.")]
        public string ServiceName { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Duration must be a positive integer.")]
        public int Duration { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Thumbnail is required.")]
        [DataType(DataType.Upload)]
        [FileExtensions(Extensions = "jpg,png,jpeg", ErrorMessage = "Only image files (jpg, png, jpeg) are allowed.")]
        public IFormFile Thumbnail { get; set; }

        [Required(ErrorMessage = "At least one image is required.")]
        public List<IFormFile> Images { get; set; } = new List<IFormFile>();

        [Required(ErrorMessage = "Service Category is required.")]
        public Guid ServiceCategoryId { get; set; }

        [Required(ErrorMessage = "At least one Skin Type is required.")]
        public List<Guid> SkinTypeIds { get; set; } = new List<Guid>();
    }
    public class ServiceDashboardViewModel
    {
        public PaginationResult<Service> Services { get; set; }
        public ServiceViewModel NewService { get; set; }
        public IEnumerable<ServiceCategory> Categories { get; set; }

        public IEnumerable<SkinType> SkinTypes { get; set; }
    }

}
