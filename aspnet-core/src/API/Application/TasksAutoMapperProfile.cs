using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Contracts.Accounts;
using API.Domain.Extended;
using AutoMapper;

namespace API.Application
{
    public class TasksAutoMapperProfile : Profile
    {
        public TasksAutoMapperProfile()
        {
            CreateMap<RegisterDto, ApplicationUser>().ReverseMap();
        }
    }
}