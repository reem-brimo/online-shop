using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopUI.ViewModel.Admin;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ShopUI.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UsersController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(
            [FromBody] CreateUserViewModel vm
)
        {
            var user = new IdentityUser()
            {
                UserName = vm.UserName
            };


            await _userManager.CreateAsync(user, "password");

            var managerClaim = new Claim("Role", "Manager");
            await _userManager.AddClaimAsync(user, managerClaim);

            return Ok();
        }
        
    }
}
