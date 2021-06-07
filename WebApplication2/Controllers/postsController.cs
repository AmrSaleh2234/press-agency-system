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
    public class postsController : Controller
    {
        private Database1Entities3 db = new Database1Entities3();

        // GET: posts
        

        // GET: posts/Details/5
        
        // GET: posts/Create
        public ActionResult Create()
        {
            ViewBag.cid = new SelectList(db.user, "Cid", "Username");
            return View();
        }

        // POST: posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Tid,artucle_title,article_body,article_type")] post post, HttpPostedFileBase imgfile)
        {
            string path = "";
            if (imgfile.FileName.Length > 0)
            {
                path = "~/images/" + Path.GetFileName(imgfile.FileName);
                imgfile.SaveAs(Server.MapPath(path));
            }
            DateTime today = DateTime.Today;
            post.cid = (int)Session["cid"];
            post.post_creatin = Session["Username"].ToString();
            post.numbers_of_views = 0;
            post.image = path;
            post.accept = 0;
            post.likes = 0;
            post.date = today;
            post.isNew = 1;
            
           
            if (ModelState.IsValid)
            {

                db.post.Add(post);
                db.SaveChanges();
               return RedirectToAction("Index", "home");

            }


            ViewBag.cid = new SelectList(db.user, "Cid", "Username", post.cid);
            return View(post);
        }

        // GET: posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            post post = db.post.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.cid = new SelectList(db.user, "Cid", "Username", post.cid);
            return View(post);
        }

        // POST: posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Tid,artucle_title,article_body,article_type")] post post, HttpPostedFileBase imgfile)
        {
            string path = "";
            if (imgfile.FileName.Length > 0)
            {
                path = "~/images/" + Path.GetFileName(imgfile.FileName);
                imgfile.SaveAs(Server.MapPath(path));
            }
            var before = db.post.Where(x => x.Tid == post.Tid).ToList().FirstOrDefault();
            if (before != null)
            {
                db.Entry(before).State = EntityState.Detached;
            }

            post.cid = before.cid;
            post.post_creatin = before.post_creatin;
            post.numbers_of_views = before.numbers_of_views;
            post.likes = before.likes;
            post.accept = before.accept;
            post.date = before.date;
            post.isNew = before.isNew;
            post.image = path;
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","home");

            }
            ViewBag.cid = new SelectList(db.user, "Cid", "Username", post.cid);
            return View(post);
        }

        // GET: posts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            post post = db.post.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            post post = db.post.Find(id);
            db.post.Remove(post);
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
        public ActionResult ShowMyPosts()
        {

            int y =(int) Session["cid"];
            var recs = db.post.Where(x => x.cid == y).ToList();

            return View(recs);
        }
        public ActionResult requests()
        {

             
            var recs = db.post.Where(x => x.isNew == 1).ToList();
            recs.ForEach(i => i.isNew = 0);
            foreach(post i in recs)
            {
            db.Entry(i).State = EntityState.Modified;
            db.SaveChanges();
            }

            return View(recs);
        }
        public ActionResult commentsAndposts(int id )
        {
            // var list = db.post.Include(c => c.comment).Include(c => c.user).Where(c=>c.Tid==id);
           
            var query = from p in db.post.ToList()
                        join c in db.comment.ToList() on p.Tid equals c.pId  into table1
                        from c in table1.DefaultIfEmpty()
                        where p.Tid == id 
                        select new postAndComment { postDetalis= p,commentsDetails =c};

        
            return View(query.ToList());
        }
        public ActionResult insert(int id)
        {
            var rec = db.post.Where(model => model.Tid == id).ToList().FirstOrDefault();

            if (rec != null)
            {
                db.Entry(rec).State = EntityState.Detached;
            }
            rec.accept = 1;
            db.Entry(rec).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "home");


        }
    }
}
