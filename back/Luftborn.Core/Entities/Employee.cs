using Luftborn.Core.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Luftborn.Core.Entities
{
    public class Employee : EntityBase
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string EmailAddress { get; private set; }
        public decimal Salary { get; private set; }

        protected Employee()
        {

        }

        public Employee(string firstName, string lastName, string email, decimal salary)
        {
            SetValues(firstName, lastName, email, salary);
        }

        public void Update(string firstName, string lastName, string email, decimal salary)
        {
            SetValues(firstName, lastName, email, salary);
        }

        private void SetValues(string firstName, string lastName, string email, decimal salary)
        {
            FirstName = firstName;
            EmailAddress = email;
            Salary = salary;
            LastName = lastName;
        } 
    }
}
