using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class JsonRepository
    {
        private readonly string _filePath;
        private List<Customer> _customers;

        public JsonRepository(string filePath)
        {
            _filePath = filePath;
            LoadData();
        }

        private void LoadData()
        {
            if (File.Exists(_filePath))
            {
                var json = File.ReadAllText(_filePath);
                _customers = JsonConvert.DeserializeObject<List<Customer>>(json) ?? new List<Customer>();
            }
            else
            {
                _customers = new List<Customer>();
            }
        }

        private void SaveData()
        {
            var json = JsonConvert.SerializeObject(_customers, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }

        public List<Customer> GetAllCustomers()
        {
            return _customers.OrderBy(c => c.FullName).ToList();
        }

        public Customer GetCustomerById(int id)
        {
            return _customers.FirstOrDefault(c => c.Id == id);
        }

        public void AddCustomer(Customer customer)
        {
            customer.Id = _customers.Any() ? _customers.Max(c => c.Id) + 1 : 1;
            _customers.Add(customer);
            SaveData();
        }

        public void UpdateCustomer(Customer customer)
        {
            var existingCustomer = GetCustomerById(customer.Id);
            if (existingCustomer != null)
            {
                existingCustomer.FullName = customer.FullName;
                existingCustomer.Address = customer.Address;
                existingCustomer.PhoneNumber = customer.PhoneNumber;
                existingCustomer.ContractNumber = customer.ContractNumber;
                existingCustomer.ContractDate = customer.ContractDate;
                existingCustomer.ServiceType = customer.ServiceType;
                existingCustomer.MonthlyPayment = customer.MonthlyPayment;
                existingCustomer.IsActive = customer.IsActive;
                SaveData();
            }
        }

        public void DeleteCustomer(int id)
        {
            var customer = GetCustomerById(id);
            if (customer != null)
            {
                _customers.Remove(customer);
                SaveData();
            }
        }
    }
}
