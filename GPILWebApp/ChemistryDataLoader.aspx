<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="ChemistryDataLoader.aspx.cs" Inherits="GPILWebApp.ChemistryDataLoader" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <h1 style="font-weight: bold; font-family: Cambria;">Chemistry Report</h1>
    </div>

    <div class="container-fluid">
        <div class="row">
            <div class="col-12">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="widget-body">
                                <div class="widget-main">
                                    <div class="col-md-3 mb-3">
                                        <label class="control-label" style="color: #1989b9">Select Option</label>
                                    </div>
                                    <div class="col-md-3 mb-3">
                                        <asp:RadioButton ID="rdbimportFromExcel" runat="server" GroupName="rdbexporttype" Text="Import From Excel " AutoPostBack="True"
                                            OnCheckedChanged="rdbimportFromExcel_CheckedChanged" ForeColor="Black" CssClass="form-control " />
                                    </div>
                                    <div class="col-md-3 mb-3">
                                        <asp:RadioButton ID="rdbmanualEntry" runat="server" GroupName="rdbexporttype"
                                            Text="Manual Entry" AutoPostBack="True"
                                            OnCheckedChanged="rdbmanualEntry_CheckedChanged" ForeColor="Black" CssClass="form-control " />
                                    </div>
                                </div>
                            </div>
                        </div>


                        <div class="row">
                            <div class="widget-body">
                                <div class="widget-main">
                                    <div class="col-md-3 mb-3">
                                        <label class="control-label" style="color: #1989b9"></label>
                                    </div>
                                    <div class="col-md-3 mb-3">
                                    </div>
                                    <div class="col-md-3 mb-3">
                                    </div>
                                    <div class="col-md-3 mb-3"></div>
                                </div>
                            </div>
                        </div>


                        <div class="row" id="idUpload" runat="server" visible="false">
                            <div class="widget-body">
                                <div class="widget-main">
                                    <div class="col-md-3 mb-3">
                                        <label class="control-label" style="color: #555555">Excel Upload</label>

                                    </div>
                                    <div class="col-md-3 mb-3">
                                        <asp:FileUpload ID="excelUpload" CssClass="form-control" runat="server" />
                                    </div>
                                    <div class="col-md-3 mb-3">

                                        <asp:Button ID="btnUpload" runat="server" Text="Upload" class="btn btn-primary btn-round" OnClick="btnUpload_Click" />

                                    </div>
                                </div>
                            </div>
                        </div>


                        <div class="row" runat="server" id="divImportSave" visible="false">
                            <div class="widget-body">
                                <div class="widget-main">
                                    <asp:Button ID="btnImportSave" runat="server" Text="Save" class="btn btn-primary rounded-pill px-4" ValidationGroup="id1" OnClick="btnImportSave_Click" />
                                </div>
                            </div>
                        </div>


                        <div class="row" runat="server" id="idManual" visible="false">

                            <div class="row">
                                <div class="widget-body">
                                    <div class="widget-main">
                                        <div class="col-md-3 mb-3">
                                            <label class="control-label" style="color: #555555">Crop</label>
                                            <asp:DropDownList runat="server" CssClass="form-control" ID="ddlCrop">
                                                <asp:ListItem Value="0">SELECT </asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="col-md-3 mb-3">
                                            <label class="control-label" style="color: #555555">Type</label>
                                            <asp:DropDownList runat="server" CssClass="form-control" ID="ddlType">
                                                <asp:ListItem Value="0">SELECT </asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-3 mb-3">
                                            <label class="control-label" style="color: #555555">Grade</label>
                                            <asp:DropDownList runat="server" CssClass="form-control" ID="ddlGrade">
                                                <asp:ListItem Value="0">SELECT </asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-3 mb-3">
                                            <label class="control-label" style="color: #555555">Variety</label>
                                            <asp:DropDownList runat="server" CssClass="form-control" ID="ddlVariety">
                                                <asp:ListItem Value="0">SELECT </asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="widget-body">
                                    <div class="widget-main">
                                        <div class="col-md-3 mb-3">
                                            <label class="control-label" style="color: #555555">Export Type</label>
                                            <asp:DropDownList runat="server" CssClass="form-control" ID="ddlExportType">
                                                <asp:ListItem Value="0">SELECT </asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="col-md-3 mb-3">
                                            <label class="control-label" style="color: #555555">Date Of Packing</label>
                                            <asp:TextBox ID="txtDateOfPacking" runat="server" CssClass="form-control border border-primary" MaxLength="2"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvDateOfPacking" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtMark" />
                                        </div>
                                        <div class="col-md-3 mb-3">
                                            <label class="control-label" style="color: #555555">Mark</label>
                                            <asp:TextBox ID="txtMark" runat="server" CssClass="form-control border border-primary" MaxLength="2"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvMark" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtFatherName" />
                                        </div>
                                        <div class="col-md-3 mb-3">
                                            <label class="control-label" style="color: #555555">Product</label>
                                            <asp:TextBox ID="txtProduct" runat="server" CssClass="form-control border border-primary" MaxLength="2"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvProduct" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtProduct" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="widget-body">
                                    <div class="widget-main">
                                        <div class="col-md-3 mb-3">
                                            <label class="control-label" style="color: #555555">Source Organization</label>
                                            <asp:TextBox ID="txtSourceOrganization" runat="server" CssClass="form-control border border-primary" MaxLength="2"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvSourceOrganization" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtSourceOrganization" />
                                        </div>

                                        <div class="col-md-3 mb-3">
                                            <label class="control-label" style="color: #555555">From No</label>
                                            <asp:TextBox ID="txtFromNo" runat="server" CssClass="form-control border border-primary" MaxLength="2"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvFromNo" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtFromNo" />
                                        </div>
                                        <div class="col-md-3 mb-3">
                                            <label class="control-label" style="color: #555555">To No</label>
                                            <asp:TextBox ID="txtToNo" runat="server" CssClass="form-control border border-primary" MaxLength="2"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvToNo" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtToNo" />
                                        </div>
                                        <div class="col-md-3 mb-3">
                                            <label class="control-label" style="color: #555555">Cl</label>
                                            <asp:TextBox ID="txtCl" runat="server" CssClass="form-control border border-primary" MaxLength="2"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtCl" />
                                        </div>
                                    </div>
                                </div>
                            </div>



                            <div class="row">
                                <div class="widget-body">
                                    <div class="widget-main">
                                        <div class="col-md-3 mb-3">
                                            <label class="control-label" style="color: #555555">Nic</label>
                                            <asp:TextBox ID="txtNic" runat="server" CssClass="form-control border border-primary" MaxLength="2"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvNic" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtNic" />
                                        </div>
                                        <div class="col-md-3 mb-3">
                                            <label class="control-label" style="color: #555555">Trs</label>
                                            <asp:TextBox ID="txtTrs" runat="server" CssClass="form-control border border-primary" MaxLength="2"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvTrs" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtTrs" />
                                        </div>
                                        <div class="col-md-3 mb-3">
                                            <label class="control-label" style="color: #555555">Moisture Percentage</label>
                                            <asp:TextBox ID="txtMoisturePercentage" runat="server" CssClass="form-control border border-primary" MaxLength="2"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvMoisturePercentage" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtMoisturePercentage" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row" runat="server" id="idManualButton" visible="false">
                            <div class="widget-body">
                                <div class="widget-main">
                                    <div class="col-md-3">
                                        <div class="form-floating mb-3">
                                            <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-primary btn-round" ValidationGroup="id1" OnClick="btnSave_Click"  />
                                            <asp:Button ID="btnClear" runat="server" Text="Clear" class="btn btn-primary btn-round" OnClick="btnClear_Click"  />
                                            <asp:Button ID="btnClose" runat="server" Text="Close" class="btn btn-primary btn-round" OnClick="btnClose_Click"  />
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-floating mb-3">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>


                        </div>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
