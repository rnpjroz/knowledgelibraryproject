<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <h1>Sample Site</h1>
      <asp:GridView ID="gvRecords" runat="server" AllowPaging="true"
        DataKeyNames="Id" AutoGenerateColumns="False"
        OnRowCommand="gvRecords_RowCommand"
        OnRowDeleting="gvRecords_RowDeleting">
        <Columns>
          <asp:CommandField ButtonType="Link" 
            ItemStyle-HorizontalAlign = "Center" ItemStyle-VerticalAlign="Middle"
            ShowSelectButton="true"/>

          <asp:TemplateField ItemStyle-HorizontalAlign="Center" 
            ItemStyle-VerticalAlign="Middle">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkDelete" Text="Delete" CommandName="Delete" runat="server" 
                      CommandArgument='<%#Container.DataItemIndex%>'/>                                        
                </ItemTemplate>
          </asp:TemplateField> 

          <asp:BoundField DataField="Name" HeaderText="Name"/>                      
          <asp:BoundField DataField="URL" HeaderText="URL" />          
        </Columns>        
        <EmptyDataTemplate>
          No Data
        </EmptyDataTemplate>
      </asp:GridView>

      <br />
      Development Type: <asp:DropDownList ID="ddlDevelopmentType" runat="server" DataTextField="Name" DataValueField="Id">
      </asp:DropDownList>
      <br />
      Name:
      <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
      <asp:HiddenField ID="hdnIdToDelete" runat="server" />
      <br />
      URL:
      <asp:TextBox ID="txtURL" runat="server" Width="469px"></asp:TextBox>
      <br />
      <br />
    
      <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="SAVE" />
    
    </div>
    </form>
</body>
</html>
