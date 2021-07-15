using AutoMapper;
using BL.DTOs;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Configration
{
    public class AutoMapperProfile
    {
        public static IMapper mapper { get; set; }

        static AutoMapperProfile()
        {
            var config = new MapperConfiguration(
                cog =>
                {
                    cog.CreateMap<reserve, ReserveDto>().ReverseMap();
                    cog.CreateMap<meal, MealDto>().ReverseMap();
                    cog.CreateMap<User, RegisterationDto>().ReverseMap();
                    cog.CreateMap<User, LoginDto>().ReverseMap();
                    
                });

            mapper = config.CreateMapper();
        }

    }
}
