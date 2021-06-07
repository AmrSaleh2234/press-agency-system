using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class favouritesController : Controller
    {
        private Database1Entities3 db = new Database1Entities3();

        // GET: favourites
        public ActionResult Index()
        {
            var favourite = db.favourite.Include(f => f.post).Include(f => f.user);
            return View(favourite.ToList());
        }

        // GET: favourites/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            favourite favourite = db.favourite.Find(id);
            if (favourite == null)
            {
                return HttpNotFound();
            }
            return View(favourite);
        }

        // GET: favourites/Create
        

        // POST: favourites/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpGet]
       
        public ActionResult Create([Bind(Include = "favId,pId")] favourite favourite)
        {
            
            favourite.userId = (int)Session["cid"];
            var rec = db.favourite.Where(model => model.userId == favourite.userId && model.pId== favourite.pId).ToList().FirstOrDefault();

            if (ModelState.IsValid)
            {
                if (rec == null) { 
                db.favourite.Add(favourite);
                db.SaveChanges();
                    return Redirect("index");
                
              }
                
            }
             return RedirectToAction("posts/commentsAndPosts/"+favourite.pId);
            
        }

        // GET: favourites/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            favourite favourite = db.favourite.Find(id);
            if (favourite == null)
            {
                return HttpNotFound();
            }
            ViewBag.pId = new SelectList(db.post, "Tid", "artucle_title", favourite.pId);
            ViewBag.userId = new SelectList(db.user, "Cid", "Username", favourite.userId);
            return View(favourite);
        }

        // POST: favourites/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "favId,pId,userId")] favourite favourite)
        {
            if (ModelState.IsValid)
            {
                db.Entry(favourite).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.pId = new SelectList(db.post, "Tid", "artucle_title", favourite.pId);
            ViewBag.userId = new SelectList(db.user, "Cid", "Username", favourite.userId);
            return View(favourite);
        }

        // GET: favourites/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            favourite favourite = db.favourite.Find(id);
            if (favourite == null)
            {
                return HttpNotFound();
            }
            return View(favourite);
        }

        // POST: favourites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            favourite favourite = db.favourite.Find(id);
            db.favourite.Remove(favourite);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult myFavPost()
        {
            var rec = db.favourite.ToList();
            return View(rec);
        }
    }
}
