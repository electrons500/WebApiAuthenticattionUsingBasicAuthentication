using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi_Authenticattion_Using_BasicAuthentication.Model;

namespace WebApi_Authenticattion_Using_BasicAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
       List<EmployeeModel> employee = new();

        [HttpPost]
        public ActionResult Post(EmployeeModel model)
        {
            employee.Add(model);

            return Ok("Employee successfully Added!");
        }

        [HttpGet] 
        public ActionResult Get()
        {
           return Ok(employee);
        }

        [HttpGet]
        public ActionResult Get(int id)
        {
            var emp = employee.Where(employee => employee.Id == id).FirstOrDefault();
            return Ok(emp);
        }

        [HttpDelete] 
        public ActionResult Delete(int id)
        {
            var emp = employee.Where(employee => employee.Id == id).FirstOrDefault();
            employee.Remove(emp);
            return Ok("Employee successfully deleted!");
        }
    }
}
