using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data;
using DomainModel;

namespace Data_Layer
{
    public class DBComms
    {
        public string
            conString = "Server=game-pc\\;Database=TradeInterchange;User Id=GenUser;Password=Password1;";
        SqlConnection con;
        
        public DBComms()
        {
            con = new SqlConnection(conString);
        }

        public List<Employee> getEmployees()
        {
            string sqlstr = "SELECT * FROM Employee";

            var emps = getEmployeeType(sqlstr);

            return emps;
        }

        public List<Employee> getEmployeeByNameAndNum(string forename, string surname, int departmentId)
        {
            SqlCommand searchCMD = new SqlCommand("SearchEmployee", con);

            searchCMD.CommandType = CommandType.StoredProcedure;

            SqlParameter forenameIn = searchCMD.Parameters.Add("@critForename", SqlDbType.NVarChar, 255);
            forenameIn.Direction = ParameterDirection.Input;
            SqlParameter surnameIn = searchCMD.Parameters.Add("@critSurname", SqlDbType.NVarChar, 255);
            surnameIn.Direction = ParameterDirection.Input;
            SqlParameter departmentIdIn = searchCMD.Parameters.Add("@critDepartmentId", SqlDbType.Int);
            departmentIdIn.Direction = ParameterDirection.Input;

            forenameIn.Value = forename;
            surnameIn.Value = surname;
            departmentIdIn.Value = departmentId;

            con.Open();

            SqlDataReader myReader = searchCMD.ExecuteReader();
            List<Employee> emps = new List<Employee>();

            while (myReader.Read())
            {
                Employee tmp = new Employee() {
                    Forename = myReader.GetString(0),
                    Surname = myReader.GetString(1),
                    Number = myReader.GetInt32(2),
                    DateOfBirth = myReader.GetDateTime(3),
                    DepartmentID = myReader.GetInt32(4)
                };

                emps.Add(tmp);
            };

            myReader.Close();

            return emps;
        }

        private List<Employee> getEmployeeType(string sqlStr)
        {
            con.Open();
            SqlDataAdapter sadapter = new SqlDataAdapter();
            sadapter.SelectCommand = new SqlCommand(sqlStr, con);
            DataSet emps = new DataSet();
            sadapter.Fill(emps);
            con.Close();

            var empList = emps.Tables[0].AsEnumerable().Select(dataRow => new Employee
            {
                Forename = dataRow.Field<string>("Forename"),
                DateOfBirth = dataRow.Field<DateTime>("DateOfBirth"),
                Surname = dataRow.Field<string>("Surname"),
                DepartmentID = dataRow.Field<int>("DepartmentId"),
                EmployeeID = dataRow.Field<int>("EmployeeId"),
                Number = dataRow.Field<int>("Number")
            }).ToList();

            return empList;
        }

        public int createEmployee(int number, string forename, string surname, DateTime dob, int departmentId)
        {
            SqlCommand searchCMD = new SqlCommand("AddEmployee", con);

            searchCMD.CommandType = CommandType.StoredProcedure;

            SqlParameter numberIn = searchCMD.Parameters.Add("@number", SqlDbType.Int);
            numberIn.Direction = ParameterDirection.Input;
            SqlParameter forenameIn = searchCMD.Parameters.Add("@forename", SqlDbType.NVarChar, 255);
            forenameIn.Direction = ParameterDirection.Input;
            SqlParameter surnameIn = searchCMD.Parameters.Add("@surname", SqlDbType.NVarChar, 255);
            surnameIn.Direction = ParameterDirection.Input;
            SqlParameter departmentIdIn = searchCMD.Parameters.Add("@departmentId", SqlDbType.Int);
            departmentIdIn.Direction = ParameterDirection.Input;
            SqlParameter dobIn = searchCMD.Parameters.Add("@dateOfBirth", SqlDbType.Date);
            dobIn.Direction = ParameterDirection.Input;

            numberIn.Value = number;
            forenameIn.Value = forename;
            surnameIn.Value = surname;
            departmentIdIn.Value = departmentId;
            dobIn.Value = dob;

            SqlParameter IdOut = searchCMD.Parameters.Add("@returnId", SqlDbType.Int);
            IdOut.Direction = ParameterDirection.Output;

            con.Open();

            searchCMD.ExecuteNonQuery();

            return int.Parse(IdOut.Value.ToString());
        }

        public bool updateEmployee(int employeeId, int number, string forename, string surname, DateTime dob, int departmentId)
        {
            SqlCommand searchCMD = new SqlCommand("UpdateEmployee", con);

            searchCMD.CommandType = CommandType.StoredProcedure;

            SqlParameter idIn = searchCMD.Parameters.Add("@employeeId", SqlDbType.Int);
            idIn.Direction = ParameterDirection.Input;
            SqlParameter numberIn = searchCMD.Parameters.Add("@number", SqlDbType.Int);
            numberIn.Direction = ParameterDirection.Input;
            SqlParameter forenameIn = searchCMD.Parameters.Add("@forename", SqlDbType.NVarChar, 255);
            forenameIn.Direction = ParameterDirection.Input;
            SqlParameter surnameIn = searchCMD.Parameters.Add("@surname", SqlDbType.NVarChar, 255);
            surnameIn.Direction = ParameterDirection.Input;
            SqlParameter departmentIdIn = searchCMD.Parameters.Add("@departmentId", SqlDbType.Int);
            departmentIdIn.Direction = ParameterDirection.Input;
            SqlParameter dobIn = searchCMD.Parameters.Add("@dateOfBirth", SqlDbType.Date);
            dobIn.Direction = ParameterDirection.Input;

            idIn.Value = employeeId;
            numberIn.Value = number;
            forenameIn.Value = forename;
            surnameIn.Value = surname;
            departmentIdIn.Value = departmentId;
            dobIn.Value = dob;

            con.Open();

            int affected = searchCMD.ExecuteNonQuery();

            con.Close();

            return affected != 0;
        }

        public bool deleteEmployee(int employeeId)
        {
            SqlCommand searchCMD = new SqlCommand("DeleteEmployee", con);

            searchCMD.CommandType = CommandType.StoredProcedure;

            SqlParameter idIn = searchCMD.Parameters.Add("@employeeId", SqlDbType.Int);
            idIn.Direction = ParameterDirection.Input;

            idIn.Value = employeeId;

            con.Open();

            int affected = searchCMD.ExecuteNonQuery();

            con.Close();

            return affected != 0;
        }

    }
}