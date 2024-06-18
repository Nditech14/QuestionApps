using AutoMapper;
using QuestionApp.Dtos;
using QuestionApp.Entity;

namespace QuestionApp.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Question, QuestionDtos>().ReverseMap();
            CreateMap<Response, ResponseDtos>().ReverseMap();
        }
    }
}
