using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOManager.Domain.DomainModel;

namespace TODOManager.Domain.DomainService.Factory
{
    public interface IProjectFactory
    {
        public ProjectID CreateProjectID();
    }
}
