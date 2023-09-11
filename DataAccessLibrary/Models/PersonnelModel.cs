using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class PersonnelModel
    {
        // Want to be able to display Employer relation to selected Employee OR
        // multiple employees relation to selected Employer
        public List<PersonModel> Employee { get; set; }
        public List<EmployerModel> Employer { get; set; }
    }
}
