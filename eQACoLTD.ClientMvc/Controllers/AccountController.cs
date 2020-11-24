using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using eQACoLTD.ClientMvc.Common;
using eQACoLTD.ClientMvc.Services;
using eQACoLTD.ViewModel.System.Account.Queries;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace eQACoLTD.ClientMvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountAPIService _apiService;
        private readonly IProductAPIService _productApiService;

        public AccountController(IAccountAPIService apiService,IProductAPIService productApiService)
        {
            _apiService = apiService;
            _productApiService = productApiService;
        }
        // [Authorize]
        public async Task<IActionResult> Cart()
        {
            var isLogin=User.Claims.FirstOrDefault(x => x.Type == "name")?.Value;
            if (isLogin != null)
            {
                var result = await _apiService.GetCart();
                return View(result.ResultObj);   
            }
            var cartDto = HttpContext.Session.GetJson<CartDto>("Cart");
            if (cartDto == null)
            {
                cartDto=new CartDto();
                cartDto.ListProduct=new List<CartDetailDto>();
                HttpContext.Session.SetJson("Cart",cartDto);
            }
            return View(cartDto);
        }
        
        [HttpPost]
        public async Task<HttpStatusCode> AddProductToCartNoLogin(string productId)
        {
            var cartDto = HttpContext.Session.GetJson<CartDto>("Cart");
            var product = await _productApiService.GetProductAsync(productId);
            if (cartDto == null)
            {
                cartDto=new CartDto();
                cartDto.ListProduct=new List<CartDetailDto>();
                cartDto.ListProduct.Add(new CartDetailDto()
                {
                    ProductId = productId,
                    Quantity = 1,
                    ImagePath = product.ResultObj.Path,
                    ProductName = product.ResultObj.Name,
                    UnitPrice = product.ResultObj.PromotionPrice==product.ResultObj.RetailPrice?product.ResultObj.RetailPrice:product.ResultObj.PromotionPrice
                });
            }
            else
            {
                var checkProduct = cartDto.ListProduct.Where(x => x.ProductId == productId).SingleOrDefault();
                if (checkProduct != null)
                {
                    checkProduct.Quantity += 1;
                }
                else
                {
                    cartDto.ListProduct.Add(new CartDetailDto()
                    {
                        Quantity = 1,
                        ProductId = productId,
                        ImagePath = product.ResultObj.Path,
                        ProductName = product.ResultObj.Name,
                        UnitPrice = product.ResultObj.PromotionPrice==product.ResultObj.RetailPrice?product.ResultObj.RetailPrice:product.ResultObj.PromotionPrice
                    });    
                }
            }
            HttpContext.Session.SetJson("Cart",cartDto);
            return HttpStatusCode.OK;
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrderNoLogin(string customerName, string customerAddress,
            string customerPhone, string customerEmail)
        {
            var cart = HttpContext.Session.GetJson<CartDto>("Cart");
            cart.CustomerName = customerName;
            cart.Address = customerAddress;
            cart.PhoneNumber = customerPhone;
            cart.Email = customerEmail;
            var result = await _apiService.CreateOrderForUnknownUser(cart);
            if (result.Code != HttpStatusCode.OK) return RedirectToAction("Cart");
            return View("OrderSuccess", result.ResultObj);
        } 

        [HttpPost]
        public async Task<HttpStatusCode> RemoveProductFromCart(string productId)
        {
            var cart = HttpContext.Session.GetJson<CartDto>("Cart");
            var checkProduct = cart.ListProduct.Where(x => x.ProductId == productId).SingleOrDefault();
            if (checkProduct != null)
            {
                if (checkProduct.Quantity == 1) cart.ListProduct.Remove(checkProduct);
                else checkProduct.Quantity -= 1;
                HttpContext.Session.SetJson("Cart",cart);
                return HttpStatusCode.OK;
            }
            return HttpStatusCode.NotFound;
        }

        [Authorize]
        public async Task<IActionResult> CreateOrder()
        {
            var result = await _apiService.CreateOrderFromCartAsync();
            return RedirectToAction("OrderSuccess");
        }

        public IActionResult OrderSuccess()
        {
            return View();
        }
        public async Task Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
        }
        [Authorize]
        public async Task<IActionResult> Info()
        {
            var result = await _apiService.GetCurrentAccountInfo();
            if (result.Code != HttpStatusCode.OK) return View(new CustomerInfo());
            return View(result.ResultObj);
        }
        public async Task<IActionResult> CheckOrder()
        {
            var isLogin=User.Claims.FirstOrDefault(x => x.Type == "name")?.Value;
            if (isLogin != null)
            {
                var result = await _apiService.GetCart();
                return View(result.ResultObj);
            }
            var cart = HttpContext.Session.GetJson<CartDto>("Cart");
            if (cart == null || cart.ListProduct == null || cart.ListProduct.Count == 0) return View("Cart");
            return View("CheckOrderNoLogin", cart);
        }
        [Authorize]
        public async Task<IActionResult> Orders(int page = 1, int size = 15)
        {
            var result = await _apiService.GetAccountOrders(page, size);
            return View(result.ResultObj);
        }
    }
}