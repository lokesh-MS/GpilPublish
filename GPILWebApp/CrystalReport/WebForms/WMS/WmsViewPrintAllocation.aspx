<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="WmsViewPrintAllocation.aspx.cs" Inherits="GPILWebApp.CrystalReport.WebForms.WMS.WmsViewPrintAllocation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="header">
        <h2 style="text-align: center; font-weight: bold; font-family: 'Times New Roman'; color: #438EB9">VIEW PRINT ALLOCATION</h2>
    </div>

    <div class="row">

        <div class="col-sm-4">
            <div class="form-sm-3-md-4">
                <label class="control-label">LOCATION CODE</label>
                <div>
                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlLocationCode" AutoPostBack="true" OnTextChanged="ddlLocationCode_TextChanged">
                        <asp:ListItem Value="0">SELECT OCATION CODE</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>

        <div class="col-sm-4">
            <div class="form-sm-3-md-4">
                <label class="control-label">DATE OF PACKING</label>
                <div>
                    <asp:TextBox ID="txtDateOfPacking" runat="server" class="form-control" TextMode="Date">                            
                    </asp:TextBox>
                </div>
            </div>
        </div>

    </div>

    <div class="row">
        <div class="col-sm-4">
            <div class="form-sm-3-md-4">
                <asp:Label ID="lblLocationCode" runat="server"
                    ForeColor="Red" Style="font-weight: 700"></asp:Label>
            </div>
        </div>


        <div id="Div2"  style="background-color:White; width:100%; height:380px; overflow: auto" align="center">
            </div>
    </div>



    <div class="align:center">
        <asp:GridView ID="GridViewSample" runat="server" AutoGenerateColumns="False" BorderColor="#0a0a0a" BorderStyle="Solid" BorderWidth="1px" 
         OnRowCommand="GridViewSample_RowCommand"   Font-Names="Verdana" Width="75%" ForeColor="Black">
            <AlternatingRowStyle BackColor="#2a87de"  />
            <FooterStyle BorderColor="#0a0a0a" BorderStyle="Solid" BorderWidth="1px" />
            <PagerStyle BorderColor="#0a0a0a" BorderStyle="Solid" BorderWidth="1px" />
            <HeaderStyle BackColor="#2a87de" BorderColor="#0a0a0a" BorderStyle="Solid"
                BorderWidth="1px" Font-Size="15px" Height="30px" />
            <RowStyle BorderColor="#0a0a0a" BorderStyle="Solid" BorderWidth="1px"
                Font-Size="13px" Height="20px" />
            <Columns>

                <asp:TemplateField HeaderText="ID">
                    <ItemTemplate>
                        <asp:Label ID="lblID" runat="server" Text='<%#Eval("ID") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="10%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Crop Year Name">
                    <ItemTemplate>
                        <asp:Label ID="lblCropYearName" runat="server" Text='<%#Eval("CropYearName") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="10%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PM Run No">
                    <ItemTemplate>
                        <asp:Label ID="lblPMRunNo" runat="server" Text='<%#Eval("PMRunNo") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="10%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Grade Code">
                    <ItemTemplate>
                        <asp:Label ID="lblGradeCode" runat="server" Text='<%#Eval("GradeCode") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="20%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Case No. From">
                    <ItemTemplate>
                        <asp:Label ID="lblCaseNoFrom" runat="server" Text='<%#Eval("CaseNoFrom") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="10%" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Case No. To">
                    <ItemTemplate>
                        <asp:Label ID="lblCaseNoTo" runat="server" Text='<%#Eval("CaseNoTo") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="10%" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Cases To Print">
                    <ItemTemplate>
                        <asp:Label ID="lblCasesToPrint" runat="server" Text='<%#Eval("CasesToPrint") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="10%" />
                </asp:TemplateField>


                <asp:TemplateField HeaderText="Existing Stock">

                    <ItemTemplate>
                        <asp:Label ID="lblIsExistingStock" runat="server" Text='<%#Eval("IsExistingStock") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="20%" />
                </asp:TemplateField>


                <asp:TemplateField HeaderStyle-Width="15%" HeaderText="Print">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnPrint" runat="server" CommandName="Print" Text="Print" />

                    </ItemTemplate>


                    <HeaderStyle Width="15%" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>


        <div>
            <asp:Label ID="lblMessage" ForeColor="Red" BackColor="Yellow" Font-Size="Large" Font-Bold="true" runat="server" Text=""></asp:Label>
        </div>
    </div>


</asp:Content>
