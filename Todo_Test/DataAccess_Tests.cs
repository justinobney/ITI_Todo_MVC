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

        [TestMethod]
        public void Todo_Repo_Get_All()
        {
            try
            {
                string connection = "";
                ITodoRepository db = new TodoRepository(connection);
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

            string connection = "";

            ITodoRepository db = new TodoRepository(connection);
            Todo[] todos = db.GetUserTasks_All(user_id).ToArray();

            int expected = 0;
            int actual = todos.Length;

            Assert.AreNotEqual(expected, actual);
        }

        [TestMethod]
        public void Todo_Repo_User_Todos_XML()
        {
            Int64 user_id = 1;
            
            ITodoRepository db = new TodoRepository_XML("");
            Todo[] todos = db.GetUserTasks_All(user_id).ToArray();

            int expected = 0;
            int actual = todos.Length;

            Assert.AreNotEqual(expected, actual);
        }
    }
}
