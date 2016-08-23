using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace Trade_Interchange_Challenge
{
    public class ServiceConnector
    {
        HttpClient client = new HttpClient();

        public ServiceConnector()
        {
            resetClient();
        }

        public List<Employee> getEmployees()
        {
            resetClient();

            using (client)
            {
                HttpResponseMessage response = client.GetAsync("employee").Result;
                if (response.IsSuccessStatusCode)
                {
                    List<Employee> emps = response.Content.ReadAsAsync<List<Employee>>().Result;
                    return emps;
                }

            }

            return null;

        }

        public List<Employee> searchEmployees(string forename, string surname, int departmentId)
        {
            resetClient();

            using (client)
            {
                HttpResponseMessage response = client.GetAsync("employee?forename=" + forename + "&surname=" + surname + "&departmentId=" + departmentId).Result;
                if (response.IsSuccessStatusCode)
                {
                    List<Employee> emps = response.Content.ReadAsAsync<List<Employee>>().Result;
                    return emps;
                }

            }

            return null;

        }

        public int createEmployee(Employee employee)
        {
            HttpResponseMessage response = client.PostAsJsonAsync("employee", employee).Result;
            
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<int>().Result;
            }

            return -1;
        }

        public bool updateEmployee(int id, Employee employee)
        {
            resetClient();

            HttpResponseMessage response = client.PutAsJsonAsync("employee/" + id, employee).Result;

            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<bool>().Result;
            }

            return false;
        }

        public bool deleteEmployee(int id)
        {
            resetClient();

            HttpResponseMessage response = client.DeleteAsync("employee/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<bool>().Result;
            }

            return false;
        }

        private void resetClient()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44304/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

    }
}