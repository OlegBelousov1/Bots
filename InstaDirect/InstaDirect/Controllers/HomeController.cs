using InstaDirect.Models;
using InstaDirect.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InstaDirect.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserManager _userManager;
        private readonly IAccountManager _accountManager;
        private readonly ITextManager _textManager;
        private readonly IMessageManager _messageManager;
        private readonly IRefferalManager _refferalManager;

        public HomeController(IUserManager userManager, IAccountManager accountManager, ITextManager textManager,
            IMessageManager messageManager, IRefferalManager refferalManager)
        {
            _userManager = userManager;
            _accountManager = accountManager;
            _textManager = textManager;
            _messageManager = messageManager;
            _refferalManager = refferalManager;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _userManager.GetBotUsersAsync());
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(long tid)
        {
            var user = await _userManager.GetUserByTIdAsync(tid);
            if (user == null)
                return NotFound();
            return View(user);
        }

        [HttpPost]
        public async Task<string> EditUser(BotUser user)
        {
            if (await _userManager.EditUserAsync(user))
                return BuildResultString(true, "Пользватель редактирован успешно");
            else
                return BuildResultString(false, "Ошибка");
        }

        [HttpGet]
        public async Task<IActionResult> AddAccounts()
        {
            return View(await _accountManager.GetCountOfAccountsAsync());
        }

        [HttpPost]
        public async Task<string> AddAccounts(string accounts)
        {
            var count = await _accountManager.AddAccountsAsync(accounts);
            return BuildResultString(true, $"{count} аккаунтов успешно добавлено");
        }

        [HttpGet]
        public async Task<IActionResult> EditText()
        {
            return View(await _textManager.GetStartTextAsync());
        }

        [HttpPost]
        public async Task<string> EditText(Text text)
        {
            await _textManager.EditStartTextAsync(text);
            return BuildResultString(true, "Стартовый текст успешно изменен");
        }

        [HttpGet]
        public IActionResult SendMessages()
        {
            return View();
        }

        [HttpPost]
        public async Task<string> SendMessages(string message)
        {
            var count = await _messageManager.SendMessagesAsync(message);
            return BuildResultString(true, $"{count} сообщений отправлено");
        }

        [HttpPost]
        public async Task<string> DeleteAllAccounts()
        {
            await _accountManager.DeleteAllAccountsAsync();
            return BuildResultString(true, "Все данные аккаунтов успешно удалены");
        }

        [HttpGet]
        public IActionResult SeeTopRefs()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SeeTopRefs(int days)
        {
            var refferals = await _refferalManager.GetTopRefferalsAsync(days);
            return PartialView("TopRefferals", refferals);
        }

        [HttpGet]
        public async Task<IActionResult> SeeFullRefferalsInfo(long tid)
        {
            return View(await _refferalManager.GetFullRefferalsInfoAsync(tid));
        }

        [HttpPost]
        public async Task<string> DeleteRefferal(int id)
        {
            await _refferalManager.DeleteRefferalAsync(id);
            return BuildResultString(true, "Реферал успешно удален");
        }

        [HttpPost]
        public async Task<string> DeleteAllRefferals()
        {
            await _refferalManager.DeleteAllRefferalsAsync();
            return BuildResultString(true, "Рефералы успешно очищены");
        }

        private static string BuildResultString(bool isGreenColor, string text)
        {
            string color = isGreenColor ? "Green" : "Red";
            string resultString = $"<p class=\"font-weight-bold\" style=\"color: {color}\">{text}</p>";
            return resultString;
        }
    }
}