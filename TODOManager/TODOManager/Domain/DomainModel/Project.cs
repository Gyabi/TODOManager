using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace TODOManager.Domain.DomainModel
{
    /// <summary>
    /// Projectを示すドメイン
    /// </summary>
    public class Project
    {
        public string projectName { get; set; }
        public ProjectID projectID { get; set; }

        public Project(string projectName, ProjectID projectID)
        {
            this.projectName = projectName;
            this.projectID = projectID;
        }
    }
}
