using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using ITI_Todo.Filters;
using Todo_DataAccess.Repositories;
using Todo_DataAccess;

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
            string user_info = user_name; //string.Format("{0} | {1}", user_name, user_id);

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
            string user_name = User.Identity.Name;
            Int64 user_id = WebSecurity.GetUserId(user_name);

            Todo new_todo = new Todo();
            new_todo.Task_Description = collection["new_todo"];
            new_todo.User_ID = user_id;
            new_todo.Timestamp = DateTime.Now;
            db.Insert(new_todo);
            db.Save();

            var model = db.GetUserTasks_All(user_id).ToArray();

            return PartialView("Partials/_UserTodos", model);
            //return Json(new { response = "Data received was: " + collection["new_todo"] });
        }

        //
        // POST: /Todo/Update/5

        [HttpPost]
        public ActionResult Update(int id, FormCollection collection)
        {
            string user_name = User.Identity.Name;
            Int64 user_id = WebSecurity.GetUserId(user_name);

            Todo todo = db.Find(id, user_id);
            todo.Task_Description = collection["todo"];
            new_todo.Timestamp = DateTime.Now;
            db.Save();

            var model = db.GetUserTasks_All(user_id).ToArray();

            return PartialView("Partials/_UserTodos", model);
        }

        //
        // POST: /Todo/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            string user_name = User.Identity.Name;
            Int64 user_id = WebSecurity.GetUserId(user_name);

            db.Delete(id,user_id);
            db.Save();

            var model = db.GetUserTasks_All(user_id).ToArray();

            return PartialView("Partials/_UserTodos", model);
        }

        //
        // POST: /Todo/Delete/5

        [HttpPost]
        public ActionResult MarkComplete(int id, FormCollection collection)
        {
            string user_name = User.Identity.Name;
            Int64 user_id = WebSecurity.GetUserId(user_name);
            bool complete = !string.IsNullOrEmpty(collection["todo_complete"]) && collection["todo_complete"] == "on";

            db.MarkComplete(id, user_id, complete);
            db.Save();

            var model = db.GetUserTasks_All(user_id).ToArray();

            return PartialView("Partials/_UserTodos", model);
        }
    }
}
