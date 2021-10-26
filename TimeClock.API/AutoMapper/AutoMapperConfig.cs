using AutoMapper;
using System.Collections.Generic;
using TimeClock.API.DTOs;
using TimeClock.API.Entities;

namespace TimeClock.API.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Entities.TimeClock, CreateTimeClockDTO>().ReverseMap();
            CreateMap<Entities.TimeClock, ResponseTimeClockDTO>().ReverseMap();
            CreateMap<Entities.TimeClock, ResponseTimeClockDTO>().ReverseMap();
        }
    }
}
