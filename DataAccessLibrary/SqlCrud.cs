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


        public List<PersonModel> GetAll()
        {
            string sqlStatement = "select * from dbo.Person;";
            return db.LoadData<PersonModel, dynamic>(sqlStatement, new { }, connection);
        }

        public PersonnelModel GetEmployerInformation(int employerId)
        {
            PersonnelModel employer = new PersonnelModel();


            string sql = @"select p.*
                           from dbo.Person p
                           inner join dbo.Employer e on e.EmployeeId = p.Id
                           where e.PersonId = @employerId";

            employer.Employee = db.LoadData<PersonModel, dynamic>(sql, new { employerId }, connection);

            return employer;
        }


    }
}
