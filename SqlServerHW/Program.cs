using Microsoft.Extensions.Configuration;
using DataAccessLibrary;
using System.Data.SqlTypes;
using DataAccessLibrary.Models;

SqlCrud sql = new SqlCrud(GetConnectionString());

GetEmployees(sql, 2);


//GetPeoplesNames(sql);



static void GetEmployees(SqlCrud sql, int employerId)
{
    PersonnelModel employer = new PersonnelModel();
    employer = sql.GetEmployerInformation(employerId);

    Console.WriteLine("The selected employer is assigned to the following employees:");
    foreach (var employee in employer.Employee)
    {
        Console.WriteLine($"{employee.Id}: {employee.FirstName} {employee.LastName}");
    }
}


static void GetPeoplesNames(SqlCrud sql)
{
    var rows = sql.GetAll();

    foreach ( var row in rows)
    {
        Console.WriteLine($"{row.Id}: {row.FirstName} {row.LastName}");
    }
}


static string GetConnectionString(string connectionString = "Default")
{
    string output = "";

    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("AppSettings.json");

    var config = builder.Build();

    output = config.GetConnectionString(connectionString);

    return output;
}