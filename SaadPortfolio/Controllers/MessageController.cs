using Microsoft.AspNetCore.Mvc;
using SaadPortfolio.Models; // Namespace de la classe Message
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SaadPortfolio.Controllers
{
    public class MessageController : Controller
    {
        private static List<Message> _messages = new List<Message>(); // Liste temporaire pour stocker les messages
        private const string TelegramBotToken = "7977606997:AAFdj8kljZIsjaxHe23zVFYTJf0zex1HGTQ"; // Votre token de bot Telegram
        private const string TelegramChatId = "5094847885"; // Votre chat ID Telegram

        // Action pour afficher tous les messages reçus (réservée à l'administrateur)
        [HttpGet]
        public IActionResult Index()
        {
            if (_messages.Count == 0)
            {
                TempData["Error"] = "Aucun message n'a encore été envoyé.";
            }

            return View(_messages); // Passer la liste des messages à la vue
        }

        // Action pour afficher les détails d'un message spécifique
        [HttpGet]
        public IActionResult Details(int id)
        {
            var message = _messages.FirstOrDefault(m => m.Id == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message); // Passer le message à la vue
        }

        // Action pour afficher le formulaire d'édition d'un message
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var message = _messages.FirstOrDefault(m => m.Id == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message); // Passer le message à la vue
        }

        // Action pour traiter l'édition d'un message
        [HttpPost]
        public IActionResult Edit(int id, Message updatedMessage)
        {
            var message = _messages.FirstOrDefault(m => m.Id == id);
            if (message == null)
            {
                return NotFound();
            }

            // Mettre à jour le message
            message.Name = updatedMessage.Name;
            message.Email = updatedMessage.Email;
            message.Content = updatedMessage.Content;

            // Message de confirmation
            TempData["Message"] = "Message mis à jour avec succès !";

            return RedirectToAction("Index"); // Rediriger vers la liste des messages
        }

        // Action pour afficher la confirmation de suppression d'un message
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var message = _messages.FirstOrDefault(m => m.Id == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message); // Passer le message à la vue de confirmation
        }

        // Action pour supprimer un message
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var message = _messages.FirstOrDefault(m => m.Id == id);
            if (message == null)
            {
                return NotFound();
            }

            // Supprimer le message
            _messages.Remove(message);

            // Message de confirmation
            TempData["Message"] = "Message supprimé avec succès !";

            return RedirectToAction("Index"); // Rediriger vers la liste des messages
        }

        // Action pour recevoir et sauvegarder un message depuis le formulaire
        [HttpPost]
        public async Task<IActionResult> SendMessage(Message message)
        {
            if (ModelState.IsValid)
            {
                // Ajouter un ID unique au message
                message.Id = _messages.Count + 1;

                // Ajouter le message à la liste (ou à une base de données)
                _messages.Add(message);

                // Message de confirmation pour l'utilisateur
                TempData["Message"] = "Votre message a été envoyé avec succès !";

                // Vérification que le message a bien été ajouté
                System.Diagnostics.Debug.WriteLine($"Message ajouté : {message.Name}, {message.Email}, {message.Content}");

                // Envoi du message sur Telegram
                await SendMessageToTelegram(message);

                // Rediriger l'utilisateur vers la page d'accueil (ou autre page)
                return RedirectToAction("Index", "Home");
            }

            // Si la validation échoue, rester sur la même page
            TempData["Error"] = "Veuillez vérifier les informations saisies.";
            return RedirectToAction("Index", "Home");
        }

        // Fonction pour envoyer le message sur Telegram
        private async Task SendMessageToTelegram(Message message)
        {
            var botClient = new HttpClient();
            var telegramUrl = $"https://api.telegram.org/bot{TelegramBotToken}/sendMessage?chat_id={TelegramChatId}&text=Nom: {message.Name}%0AEmail: {message.Email}%0AMessage: {message.Content}";

            try
            {
                // Envoi de la requête GET à l'API de Telegram
                await botClient.GetAsync(telegramUrl);
            }
            catch (Exception ex)
            {
                // Log de l'erreur si l'envoi échoue
                System.Diagnostics.Debug.WriteLine($"Erreur d'envoi à Telegram: {ex.Message}");
            }
        }
    }
}
