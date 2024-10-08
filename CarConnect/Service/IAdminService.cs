using CarConnect.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.doa
{
    internal interface IAdminService
    {
        void GetAdminById(int adminId);
        Admin GetAdminByUsername(string username);

        void RegisterAdmin();
        void UpdateAdmin(Admin adminData);
        void DeleteAdmin(int adminId);
        void UpdateAdmin(int adminIdToUpdate, Admin updatedAdmin);
        
      
    }
}
