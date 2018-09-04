using System.Collections.Generic;
using System.Web.Http;
using DapperAPI.DAL;
using DapperAPI.Models;

namespace DapperAPI.Controllers
{
    /*
     *
     * Dapper is meant to be a wrapper for ADO.NET.
     */
    public class CustomerController : ApiController
    {
        private CustomerRespository _customersRepo = new CustomerRespository();

        // GET: api/Customer
        
        [Route("Customers/{amount}/{sort}")]
        [HttpGet]
        public IEnumerable<Customer> Get(int amount, string sort)
        {
            return _customersRepo.GetCustomers(amount, sort);
        }

        [Route("Customers/{id}")]
        [HttpGet]
        // GET: api/Customer/5
        public Customer Get(int id)
        {
            return _customersRepo.GetSingleCustomer(id);
        }

        // POST: api/Customer
        [Route("Customers")]
        [HttpPost]
        public bool Post([FromBody]Customer ourCustomer)
        {
            return _customersRepo.InsertCustomer(ourCustomer);
        }

        [Route("Customers")]
        [HttpPut]
        // PUT: api/Customer/5
        public bool Put([FromBody]Customer customer)
        {
            return _customersRepo.UpdateCustomer(customer);
        }

        [Route("Customers/{id}")]
        [HttpDelete]
        // DELETE: api/Customer/5
        public void Delete(int id)
        {
            _customersRepo.DeleteCustomer(id);
        }
    }
}
