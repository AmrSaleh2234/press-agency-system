using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private Database1Entities3 db = new Database1Entities3();
        [HttpGet]
        [ActionName("index")]

        public ActionResult Index_get()
        {
           
                var post = db.post.Where(model =>model.accept!=0).ToList();
                return View(post);
           
        }
        [HttpPost]
        [ActionName("index")]
        public ActionResult index_post(string filter)
        {
            var post = db.post.Where(model=>model.article_type==filter || model.artucle_title.Contains(filter) || model.user.Username==filter).ToList();
            return View(post);
        }
        
        [HttpPost]
        [ActionName("register")]
        public ActionResult register_post([Bind(Include = "Username,Fname,lname,password,pnum")] user user, HttpPostedFileBase imgfile)
        {
            string path = "";
            if (imgfile.FileName.Length > 0)
            {
                path = "~/images/" + Path.GetFileName(imgfile.FileName);
                imgfile.SaveAs(Server.MapPath(path));
            }
            user.user_role = "user";
            user.photo = path;
            ViewBag.Message =user.Lname;
            db.user.Add(user);
            db.SaveChanges();
            return RedirectToAction("index");
        }


       


        [HttpPost]
        [ActionName("login")]
        public ActionResult login_post([Bind(Include = "Username,Password")] user user)
        {

          var rec = db.user.Where(x => x.Username == user.Username && x.Password == user.Password).ToList().FirstOrDefault();
            if (rec != null)
            {  
                Session["cid"] = rec.Cid;
                Session["Username"] = rec.Username;
                Session["userRole"] = rec.user_role;
               
                return RedirectToAction("index");
                
            
                
            }
            else
            {
                TempData["error_message"] = "username or passoword is wrong";
                return RedirectToAction("Index");

            }

       
        }
        [HttpPost]
        [ActionName("logout")]
        public ActionResult logout_post([Bind(Include = "Username,Password")] user user)
        {

            Session.Clear();

            
                
                return RedirectToAction("index");

           
        }
        [HttpGet]
        [ActionName("login")]
        public ActionResult login_get()
        {
            return RedirectToAction("Index"); 
        }



       
    }
}