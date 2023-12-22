<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="FarmerAuthorisedQtyReport.aspx.cs" Inherits="GPILWebApp.FarmerAuthorisedQtyReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-header">
        <h1>Farmer Authorised Quantity Report
								
        </h1>
    </div>

    <div class="container-fluid">
        <div class="row">
            <div class="col-12">
                <div class="card">
                    <div class="card-body">
                        <%--  <h6 class="card-title">Searching Criteria</h6>--%>
                        <div class="row">
                            <div class="widget-body">
                                <div class="widget-main">
                                    <div class="col-md-3 mb-3">
                                        <label for="form-field-3">Select From Date</label>
                                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control border border-primary" MaxLength="10" TextMode="Date" required></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 mb-3">
                                        <label for="form-field-3">Select To Date</label>
                                        <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control border border-primary" MaxLength="10" TextMode="Date" required></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 mb-3">
                                        <label for="form-field-3">Select Crop</label>
                                        <asp:DropDownList ID="ddlCrop" runat="server" class="form-control" required />
                                    </div>
                                    <div class="col-md-3 mb-3">
                                        <label for="form-field-3">Select Crop</label>
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
                        <div class="row">
                            <div class="widget-body">
                                <div class="widget-main">
                                    <div class="col-md-12 mb-12">
                                        <asp:GridView ID="file_export" class="table table-bordered table-hover" runat="server" AutoGenerateColumns="False"
                                            Width="100%" OnPageIndexChanging="gvReport_PageIndexChanging" EmptyDataText="No Data Found">
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
                                                        <asp:Label ID="lblTodayOffered" runat="server" Text='<%#Eval("FARM_NAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Village Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTodateOffered" runat="server" Text='<%#Eval("VILLAGE_NAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Authorised Qty">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTodayCR" runat="server" Text='<%#Eval("Authorised_Qty") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Todate Sold">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTodayRR" runat="server" Text='<%#Eval("TODATE_SOLD") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="Today Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTodayDAte" runat="server" Text='<%#Eval("TODAY_DATE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Today Sold">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTodateCR" runat="server" Text='<%#Eval("TODAY_SOLD") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Difference">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTodateRR" runat="server" Text='<%#Eval("TOTAL_Difference") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>


                        <div class="row">

                            <div class="col-md-3 mb-3">
                                <label for="form-field-3">Toatl Authorised Qty</label>
                                <asp:Label ID="lblOffered" runat="server" CssClass="form-control label-default" BackColor="#438EB9" BorderStyle="None" Font-Bold="true" ForeColor="#FFFFFF" Font-Size="15px" Text="" />
                            </div>
                            <div class="col-md-3 mb-3">
                                <label for="form-field-3">Total TODAY_SOLD</label>
                                <asp:Label ID="lblCR" runat="server" CssClass="form-control label-default" BackColor="#438EB9" BorderStyle="None" Font-Bold="true" ForeColor="#FFFFFF" Font-Size="15px" Text="" />
                            </div>
                            <div class="col-md-3 mb-3">
                                <label for="form-field-3">Total TODATE_SOLD</label>
                                <asp:Label ID="lblRR" runat="server" CssClass="form-control label-default" BackColor="#438EB9" BorderStyle="None" Font-Bold="true" ForeColor="#FFFFFF" Font-Size="15px" Text="" />

                            </div>
                            <div class="col-md-3 mb-3">
                                <label for="form-field-3">Total TOTAL_Difference</label>
                                <asp:Label ID="lblSold" runat="server" CssClass="form-control label-default" BackColor="#438EB9" BorderStyle="None" Font-Bold="true" ForeColor="#FFFFFF" Font-Size="15px" Text="" />
                            </div>
                        </div>



                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
