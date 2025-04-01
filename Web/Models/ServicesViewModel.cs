using BusinessObject.Entities;
using BusinessObject.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class ServicesViewModel
    {
        public  string ServiceName { get; set; } 
        public Guid Id { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; }
        public string Thumbnail { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string CategotyName {  get; set; }

      //  public Guid ServiceCategoryId { get; set; }
    }
}
