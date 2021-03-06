﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Todo_DataAccess.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        #region IRepository<Todo> Members

        protected ITI_Todo_DemoDataContext db;
            
        public TodoRepository(string connectionstring) { 
            db = new ITI_Todo_DemoDataContext(connectionstring);
        }

        public TodoRepository(IDbConnection connection)
        {
            db = new ITI_Todo_DemoDataContext(connection);
        }

        public IEnumerable<Todo> All
        {
            get { return db.Todos.ToList(); }
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

        public void MarkComplete(long id, long user_id, bool complete)
        {
            Todo item = Find(id, user_id);
            item.Task_Complete = complete;
        }

        public void Delete(long id)
        {
            throw new Exception("This is not a secure delete..");
        }

        public void Delete(long id, long user_id)
        {
            Todo item = Find(id, user_id);
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

        public Todo Find(long id, long user_id)
        {
            Todo item = db.Todos.Single(t => t.ID == id);

            if (item.User_ID != user_id)
                throw new Exception("User does not have permission");

            return item;
        }

        #endregion
    }
}
