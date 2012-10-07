using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Todo_DataAccess.Repositories
{
    public class TodoRepository_XML : ITodoRepository
    {
        private string XML_Path = "";

        public TodoRepository_XML(string xml_path)
        {
            XML_Path = xml_path;
        }

        #region ITodoRepository Members

        public IEnumerable<Todo> GetUserTasks_All(long user_id)
        {
            var XMLDoc = XDocument.Load(XML_Path);

            var user_todos = (from c in
                                  XMLDoc.Descendants("row")
                              where (Int64)c.Element("User_ID") == user_id
                              select new Todo() {
                                  ID = int.Parse(c.Element("ID").Value),
                                  User_ID = Int64.Parse(c.Element("User_ID").Value),
                                  Task_Description = c.Element("Task_Description").Value,
                                  Task_Complete = (c.Element("Task_Complete").Value == "1"),
                                  Timestamp = DateTime.Parse(c.Element("Timestamp").Value)
                              }).ToList();
            
            return user_todos;
        }

        public IEnumerable<Todo> GetUserTasks_Active(long user_id)
        {
            throw new NotImplementedException();
        }

        public Todo Find(long id, long user_id)
        {
            throw new NotImplementedException();
        }

        public void Delete(long id, long user_id)
        {
            throw new NotImplementedException();
        }

        public void MarkComplete(long id, long user_id, bool complete)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IRepository<Todo> Members

        public IEnumerable<Todo> All
        {
            get { throw new NotImplementedException(); }
        }

        public Todo Find(long id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Todo item)
        {
            throw new NotImplementedException();
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
