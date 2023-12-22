<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="FarmerWisePurchaseSummary.aspx.cs" Inherits="GPILWebApp.FarmerWisePurchaseSummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="Scripts/ScrollableGridViewPlugin_ASP.NetAJAXmin.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <h1>Farmer Wise Purchase Summary Report
								
        </h1>
    </div>


    <div class="container-fluid">
        <div class="row">
            <div class="col-12">
                <div class="card">
                    <div class="card-body">
                    <%--    <h6 class="card-title">Searching Criteria</h6>--%>
                        <div class="row">
                            <div class="widget-body">
                                <div class="widget-main">
                                    <div class="col-md-6 mb-6">
                                        <label for="form-field-6">Crop</label>
                                        <asp:DropDownList ID="ddlCrop" runat="server" class="form-control" required />
                                    </div>

                                    <div class="col-md-6 mb-6">
                                        <label for="form-field-6">Variety</label>
                                        <asp:DropDownList ID="ddlVariety" runat="server" class="form-control" required />
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="widget-body">
                                <div class="widget-main">
                                    <asp:Button ID="btnView" runat="server" Text="Save" class="btn btn-info rounded-pill px-4 mt-3" OnClick="btnView_Click" ValidationGroup="id1" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-info rounded-pill px-4 mt-3" OnClick="btnCancel_Click" /><%----%>
                                </div>
                            </div>
                        </div>
                          <div class="row">
                            <div class="widget-body" align="right">
                                <div class="widget-main">
                                    <asp:ImageButton ID="btnExcel" ToolTip="Excel Download" runat="server" ImageUrl="~/assets/images/avatars/Excel-download-icon.png" OnClick="btnExcel_Click" Width="30px" Height="30px" />
                                </div>
                            </div>
                        </div>

                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="row">
                                    <div class="widget-body">
                                        <div class="widget-main">
                                            <div class="col-md-12 mb-12">
                                                <div class="table-responsive" style="overflow-y: scroll; overflow-x: scroll; height:500PX">                                                    
                                                    <asp:GridView ID="gvReport" class="table table-striped table-bordered display" runat="server" AutoGenerateColumns="False" 
                                                        OnPageIndexChanging="gvReport_PageIndexChanging" EmptyDataText="No Data Found" >
                                                        <AlternatingRowStyle BackColor="#F1F5F9" BorderColor="#438EB9" />
                                                        <FooterStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#438EB9" />
                                                        <PagerStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#438EB9" />
                                                        <HeaderStyle BackColor="#438EB9" BorderColor="#438EB9" BorderStyle="Solid" BorderWidth="1px"
                                                            Font-Size="13px" Height="30px" ForeColor="White" Font-Bold="false" />
                                                        <RowStyle BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" Font-Size="13px"
                                                            Height="20px" ForeColor="Black" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.No.">
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Farmer Code">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFarmerCode" runat="server" Text='<%#Eval("FARMER_CODE") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Farmer Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFarmerName" runat="server" Text='<%#Eval("FARM_NAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Farmer Father's Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFatherName" runat="server" Text='<%#Eval("FATHER_NAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Farmer Village">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblVillage" runat="server" Text='<%#Eval("VILLAGE") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Bank Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBankName" runat="server" Text='<%#Eval("BANK_NAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Account No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBANK_ACCOUNT_NO" runat="server" Text='<%#Eval("AccNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="BRANCH_NAME">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBranchName" runat="server" Text='<%#Eval("BRANCH_NAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="IFSC_CODE">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIFSCCode" runat="server" Text='<%#Eval("IFSC_CODE") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Number of Bale Purchase">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNoOfBales" runat="server" Text='<%#Eval("BALES") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Quantity (Kgs.)">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblQuantity" runat="server" Text='<%#Eval("QUANTITY") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Net Purchase Amount (Including Freight) (Rs.Ps.)">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNetPurchaseAmount" runat="server" Text='<%#Eval("TOTAL_VALUE") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="200px" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="row">

                            <div class="col-md-3 mb-3">
                                <label for="form-field-3">Total No. of Farmers</label>
                                <asp:Label ID="lblFarmerCount" runat="server" CssClass="form-control label-default" BackColor="#438EB9" BorderStyle="None" Font-Bold="true" ForeColor="#FFFFFF" Font-Size="15px" Text="" />
                            </div>
                            <div class="col-md-3 mb-3">
                                <label for="form-field-3">Total No. of Bales Purchase</label>
                                <asp:Label ID="lblBaleCount" runat="server" CssClass="form-control label-default" BackColor="#438EB9" BorderStyle="None" Font-Bold="true" ForeColor="#FFFFFF" Font-Size="15px" Text="" />
                            </div>
                            <div class="col-md-3 mb-3">
                                <label for="form-field-3">Total Purchase Quantity</label>
                                <asp:Label ID="lblQuantity" runat="server" CssClass="form-control label-default" BackColor="#438EB9" BorderStyle="None" Font-Bold="true" ForeColor="#FFFFFF" Font-Size="15px" Text="" />

                            </div>
                            <div class="col-md-3 mb-3">
                                <label for="form-field-3">Total Net Purchase Amount</label>
                                <asp:Label ID="lblValue" runat="server" CssClass="form-control label-default" BackColor="#438EB9" BorderStyle="None" Font-Bold="true" ForeColor="#FFFFFF" Font-Size="15px" Text="" />
                            </div>
                        </div>



                    </div>
                </div>
            </div>
        </div>

    </div>

    
    <script>
        $(document).ready(function () {
            $('#<%=gvReport.ClientID %>').Scrollable({
            ScrollHeight: 300,
            IsInUpdatePanel: true
        });
    });
    </script>


</asp:Content>
