using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TODOManager.Domain.DomainModel;

namespace Test.Sample
{
    [TestFixture]
    internal class SampleTest
    {
        [TestCase("test")]
        [TestCase("test2")]
        public void ExeTest1(string itemName)
        {
            TodoItem todoitem = new TodoItem(itemName);
            Assert.AreEqual(todoitem.ItemName, itemName);
        }
    }
}
