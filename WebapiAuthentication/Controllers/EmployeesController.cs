using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebapiAuthentication.ApiModel;
using WebapiAuthentication.Models.Data.CompanyDBContext;

namespace WebapiAuthentication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private CompanyContext _Context;
        public EmployeesController(CompanyContext context)
        {
            _Context = context;
        }

        [HttpPost]
        public ActionResult Post(AddEmployeeApiModel model)
        {
            Employee employee = new()
            {
                EmployeeName = model.EmployeeName,
                EmployeeAge = model.EmployeeAge,
                EmployeeSalary = model.EmployeeSalary
            };
            _Context.Employees.Add(employee);
            _Context.SaveChanges();
            return Ok("Employee successfully Added!");
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(_Context.Employees.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var emp = _Context.Employees.Where(employee => employee.EmployeeId == id).FirstOrDefault();
            return Ok(emp);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var emp = _Context.Employees.Where(employee => employee.EmployeeId == id).FirstOrDefault();
            _Context.Employees.Remove(emp);
            _Context.SaveChanges();
            return Ok("Employee successfully deleted!");
        }
    }
}
