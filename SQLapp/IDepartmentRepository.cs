using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bestbuy
{
    internal interface IDepartmentRepository
    {
        public interface IDepartmentRepository
        {
            IEnumerable<Department> GetAllDepartments();
            void CreateDepartment(string Name);
        }



    }
}
