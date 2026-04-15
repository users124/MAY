using Microsoft.AspNetCore.Mvc;
using MAY.Models.ViewModels;
using MAY.DataAccess.Repository.IRepository;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using MAY.Utility;
namespace MAYWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class AnalyticsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public AnalyticsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var productList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();

            var model = new AnalyticsVM
            {
                TotalProducts = productList.Count,

                // Totale waarde: Som van (Inkoopprijs * Voorraad)
                TotalInventoryValue = productList.Sum(u => (u.PurchasePrice ?? 0) * u.Stock),

                // Totale winst: Som van ((Verkoop - Inkoop) * Voorraad)
                TotalPotentialProfit = productList.Sum(u => (u.ListPrice - (u.PurchasePrice ?? 0)) * u.Stock),

                // Product met de hoogste marge per stuk
                MostProfitableProduct = productList
                    .OrderByDescending(u => u.ListPrice - (u.PurchasePrice ?? 0))
                    .Select(u => u.Title)
                    .FirstOrDefault() ?? "Geen data"
            };

            // Gemiddelde Marge berekenen om delen door 0 te voorkomen
            if (model.TotalInventoryValue > 0)
            {
                model.AverageMarkup = (model.TotalPotentialProfit / model.TotalInventoryValue) * 100;
            }

            return View(model);
        }
    }
}
