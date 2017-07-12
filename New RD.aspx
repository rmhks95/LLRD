 <%@ Page Title="New R&D::" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="New RD.aspx.cs" Inherits="About" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
   <h2> <asp:Image ID="Image2" runat="server" Height="87px" ImageUrl="~/Content/B&amp;W-Logo.jpg" Width="165px" />
    New R&amp;D Part&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Copy_Lab" runat="server" Font-Bold="True" Font-Size="Medium" Text="Copy from part:"></asp:Label>
        <asp:TextBox ID="CNum_Box" runat="server" Height="22px" Width="205px" Font-Size="Small"></asp:TextBox>
&nbsp;<asp:Button ID="Get_but" runat="server" Font-Size="Small" Height="22px" Text="Get" Width="62px" formnovalidate="true" OnClick="Get_but_Click" />
&nbsp;<asp:Button ID="GP_But" runat="server" Font-Size="Small" Height="22px" Text="Get from previous" formnovalidate="true" Width="146px" OnClick="GP_But_Click" />
    </h2>
<html>

<body>
    <asp:Panel runat="server" DefaultButton="Submit_Button">
			<p>
                Initials:<asp:TextBox ID="Initials_Box" runat="server" required="true" ></asp:TextBox>
                <asp:Label ID="Label2" runat="server" ForeColor="Red" Height="25px" Text="*"></asp:Label>
                &nbsp;</p>
            <p>
                Part Number:
                <asp:Label ID="Part_Num_Box" runat="server" Font-Bold="True" Font-Size="Large"></asp:Label>
            </p>
            <p>
                Part Description:<asp:TextBox ID="Desc_Box" runat="server" EnableTheming="True" required="true" ></asp:TextBox>
                <asp:Label ID="Label13" runat="server" ForeColor="Red" Height="25px" Text="*"></asp:Label>
                &nbsp;</p>
            <p>
                 Project:<asp:TextBox ID="Proj_Box" runat="server"></asp:TextBox>
            </p>

            <p>
                Product Line:<asp:DropDownList ID="Prod_Drop" runat="server" EnableViewState="true" OnSelectedIndexChanged="Prod_Drop_Changed" AutoPostBack="true"  >
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>Farm &amp; Ranch</asp:ListItem>
                        <asp:ListItem>Balls</asp:ListItem>
                        <asp:ListItem>Ball Mounts</asp:ListItem>
                        <asp:ListItem>Cab Protector</asp:ListItem>
                        <asp:ListItem>Flat Bed</asp:ListItem>
                        <asp:ListItem>Gooseneck</asp:ListItem>
                        <asp:ListItem>Job Shop</asp:ListItem>
                        <asp:ListItem>Motorcycle Latch</asp:ListItem>
                        <asp:ListItem>RCVR Hitch</asp:ListItem>
                        <asp:ListItem>RV</asp:ListItem>
                        <asp:ListItem>GN Coupler</asp:ListItem>
                        <asp:ListItem>Tow/Stow</asp:ListItem>
                        <asp:ListItem>Bison</asp:ListItem>
                        <asp:ListItem>Other</asp:ListItem>
                    </asp:DropDownList>
                &nbsp;<asp:Label ID="Prod_Line_exp" runat="server" Text="Explain:" Visible="False"></asp:Label>
                <asp:TextBox ID="Prod_Ex_box" runat="server" Visible="False"></asp:TextBox>
            </p>
            <p>
                Department to Charge:<asp:DropDownList ID="Dept_Drop" runat="server"   EnableViewState="true" OnSelectedIndexChanged="Dept_Drop_Changed" AutoPostBack="true"  >
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem>R&amp;D Product Prototypes</asp:ListItem>
                    <asp:ListItem>Engineering, Testing</asp:ListItem>
                    <asp:ListItem>Production Equipment</asp:ListItem>
                    <asp:ListItem>Fixtures, Jigs, Tooling</asp:ListItem>
                    <asp:ListItem>Show Inventory</asp:ListItem>
                    <asp:ListItem>Marketing</asp:ListItem>
                    <asp:ListItem>Maintance</asp:ListItem>
                    <asp:ListItem>Safety</asp:ListItem>
                    <asp:ListItem>Quality</asp:ListItem>
                    <asp:ListItem>Other</asp:ListItem>
                </asp:DropDownList>
                &nbsp;<asp:Label ID="Dept_exp" runat="server" Text="Explain:" Visible="False"></asp:Label>
                <asp:TextBox ID="Dep_Ex_box" runat="server" Visible="False"></asp:TextBox>
            </p>

             Notes:<asp:TextBox ID="Notes_Box" runat="server" Width="373px" Height="42px" TextMode="MultiLine" onkeydown = "return (event.keyCode!=13);" ></asp:TextBox>
    <br />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <br />
    <p>
    <asp:Label ID="Label12" runat="server" ForeColor="Red" Height="25px" Text="*"></asp:Label>
    Required&nbsp;&nbsp;&nbsp;</p>
    <p>
            <asp:Button ID="Submit_Button" runat="server" OnClick="Button1_Click" Text="Submit"/>
	&nbsp;&nbsp;&nbsp;
            <asp:Button ID="SC_But" runat="server" Font-Bold="True" Text="Submit &amp; Copy" OnClientClick ="disable()" onclick="SC_But_Click"/>
	</p>
      </asp:Panel>
 



</body>
</html>



 </asp:Content>


