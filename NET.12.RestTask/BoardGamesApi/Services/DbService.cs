using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BoardGamesApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace BoardGamesApi.Services
{
    public class DbService : IDbService
    {
        private readonly BoardGameContext dbContext;
        private readonly IMemoryCache memoryCache;
        private readonly IMapper mapper;

        public DbService(BoardGameContext dbContext, IMemoryCache memoryCache, IMapper mapper)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException($"Context {nameof(dbContext)} has null value.");
            this.memoryCache = memoryCache ?? throw new ArgumentNullException($"Cacche {nameof(memoryCache)} has null value.");
            this.mapper = mapper ?? throw new ArgumentNullException($"Cacche {nameof(mapper)} has null value.");
        }

        public async Task AddBoardGameAsync(UpdateBoardGameRequest game)
        {
            var dbGames = await dbContext.BoardGames.Where(g => g.Name == game.Name).ToArrayAsync();
            if (dbGames.Length > 0)
            {
                throw new RequestedResourceHasConflictException("name");
            }

            await dbContext.BoardGames.AddAsync(mapper.Map<BoardGame>(game));
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<BoardGame>> GetAllBoardGamesAsync()
        {
            return await dbContext.BoardGames.ToListAsync();
        }

        public async Task<BoardGame> GetBoardGameByIdAsync(int id)
        {
            BoardGame game = null;
            if (!memoryCache.TryGetValue(id, out game))
            {
                game = await dbContext.BoardGames.FindAsync(id);
                if (game != null)
                {
                    memoryCache.Set(
                        game.Id,
                        game,
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(24)));
                }
            }

            return game;
        }

        public async Task UpdateGameAsync(int id, UpdateBoardGameRequest gameToUpdate)
        {
            var dbGames = await dbContext.BoardGames.Where(g => g.Id == id).ToArrayAsync();
            if (dbGames.Length == 0)
            {
                throw new RequestedResourceNotFoundException();
            }

            var dbGame = dbGames[0];
            mapper.Map(gameToUpdate, dbGame);
            dbGame.Modified = DateTime.Now;
            dbContext.Entry(dbGames[0]).State = EntityState.Modified;

            await dbContext.SaveChangesAsync();
        }
    }
}
