using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace Business_Layer.Controllers
{
    public class EmployeeController : ApiController
    {
        ServiceConnector sc = new ServiceConnector();

        // GET: api/Employee
        public async Task<JsonResult<List<Employee>>> Get()
        {
            return Json(await sc.getEmployees());
        }

        // GET: api/Employee?forename={forename}&surname={surname}&departmentId={departmentId}
        public async Task<JsonResult<List<Employee>>> Get(string forename, string surname, int departmentId)
        {
            return Json(await sc.searchEmployees(forename, surname, departmentId));
        }

        // POST: api/Employee
        public async Task<JsonResult<int>> Post(Employee employee)
        {
            if (DateTime.Today.Year - employee.DateOfBirth.Year < 16) 
            {
                return Json(-1);
            }
            else
            {
                return Json(await sc.createEmployee(employee));
            }
        }

        // PUT: api/Employee/5
        public async Task<JsonResult<bool>> Put([FromUri]int id, Employee employee)
        {
            if (id > 0)
            {
                return Json(await sc.updateEmployee(id, employee));
            }
            else
            {
                return Json(false);
            }
        }

        // DELETE: api/Employee/5
        public async Task<JsonResult<bool>> Delete(int id)
        {
            if (id > 0)
            {
                return Json(await sc.deleteEmployee(id));
            }
            else
            {
                return Json(false);
            }
        }
    }
}
