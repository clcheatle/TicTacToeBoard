﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoardCrud.Models;
using BoardCrud.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BoardCrud.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BoardController : ControllerBase
    {
        private readonly IBoardService _boardService;

        public BoardController(IBoardService boardService)
        {
            _boardService = boardService;
        }

        [HttpPost("createGame")]
        public GameState CreateGame(GameStateDTO gs)
        {
            return _boardService.CreateGameState(gs.Player1, gs.Player2);
        }

        
    }
}
