using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Trade_Interchange_Challenge
{
    public partial class EmployeeView : System.Web.UI.Page
    {
        ServiceConnector sc = new ServiceConnector();

        protected void Page_Load(object sender, EventArgs e)
        {
            var emps = sc.getEmployees();
            ListView1.DataSource = emps;
            ListView1.DataBind();
        }

        protected void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ListView1_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            Employee emp = new Employee()
            {
                EmployeeID = int.Parse(e.NewValues["EmployeeId"].ToString()),
                Forename = e.NewValues["Forename"].ToString(),
                Surname = e.NewValues["Surname"].ToString()
            };
            var res = sc.updateEmployee(emp.EmployeeID, emp);

        }

        protected void ListView1_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            ListView1.EditIndex = e.NewEditIndex;
            ListView1.DataSource = sc.getEmployees();
            ListView1.DataBind();
        }

    }
}