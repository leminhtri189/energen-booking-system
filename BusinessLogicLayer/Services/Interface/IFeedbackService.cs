using BusinessLogicLayer.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.Interface
{
    public interface IFeedbackService
    {
        Task<ServiceResult> CreateFeedback(Guid booking_id,  int therapist_rating, int service_rating, string therapist_feedback, string service_feedback);
    }
}
