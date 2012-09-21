using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Todo_DataAccess;

namespace Todo_DataAccess.Repositories
{
    public interface ITodoRepository : IRepository<Todo>
    {
        IEnumerable<Todo> GetUserTasks_All(Int64 user_id);
        IEnumerable<Todo> GetUserTasks_Active(Int64 user_id);
    }
}