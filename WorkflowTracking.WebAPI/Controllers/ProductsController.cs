using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WorkflowTracking.Core.Db;
using System.Web.Http;
using CommonLib.Web;
using System.Web.Mvc;
using WorkflowTracking.Core;

namespace WorkflowTracking.WebAPI.Controllers
{
    [System.Web.Http.Authorize]
    public class ProductsController : ApplicationController
    {
        private WorkflowTracking.Core.Db.WorkflowTrackingDbEntities db = new WorkflowTracking.Core.Db.WorkflowTrackingDbEntities();

      
        [System.Web.Http.HttpPost]
        public object GetProducts()
        {
            return Json(
             CheckAccess(WorkflowTrackingAccessNames.GetProducts,
             success: delegate()
             {
                 return new StatusMessage(db.Products, ReturnStatus.Success);
             })
         );

        }

        [System.Web.Http.HttpPost]
        public object GetProduct(long id)
        {
            return Json(
              CheckAccess(WorkflowTrackingAccessNames.GetProduct,
              success: delegate()
              {
                  Product product = db.Products.Find(id);
                  return new StatusMessage(new List<Product>() { product }, ReturnStatus.Success);
              })
          );
        }


        [System.Web.Http.HttpPost]
        public object FindProduct(string name)
        {
            return Json(
                CheckAccess(WorkflowTrackingAccessNames.FindProduct,
                success: delegate()
                {
                    var products = db.Products.Where(p => p.Name.Contains(name)).ToList();
                    return new StatusMessage(products, ReturnStatus.Success);
                })
            );
        }

        [System.Web.Http.HttpPost]
        public object CreateProduct(Product product)
        {

            return Json(
                CheckAccess(WorkflowTrackingAccessNames.CreateProduct,
                success: delegate()
                {
                    if (!string.IsNullOrEmpty(product.Name) && product.Price>0)
                    {
                        product.CreatedAt = DateTime.Now;
                        db.Products.Add(product);
                        if (db.SaveChanges() > 0)
                        {
                            return new StatusMessage(null, ReturnStatus.Success);
                        }
                        else
                        {
                            return new StatusMessage(null, ReturnStatus.Failed);
                        }
                    }
                    else
                    {
                        return new StatusMessage(null, ReturnStatus.InvalidInput);
                    }
                  
                       
                    
                })
            );

        }
    }
}