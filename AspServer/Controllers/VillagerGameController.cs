using AspServer.Game;
using AspServer.Models.game;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace AspServer.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VillagerGameController : ControllerBase
    {

        private readonly ShareDb _context;
        private List<VillagerGame> gamesList;

        public VillagerGameController(ShareDb context)
        {
            _context = context;
            gamesList = new List<VillagerGame>();
            //tokenHandler = new JwtSecurityTokenHandler();
        }



    }
}
