﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Leikjavefur.Entities;
using Leikjavefur.Models.Context;

namespace Leikjavefur.Controllers
{
    public class ClanController : Controller
    {
        private ApplicationContext db = new ApplicationContext();

        //
        // GET: /Clan/

        public ActionResult Index()
        {
            return View(db.Clans.ToList());
        }

        //
        // GET: /Clan/Details/5

        public ActionResult Details(int id = 0)
        {
            Clan clan = db.Clans.Find(id);
            if (clan == null)
            {
                return HttpNotFound();
            }
            return View(clan);
        }

        //
        // GET: /Clan/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Clan/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Clan clan)
        {
            if (ModelState.IsValid)
            {
                clan.DateCreated = DateTime.Now;
                db.Clans.Add(clan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(clan);
        }

        //
        // GET: /Clan/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Clan clan = db.Clans.Find(id);
            if (clan == null)
            {
                return HttpNotFound();
            }
            return View(clan);
        }

        //
        // POST: /Clan/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Clan clan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(clan);
        }

        //
        // GET: /Clan/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Clan clan = db.Clans.Find(id);
            if (clan == null)
            {
                return HttpNotFound();
            }
            return View(clan);
        }

        //
        // POST: /Clan/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Clan clan = db.Clans.Find(id);
            db.Clans.Remove(clan);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}