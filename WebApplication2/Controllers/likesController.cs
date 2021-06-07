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
    public class likesController : Controller
    {
        private Database1Entities3 db = new Database1Entities3();

        // GET: likes
        public ActionResult Index()
        {
            var like = db.like.Include(l => l.post).Include(l => l.user);
            return View(like.ToList());
        }

        // GET: likes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            like like = db.like.Find(id);
            if (like == null)
            {
                return HttpNotFound();
            }
            return View(like);
        }

        // GET: likes/Create
        

        // POST: likes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpGet]
       
        public ActionResult Create([Bind(Include = "postId,lId")] like like)
        {

            
            like.userId = (int) Session["cid"];
            var rec = db.like.Where(model => model.userId == like.userId && model.postId == like.postId).ToList().FirstOrDefault();
            if (ModelState.IsValid)
            {
                if (rec == null)
                {
                db.like.Add(like);
                db.SaveChanges();
                return RedirectToAction("index");
                }
                else
                {
                    return RedirectToAction("index");
                }
                
            }

         
            return RedirectToAction("home/index");
        }

        // GET: likes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            like like = db.like.Find(id);
            if (like == null)
            {
                return HttpNotFound();
            }
            ViewBag.postId = new SelectList(db.post, "Tid", "artucle_title", like.postId);
            ViewBag.userId = new SelectList(db.user, "Cid", "Username", like.userId);
            return View(like);
        }

        // POST: likes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "postId,userId,lId")] like like)
        {
            if (ModelState.IsValid)
            {
                db.Entry(like).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.postId = new SelectList(db.post, "Tid", "artucle_title", like.postId);
            ViewBag.userId = new SelectList(db.user, "Cid", "Username", like.userId);
            return View(like);
        }

        // GET: likes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            like like = db.like.Find(id);
            if (like == null)
            {
                return HttpNotFound();
            }
            return View(like);
        }

        // POST: likes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            like like = db.like.Find(id);
            db.like.Remove(like);
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
    }
}
