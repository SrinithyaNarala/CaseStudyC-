using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Model
{
    public class Customer
    {
        private int customerId;
        private string firstName;
        private string lastName;
        private string email;
        private string phoneNumber;
        private string address;
        private string username;
        private string password;
        private DateTime registrationDate;

        // Default constructor
        public Customer() { }

        //Parameterized Constructor
        public Customer(int customerID, string firstName, string lastName, string email,
                        string phoneNumber, string address, string username, string password, DateTime registrationDate)
        {
            this.customerId = customerID;
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.phoneNumber = phoneNumber;
            this.address = address;
            this.username = username;
            this.password = password;
            this.registrationDate = registrationDate;
        }

        //Getters and Setters
        public int CustomerId { get { return customerId; } set { customerId = value; } }
        public string FirstName { get { return firstName; } set { firstName = value; } }
        public string LastName { get { return lastName; } set { lastName = value; } }
        public string Email { get { return email; } set { email = value; } }
        public string PhoneNumber { get { return phoneNumber; } set { phoneNumber = value; } }
        public string Address { get { return address; } set { address = value; } }
        public string Username { get { return username; } set { username = value; } }
        public string Password { get { return password; } set { password = value; } }
        public DateTime RegistrationDate { get { return registrationDate; } set { registrationDate = value; } }

        public override string ToString()
        {
            return $"\nCustomer ID: {CustomerId}\nCustomer Name: {FirstName} {LastName}\nEmail: {Email}\nPhone Number: {PhoneNumber}\nAddress: {Address}\nUsername: {Username}\nPassword: {Password}\nRegistration Date: {RegistrationDate}";
        }

        public bool Authenticate(string password)
        {
            return Password == password;
        }
    }
}
