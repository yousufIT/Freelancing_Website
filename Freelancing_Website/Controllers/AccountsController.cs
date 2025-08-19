using CodeSphere.Domain.Models;
using Freelancing_Website.Models;
using Freelancing_Website.Models.ForCreate;
using Freelancing_Website.Services;
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
            };
            freelancer.Profile.Freelancer = freelancer;

            var result = await _userManager.CreateAsync(freelancer, model.PasswordHash);

            freelancer.Profile.FreelancerId = freelancer.Id;
            freelancer.ProfileId = freelancer.Profile.Id;

            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync("Freelancer"))
                    await _roleManager.CreateAsync(new IdentityRole("Freelancer"));

                await _userManager.AddToRoleAsync(freelancer, "Freelancer");

                // get actual role and create token
                var roles = await _userManager.GetRolesAsync(freelancer);
                var role = roles.FirstOrDefault() ?? "Freelancer";

                var token = _jwtService.CreateJWT(freelancer.Email, freelancer.UserName, role);
                return Ok(new { token, id = freelancer.Id, role });
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
                if (!await _roleManager.RoleExistsAsync("Client"))
                    await _roleManager.CreateAsync(new IdentityRole("Client"));

                await _userManager.AddToRoleAsync(client, "Client");

                // get actual role and create token
                var roles = await _userManager.GetRolesAsync(client);
                var role = roles.FirstOrDefault() ?? "Client";

                var token = _jwtService.CreateJWT(client.Email, client.UserName, role);
                return Ok(new { token, id = client.Id, role });
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
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var user = await _userManager.FindByEmailAsync(loginData.Email);
        if (user == null)
            return BadRequest(new { message = "Invalid login attempt." });

        // validate password without issuing cookie
        if (!await _userManager.CheckPasswordAsync(user, loginData.Password))
            return BadRequest(new { message = "Invalid login attempt." });

        // get role from Identity (works even if you added role on register)
        var roles = await _userManager.GetRolesAsync(user);
        var role = roles.FirstOrDefault() ?? user.Role ?? "Client";

        // create JWT using actual user info + role
        var token = _jwtService.CreateJWT(user.Email, user.UserName, role);

        return Ok(new { token, id = user.Id, role });
    }


    // Logout
    [HttpPost("Logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok(new { message = "User logged out successfully." });
    }



}
