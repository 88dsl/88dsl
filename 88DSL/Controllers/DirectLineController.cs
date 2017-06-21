using eBay.Service.Call; 
using eBay.Service.Core.Sdk;
using eBay.Service.Core.Soap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _88DSL.Controllers
{
    public class DirectLineController : Controller
    {

       [RoleAuthorizationAttribute(Roles = "customers")]  
        public ActionResult DirectLineOrderMgr()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetEbayOrderData()
        {
            DirectlineOrder order = new DirectlineOrder();
            List<DirectlineOrder> result = new List<DirectlineOrder>();
/*

             using (DSLEntities entity = new DSLEntities())
             {


                 result = entity.DirectlineOrder.OrderBy(a=>a.DirectlineOrderId).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList(); ;
                 //写回数据库
                 entity.SaveChanges();
             }
             Console.WriteLine("OK");

             */


            ApiContext apiContext = new ApiContext();

            apiContext.SoapApiServerUrl = "https://api.sandbox.ebay.com/wsapi";

            ApiCredential apiCredential = new ApiCredential();
            apiCredential.eBayToken = "AgAAAA**AQAAAA**aAAAAA**N7s/WQ**nY+sHZ2PrBmdj6wVnY+sEZ2PrA2dj6wFk4GkCpiGpg2dj6x9nY+seQ**wi8EAA**AAMAAA**7o44E/rl2J6N6sif8itG2DKpkdFQqAJK8d28n2dv8mzwagpQVHIjFjmn8RuPzyiw06c9T7vcRo81RNElYv/A8kFvEEcKxGeEtFxuitv2VBs9GO/GkOGJqK3dtl6ivmKazSb5T3XyG9hyer+lk2TgcGZEYgmXbus/D3Ql3vEkjaT7Zr04yowX6EVNa/sv6j2YvncL8/A9nzq9z169en8xS6bOTvcLm/k5XXfbZ4Jyr6zk66ifCK7fJ9Li7yKSsPHHCU9hQo6vgDLLyf/GEDb5j4HV2OGN02vb6iWyMny/HYP17hPNCue6jbc7jNv0klTpPXJ7i2z0pDzqRmfPFSluqeohRojd81FChqjTob+VXJ7nuS1SpG5XxxCAn0B9dylLm91lncPmoi6FJ9HRM1SlHoH/dWqbtlHyjZ5yAuuYpHwAAfqMXuaDsGr6dNfS7JsRePkaKhbkwna2XYasdqZ2VK2jDVoZYwPnsWj7m9S/QxGJ09I+Ar5J1kDpzGjiezy1mruSV3pflqfs8E+HWybzeB+yisrtLf5eBKS8AgW2otQOg/lI3KDJR1+faS5M6ZExnT+K5t8TZn9RKtBaMa07D6/MRkhANKx5RSoMFfT488JETTGNE1IAV/1kf7Pzm1HNqBCxEn97b27SECe6dwVTWwqi6ZR/2G00D1IQDVCmNkSxloApEdlV7qRtOpmUFHzCa8zdS53f0RHSjxV8JfCLvB4FXw/+849jtNek+wtNO3Fcd3nimPqaqF1eWfrIeCBq";

            apiContext.ApiCredential = apiCredential;




            GetOrdersCall get = new GetOrdersCall(apiContext);

            OrderTypeCollection types = get.GetOrders(DateTime.Parse("2017-01-01 12:33:33"), DateTime.Parse("2017-06-20 12:33:33"), TradingRoleCodeType.Seller, OrderStatusCodeType.All);


            if (types != null && types.Count > 0)
            {

                foreach (OrderType o in types)
                {


                    DirectlineOrder addOrder = new DirectlineOrder();

                    addOrder.BuyerName = o.BuyerUserID;

                    addOrder.BBCode = o.TransactionArray[0].Item.ItemID;

                    addOrder.Quantity = o.TransactionArray[0].Item.Quantity.ToString();

                    addOrder.Status = o.OrderStatus.ToString();


                   
                    result.Add(addOrder);


                    //  textBox.Text = "Order Id:" + types[0].TransactionArray[0].Item.Title + ", Buyer Name:" + types[0].ShippingAddress.Name;
                }

            }
            return Json(result, JsonRequestBehavior.AllowGet);

        }



    }
}