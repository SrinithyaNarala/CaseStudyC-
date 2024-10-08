using CarConnect.dao;
using CarConnect.doa;
using CarConnect.Model;
using System;

namespace CarConnect.Service
{
    internal class AdminService : IAdminService
    {
        private readonly IAdminRepository adminRepository;

        public AdminService(IAdminRepository adminRepository)
        {
            this.adminRepository = adminRepository ?? throw new ArgumentNullException(nameof(adminRepository));
        }

        public AdminService()
        {
            adminRepository = new AdminRepository(); // Ensure default constructor initializes the repository
        }

        public void GetAdminById(int id)
        {
            Admin admin = adminRepository.GetAdminById(id);
            if (admin != null)
            {
                Console.WriteLine($"Admin ID: {admin.AdminId}, Name: {admin.FirstName} {admin.LastName}, Role: {admin.Role}");
            }
            else
            {
                Console.WriteLine("Admin not found.");
            }
        }

        public Admin GetAdminByUsername(string username)
        {
            Admin admin = adminRepository.GetAdminByUsername(username);
            if (admin != null)
            {
                Console.WriteLine($"Admin found: {admin.FirstName} {admin.LastName}, Admin ID: {admin.AdminId}, Role: {admin.Role}");
            }
            else
            {
                Console.WriteLine("Admin not found.");
            }
            return admin; // Return the admin found
        }

        public bool RegisterAdmin(Admin admin)
        {
            if (admin == null) throw new ArgumentNullException(nameof(admin));

            int registerStatus = adminRepository.RegisterAdmin(admin);
            return registerStatus > 0; // Returns true if successfully registered
        }

        public bool UpdateAdmin(int id, Admin adminData)
        {
            if (adminData == null) throw new ArgumentNullException(nameof(adminData));

            int updateStatus = adminRepository.UpdateAdmin(id, adminData);
            return updateStatus > 0; // Returns true if successfully updated
        }

        public bool DeleteAdmin(int id)
        {
            int deleteStatus = adminRepository.DeleteAdmin(id);
            return deleteStatus > 0; // Returns true if successfully deleted
        }

        // Implementing the interface method correctly
        Admin IAdminService.GetAdminByUsername(string username)
        {
            return GetAdminByUsername(username); // Calls the implemented method
        }

        // Optional: You can implement a method to register an admin directly
        public void RegisterAdmin()
        {
            // Logic to gather admin details from the user and create a new admin
            Admin admin = new Admin(); // Initialize a new Admin object
            // Gather admin details from user input (or other sources)
            // Example:
            Console.WriteLine("Enter Admin First Name: ");
            admin.FirstName = Console.ReadLine();

            Console.WriteLine("Enter Admin Last Name: ");
            admin.LastName = Console.ReadLine();

            Console.WriteLine("Enter Admin Username: ");
            admin.Username = Console.ReadLine();

            Console.WriteLine("Enter Admin Password: ");
            admin.Password = Console.ReadLine();

            // Call the method to register admin
            if (RegisterAdmin(admin))
            {
                Console.WriteLine("==========Admin added Successfully=========");
            }
            else
            {
                Console.WriteLine("==========Failed to add Admin===========");
            }
        }

        public void UpdateAdmin(Admin adminData)
        {
            throw new NotImplementedException();
        }

        void IAdminService.DeleteAdmin(int adminId)
        {
            throw new NotImplementedException();
        }

        void IAdminService.UpdateAdmin(int adminIdToUpdate, Admin updatedAdmin)
        {
            throw new NotImplementedException();
        }
    }
}
