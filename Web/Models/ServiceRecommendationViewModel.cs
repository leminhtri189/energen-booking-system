using BusinessObject.Entities;

namespace Web.Models
{
    public class ServiceRecommendationViewModel
    {
        public Dictionary<SkinType, double> SkinTypes { get; set; } = new();
        public List<Service>? Services { get; set; }
    }
}
