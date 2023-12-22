<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="FarmerPurchaseCRRRBalesDetails.aspx.cs" Inherits="GPILWebApp.FarmerPurchaseCRRRBalesDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <h1>Farmer Purchase CR-RR Bales Details								
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
                                        <label for="form-field-3">From Date</label>
                                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control border border-primary" MaxLength="10" TextMode="Date"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 mb-3">
                                        <label for="form-field-3">To Date</label>
                                        <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control border border-primary" MaxLength="10" TextMode="Date"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 mb-3">
                                        <label for="form-field-3">Crop</label>
                                        <asp:DropDownList ID="ddlCrop" runat="server" class="form-control" required />
                                    </div>
                                    <div class="col-md-3 mb-3">
                                        <label for="form-field-3">Variety</label>
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


                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="row">
                                    <div class="widget-body">
                                        <div class="widget-main">
                                            <div class="col-md-12 mb-12">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="gvReport" class="table table-striped table-bordered display" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                                        OnPageIndexChanging="gvReport_PageIndexChanging" AllowPaging="true" PageSize="5" EmptyDataText="No Data Found">
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
                                                            <asp:TemplateField HeaderText="Organization Code" ControlStyle-Width="100px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOrganizationCode" runat="server" Text='<%#Eval("ORGN_CODE") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Purchase Header" ControlStyle-Width="100px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPurchaseHeader" runat="server" Text='<%#Eval("HEADER_ID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total Offered Bales">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOfferedBales" runat="server" Text='<%#Eval("TOTAL_OFFERED_BALES") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total CR/RR Bales">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCRRRBales" runat="server" Text='<%#Eval("TOTAL_REJ_BALES") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Farmer Code" ControlStyle-Width="110px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFarmerCode" runat="server" Text='<%#Eval("FARMER_CODE") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Farmer Name" ControlStyle-Width="250px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFarmerName" runat="server" Text='<%#Eval("FARM_NAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Village Code" ControlStyle-Width="100px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblVillageCode" runat="server" Text='<%#Eval("VILLAGE_CODE") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Village Name" ControlStyle-Width="170px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblVillageName" runat="server" Text='<%#Eval("VILLAGE_NAME") %>'></asp:Label>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Bale Number" ControlStyle-Width="120px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBaleNumber" runat="server" Text='<%#Eval("GPIL_BALE_NUMBER") %>'></asp:Label>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Remarks" ControlStyle-Width="120px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRemarks" runat="server" Text='<%#Eval("REMARKS") %>'></asp:Label>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Rejection Type" ControlStyle-Width="70px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRejectionType" runat="server" Text='<%#Eval("REJE_TYPE") %>'></asp:Label>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Purchase Date" ControlStyle-Width="150px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPurchaseDate" runat="server" Text='<%#Eval("CREATED_DATE") %>'></asp:Label>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>



                                                            <asp:TemplateField HeaderText="Lot-Serial Number" ControlStyle-Width="100px">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLotSerialNumber" runat="server" Text='<%#Eval("LOT_NO") %>'></asp:Label>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>


                                                        </Columns>
                                                    </asp:GridView>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-3 mb-3" align="center"></div>
                                    <div class="col-md-3 mb-3" align="center">
                                        <label for="form-field-3">Total CR Bales</label>
                                        <asp:Label ID="lblRR" runat="server" CssClass="form-control label-default" BackColor="#438EB9" BorderStyle="None" Font-Bold="true" ForeColor="#FFFFFF" Font-Size="15px" Text="" />
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>




                    </div>
                </div>
            </div>
        </div>

    </div>


















</asp:Content>
