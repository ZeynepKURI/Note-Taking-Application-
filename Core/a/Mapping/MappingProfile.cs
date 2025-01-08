using System;
using Application.DTOs;
using AutoMapper;
using Domain.Enitities;

namespace Application.Mapping
{
	public class MappingProfile : Profile
	{
        public MappingProfile()
        {
            // Entity -> DTO dönüşümü
            CreateMap<Note, NoteDTO>().ReverseMap();
        }
    }
}

