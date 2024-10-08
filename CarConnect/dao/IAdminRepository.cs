using CarConnect.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.dao
{
    internal interface IAdminRepository
    {
        List<Admin> GetAllAdmins();
        Admin GetAdminById(int adminId);
        Admin GetAdminByUsername(string username);
        int RegisterAdmin(Admin adminData);
        int UpdateAdmin(int id, Admin adminData);
        int DeleteAdmin(int adminId);
        Admin GetAdminByUsername(object username);
    }
}
