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

        [RoleAuthorizationAttribute(Roles = "admins")]
        public ActionResult UserMgr()
        {
            return View();
        }



        [RoleAuthorizationAttribute(Roles = "admins,customers")]
        public ActionResult EditProfile()
        {
            return View();
        }


        private string DecodeIdValue(String orValue)
        {
            string tmp = orValue == null ? "" : orValue.Replace("QT", "").Replace("ZU", "");

            if (!string.IsNullOrEmpty(tmp))
            {
                return (int.Parse(tmp) / 1234).ToString();
            }

            return tmp;

        }

        [RoleAuthorizationAttribute(Roles = "admins")]

        public ActionResult UserDetail(String id)
        {

            ViewBag.UserId = DecodeIdValue(id);
            return View();
        }


        public ActionResult ProductMgr()
        {
            return View();
        }


        [RoleAuthorizationAttribute(Roles = "admins")]

        public ActionResult ProductDetail(String id)

        {
            ViewBag.ProductId = id;
            return View();
        }



        public ActionResult PagingControl()
        {
            return View();
        }


        [RoleAuthorizationAttribute(Roles = "admins")]
        [HttpPost]
        public JsonResult GetAllUserData()
        {
            UserInfo order = new UserInfo();
            List<UserInfo> result = null;


            using (DSLEntities entity = new DSLEntities())
            {


                result = entity.UserInfo.ToList();
                //写回数据库
                entity.SaveChanges();
            }
            Console.WriteLine("OK");



            return Json(result, JsonRequestBehavior.AllowGet);

        }





        [HttpPost]
        [RoleAuthorizationAttribute(Roles = "admins")]
        public String SubmitUserInfo(string userInfo)
        {


            UserInfo us = (UserInfo)JsonConvert.DeserializeObject(userInfo, typeof(UserInfo));

            if (us.UserId == 0)
            {
                return AddUserInfo(us);

            }

            UserInfo order = new UserInfo();


            UserInfo existingInfo = null;


            using (DSLEntities entity = new DSLEntities())
            {


                existingInfo = entity.UserInfo.Where(a => a.UserId == us.UserId).ToList().FirstOrDefault();


            }

            if (existingInfo != null)
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

        [RoleAuthorizationAttribute(Roles = "admins")]
        private String AddUserInfo(UserInfo userInfo)
        {


            using (DSLEntities entity = new DSLEntities())
            {


                entity.UserInfo.Add(userInfo);
                entity.SaveChanges();

            }


            return "0";

        }



        [RoleAuthorizationAttribute(Roles = "customers")]

        [HttpPost]
        public JsonResult GetProfileByUserId()
        {

            String userId = Utils.GetUserInfo().UserId;
            List<UserInfo> result = null;

            using (DSLEntities entity = new DSLEntities())
            {

                result = entity.UserInfo.OrderBy(a => a.UserName).Where(a => a.UserId.ToString().Equals(userId)).ToList();
        
            }
            Console.WriteLine("OK");



            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [RoleAuthorizationAttribute(Roles = "customers")]
        [HttpPost]
        public String SubmitProfilesInfo(string userInfo)
        {


            UserInfo us = (UserInfo)JsonConvert.DeserializeObject(userInfo, typeof(UserInfo));




            UserInfo existingData = new UserInfo();


            using (DSLEntities entity = new DSLEntities())
            {


                existingData = entity.UserInfo.Where(a => a.UserId == us.UserId).ToList().FirstOrDefault();


            }

            if (existingData != null)
            {

                using (DSLEntities entity = new DSLEntities())
                {
                    existingData = us;

                    entity.Entry(existingData).State = System.Data.Entity.EntityState.Modified;
                    entity.SaveChanges();

                }
            }


            return "0";

        }


        [RoleAuthorizationAttribute(Roles = "admins")]
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








        [HttpPost]
        public JsonResult GetAllProductData()
        {
            ProductInfo order = new ProductInfo();
            List<ProductInfo> result = null;


            using (DSLEntities entity = new DSLEntities())
            {


                result = entity.ProductInfo.ToList();
                //写回数据库
                entity.SaveChanges();
            }
            Console.WriteLine("OK");



            return Json(result, JsonRequestBehavior.AllowGet);

        }





        [HttpPost]
        public String SubmitProductInfo(string userInfo)
        {


            ProductInfo us = (ProductInfo)JsonConvert.DeserializeObject(userInfo, typeof(ProductInfo));

            ProductInfo proInfo = new ProductInfo();


            ProductInfo existingData = new ProductInfo();


            using (DSLEntities entity = new DSLEntities())
            {


                existingData = entity.ProductInfo.Where(a => a.ProductId == us.ProductId).ToList().FirstOrDefault();


            }

            if (existingData != null)
            {

                using (DSLEntities entity = new DSLEntities())
                {
                    existingData = us;

                    entity.Entry(existingData).State = System.Data.Entity.EntityState.Modified;
                    entity.SaveChanges();

                }
            }


            return "0";

        }
        [HttpPost]
        public JsonResult GetProductById(int id)
        {
            UserInfo order = new UserInfo();
            List<ProductInfo> result = null;


            using (DSLEntities entity = new DSLEntities())
            {

                result = entity.ProductInfo.OrderBy(a => a.ProductName).Where(a => a.ProductId == id).ToList();
                //写回数据库
                entity.SaveChanges();
            }
            Console.WriteLine("OK");



            return Json(result, JsonRequestBehavior.AllowGet);

        }


    }
}