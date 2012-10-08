using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Todo_DataAccess.Repositories;
using Todo_DataAccess;

namespace Todo_Test
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class DataAccess_Tests
    {
        public DataAccess_Tests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        private enum TodoRepoType { 
            SQL,
            XML
        }

        private ITodoRepository GetRepository(TodoRepoType dbType) {
            ITodoRepository db = null;

            switch (dbType) { 
                case TodoRepoType.SQL:
                    db = new TodoRepository(@"Data Source=.\SQLEXPRESS;Initial Catalog=aspnet-ITI_Todo-20120914092713;Integrated Security=SSPI");
                    break;
                case TodoRepoType.XML:
                    db = new TodoRepository_XML("Data/Todos.xml");
                    break;
            }

            return db;
        }

        #region SQL Repository Methods

        [TestMethod]
        public void Todo_Repo_Get_All()
        {
            try
            {
                ITodoRepository db = GetRepository(TodoRepoType.SQL);
                IEnumerable<Todo> todos = db.All;
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void Todo_Repo_User_Todos()
        {
            Int64 user_id = 2;

            ITodoRepository db = GetRepository(TodoRepoType.SQL);
            Todo[] todos = db.GetUserTasks_All(user_id).ToArray();

            int expected = 0;
            int actual = todos.Length;

            Assert.AreNotEqual(expected, actual);
        }


        [TestMethod]
        public void Todo_Repo_Create_Todo()
        {
            Int64 user_id = 3;
            Todo new_todo = new Todo() { 
                User_ID = user_id,
                Task_Description = "Test Todo: " + DateTime.Now.ToString(),
                Task_Complete = false,
                Timestamp = DateTime.Now
            };

            ITodoRepository db = GetRepository(TodoRepoType.SQL);
            
            int expected = db.All.ToList().Count + 1; // We expect to have ONE more after the INSERT
            db.Insert(new_todo);
            db.Save();
            int actual = db.All.ToList().Count;

            Assert.AreEqual(expected, actual);
        }

        #endregion
        
        #region XML Repository Methods

        [TestMethod]
        public void Todo_Repo_User_Todos_XML()
        {
            Int64 user_id = 1;
            
            ITodoRepository db = GetRepository(TodoRepoType.XML);
            Todo[] todos = db.GetUserTasks_All(user_id).ToArray();

            int expected = 0;
            int actual = todos.Length;

            Assert.AreNotEqual(expected, actual);
        }

        #endregion
    }
}
