using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODOManager.Domain.DomainModel
{
    public class ProjectID
    {
        public string id { get; set; }

        public ProjectID(string id)
        {
            this.id = id;
        }
    }
}
