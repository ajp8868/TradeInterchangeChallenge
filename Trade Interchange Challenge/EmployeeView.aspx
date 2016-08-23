<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmployeeView.aspx.cs" Inherits="Trade_Interchange_Challenge.EmployeeView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ListView ID="ListView1" runat="server" OnSelectedIndexChanged="ListView1_SelectedIndexChanged" OnItemEditing="ListView1_ItemEditing" OnItemUpdating="ListView1_ItemUpdating">
        <LayoutTemplate>
          <div id="employees">
            <div runat="server" id="itemPlaceholder" />
          </div>
          <asp:DataPager runat="server" ID="ContactsDataPager" PageSize="12">
            <Fields>
              <asp:NextPreviousPagerField ShowFirstPageButton="true" ShowLastPageButton="true"
                FirstPageText="|&lt;&lt; " LastPageText=" &gt;&gt;|"
                NextPageText=" &gt; " PreviousPageText=" &lt; " />
            </Fields>
          </asp:DataPager>
        </LayoutTemplate>
        <ItemTemplate>
          <div class="employee" runat="server">
              <asp:Label CssClass="namelbl" runat="Server" Text='<%# String.Format("{0} {1}", Eval("Forename"), Eval("Surname")) %>' />
              <asp:Label CssClass="numberlbl" runat="Server" Text='<%# "Employee " + Eval("Number") %>' />
              <asp:Label CssClass="doblbl" runat="Server" Text='<%# String.Format("{0:d MMM yyyy}", Eval("DateOfBirth")) + " - " + (DateTime.Today.Year - (DateTime.Parse(Eval("DateOfBirth").ToString()).Year))  %>' />
              <asp:Label CssClass="departmentlbl" runat="Server" Text='<%#"Dept. " + Eval("DepartmentId") %>' />
              <div class="employeeBtn">
              <asp:LinkButton class="EditButton" runat="Server" Text="Edit" CommandName="Edit" CommandArgument='<%# Eval("EmployeeId") %>' />
            </div>
          </div>
        </ItemTemplate>
        <EditItemTemplate>
          <tr style="background-color: #ADD8E6">
            <td>
              <asp:LinkButton ID="UpdateButton" runat="server" CommandName="Update" Text="Update" />&nbsp;
              <asp:LinkButton ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" />
            </td>
            <td>
              <asp:TextBox ID="FirstNameTextBox" runat="server" Text='<%#Bind("Forename") %>' 
                MaxLength="50" /><br />
                <asp:TextBox ReadOnly="true" style="display: none;" ID="TextBox1" runat="server" Text='<%#Bind("EmployeeId") %>' 
                MaxLength="0" />
            </td>
            <td>
              <asp:TextBox ID="LastNameTextBox" runat="server" Text='<%#Bind("Surname") %>' 
                MaxLength="50" /><br />
            </td>
          </tr>
        </EditItemTemplate>
    </asp:ListView>
</asp:Content>
