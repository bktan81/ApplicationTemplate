using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
//using System.Web.Http;
//using System.Web.Http.Description;

using System.Web;
using System.Web.Mvc;


namespace WebApplication1.Controllers
{
    public class ProductsController : Controller
    {
     

        public ActionResult Index()
        {
            return View();
        }

       
    }
}