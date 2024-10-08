using CarConnect.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.dao
{
    public interface IVehicleRepository
    {
        Vehicle GetVehicleById(int vehicleId);
        List<Vehicle> GetAvailableVehicles();
        int AddVehicle(Vehicle vehicle);
        int UpdateVehicle(int id, Vehicle vehicle);
        int RemoveVehicle(int id);
    }
}
