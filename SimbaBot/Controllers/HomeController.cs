using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimbaBot.Services.Interfaces;

namespace SimbaBot.Controllers
{
    [Authorize(Roles = "admin")]
    public class HomeController : Controller
    {
        private readonly IUserManager _userManager;
        private readonly IWithdrawalManager _withdrawalManager;
        public HomeController(IUserManager userManager, IWithdrawalManager withdrawalManager)
        {
            _userManager = userManager;
            _withdrawalManager = withdrawalManager;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _userManager.GetBotUsersAsync());
        }

        public async Task<IActionResult> AllWithdrawals()
        {
            return View(await _withdrawalManager.GetWithdrawalsAsync());
        }

        public async Task<IActionResult> CompleteWithdrawal(int id)
        {
            await _withdrawalManager.CompleteWithdrawalAsync(id);
            return RedirectToAction("AllWithdrawals");
        }
    }
}
