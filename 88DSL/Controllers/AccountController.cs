using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using _88DSL.Models;
using System.Web.Security;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Net;
using System.Text;
using System.IO;
using PayPal.Api;

namespace _88DSL.Controllers
{

    public class AccountController : Controller
    {


        [HttpPost]
        public String Logout()
        {

            FormsAuthentication.SignOut();

            return "";

        }



        [AllowAnonymous]
        public ActionResult Recharge()
        {
            return View();
        }

        [RoleAuthorizationAttribute(Roles = "customers")]
        [HttpPost]
        public String SubmitRechargeInfo(String paymentId,String amount,String paymentTime)
        {
            OAuthTokenCredential oa = new OAuthTokenCredential("AQmW6s0chChHeblhmaIYQda6Bn1gmfSSUnvUCHNCLZM_7BiLOb5BMrINuIxUswdG8Prb8K3ktHQpZuho", "ENQKqkutUnKuTGBWiNDvJ_sA131lwCU4c1eaJwFcn58M88LtjHJd9TLgvjT4nuXkruuSkD0OF0u_XP_W");


            PayPal.Api.APIContext apiContext = new PayPal.Api.APIContext(oa.GetAccessToken());

            try
            {
                Payment payment = PayPal.Api.Payment.Get(apiContext, paymentId);

                if (payment == null || String.IsNullOrEmpty(payment.id))
                {
                    return "11";
                }
            }
            catch (Exception ex)
            {
                return "11";
            }

            LoginViewModel user = Utils.GetUserInfo();

            PaymentInfo pInfo = null;
            using (DSLEntities entity = new DSLEntities())
            {



                pInfo = entity.PaymentInfo.Where(a => a.UserId.ToString() == user.UserId && a.PaymentId==paymentId).ToList().FirstOrDefault();


            }

            if (pInfo != null)
            {
                return "1";
            }



            UserInfo existingData = null;


            using (DSLEntities entity = new DSLEntities())
            {


                existingData = entity.UserInfo.Where(a => a.UserId.ToString() ==user.UserId).ToList().FirstOrDefault();


            }

            if (existingData != null)
            {

                using (DSLEntities entity = new DSLEntities())
                {
                    existingData.Balance += decimal.Parse(amount);                                        
                    entity.Entry(existingData).State = System.Data.Entity.EntityState.Modified;
                    entity.SaveChanges();

                }
            }

            PaymentInfo addPayInfo = new PaymentInfo();

            addPayInfo.Amount = Decimal.Parse(amount);
            addPayInfo.PaymentId = paymentId;
            addPayInfo.PaymentState = "Approval";
            addPayInfo.PaymentType = "PayPal";
            addPayInfo.UserId = int.Parse( user.UserId);
            addPayInfo.PaymentTime = DateTime.Parse(paymentTime);



            using (DSLEntities entity = new DSLEntities())
            {

                entity.PaymentInfo.Add(addPayInfo);   
                entity.SaveChanges();

            }


            return "0";

        }


        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {      

            ViewBag.ReturnUrl = returnUrl;
            ViewBag.PageId = "login";
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            ViewBag.PageId = "login";

            if (!ModelState.IsValid)
            {
                return View(model);
            }


            UserInfo existingInfo = null;

            using (DSLEntities entity = new DSLEntities())
            {


                existingInfo = entity.UserInfo.Where(a => a.Email == model.UserEmail && a.Pwd==model.Password).ToList().FirstOrDefault();


            }

            if (existingInfo != null)
            {




                String role = "admins";

                //1: backend   2: customer
                if (existingInfo.ClientType.Equals("2"))
                {
                    role = "customers";
                }


                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                    1,
                    "sdf",
                    DateTime.Now,
                    DateTime.Now.AddMinutes(30),
                    false,
                    existingInfo.UserId + "," + existingInfo.UserName + "," + role + "," + existingInfo.Balance.ToString()
                    );

                               



                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                System.Web.HttpCookie authCookie = new System.Web.HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                System.Web.HttpContext.Current.Response.Cookies.Add(authCookie);




                Response.Redirect("~/DirectLine/DirectLineOrderMgr");

                return View(model);
            }
            else
            {

                ModelState.AddModelError("", "Invalid Email Addres Or Password.");

                return View(model);
            }
        }
    }
}