<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="TodateCRRRReports.aspx.cs" Inherits="GPILWebApp.TodateCRRRReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <h1>Today-Todate CR-RR Details
								
        </h1>
    </div>

    <div class="container-fluid">
        <div class="row">
            <div class="col-12">
                <div class="card">
                    <div class="card-body">
                        <h6 class="card-title">Searching Criteria</h6>
                        <div class="row">
                            <div class="widget-body">
                                <div class="widget-main">
                                    <div class="col-md-3 mb-3">
                                        <label for="form-field-3">To Date</label>
                                        <asp:TextBox ID="txtDate" runat="server" CssClass="form-control border border-primary" MaxLength="10" TextMode="Date" required></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 mb-3">
                                        <label for="form-field-3">Crop</label>
                                        <asp:DropDownList ID="ddlCrop" runat="server" class="form-control" />
                                    </div>
                                    <div class="col-md-3 mb-3">
                                        <label for="form-field-3">Variety</label>
                                        <asp:DropDownList ID="ddlVariety" runat="server" class="form-control" />
                                    </div>

                                </div>
                            </div>
                        </div>


                        <div class="row">
                            <div class="widget-body">
                                <div class="widget-main">
                                    <asp:Button ID="btnView" runat="server" Text="View" class="btn btn-info rounded-pill px-4 mt-3" OnClick="btnView_Click" />
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



                        <asp:UpdatePanel ID="upMain" runat="server">
                            <ContentTemplate>

                                <div class="row">
                                    <div class="widget-body">
                                        <h6 class="card-title">Details</h6>
                                        <div class="widget-main">
                                            <div class="col-md-12 mb-12">
                                                <div class="table-responsive" style="overflow-y: scroll; overflow-x: scroll; height: 500PX">
                                                    <asp:GridView ID="gvReport" class="table table-striped table-bordered display" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                                        Width="100%" OnPageIndexChanging="gvReport_PageIndexChanging" EmptyDataText="No Data Found" PageSize="5" AllowPaging="true">
                                                        <AlternatingRowStyle BackColor="#F1F5F9" BorderColor="#438EB9" />
                                                        <FooterStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#438EB9" />
                                                        <PagerStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#438EB9" />
                                                        <HeaderStyle BackColor="#438EB9" BorderColor="#438EB9" BorderStyle="Solid" BorderWidth="1px"
                                                            Font-Size="13px" Height="30px" ForeColor="White" Font-Bold="false" />
                                                        <RowStyle BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" Font-Size="13px"
                                                            Height="20px" ForeColor="Black" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="SI.No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSno" runat="server" Text='<%#Eval("SNO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Organization Code">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOrganizationCode" runat="server" Text='<%#Eval("ORGN_CODE") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Today Offered">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTodayOffered" runat="server" Text='<%#Eval("TODAY_OFFERED") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Todate Offered">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTodateOffered" runat="server" Text='<%#Eval("TODATE_OFFERED") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Today CR">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTodayCR" runat="server" Text='<%#Eval("TODAY_CR") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Todate CR">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTodateCR" runat="server" Text='<%#Eval("TODATE_CR") %>'></asp:Label>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Today RR">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTodayRR" runat="server" Text='<%#Eval("TODAY_RR") %>'></asp:Label>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Todate RR">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTodateRR" runat="server" Text='<%#Eval("TODATE_RR") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Today Sold">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTodaySold" runat="server" Text='<%#Eval("TODAY_SOLD") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Todate Sold">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTodateSold" runat="server" Text='<%#Eval("TODATE_SOLD") %>'></asp:Label>
                                                                </ItemTemplate>

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
                                <label for="form-field-3">Total Offered</label>
                                <asp:Label ID="lblOffered" runat="server" CssClass="form-control label-default" BackColor="#438EB9" BorderStyle="None" Font-Bold="true" ForeColor="#FFFFFF" Font-Size="15px" Text="" />
                            </div>
                            <div class="col-md-3 mb-3">
                                <label for="form-field-3">Total CR</label>
                                <asp:Label ID="lblCR" runat="server" CssClass="form-control label-default" BackColor="#438EB9" BorderStyle="None" Font-Bold="true" ForeColor="#FFFFFF" Font-Size="15px" Text="" />
                            </div>
                            <div class="col-md-3 mb-3">
                                <label for="form-field-3">Total RR</label>
                                <asp:Label ID="lblRR" runat="server" CssClass="form-control label-default" BackColor="#438EB9" BorderStyle="None" Font-Bold="true" ForeColor="#FFFFFF" Font-Size="15px" Text="" />

                            </div>
                            <div class="col-md-3 mb-3">
                                <label for="form-field-3">Total Net Sold</label>
                                <asp:Label ID="lblSold" runat="server" CssClass="form-control label-default" BackColor="#438EB9" BorderStyle="None" Font-Bold="true" ForeColor="#FFFFFF" Font-Size="15px" Text="" />
                            </div>
                        </div>




                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
