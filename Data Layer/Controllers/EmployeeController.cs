using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace Data_Layer.Controllers
{
    public class EmployeeController : ApiController
    {
        DBComms db = new DBComms();

        // GET: api/Person
        public JsonResult<List<Employee>> Get()
        {
            List<Employee> emps = db.getEmployees();
            return Json(emps);
        }

        // GET: api/Person?forename={forename}&surname={surname}&departmentId={departmentId}
        public JsonResult<List<Employee>> Get(string forename, string surname, int departmentId)
        {
            return Json(db.getEmployeeByNameAndNum(forename, surname, departmentId));
        }

        // POST: api/Person
        public JsonResult<int> Post(Employee emp)
        {
            return Json(db.createEmployee(emp.Number, emp.Forename, emp.Surname, emp.DateOfBirth, emp.DepartmentID));
        }

        // PUT: api/Person/5
        public JsonResult<bool> Put(int id, Employee emp)
        {
            return Json(db.updateEmployee(id, emp.Number, emp.Forename, emp.Surname, emp.DateOfBirth, emp.DepartmentID));
        }

        // DELETE: api/Person/5
        public JsonResult<bool> Delete(int id)
        {
            return Json(db.deleteEmployee(id));
        }
    }
}
