using CodeSphere.Domain.Models;
using Freelancing_Website.Models;
using Freelancing_Website.Models.ForCreate;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AccountsController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly JWTService _jwtService;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AccountsController(UserManager<User> userManager, SignInManager<User> signInManager, JWTService jwtService, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtService = jwtService;
        _roleManager = roleManager;
    }

    // Register Freelancer
    [HttpPost("Freelancer")]
    public async Task<IActionResult> RegisterFreelancer(FreelancerForCreate model)
    {
        if (ModelState.IsValid)
        {
            var profile = model.Profile;
            var freelancer = new Freelancer
            {
                UserName = model.UserName,
                Email = model.Email,
                Name = model.Name,
                Role = "Freelancer",
                Rating = model.Rating,
                Profile = new Profile()
                {
                    Bio = profile.Bio,
                },
                Hourlysalary = model.Hourlysalary
            };
            freelancer.Profile.Freelancer = freelancer;

            var result = await _userManager.CreateAsync(freelancer, model.PasswordHash);

            freelancer.Profile.FreelancerId = freelancer.Id;
            freelancer.ProfileId = freelancer.Profile.Id;

            if (result.Succeeded)
            {

                var roleExists = await _roleManager.RoleExistsAsync("Freelancer");
                if (!roleExists)
                {
                    await _roleManager.CreateAsync(new IdentityRole("Freelancer"));
                }
                // Add the user to the "Freelancer" role
                await _userManager.AddToRoleAsync(freelancer, "Freelancer");
                var token = _jwtService.CreateJWT(model);
                return Ok(new { token, freelancer.Id, freelancer.Role });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return BadRequest(ModelState);
    }

    // Register Client
    [HttpPost("Client")]
    public async Task<IActionResult> RegisterClient(ClientForCreate model)
    {
        if (ModelState.IsValid)
        {
            var client = new Client
            {
                UserName = model.UserName,
                Email = model.Email,
                Name = model.Name,
                CompanyName = model.CompanyName,
                ContactNumber = model.ContactNumber,
                Role = "Client",
                Rating = model.Rating
            };

            var result = await _userManager.CreateAsync(client, model.PasswordHash);

            if (result.Succeeded)
            {
                // Check if the role "Client" exists, if not, create it
                var roleExists = await _roleManager.RoleExistsAsync("Client");
                if (!roleExists)
                {
                    await _roleManager.CreateAsync(new IdentityRole("Client"));
                }

                // Add the user to the "Client" role
                await _userManager.AddToRoleAsync(client, "Client");

                var token = _jwtService.CreateJWT(model);
                return Ok(new { token, client.Id, client.Role });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return BadRequest(ModelState);
    }

    // Login
    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginData loginData)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(loginData.Email);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, loginData.Password,
                    false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    UserForCreate model = new UserForCreate
                    {
                        UserName = user.UserName,
                        Role = user.Role,
                        Email = user.Email,
                        Name = user.Name
                    };
                    var token = _jwtService.CreateJWT(model);
                    return Ok(new { token, user.Id, user.Role });
                }
            }
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        }

        return BadRequest(ModelState);
    }

    // Logout
    [HttpPost("Logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok(new { message = "User logged out successfully." });
    }
}
