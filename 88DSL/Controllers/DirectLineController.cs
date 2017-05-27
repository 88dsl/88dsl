using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _88DSL.Controllers
{
    public class DirectLineController : Controller
    {
        public ActionResult DirectLineOrderMgr()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetData(int pageSize, int currentPage)
        {
            DirectlineOrder order = new DirectlineOrder();
            List<DirectlineOrder> result = null;


            using (DSLEntities entity = new DSLEntities())
            {


                result = entity.DirectlineOrder.OrderBy(a=>a.id).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList(); ;
                //写回数据库
                entity.SaveChanges();
            }
            Console.WriteLine("OK");
        
            

            return Json(result, JsonRequestBehavior.AllowGet);

        }


    }
}