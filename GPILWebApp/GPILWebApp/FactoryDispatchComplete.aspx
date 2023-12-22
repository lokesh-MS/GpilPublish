<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeFile="FactoryDispatchComplete.aspx.cs" Inherits="GPILWebApp.FactoryDispatchComplete" %>

<script runat="server">

    protected void GridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }

    protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void GridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }

    protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void GridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void GridView_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
</script>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <h1 style="font-weight: bold; font-family: Cambria;">Factory Dispatch Complete</h1>
    </div>
    <div class="container-fluid">
        <div class="row">
            <div class="col-12">
                <div class="card">
                    <div class="card-body">
                        <h4 class="card-title">Searching Criteria</h4>
                        <div class="row">
                            <div class="widget-body">
                                <div class="widget-main">
                                    <div class="col-md-3 mb-3">
                                        <asp:Label ID="lblLRDate" runat="server" Text="LR Date"></asp:Label>
                                        <asp:TextBox ID="txtLRDate" runat="server" class="form-control" TextMode="Date"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 mb-3">
                                        <asp:Label ID="lblReceiverLocationCode" runat="server" Text="Receiver Location Code"></asp:Label>
                                        <asp:DropDownList ID="ddlReceiverLocationCode" runat="server" class="form-control" />
                                    </div>
                                    <div class="col-md-3 mb-3">
                                        <asp:Label ID="lblTransporterCode" runat="server" Text="Transporter Code"></asp:Label>
                                        <asp:DropDownList ID="ddlTransporterCode" runat="server" class="form-control" />
                                    </div>
                                    <div class="col-md-3 mb-3">
                                        <asp:Label ID="lblTruckNumber" runat="server" Text="Truck Number"></asp:Label>
                                        <asp:DropDownList ID="ddlTruckNumber" runat="server" class="form-control" />
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="widget-body">
                                <div class="widget-main">
                                    <div class="col-md-3 mb-3">
                                    </div>
                                    <div class="col-md-3 mb-3">
                                        <asp:Button ID="btnView" runat="server" Text="View" CssClass="btn btn-info rounded-pill px-4 mt-3" OnClick ="btnView_Click"  />

                                    </div>
                                    <div class="col-md-3 mb-3">
                                    </div>
                                    <div class="col-md-3 mb-3">
                                    </div>

                                </div>
                            </div>
                        </div>
                        <asp:GridView ID="GridView1" runat="server" class="table table-success table-bordered"
                                                    OnRowCancelingEdit="GridView1_RowCancelingEdit"      
                                                    OnRowCommand="GridView1_RowCommand"       
                                                    OnRowUpdating="GridView1_RowUpdating"       PageSize="10"
                                                    Width="90%" ForeColor="Black"
                                                    OnRowDataBound="GridView1_RowDataBound"       AllowSorting="True" ShowFooter="True" EmptyDataText="No Data Found" AutoGenerateColumns="False" OnRowDeleting="GridView1_RowDeleting"  OnRowEditing="GridView1_RowEditing"      >
                                                    <AlternatingRowStyle BackColor="#F0F0F0" />
                                                    <RowStyle Font-Bold="True" Font-Names="Calibri" Font-Size="10pt" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" SortExpression="NO.">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Shipment Number" SortExpression="SHIPMENT_NO">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lShipmentNumber" ItemStyle-ForeColor="#ff0000" runat="server" Text='<%# Eval("SHIPMENT_NO") %>' />
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtShipmentNumber" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqShipmentNumber" runat="server"
                                                                    ControlToValidate="txtShipmentNumber" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Sender Location" SortExpression="SENDER_ORGN_CODE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lSenderLocation" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("SENDER_ORGN_CODE") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtSenderLocation" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("SENDER_ORGN_CODE") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddSenderLocation" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqSenderLocation" runat="server"
                                                                    ControlToValidate="txtAddSenderLocation" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Sender Truck" SortExpression="SENDER_TRUCK_NO">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lSenderTruck" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("SENDER_TRUCK_NO") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtSenderTruck" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("SENDER_TRUCK_NO") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddSenderTruck" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqSenderTruck" runat="server"
                                                                    ControlToValidate="txtAddSenderTruck" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Sender Date" SortExpression="SENDER_DATE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lSenderDate" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("SENDER_DATE") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtSenderDate" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("SENDER_DATE") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddSenderDate" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqSenderDate" runat="server"
                                                                    ControlToValidate="txtAddSenderDate" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>                                           

                                                        <asp:TemplateField HeaderText="Received Date" SortExpression="RECEIVED_DATE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lReceivedDate" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("RECEIVED_DATE") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtReceivedDate" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("RECEIVED_DATE") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddReceivedDate" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqReceivedDate" runat="server"
                                                                    ControlToValidate="txtAddReceivedDate" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="No's Cases" SortExpression="CASES">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lNoCases" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("CASES") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtNoCases" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("CASES") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddNoCases" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqNoCases" runat="server"
                                                                    ControlToValidate="txtAddNoCases" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Quantity (Kgs)" SortExpression="QUANTITY">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lQuantity" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("QUANTITY") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("QUANTITY") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddQuantity" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqQuantity" runat="server"
                                                                    ControlToValidate="txtAddQuantity" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                </asp:GridView>
                        <div class="row">
                            <div class="widget-body">
                                <div class="widget-main">
                                    <div class="col-md-3 mb-3">
                                        <asp:Button ID="btnCompleteTruckDispatch" runat="server" Text="Complete Truck Dispatch" CssClass="btn btn-info rounded-pill px-4 mt-3" OnClick ="btnCompleteTruckDispatch_Click"  />
                                        <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-info rounded-pill px-4 mt-3" OnClick ="btnClose_Click"  />
                                        </div> 
                                    </div> </div> </div> 



                    </div>
                </div>
            </div>
        </div>
    </div>



</asp:Content>
