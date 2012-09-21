using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Todo_DataAccess.Repositories;

namespace Todo_DataAccess.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        #region IRepository<Todo> Members

        protected ITI_Todo_DemoDataContext db = new ITI_Todo_DemoDataContext();

        public IEnumerable<Todo> All
        {
            get { return db.Todos; }
        }

        public Todo Find(long id)
        {
            Todo item = db.Todos.Single(t => t.ID == id);
            return item;
        }

        public void Insert(Todo item)
        {
            db.Todos.InsertOnSubmit(item);
        }

        public void Delete(long id)
        {
            Todo item = Find(id);
            db.Todos.DeleteOnSubmit(item);
        }

        public void Save()
        {
            db.SubmitChanges();
        }

        #endregion

        #region ITodoRepository Members

        public IEnumerable<Todo> GetUserTasks_All(long user_id)
        {
            IEnumerable<Todo> todos = db.Todos.Where(t => t.User_ID == user_id);
            return todos;
        }

        public IEnumerable<Todo> GetUserTasks_Active(long user_id)
        {
            IEnumerable<Todo> todos = db.Todos.Where(t => t.User_ID == user_id && t.Task_Complete == false);
            return todos;
        }

        #endregion
    }
}
