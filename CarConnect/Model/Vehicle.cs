using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Model
{
    public class Vehicle
    {

        private int vehicleId;
        private string model;
        private string make;
        private int year;
        private string color;
        private string registrationNumber;
        private bool availability;
        private decimal dailyRate;

        // Default constructor
        public Vehicle() { }

        // Parameterized constructor
        public Vehicle(int vehicleID, string model, string make, int year,
                       string color, string registrationNumber, bool availability, decimal dailyRate)
        {
            this.vehicleId = vehicleID;
            this.model = model;
            this.make = make;
            this.year = year;
            this.color = color;
            this.registrationNumber = registrationNumber;
            this.availability = availability;
            this.dailyRate = dailyRate;
        }

        //Getters and Setters
        public int VehicleId { get { return vehicleId; } set { vehicleId = value; } }
        public string Model { get { return model; } set { model = value; } }
        public string Make { get { return make; } set { make = value; } }
        public int Year { get { return year; } set { year = value; } }
        public string Color { get { return color; } set { color = value; } }
        public string RegistrationNumber { get { return registrationNumber; } set { registrationNumber = value; } }
        public bool Availability { get { return availability; } set { availability = value; } }
        public decimal DailyRate { get { return dailyRate; } set { dailyRate = value; } }

        public override string ToString()
        {
            return $"\nVehicle ID: {VehicleId}\nModel: {Model}\nMake: {Make}\nYear: {Year}\nColor: {Color}\nRegistration Mumber: {RegistrationNumber}\nAvailability: {Availability}\nDaily Rate: {DailyRate}\n";
        }
    }
}
