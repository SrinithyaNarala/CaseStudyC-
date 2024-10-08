using CarConnect.Model;
using CarConnect.dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarConnect.doa;

namespace CarConnect.Service
{
    internal class CustomerService : ICustomerService
    {
        readonly ICustomerRepository customerRepository;
        public CustomerService(utility.DatabaseContext dbContext)
        {
            customerRepository = new CustomerRepository();
        }

        public CustomerService()
        {
            customerRepository = new CustomerRepository();
        }

        public Customer GetCustomerById(int id)
        {
            Customer customer = customerRepository.GetCustomerById(id);
            return customer;
        }

        public Customer GetCustomerByUsername(string name)
        {
            Customer customer = customerRepository.GetCustomerByUsername(name);
            return customer;
        }

        public void RegisterCustomer(Customer customer)
        {
            int registerStatus = customerRepository.RegisterCustomer(customer);
            if (registerStatus > 0)
            {
                Console.WriteLine("=====Customer added Successfully=====");
            }
            else
            {
                Console.WriteLine("=====Registration Failed=====");
            }
        }

        public void UpdateCustomer(int id, Customer customer)
        {
            int registerStatus = customerRepository.UpdateCustomer(id, customer);
            if (registerStatus > 0)
            {
                Console.WriteLine("=====Customer updated Successfully=====");
            }
            else
            {
                Console.WriteLine("======Updation Failed=======");
            }
        }

        public void DeleteCustomer(int id)
        {
            int registerStatus = customerRepository.DeleteCustomer(id);
            if (registerStatus > 0)
            {
                Console.WriteLine("=====Customer deleted Successfully======");
            }
            else
            {
                Console.WriteLine("========Deletion Failed=======");
            }
        }

        public bool UpdateCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
