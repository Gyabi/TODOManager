using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODOManager.Domain.DomainModel
{
    public class TodoItemID
    {
        public string id { get; set; }

        public TodoItemID(string id)
        {
            this.id = id;
        }
    }
}
