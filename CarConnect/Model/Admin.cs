using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Model
{
    internal class Admin
    {
        private int adminId;
        private string firstName;
        private string lastName;
        private string email;
        private string phoneNumber;
        private string username;
        private string password;
        private string role;
        private DateTime joinDate;

        // Default constructor
        public Admin() { }

        // Parameterized constructor
        public Admin(int adminId, string firstName, string lastName, string email,
                     string phoneNumber, string username, string password, string role, DateTime joinDate)
        {
            this.adminId = adminId;
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.phoneNumber = phoneNumber;
            this.username = username;
            this.password = password;
            this.role = role;
            this.joinDate = joinDate;
        }

        //Getters and Setters
        public int AdminId { get { return adminId; } set { adminId = value; } }
        public string FirstName { get { return firstName; } set { firstName = value; } }
        public string LastName { get { return lastName; } set { lastName = value; } }
        public string Email { get { return email; } set { email = value; } }
        public string PhoneNumber { get { return phoneNumber; } set { phoneNumber = value; } }
        public string Username { get { return username; } set { username = value; } }
        public string Password { get { return password; } set { password = value; } }
        public string Role { get { return role; } set { role = value; } }
        public DateTime JoinDate { get { return joinDate; } set { joinDate = value; } }

        public override string ToString()
        {
            return $"Admin ID: {AdminId},\nName: {FirstName} {LastName},\nEmail: {Email},\nPhone: {PhoneNumber},\nUsername: {Username},\nRole: {Role},\nJoined: {JoinDate.ToShortDateString()}";
        }

        internal bool Authenticate(string? password)
        {
            throw new NotImplementedException();
        }
    }
}
