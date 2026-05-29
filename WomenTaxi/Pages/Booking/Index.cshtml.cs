using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WomenTaxi.Pages.Booking
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
            //показывает форму
        }

        public IActionResult OnPost(string pickupAddress, string destinationAddress, string phone)
        {
            //здесь будет сохранение в бд
            //пока просто показывает сообщение

            ViewData["Message"] = $"Заказ принят! Машина приедет по адресу: {pickupAddress}. Оператор свяжется с Вами по телефону {phone}.";

            return Page();
        }

    }

}
