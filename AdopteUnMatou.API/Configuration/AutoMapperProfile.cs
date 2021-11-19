using AdopteUnMatou.API.Models.Cats;
using AdopteUnMatou.API.Models.Users;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdopteUnMatou.API.Configuration
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CatSubmitModel, Cat>();
            CreateMap<UserSubmitModel, User>();
        }
    }
}
