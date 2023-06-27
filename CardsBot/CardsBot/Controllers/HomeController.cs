using CardsBot.Models;
using CardsBot.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace CardsBot.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserManager _userManager;
        private readonly IMessageManager _messageManager;
        private readonly IPhotoManager _photoManager;
        public HomeController(IUserManager userManager, IMessageManager messageManager, IPhotoManager photoManager)
        {
            _userManager = userManager;
            _messageManager = messageManager;
            _photoManager = photoManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.GetAllUsersAsync();
            ViewBag.Count = await _userManager.GetCountUsersWhoBannedBotAsync();
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> EditHelloMessage()
        {
            var message = await _messageManager.GetMessageAsync(MessageType.Hello);
            return View(message);
        }

        [HttpPost]
        public async Task<string> EditHelloMessage(string message)
        {
            await _messageManager.EditMessageAsync(message,MessageType.Hello);
            return BuildResultString(true, "Сообщение успешно изменено");
        }

        [HttpGet]
        public async Task<IActionResult> EditCreditMessage()
        {
            var message = await _messageManager.GetMessageAsync(MessageType.Credit);
            return View(message);
        }

        [HttpPost]
        public async Task<string> EditCreditMessage(string message)
        {
            await _messageManager.EditMessageAsync(message, MessageType.Credit);
            return BuildResultString(true, "Сообщение успешно изменено");
        }

        [HttpGet]
        public async Task<IActionResult> EditDebetMessage()
        {
            var message = await _messageManager.GetMessageAsync(MessageType.Debet);
            return View(message);
        }

        [HttpPost]
        public async Task<string> EditDebetMessage(string message)
        {
            await _messageManager.EditMessageAsync(message, MessageType.Debet);
            return BuildResultString(true, "Сообщение успешно изменено");
        }

        [HttpGet]
        public async Task<IActionResult> EditAdmissionMessage()
        {
            var message = await _messageManager.GetMessageAsync(MessageType.Admission);
            return View(message);
        }

        [HttpPost]
        public async Task<string> EditAdmissionMessage(string message)
        {
            await _messageManager.EditMessageAsync(message, MessageType.Admission);
            return BuildResultString(true, "Сообщение успешно изменено");
        }

        [HttpGet]
        public IActionResult SendMessages()
        {
            return View();
        }

        [HttpPost]
        public async Task<string> SendMessages(string message, IFormFile photo)
        {
            var photoPath = await _photoManager.SavePhotoAsync(photo);
            var count = await _messageManager.SendPostMessageAsync(message, photoPath);
            return BuildResultString(true, $"Было отправлено {count} сообщений");
        }

        private static string BuildResultString(bool isGreenColor, string text)
        {
            string color = isGreenColor ? "Green" : "Red";
            string resultString = $"<p class=\"font-weight-bold\" style=\"color: {color}\">{text}</p>";
            return resultString;
        }
    }
}