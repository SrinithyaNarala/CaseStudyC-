using NUnit.Framework;
using System.Security.Authentication;
using CarConnect.MainModule;
using CarConnect.Service;
using CarConnect.dao;
using CarConnect.Model;



namespace CarConnect.Tests
{
    public class Tests
    {
        private CarConnectMenu carConnectMenu;
       // private VehicleService vehicleService;
        private VehicleRepository vehicleRepository;

        [SetUp]
        public void Setup()
        {
            carConnectMenu = new CarConnectMenu();
           // vehicleService = new VehicleService();
            vehicleRepository = new VehicleRepository();
        }

        [Test]
        public void AuthenticateCustomer_Invalid_ThrowsAuthenticationException()
        {
            // Arrange
            string invalidUsername = "invalidUser";
            string password = "password";

            // Act & Assert
            var ex = Assert.Throws<AuthenticationException>(() => carConnectMenu.AuthenticateCustomer(invalidUsername, password));
            Assert.That(ex.Message, Is.EqualTo("Invalid credentials."));
        }

        //[Test]
        //public void UpdateCustomerInfo_ValidCustomer_ShouldUpdateSuccessfully()
        //{
        //    // Arrange
        //    var customerService = new CustomerService();
        //    var customer = new Customer
        //    {
        //        CustomerId = 1,
        //        Username = "John Doe",
        //        Email = "john@example.com",
        //        PhoneNumber = "1234567890"
        //    };
        //    customerService.AddCustomer(customer);

        //    // Act
        //    customer.Email = "newemail@example.com"; // Updating email
        //    bool updateResult = customerService.UpdateCustomer(customer);

        //    // Assert
        //    Assert.IsTrue(updateResult, "Customer information should be updated successfully.");
        //}




        //[Test]
        //public void AddNewVehicle_ValidVehicle_ShouldBeAddedSuccessfully()
        //{
        //    // Arrange
        //    var vehicleService = new VehicleService();
        //    var vehicle = new Vehicle
        //    {
        //        VehicleId = 101,
        //        Model = "Toyota Corolla",
        //        RegistrationNumber = "XYZ123",
        //        Availability = true
        //    };

        //    // Act
        //    bool addResult = vehicleService.AddVehicle(vehicle);

        //    // Assert
        //    Assert.That(addResult, Is.True, "The vehicle should be added successfully.");
        //}




        [Test]
        public void UpdateVehicle_ValidId_ShouldUpdateVehicleDetails()
        {
            // Arrange
            int vehicleId = 7;

            Vehicle updatedVehicle = new Vehicle
            {
                VehicleId = vehicleId,
                Model = "Honda",
                Make = "Accord",
                Year = 2023,
                Color = "Red",
                RegistrationNumber = "HND1234",
                Availability = false,
                DailyRate = 70.00m
            };


            // Act: Update the vehicle
            int updateStatus = vehicleRepository.UpdateVehicle(vehicleId, updatedVehicle);

            // Act: Retrieve the updated vehicle
            Vehicle latestVehicle = vehicleRepository.GetVehicleById(vehicleId);

            // Assert: Verify that the updated vehicle matches the new details
            Assert.That(latestVehicle.Model, Is.EqualTo(updatedVehicle.Model), "Model should match");
            Assert.That(latestVehicle.Make, Is.EqualTo(updatedVehicle.Make), "Make should match");
            Assert.That(latestVehicle.Year, Is.EqualTo(updatedVehicle.Year), "Year should match");
            Assert.That(latestVehicle.Color, Is.EqualTo(updatedVehicle.Color), "Color should match");
            Assert.That(latestVehicle.RegistrationNumber, Is.EqualTo(updatedVehicle.RegistrationNumber), "Registration Number should match");
            Assert.That(latestVehicle.Availability, Is.EqualTo(updatedVehicle.Availability), "Availability should match");
            Assert.That(latestVehicle.DailyRate, Is.EqualTo(updatedVehicle.DailyRate), "Daily Rate should match");
        }


        [Test]
        public void GetAvailableVehicles_ShouldReturnListOfAvailableVehicles()
        {
            // Act: Retrieve available vehicles from the database
            List<Vehicle> availableVehicles = vehicleRepository.GetAvailableVehicles();

            // Assert: Verify that the available vehicles list is not null and contains items
            Assert.That(availableVehicles, Is.Not.Null, "Available vehicles list should not be null");
            Assert.That(availableVehicles, Is.Not.Empty, "Available vehicles list should not be empty");

            foreach (var vehicle in availableVehicles)
            {
                Assert.That(vehicle.Availability, Is.True, "Vehicle should be available");
            }


        }


        [Test]
        public void GetAllVehicles_ShouldReturnListOfVehicles()
        {
            // Act: Retrieve all vehicles from the database
            List<Vehicle> vehicles = vehicleRepository.GetAllVehicles();

            // Assert: Verify that the vehicles list is not null and contains items
            Assert.That(vehicles, Is.Not.Null, "Vehicle list should not be null");
            Assert.That(vehicles, Is.Not.Empty, "Vehicle list should not be empty");

        }

    }
}