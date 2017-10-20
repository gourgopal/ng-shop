using Shopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Shopping.Controllers
{
    public class WebApiController : ApiController
    {
        [HttpPost]
        [Route("api/CheckEmail")]
        public bool CheckEmail(User data) => UserLayer.CheckEmail(data);
        [HttpPost]
        [Route("api/CheckUsername")]
        public bool CheckUsername(User data) => UserLayer.CheckUsername(data);
        [HttpPost]
        [Route("api/InsertUser")]
        public int InsertUser(User data) => UserLayer.InsertUser(data);
        [HttpPost]
        [Route("api/SignInUser")]
        public int SignInUser(User data) => UserLayer.SignInUser(data);
        [HttpGet]
        [Route("api/GetUserType/{UserKey}")]
        public int GetUserType(int? UserKey) => UserLayer.GetUserType(UserKey);
        [HttpGet]
        [Route("api/GetBooks/")]
        public List<Book> GetBooks() => BookLayer.GetBooks();
        [HttpPost]
        [Route("api/AddToCart")]
        public int AddToCart(Cart data) => BookLayer.AddToCart(data);
        [HttpPost]
        [Route("api/GetBooksInCart")]
        public List<Book> GetBooksInCart(User data) => CartLayer.GetBooksInCart(data);
        [HttpPost]
        [Route("api/BuyBooks")]
        public int BuyBooks(Cart data) => CartLayer.BuyBooks(data);
        [HttpPost]
        [Route("api/DeleteFromCart")]
        public int DeleteFromCart(Cart data) => CartLayer.DeleteFromCart(data);
    }
}
