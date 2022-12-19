using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AspServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AspServer.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ShareDb _context;
        private JwtSecurityTokenHandler tokenHandler;
        private Dictionary<string, string> tokenUserDict;

        public UsersController(ShareDb context)
        {
            _context = context;
            tokenHandler = new JwtSecurityTokenHandler();
            tokenUserDict = new Dictionary<string, string>();

            var user = new User();
            user.Name = "asd"; user.Password = "asdasd"; user.Email = "asd@asd.de";
            this.Register(user);
        }

        [HttpPost]
        public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordInput input)
        {
            var token = input.Token;
            var newPassword = input.Password;
            if (this.tokenUserDict[token] != null)
            {
                var username = tokenUserDict[token];
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == username);
                if(user != null)
                {
                    user.Password = newPassword;
                    await _context.SaveChangesAsync();
                    return Ok();
                }
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult> Register([FromBody] User user)
        {
            try
            {
                var foundUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email || u.Name == user.Name);
                if (foundUser != null)
                {
                    return BadRequest("User name or email already exists.");
                }
                else
                {
                    await _context.Users.AddAsync(user);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> Login([FromBody] LoginModel user)
        {
            var foundUser = await _context.Users.FirstOrDefaultAsync(u => u.Name == user.Name);
            if (foundUser == null)
            {
                return BadRequest("Invalid client request");
            }
            else if (foundUser.Password == user.Password)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                    issuer: "https://localhost:7287",
                    audience: "https://localhost:7287",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: signinCredentials
                );
                var tokenString = tokenHandler.WriteToken(tokeOptions);
                this.tokenUserDict.Add(tokenString, user.Name);
                return Ok(new AuthenticationResponse { Token = tokenString });
            }
            return ValidationProblem();
        }
        [HttpGet]
        [Authorize]
        public ActionResult Logout()
        {
            return Ok("log out was authorized");
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int? id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int? id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int? id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int? id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
