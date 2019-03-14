using BoardGamesApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoardGamesApi.Services
{
    public interface IDbService
    {
        Task<IEnumerable<BoardGame>> GetAllBoardGamesAsync();

        Task<BoardGame> GetBoardGameByIdAsync(int id);

        Task AddBoardGameAsync(UpdateBoardGameRequest game);

        Task UpdateGameAsync(int id, UpdateBoardGameRequest gameToUpdate);  
    }
}
