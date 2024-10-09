using CodeSphere.Domain.Models;
using Freelancing_Website.Models.ForCreate;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AccountsController : Controller
{
    private readonly UserManager<Freelancer> _userManagerForFreelancer;
    private readonly UserManager<Client> _userManagerForCLient;
    private readonly SignInManager<Freelancer> _signInManagerForFreelancer;
    private readonly SignInManager<Client> _signInManagerForCLient;
    private readonly JWTService _jwtService;

    public AccountsController(UserManager<Freelancer> userManagerForFreelancer, SignInManager<Freelancer> signInManagerForFreelancer, UserManager<Client> userManagerForCLient, SignInManager<Client> signInManagerForCLient, JWTService jwtService)
    {
        _userManagerForFreelancer = userManagerForFreelancer;
        _userManagerForCLient = userManagerForCLient;
        _signInManagerForFreelancer = signInManagerForFreelancer;
        _signInManagerForCLient = signInManagerForCLient;
        _jwtService = jwtService;
    }

    // تسجيل Freelancer
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
                Profile = new Profile()
                {
                    Bio = profile.Bio,
                },
                Hourlysalary = model.Hourlysalary
            };
            freelancer.Profile.Freelancer = freelancer;

            var result = await _userManagerForFreelancer.CreateAsync(freelancer, model.PasswordHash);

            freelancer.Profile.FreelancerId = freelancer.Id;
            freelancer.ProfileId=freelancer.Profile.Id;

            if (result.Succeeded)
            {
                await _userManagerForFreelancer.AddToRoleAsync(freelancer, "Freelancer");
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
                ContactNumber = model.ContactNumber
            };

            var result = await _userManagerForCLient.CreateAsync(client, model.PasswordHash);

            if (result.Succeeded)
            {
                await _userManagerForCLient.AddToRoleAsync(client, "Client");
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

    [HttpPost("Login")]
    public async Task<IActionResult> Login(string email, string password)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManagerForFreelancer.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await _signInManagerForFreelancer.PasswordSignInAsync(user, password,
                    false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var role = (await _userManagerForFreelancer.GetRolesAsync(user)).FirstOrDefault();

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
