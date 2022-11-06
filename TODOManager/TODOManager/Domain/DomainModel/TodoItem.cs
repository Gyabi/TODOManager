using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODOManager.Domain.DomainModel
{
    internal class TodoItem
    {
        public string ItemName { get; set; }

        public TodoItem(string itemName)
        {
            this.ItemName = itemName;
        }
    }
}
