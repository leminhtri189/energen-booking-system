using AutoMapper;
using BusinessObject.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Web.Models;

namespace Web.Extensions
{
    public class Mapper : Profile
    {
       public Mapper() {

            CreateMap<ServicesViewModel, Service>().ReverseMap();
            CreateMap<Question, QuestionsViewModel>()
                .ForMember(dest => dest.AnswerViewModels, opt => opt.MapFrom(src => src.QuestionOptions));

            // Ánh xạ QuestionOption → AnswerViewModel
            CreateMap<QuestionOption, AnswerViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.AnswerContent, opt => opt.MapFrom(src => src.Content));
            CreateMap<Booking, BookingServiceViewModel>()
                .ForMember(dest => dest.SelectedDate, opt => opt.MapFrom(src => src.ReservedDate))
                .ForMember(dest => dest.SelectedTime, opt => opt.MapFrom(src => src.ReservedStartTime))
                .ReverseMap();
        }
    }
}
