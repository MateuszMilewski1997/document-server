using AutoMapper;
using fakultet.Comends;
using fakultet.Models;
using System;

namespace fakultet.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DocumentsCOM, Documents>()
                .ForMember(
                    x => x.Send_Date,
                    y => y.MapFrom(src => DateTime.Now.ToString())
                );
        }
    }
}
