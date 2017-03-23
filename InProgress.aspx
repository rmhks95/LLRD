<%@ Page Title="In Progress::" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="InProgress.aspx.cs" Inherits="Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">  
    <script src="Scripts/sorttable.js"></script>
    <meta http-equiv="refresh" content="60" />
   <h1> <asp:Image ID="Image2" runat="server" Height="87px" ImageUrl="~/Content/B&amp;W-Logo.jpg" Width="165px" />
    In Progress</h1>
    <br />

    <asp:GridView ID="Grid"  runat="server" class="sortable" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3">
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <HeaderStyle BackColor="#015F9F" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#0000A9" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#000065" />
    </asp:GridView>
  
    <asp:Label ID="NoneLable" runat="server" Font-Bold="True" Font-Italic="False" Font-Size="Large" Font-Underline="True" Text="No Parts Are In Process"></asp:Label>
  
<br />
  
<script type="text/javascript">  
// for check all checkbox  
        function CheckAll(Checkbox) {  
            var GridVwHeaderCheckbox = document.getElementById("<%=Grid.ClientID %>");  
            for (i = 1; i < GridVwHeaderCheckbox.rows.length; i++) {  
                GridVwHeaderCheckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;  
            }  
        }  
    </script>  
    
</asp:Content>
      


