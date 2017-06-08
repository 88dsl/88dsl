using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
namespace _88DSL.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult UserMgr()
        {
            return View();
        }


        public ActionResult UserDetail(String id)
            
        {
            ViewBag.UserId = id;
            return View();
        }



        public ActionResult PagingControl()
        {
            return View();
        }

        

        [HttpPost]
        public JsonResult GetData( )
        {
            UserInfo order = new UserInfo();
            List<UserInfo> result = null;


            using (DSLEntities entity = new DSLEntities())
            {


                result = entity.UserInfo.OrderBy(a=>a.UserName).ToList(); ;
                //写回数据库
                entity.SaveChanges();
            }
            Console.WriteLine("OK");
        
            

            return Json(result, JsonRequestBehavior.AllowGet);

        }

     



        [HttpPost]
        public String SubmitUserInfo(string userInfo)
        {


            UserInfo us = (UserInfo)JsonConvert.DeserializeObject(userInfo, typeof(UserInfo));

            UserInfo order = new UserInfo();

    
            UserInfo existingInfo = null;


            using (DSLEntities entity = new DSLEntities())
            {


                existingInfo = entity.UserInfo.Where(a => a.UserId == us.UserId).ToList().FirstOrDefault();


            }
          
            if (existingInfo!=null)
            {

                using (DSLEntities entity = new DSLEntities())
                {
                    existingInfo = us;

                    entity.Entry(existingInfo).State = System.Data.Entity.EntityState.Modified;
                    entity.SaveChanges();

                }  
            }


            return "0";

        }
        [HttpPost]
        public JsonResult GetUserById(int id)
        {
            UserInfo order = new UserInfo();
            List<UserInfo> result = null;


            using (DSLEntities entity = new DSLEntities())
            {

                result = entity.UserInfo.OrderBy(a => a.UserName).Where(a => a.UserId == id).ToList();
                //写回数据库
                entity.SaveChanges();
            }
            Console.WriteLine("OK");



            return Json(result, JsonRequestBehavior.AllowGet);

        }


    }
}