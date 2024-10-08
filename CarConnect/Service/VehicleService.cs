
using CarConnect.Model;
using CarConnect.dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarConnect.doa;

namespace CarConnect.Service.VehicleService
{
    internal class VehicleService : IVehicleService
    {
        readonly IVehicleRepository vehicleRepository;
        public VehicleService(utility.DatabaseContext dbContext)
        {
            vehicleRepository = new VehicleRepository();
        }

        public VehicleService()
        {
            vehicleRepository = new VehicleRepository();
        }

        public void GetVehicleById(int id)
        {
            Vehicle vehicle = vehicleRepository.GetVehicleById(id);
            Console.WriteLine(vehicle);
        }

        public void GetAvailableVehicles()
        {
            List<Vehicle> vehicles = vehicleRepository.GetAvailableVehicles();
            foreach (Vehicle vehicle in vehicles)
            {
                Console.WriteLine(vehicle);
            }
        }

        public void AddVehicle(Vehicle vehicle)
        {
            int addStatus = vehicleRepository.AddVehicle(vehicle);
            if (addStatus > 0)
            {
                Console.WriteLine("Vehicle added Successfully");
            }
            else
            {
                Console.WriteLine("Addition Failed");
            }
        }

        public void UpdateVehicle(int id, Vehicle vehicle)
        {
            int Status = vehicleRepository.UpdateVehicle(id, vehicle);
            if (Status > 0)
            {
                Console.WriteLine("Vehicle updated Successfully");
            }
            else
            {
                Console.WriteLine("Updation Failed");
            }
        }

        public void RemoveVehicle(int id)
        {
            int Status = vehicleRepository.RemoveVehicle(id);
            if (Status > 0)
            {
                Console.WriteLine("Vehicle deleted Successfully");
            }
            else
            {
                Console.WriteLine("Deleted Failed");
            }
        }
    }
}
