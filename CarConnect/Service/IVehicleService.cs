using CarConnect.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.doa
{
    public interface IVehicleService
    {
        void GetVehicleById(int vehicleID);
        void GetAvailableVehicles();
        void AddVehicle(Vehicle vehicleData);
        void UpdateVehicle(int vehicleID,Vehicle vehicleData);
        void RemoveVehicle(int vehicleID);
    }
}
