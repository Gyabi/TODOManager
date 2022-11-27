using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOManager.Domain.DomainModel;

namespace TODOManager.Domain.DomainService.Repository
{
    public interface IProjectRepository
    {
        public List<Project> ReadProjects();
        public void InsertData(string id, string name);
        public void DeleteData(string id);
    }
}
