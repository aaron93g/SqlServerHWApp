using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public class SqlCrud
    {
        private readonly string connection;
        SqlDataAccess db = new SqlDataAccess();

        public SqlCrud(string connection)
        {
            this.connection = connection;
        }


        // CREATE
        public void CreateCompanyEmployee(PersonnelModel personnel)
        {
            string sql = "insert into dbo.Person (FirstName, LastName) values (@FirstName, @LastName);";
            db.SaveData(sql, new {FirstName = personnel.Employee[0].FirstName, LastName = personnel.Employee[0].LastName }, connection);


            sql = "select Id from dbo.Person where FirstName = @FirstName and LastName = @LastName;";
            personnel.Employee[0].Id = db.LoadData<PersonModel, dynamic>(sql, new { personnel.Employee[0].FirstName, personnel.Employee[0].LastName }, connection).First().Id;

            // INSERT NEW EMPLOYEE RELATION TO EMPLOYER
            sql = "insert into dbo.Employer (PersonId, EmployeeId) values (@PersonId, @EmployeeId);";
            db.SaveData(sql, new { PersonId = personnel.Employer[0].Id, EmployeeId = personnel.Employee[0].Id }, connection);


            foreach (var email in personnel.Employee[0].Emails)
            {
                if(email.Id == 0)
                {
                    sql = "insert into dbo.Email (EmailAddress) values (@Email);";
                    db.SaveData(sql, new { Email = email.EmailAddress }, connection);

                    sql = "select Id from dbo.Email where EmailAddress = @Email;";
                    email.Id = db.LoadData<EmailModel, dynamic>(sql, new { Email = email.EmailAddress }, connection).First().Id;
                }

                sql = "insert into dbo.EmailConnection (PersonId, EmailAddressId) values (@EmployeeId, @EmailAddressId);";
                db.SaveData(sql, new {EmployeeId = personnel.Employee[0].Id, EmailAddressId = email.Id }, connection);
            }











        }

        // READ

        public List<PersonModel> GetAll()
        {
            string sqlStatement = "select * from dbo.Person;";
            return db.LoadData<PersonModel, dynamic>(sqlStatement, new { }, connection);
        }

        public PersonnelModel GetEmployeeInformation(int employerId)
        {
            PersonnelModel employer = new PersonnelModel();


            string sql = @"select p.*
                           from dbo.Person p
                           inner join dbo.Employer e on e.EmployeeId = p.Id
                           where e.PersonId = @employerId";

            employer.Employee = db.LoadData<PersonModel, dynamic>(sql, new { employerId }, connection);

            //employer.Employee[0].Id;

            for(int i = 0; i < employer.Employee.Count; i++)
            {
                sql = @"select e.* from dbo.Email e
                           inner join dbo.EmailConnection ec on ec.EmailAddressId = e.Id
                           where ec.PersonId = @employeeId";
                int employeeId = employer.Employee[i].Id;
                employer.Employee[i].Emails = db.LoadData<EmailModel, dynamic>(sql, new { employeeId }, connection);
            }


            return employer;
        }



        // UPDATE

        public EmailModel UpdateEmail(int emailId, string newAddress)
        {
            string sql = "update dbo.Email set EmailAddress = @newAddress where Id = @emailId;";
            db.SaveData(sql, new { newAddress, emailId }, connection);

            sql = "select EmailAddress from dbo.Email where Id = @emailId;";
            EmailModel email = db.LoadData<EmailModel, dynamic>(sql, new { emailId }, connection).First();

            return email;
        }


        // DELETE

        public void DeletePerson(int personId)
        {
            string sql = "delete from dbo.Person where Id = @personId;";
            db.SaveData(sql, new { personId }, connection);

            sql = "delete from dbo.Employer where EmployeeId = @personId;";
            db.SaveData(sql, new { personId }, connection);

            sql = "select Id, PersonId, EmailAddressId from dbo.EmailConnection where PersonId = @personId;";
            var DeletedEmailId = db.LoadData<EmailConnectionModel, dynamic>(sql, new { personId }, connection);

            sql = "delete from dbo.EmailConnection where PersonId = @personId;";
            db.SaveData(sql, new { personId }, connection);



            foreach (var emailId in DeletedEmailId)
            {
                int Id = emailId.EmailAddressId;

                sql = "select * from dbo.EmailConnection where EmailAddressId = @Id;";
                var emailConnections = db.LoadData<EmailConnectionModel, dynamic>(sql, new { Id }, connection);

                if(emailConnections.Count == 0)
                {
                    sql = "delete from dbo.Email where Id = @Id;";
                    db.SaveData(sql, new { Id }, connection);
                }
            }

        }

    }
}
