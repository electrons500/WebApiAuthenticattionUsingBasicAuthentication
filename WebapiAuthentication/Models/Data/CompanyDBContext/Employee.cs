using System;
using System.Collections.Generic;

namespace WebapiAuthentication.Models.Data.CompanyDBContext;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string EmployeeName { get; set; } = null!;

    public string EmployeeSalary { get; set; } = null!;

    public int EmployeeAge { get; set; }
}
