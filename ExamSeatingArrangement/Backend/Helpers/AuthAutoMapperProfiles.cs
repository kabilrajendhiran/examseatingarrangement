using AutoMapper;
using ExamSeatingArrangement2020.Dtos;
using ExamSeatingArrangement2020.Models;

namespace ExamSeatingArrangement2020.Helpers
{
    public class AuthAutoMapperProfiles : Profile
    {
        public AuthAutoMapperProfiles()
        {
            CreateMap<UserForRegisterDto, User>();
            CreateMap<User, UserToReturnDto>();
        }
    }
}