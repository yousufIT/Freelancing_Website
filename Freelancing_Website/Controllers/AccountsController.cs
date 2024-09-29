using CodeSphere.Domain.Models;
using Freelancing_Website.Models.ForCreate;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

public class AccountsController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly JWTService _jwtService;

    public AccountsController(UserManager<User> userManager, SignInManager<User> signInManager, JWTService jwtService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtService = jwtService;
    }

    // تسجيل Freelancer
    [HttpPost]
    public async Task<IActionResult> RegisterFreelancer(FreelancerForCreate model)
    {
        if (ModelState.IsValid)
        {
            var freelancer = new Freelancer
            {
                UserName = model.Email,
                Email = model.Email,
                Name = model.Name,
                Skills = model.Skills,
                ProfileId = model.ProfileId,
                Profile = model.Profile,
                Hourlysalary = model.Hourlysalary
            };

            var result = await _userManager.CreateAsync(freelancer, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(freelancer, "Freelancer");
                var token = _jwtService.CreateJWT(model);
                return Ok(new { token });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return BadRequest(ModelState);
    }

    // تسجيل Client
    [HttpPost]
    public async Task<IActionResult> RegisterClient(ClientForCreate model)
    {
        if (ModelState.IsValid)
        {
            var client = new Client
            {
                UserName = model.Email,
                Email = model.Email,
                Name = model.Name,
                CompanyName = model.CompanyName,
                ContactNumber = model.ContactNumber
            };

            var result = await _userManager.CreateAsync(client, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(client, "Client");
                var token = _jwtService.CreateJWT(model);
                return Ok(new { token });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return BadRequest(ModelState);
    }

    [HttpPost]
    public async Task<IActionResult> Login(string email, string password)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, password,
                    false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

                    UserForCreate model;

                    if (role == "Freelancer")
                    {
                        model = new FreelancerForCreate
                        {
                            Email = user.Email,
                            Name = user.Name,
                            Role = role
                        };
                    }
                    else if (role == "Client")
                    {
                        model = new ClientForCreate
                        {
                            Email = user.Email,
                            Name = user.Name,
                            Role = role
                        };
                    }
                    else
                    {
                        return BadRequest("Invalid user role.");
                    }

                    var token = _jwtService.CreateJWT(model);
                    return Ok(new { token });
                }
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        }

        return BadRequest(ModelState);
    }
}
