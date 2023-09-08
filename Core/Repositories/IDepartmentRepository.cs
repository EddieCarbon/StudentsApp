using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IDepartmentRepository
    {
        Department GetById(int DepartmentId);
        IQueryable<Department> GetAll();
        Department Add(Department department);
        void Update(Department department);
        void Delete(Department department);
    }
}
