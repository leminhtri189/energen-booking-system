using Microsoft.EntityFrameworkCore;
using SkinTime.DAL.Entities;
using SkinTime.DAL.Enum;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SkinTime.MVC.Models
{
    public class ServiceModel
    {
        public required string ServiceName { get; set; }//
        public string Description { get; set; }//
        public int Duration { get; set; }
        public string Thumbnail { get; set; }// 
        public decimal Price { get; set; }
        public string ServiceCategoryName { get; set; }
        public double Rating { get; set; }

    }
}
