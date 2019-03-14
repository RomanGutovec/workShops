using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoardGamesApi.Models;
using BoardGamesApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BoardGamesApi.Controllers
{
    [Route("api/[controller]")]
    public class BoardGamesController : Controller
    {
        private readonly IDbService service;

        public BoardGamesController(IDbService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Returns all board games.
        /// </summary>
        /// <remarks></remarks>
        /// <response code="200">Board games returned successfully.</response>
        /// <response code="500">Internal server error.</response>       
        [HttpGet]
        [ProducesResponseType(typeof(BoardGame), 200)]
        public async Task<ActionResult<IEnumerable<BoardGame>>> GetAllBoardGames()
        {
            var result = await service.GetAllBoardGamesAsync();
            return Ok(result);
        }

        /// <summary>
        /// Returns board game by id.
        /// </summary>
        /// <param name="id">Identifier to find board game.</param>
        /// <response code="200">Board game returned successfully.</response>
        /// /// <response code="404">Board game not found.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<BoardGame>> GetBoardGameById(int id)
        {
            var game = await service.GetBoardGameByIdAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            return Ok(game);
        }

        /// <summary>
        /// Creates a new board game.
        /// </summary>
        /// <remarks>       
        /// Sample request:
        ///
        ///     POST BoardGame
        ///     {        
        ///        "Name": "GameName",
        ///        "Description": "Description of the game"
        ///     }
        ///     
        /// </remarks>
        /// <param name="newGame">Data to create a new game.</param>
        /// <returns>A newly created board game.</returns>
        /// <response code="200">Board game created successfully.</response>
        /// <response code="400">The board game has null value.</response>  
        /// <response code="500">Internal server error.</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> CreateBoardGame([FromBody] UpdateBoardGameRequest newGame)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await service.AddBoardGameAsync(newGame);
            return Ok();
        }

        /// <summary>
        /// Update the board game.
        /// </summary>
        /// <param name="id">Identifier to modify board game.</param>
        /// <param name="dataToUpdate">Data to update an appropriate game.</param>
        /// <response code="200">Board games updated successfully.</response>
        /// <response code="404">Board games not found.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> UpdateBoardGame(int id, [FromBody] UpdateBoardGameRequest dataToUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await service.UpdateGameAsync(id, dataToUpdate);
            return Ok();
        }
    }
}
