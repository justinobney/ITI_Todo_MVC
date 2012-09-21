using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using ITI_Todo.Filters;
using Todo_DataAccess.Repositories;

namespace ITI_Todo.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class TodoController : Controller
    {
        //
        // GET: /Todo/

        protected TodoRepository db = new TodoRepository();

        public ActionResult Index()
        {
            string user_name = User.Identity.Name;
            Int64 user_id = WebSecurity.GetUserId(user_name);
            string user_info = string.Format("{0} | {1}", user_name, user_id);

            ViewBag.user_info = user_info;

            var model = db.GetUserTasks_All(user_id).ToArray();

            return View(model);
        }

        //
        // GET: /Todo/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Todo/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Todo/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Todo/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Todo/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Todo/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Todo/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
