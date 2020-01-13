using AutoMapper;
using DatingWebApi.Helper;
using DatingWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingWebApi.DTO
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserForListDto>()
                .ForMember(dest => dest.PhotoUrl, src=> {
                    src.MapFrom(user => user.Photos.FirstOrDefault(p => p.IsMain == true).Url);
                })
                .ForMember(dest => dest.Age, src=> {
                    src.MapFrom(user => user.DateOfBirth.CalculateAge());
                });
            CreateMap<User, UserForDetailedDto>()
                .ForMember(dest => dest.PhotoUrl, src => {
                    src.MapFrom(user => user.Photos.FirstOrDefault(p => p.IsMain == true).Url);
                })
                .ForMember(dest => dest.Age, src => {
                    src.MapFrom(user => user.DateOfBirth.CalculateAge());
                });
            CreateMap<Photo, PhotosForDetailedDto>();
            CreateMap<UserForUpdateDto, User>();
            CreateMap<PhotosToCreateDto, Photo>();
            CreateMap<Photo, PhotoForReturnDto>();
            CreateMap<UserToRegisterDto, User>();
        }
    }
}
