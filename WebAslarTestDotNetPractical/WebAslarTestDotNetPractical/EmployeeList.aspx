<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeList.aspx.cs" Inherits="WebAslarTestDotNetPractical.EmployeeList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <br />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Export Data" Font-Names="Arial" />
            <br />
            <br />
            <br />
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Height="185px" Width="1705px" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" GridLines="Vertical" ForeColor="Black">
                <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField HeaderText="Employee ID">
                    <ItemTemplate>
                       <asp:TextBox ID="IdText" runat="server" Text='<%# Eval("Id")%>' ReadOnly="true"></asp:TextBox> 
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Employee Name">
                    <ItemTemplate>
                        <asp:TextBox ID="NameText" runat="server" Text='<%# Eval("Name")%>' ReadOnly="true"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Joining Date">
                    <ItemTemplate>
                       <asp:TextBox ID="JdateText" runat="server" Text='<%# Eval("JDate")%>' ReadOnly="true"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date Of Birth">
                    <ItemTemplate>
                       <asp:TextBox ID="DOBText" runat="server" Text='<%# Eval("DOB")%>' ReadOnly="true"></asp:TextBox> 
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Skills">
                    <ItemTemplate>
                        <asp:TextBox ID="SkillsText" runat="server" Text='<%# Eval("Skills")%>' ReadOnly="true"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Salary">
                    <ItemTemplate>
                        <asp:TextBox ID="SalaryText" runat="server" Text='<%# Eval("Salary")%>' ReadOnly="true"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Designation">
                    <ItemTemplate>                       
                        <asp:DropDownList ID="DesignationText" runat="server" Text='<%# Eval("Designation") %>' Enabled="false">
                    <asp:ListItem Text="Manager" Value="Manager"></asp:ListItem>
                    <asp:ListItem Text="Developer" Value="Developer"></asp:ListItem>
                    <asp:ListItem Text="Tester" Value="Tester"></asp:ListItem>
                    <asp:ListItem Text="Sr. Developer" Value="Sr. Developer"></asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Email">
                    <ItemTemplate>
                       <asp:TextBox ID="EmailText" runat="server" Text='<%# Eval("Email")%>' ReadOnly="true"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:LinkButton style="text-decoration:none;" ID="edit_link" runat="server" OnClick="edit_Employee">Edit</asp:LinkButton>
                        <asp:LinkButton style="text-decoration:none;" ID="update_link" runat="server" OnClick="update_Employee">Update</asp:LinkButton>
                        <asp:LinkButton style="text-decoration:none;" ID="del_link" runat="server" OnClick="del_Employee">Delete</asp:LinkButton>
                    </ItemTemplate>                   
                </asp:TemplateField>
            </Columns>
                <FooterStyle BackColor="#CCCC99" />
                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                <RowStyle BackColor="#F7F7DE" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#FBFBF2" />
                <SortedAscendingHeaderStyle BackColor="#848384" />
                <SortedDescendingCellStyle BackColor="#EAEAD3" />
                <SortedDescendingHeaderStyle BackColor="#575357" />
        </asp:GridView>
            <br />
            <br />
            <br />
            <br />
            &nbsp;Want to Import Data to Db?<br />
            <br />
&nbsp;Browse the file and click Import Data.<br />
            <br />
            <br />
            <br />
            <asp:FileUpload ID="FileUpload1" runat="server" />
&nbsp;<asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Import Data" />
        </div>
    </form>
</body>
</html>
