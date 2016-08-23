using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace Business_Layer
{
    public class ServiceConnector
    {
        HttpClient client = new HttpClient();

        public ServiceConnector()
        {
            client.BaseAddress = new Uri("https://localhost:44302/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Employee>> getEmployees()
        {
            using (client)
            {
                HttpResponseMessage response = await client.GetAsync("employee");
                if (response.IsSuccessStatusCode)
                {
                    List<Employee> emps = await response.Content.ReadAsAsync<List<Employee>>();
                    return emps;
                }

            }

            return null;

        }

        public async Task<List<Employee>> searchEmployees(string forename, string surname, int departmentId)
        {
            using (client)
            {
                HttpResponseMessage response = await client.GetAsync("employee?forename=" + forename + "&surname=" + surname + "&departmentId=" + departmentId);
                if (response.IsSuccessStatusCode)
                {
                    List<Employee> emps = await response.Content.ReadAsAsync<List<Employee>>();
                    return emps;
                }

            }

            return null;

        }

        public async Task<int> createEmployee(Employee employee)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("employee", employee);
            
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<int>();
            }

            return -1;
        }

        public async Task<bool> updateEmployee(int id, Employee employee)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync("employee/" + id, employee);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<bool>();
            }

            return false;
        }

        public async Task<bool> deleteEmployee(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync("employee/" + id);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<bool>();
            }

            return false;
        }

    }
}