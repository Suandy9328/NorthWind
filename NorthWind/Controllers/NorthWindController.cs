using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthWind.Data;
using NorthWind.Models;

namespace NorthWind.Controllers
{
    public class NorthWindController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public NorthWindController(ApplicationDbContext dbContext)
        {
           this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task< IActionResult> Add(AddProductViewModel viewModel)
        {
            var product = new Product
            {
                //ProductID = viewModel.ProductID,
                ProductName = viewModel.ProductName,
                QuantityPerUnit = viewModel.QuantityPerUnit,
                UnitPrice = viewModel.UnitPrice,
                Discontinued = viewModel.Discontinued
            };

            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();

            //return View();
            return RedirectToAction("List", "NorthWind");
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var product = await dbContext.Products.ToListAsync();

            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int ProductID)
        {
            var product = await dbContext.Products.FindAsync(ProductID);

            return View(product);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product viewModel)
        {
            var product = await dbContext.Products.FindAsync(viewModel.ProductID);

            if(product is not null)
            {
                product.ProductName = viewModel.ProductName;
                product.QuantityPerUnit = viewModel.QuantityPerUnit;
                product.UnitPrice = viewModel.UnitPrice;
                product.Discontinued = viewModel.Discontinued;

                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List","NorthWind");

        }

        [HttpPost]
        public async Task<IActionResult> Delete(Product viewModel)        
        {
            var product =   await dbContext.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(x =>x.ProductID == viewModel.ProductID);

            if(product is not null )
            {
                //dbContext.Products.Remove(product);
                dbContext.Products.Remove(viewModel);
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "NorthWind");

        }
    }


}
