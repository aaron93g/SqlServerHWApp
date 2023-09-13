using Microsoft.Extensions.Configuration;
using DataAccessLibrary;
using System.Data.SqlTypes;
using DataAccessLibrary.Models;

SqlCrud sql = new SqlCrud(GetConnectionString());



#region CreatePersonExample
PersonnelModel person = new PersonnelModel();
EmployerModel employer = new EmployerModel();
PersonModel employee = new PersonModel();

EmailModel email1 = new EmailModel();
EmailModel email2 = new EmailModel();


employee.FirstName = "Andrew"; employee.LastName = "Gar";

email1.Id = 1002; email1.EmailAddress = "aaron@outlook.com";
employee.Emails.Add(email1);
email2.EmailAddress = "Andrew@outlook.com";
employee.Emails.Add(email2);

employer.Id = 2;

person.Employee.Add(employee);
person.Employer.Add(employer);
#endregion

//CreateAnEmployee(sql, person);



//GetEmployees(sql, 2);
//GetPeoplesNames(sql);

//UpdateEmailAddress(sql, 2, "BigBoss@Boss.Mail");

DeletePerson(sql,1);


// CREATE
static void CreateAnEmployee(SqlCrud sql, PersonnelModel model)
{
    sql.CreateCompanyEmployee(model);
}


// READ
static void GetEmployees(SqlCrud sql, int employerId)
{
    PersonnelModel employer = new PersonnelModel();
    employer = sql.GetEmployeeInformation(employerId);
    
    Console.WriteLine("The selected employer is assigned to the following employees:");
    foreach (var employee in employer.Employee)
    {
        
        Console.WriteLine($"{employee.Id}: {employee.FirstName} {employee.LastName}");
        
        foreach(var email in employee.Emails)
        {
            Console.WriteLine($" Email: {email.EmailAddress}");
        }
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


// UPDATE
static void UpdateEmailAddress(SqlCrud sql, int emailId, string newEmail)
{
    EmailModel email = sql.UpdateEmail(emailId, newEmail);
    Console.WriteLine($"The previous email has been changed to {email.EmailAddress}");
}


// DELETE
static void DeletePerson(SqlCrud sql, int personId)
{
    sql.DeletePerson(personId);
    Console.WriteLine( "process complete");
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