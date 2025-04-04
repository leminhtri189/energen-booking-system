using BusinessLogicLayer.Commons;
using BusinessLogicLayer.Services.Interface;
using BusinessObject.Entities;
using DataAccessLayer.Repositories.Implementation;
using DataAccessLayer.Repositories.Interface;
using DataAccessLayer.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.Implementation
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IBookingRepository _bookingRepository;

        public FeedbackService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _feedbackRepository = _unitOfWork.Repository<FeedbackRepository>();
            _bookingRepository = _unitOfWork.Repository<BookingRepository>();
        }

        public async Task<ServiceResult> CreateFeedback(Guid booking_id, int therapist_rating, int service_rating, string therapist_feedback, string service_feedback)
        {
            Booking? booking = await _bookingRepository.GetByIdAsync(booking_id);

            if (booking == null) {
                return ServiceResult.Failed(Error.OperationFailed("Booking not found for given id"));
            }

            Feedback feedback = new Feedback
            {
                BookingId = booking_id,
                TherapistRating = therapist_rating,
                TherapistFeedback = therapist_feedback,
                ServiceRating = service_rating,
                ServiceFeedback = service_feedback
            };

            try
            {
                await _feedbackRepository.AddAsync(feedback);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                return ServiceResult.Failed(Error.OperationFailed(ex.Message));
            }

            return ServiceResult.Success();
        }
    }
}
