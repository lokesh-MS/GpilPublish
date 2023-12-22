<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true"  EnableEventValidation="false" CodeBehind="VillageMaster.aspx.cs" Inherits="GPILWebApp.VillageMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="page-header">
        <h1>Village Master</h1>
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
                                        <label class="control-label" style="color: #4d5d89">Select Option</label></div>
                                    <div class="col-md-3 mb-3">
                                        <asp:RadioButton ID="rdbimport" runat="server" GroupName="rdbexporttype" Text="Import" AutoPostBack="True"
                                           OnCheckedChanged="rdbimport_CheckedChanged"   ForeColor="Black" CssClass="form-control " />
                                    </div>
                                    <div class="col-md-3 mb-3">
                                        <asp:RadioButton ID="rdbmanual" runat="server" GroupName="rdbexporttype"
                                            Text="Manual" AutoPostBack="True"
                                            OnCheckedChanged="rdbmanual_CheckedChanged"  ForeColor="Black" CssClass="form-control " />
                                    </div>
                                    <div class="col-md-3 mb-3"></div>
                                </div>
                            </div>
                        </div>


                        <div class="row" id="idUpload" runat="server" visible="false">
                            <div class="widget-body">
                                <div class="widget-main">
                                    <div class="col-md-6 mb-6">
                                        <label class="control-label" style="color: #4d5d89">Excel Upload</label>
                                        <asp:FileUpload ID="excelUpload" CssClass="form-control" runat="server" />
                                    </div>
                                    <div class="col-md-3 mb-3">
                                        <asp:Button ID="btnUpload" runat="server" Text="Upload" class="btn btn-light-secondary rounded-pill px-4 ms-2 text-white" OnClick="btnUpload_Click"  />

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
                            <div class="widget-body">
                                <div class="widget-main">
                                    <div class="col-md-3 mb-3">
                                        <label class="control-label" style="color: #4d5d89">* Village Code</label>
                                        <asp:TextBox ID="txtVillageCode" runat="server" CssClass="form-control border border-primary" MaxLength="2" onkeypress="return isNumber(event)" required=''></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvVillageCode" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtVillageCode" />


                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-floating mb-3">
                                            <label class="control-label" style="color: #4d5d89">* Village Name</label>
                                            <asp:TextBox ID="txtVillageName" CssClass="form-control border border-primary" runat="server" MaxLength="15" onkeypress="return onlyAlphaNumeric(event,this)" required=''></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvVillageName" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtVillageName" />
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-floating mb-3">
                                            <label class="control-label" style="color: #4d5d89">* Cluster Code</label>
                                            <asp:DropDownList ID="ddlClusterCode" runat="server" class="form-control" />
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-floating mb-3">
                                            <label class="control-label" style="color: #4d5d89">* Status</label>
                                            <asp:DropDownList ID="ddlStatus" runat="server" class="form-control" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>



                        <div class="row" runat="server" id="idManualButton" visible="false">
                            <div class="widget-body">
                                <div class="widget-main">
                                    <asp:Button ID="btnManualSave" runat="server" Text="Save" class="btn btn-primary rounded-pill px-4" ValidationGroup="id1" OnClick="btnManualSave_Click"  />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-primary rounded-pill px-4" OnClick="btnCancel_Click" />                                
                                </div>
                            </div>
                        </div>


                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
