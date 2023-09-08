using Core.Entities;
using Core.Repositories;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly StudentAppContext _context;

        public DepartmentRepository(StudentAppContext context)
        {
            _context = context;
        }

        public IQueryable<Department> GetAll()
        {
            return _context.Departments;
        }

        public Department GetById(int Id)
            => _context.Departments.SingleOrDefault(x => x.DepartmentId == Id);


        public Department Add(Department department)
        {
            _context.Departments.Add(department);
            _context.SaveChanges();
            return department;
        }

        public void Update(Department department)
        {
            _context.Departments.Update(department);
            _context.SaveChanges();
        }

        public void Delete(Department department)
        {
            _context.Departments.Remove(department);
            _context.SaveChanges();
        }
    }
}
