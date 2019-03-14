using AutoMapper;
using BoardGamesApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGamesApi.Services
{
    public sealed class BoardGamesMappingProfile : Profile
    {
        public BoardGamesMappingProfile()
        {
            CreateMap<UpdateBoardGameRequest, BoardGame>()
                .ForMember(g => g.Modified, opt => opt.MapFrom(p => DateTime.UtcNow));
            CreateMap<BoardGame, UpdateBoardGameRequest>();
        }

    }
}
