using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using ITI_Todo.Filters;
using Todo_DataAccess.Repositories;
using Todo_DataAccess;
using System.Configuration;
using System.IO;
using StackExchange.Profiling.Data;
using StackExchange.Profiling;
using System.Data.SqlClient;

namespace ITI_Todo.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class TodoController : Controller
    {
        //
        // GET: /Todo/
        //var profiled = new ProfiledDbConnection(cnn, MiniProfiler.Current);
        protected ITodoRepository db; // = new TodoRepository(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        public TodoController() {
            MiniProfiler profiler = MiniProfiler.Current;
            // Setup a profiled connection.
            var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            var profiledConn = new StackExchange.Profiling.Data.ProfiledDbConnection(conn, profiler);

            db = new TodoRepository(profiledConn);
        }

        public ActionResult Index()
        {
            //db = new TodoRepository(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

            //db = new TodoRepository_XML(
            //    Server.MapPath(ConfigurationManager.ConnectionStrings["XMLConnection"].ConnectionString)
            //);

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

            Loggr.Events.Create()
                .Text("Create Todo")
                .Tags("CRUD")
                .Source(User.Identity.Name)
                .Data("user-id: {0} | on: {1} | data: {2}", user_id, DateTime.Now, new_todo.Task_Description)
                .Post();

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
            todo.Timestamp = DateTime.Now;
            db.Save();

            Loggr.Events.Create()
                .Text("Update Todo")
                .Tags("CRUD")
                .Source(User.Identity.Name)
                .Data("user-id: {0} | on: {1} | data: {2}", user_id, DateTime.Now, todo.Task_Description)
                .Post();

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

            Loggr.Events.Create()
                .Text("Delete Todo")
                .Tags("CRUD")
                .Source(User.Identity.Name)
                .Data("user-id: {0} | on: {1} | data: {2}", user_id, DateTime.Now, db.Find(id, user_id).Task_Description)
                .Post();

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
