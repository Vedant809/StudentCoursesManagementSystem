using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthServer.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new IdentityUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("User registered successfully!");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null || !(await _userManager.CheckPasswordAsync(user, model.Password)))
                    return Unauthorized("Invalid credentials");

                var token = await GenerateJwtToken(user);
                return Ok(new { token });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        private async Task<string> GenerateJwtToken(IdentityUser user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("VGhpcy1pcy1hLXNlY3VyZS1zZWNyZXQta2V5LWZvci1KV1QtU2lnbmluZw==\r\n"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };

            // Add roles to claims
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRoleAsync(Role role)
        {
            var roles = new IdentityRole { Name = role.RoleName };
            var roleEntity = await _roleManager.CreateAsync(roles);
            return Ok("Role Created Successfully");
        }

        [HttpPost("AssignUserRole")]
        public async Task<IActionResult> AssignRole(string userName,List<string> roles)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var assign = await _userManager.AddToRolesAsync(user, roles);
            if (assign.Errors.Any())
            {
                return BadRequest("Error Occured");
            }
            return Ok($"Roles assigned to the User {userName}");
        }
    }

    public class Role
    {
        public string RoleName { get; set; }
    }
    public class RegisterModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
