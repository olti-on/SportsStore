using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Concrete;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository repository;
        private IOrderProcessor orderProcessor;

        public CartController (IProductRepository repo , IOrderProcessor proc)
        {
            repository = repo;
            orderProcessor = proc;
        }

        public RedirectToRouteResult AddToCart (int productId , string returnUrl)
        {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);

            if(product != null) 
                GetCart().AddItem(product, 1);

            return RedirectToAction("Index", new { returnUrl });

        }

        public RedirectToRouteResult RemoveFromCart(int productId, string returnUrl)
        {
            Product product = repository.Products
            .FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                GetCart().RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        private Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;

            }
            return cart;
        }

        // GET: Cart
        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = GetCart(),
                ReturnUrl=returnUrl
            });
        }




        public PartialViewResult Summary ()
        {
            return PartialView(GetCart());
        }

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }


        [HttpPost]
        public ViewResult Checkout(ShippingDetails shippingDetails)
        {
            

            if(ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(GetCart() ,shippingDetails);
                GetCart().Clear();
                return View("Completed");

                   
            }
            else
            {
                return View(shippingDetails);
            }
        }

    }
}