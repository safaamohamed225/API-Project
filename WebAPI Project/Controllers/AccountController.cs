using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI_Project.DTO;
using WebAPI_Project.Models;


namespace WebAPI_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration config;


        public AccountController(UserManager<ApplicationUser> userManager , IConfiguration config)
        {
            this.userManager = userManager;
            this.config = config;
        }
        [HttpPost ("Register")]
        public async Task <IActionResult> Register(RegisterUserDTO dataUserFromRequest)
        {
            if(ModelState.IsValid)
            {
                ApplicationUser applicationUser = new ApplicationUser();
                applicationUser.UserName=dataUserFromRequest.UserName;
                applicationUser.Email=dataUserFromRequest.Email;

                IdentityResult result= await userManager.CreateAsync(applicationUser, dataUserFromRequest.Password);
                if(result.Succeeded)
                {
                    return Ok("Created");
                }
                foreach(var item in result.Errors)
                {
                    ModelState.AddModelError("Password", item.Description);
                }
               
            }

            return BadRequest(ModelState);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginUser)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser userFromDb = 
                    await userManager.FindByNameAsync(loginUser.UserName);

                if (userFromDb != null)
                {


                    bool found =
                        await userManager.CheckPasswordAsync(userFromDb, loginUser.Password);

                    if (found == true)
                    {

                        List<Claim> userClaims = new List<Claim>();

                        userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                        userClaims.Add(new Claim(ClaimTypes.NameIdentifier, userFromDb.Id));
                        userClaims.Add(new Claim(ClaimTypes.Name, userFromDb.UserName));

                        var userRoles = await userManager.GetRolesAsync(userFromDb);

                        foreach (var roleName in userRoles)
                        {
                            userClaims.Add(new Claim(ClaimTypes.Role, roleName));
                        }

                        var signKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:SecurityKey"]));
                        SigningCredentials signCred = new SigningCredentials(signKey, SecurityAlgorithms.HmacSha256);
                        JwtSecurityToken userSecure = new JwtSecurityToken(
                            issuer: config["JWT:IssuerIP"],
                            audience: config["JWT:AudienceIP"],
                            expires: DateTime.Now.AddHours(1),
                            claims: userClaims,
                            signingCredentials: signCred

                            );

                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(userSecure),
                            expiration=DateTime.Now.AddHours(1)

                        });



                    }
                }
                ModelState.AddModelError("UserName", "UserName or Password wrong");
            }
            return BadRequest(ModelState);
        }
    }
}
