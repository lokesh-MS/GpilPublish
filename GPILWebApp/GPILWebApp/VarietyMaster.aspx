 <%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="VarietyMaster.aspx.cs" Inherits="GPILWebApp.VarietyMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }


        function onlyAlphaNumeric(e, t) {
            try {
                if (window.event) {
                    var charCode = window.event.keyCode;
                }
                else if (e) {
                    var charCode = e.which;
                }
                else { return true; }
                if ((charCode >= 48 && charCode <= 57) || (charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123))
                    return true;
                else
                    return false;
            }
            catch (err) {
                alert(err.Description);
            }
        }

        //$(document).ready(function () {
        //    $('#btnManualSave').click(function () {
        //        if ($('#txtVarietyCode').val() == "") {
        //            alert('Please complete the field');
        //        }
        //    });
        //});

        $('#btnManualSave').click(function () {
            var errors = [];
            var vCode = $('#txtVarietyCode ').val();
            var vType = $('#txtVarietyType').val();
            var vName = $('#txtVarietyName').val();
            var vDes = $('#txtVarietyDesc').val();

            if (!vCode) {
                errors.push("Name can't be left blank");
            }
            if (!vType) {
                errors.push("Vorname can't be left blank");
            }
            if (!vName) {
                errors.push("Vorname can't be left blank");
            }
            if (!vDes) {
                errors.push("Vorname can't be left blank");
            }
            if (errors.length == 0) {
                console.log('Ajax started');
                //put here your ajax function
            } else {
                for (var i in errors) {
                    console.log(errors[i]);
                }
            }
        })


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-header">
        <h1>Variety Master				
        </h1>
    </div>


    <div class="container-fluid">
        <div class="row">
            <div class="col-12">
                <div class="card">
                    <div class="card-body">
                        <%--<h6 class="card-title">Creation</h6>--%>
                        <div class="row">
                            <div class="widget-body">
                                <div class="widget-main">
                                    <div class="col-md-3 mb-3"> <label class="control-label" style="color: #4d5d89">Select Option</label></div>
                                        <div class="col-md-3 mb-3">
                                            <asp:RadioButton ID="rdbimport" runat="server" GroupName="rdbexporttype" Text="Import " AutoPostBack="True"
                                                OnCheckedChanged="rdbimport_CheckedChanged" ForeColor="Black" CssClass="form-control " />
                                        </div>
                                        <div class="col-md-3 mb-3">
                                            <asp:RadioButton ID="rdbmanual" runat="server" GroupName="rdbexporttype"
                                                Text="Manual" AutoPostBack="True"
                                                OnCheckedChanged="rdbmanual_CheckedChanged" ForeColor="Black" CssClass="form-control " />
                                        </div>
                                     <div class="col-md-3 mb-3"> </div>
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
                                        <asp:Button ID="btnUpload" runat="server" Text="Upload" class="btn btn-light-secondary rounded-pill px-4 ms-2 text-white" OnClick="btnUpload_Click" />
                                       
                                    </div>


                                </div>
                            </div>
                        </div>

                        <asp:UpdatePanel ID="upMain" runat="server">
                            <ContentTemplate>

                                <div class="row" id="divGrid" runat="server" visible="false">
                                    <div class="widget-body">
                                        <h3 class="card-title">Details</h3>
                                        <div class="widget-main">
                                            <div class="col-md-12 mb-12">

                                                <asp:GridView ID="gvVariety" runat="server" class="table table-success table-bordered"
                                                    OnPageIndexChanging="gvVariety_PageIndexChanging"
                                                    OnRowCancelingEdit="gvVariety_RowCancelingEdit"
                                                    OnRowCommand="gvVariety_RowCommand"
                                                    OnRowUpdating="gvVariety_RowUpdating" PageSize="10"
                                                    Width="90%" ForeColor="Black"
                                                    OnRowDataBound="gvVariety_RowDataBound" AllowSorting="True" ShowFooter="True" EmptyDataText="No Data Found" AutoGenerateColumns="False" OnRowDeleting="gvVariety_RowDeleting" OnRowEditing="gvVariety_RowEditing">
                                                    <AlternatingRowStyle BackColor="#F0F0F0" />
                                                    <RowStyle Font-Bold="True" Font-Names="Calibri" Font-Size="10pt" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" SortExpression="NO.">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Variety" SortExpression="Variety">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lVariety" ItemStyle-ForeColor="#ff0000" runat="server" Text='<%# Eval("Variety") %>' />
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddvareity" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqvareity" runat="server"
                                                                    ControlToValidate="txtAddvareity" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Variety Type" SortExpression="Variety_Type">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lVType" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("Variety_Type") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtvrytyp" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("VARIETY_TYPE") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddvrytyp" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqvrytyp" runat="server"
                                                                    ControlToValidate="txtAddvrytyp" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Variety Name" SortExpression="Variety_Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblvrypname" runat="server" Text='<%#Eval("VARIETY_NAME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtvrypname" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("VARIETY_NAME") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddvrypname" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqvrypname" runat="server"
                                                                    ControlToValidate="txtAddvrypname" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Variety Description" SortExpression="Variety_desc">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblvrtydesc" runat="server" Text='<%#Eval("VARIETY_DESC") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtvrtydesc" runat="server" CssClass="form-control border border-primary"
                                                                    Text='<%#Eval("VARIETY_DESC") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddvrtydesc" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqvrtydesc" runat="server"
                                                                    ControlToValidate="txtAddvrtydesc" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>

                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Variety Description" SortExpression="Insert Sts">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblupdatests" runat="server" Text='<%#Eval("INS_STS") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtupdatests" runat="server" CssClass="form-control border border-primary"
                                                                    Text='<%#Eval("INS_STS") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddupdatests" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqtupdatests" runat="server"
                                                                    ControlToValidate="txtAddupdatests" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>

                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Edit/Delete">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="imgEdit" ImageUrl="~/images/blueEdit.png" runat="server" ToolTip="Edit" Width="20" Height="20" CommandName="Edit" CausesValidation="false" />

                                                                <asp:ImageButton ID="imgDelete" ImageUrl="~/images/blueDelete1.png" runat="server" Width="20" Height="20" CommandName="Delete" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete this record?')" ToolTip="Delete" />

                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:LinkButton ID="btnUpdate0" runat="server" CommandName="Update" Text="Update" />

                                                                <asp:LinkButton ID="btnCancel0" runat="server" CommandName="Cancel" Text="Cancel" />
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Button ID="btnInsertRecord0" runat="server" CommandName="Insert" Text="Add" ValidationGroup="ValgrpCust" />
                                                            </FooterTemplate>

                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>

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
                                        <label class="control-label" style="color: #4d5d89">* Variety Code</label>
                                        <asp:TextBox ID="txtVarietyCode" runat="server" CssClass="form-control border border-primary" MaxLength="2" onkeypress="return isNumber(event)" required=''></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvCode" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtVarietyCode" />


                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-floating mb-3">
                                            <label class="control-label" style="color: #4d5d89">* Variety Type</label>
                                            <asp:TextBox ID="txtVarietyType" CssClass="form-control border border-primary" runat="server" MaxLength="15" onkeypress="return onlyAlphaNumeric(event,this)" required=''></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtVarietyType" />
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-floating mb-3">
                                            <label class="control-label" style="color: #4d5d89">* Variety Name</label>
                                            <asp:TextBox ID="txtVarietyName" CssClass="form-control border border-primary" runat="server" MaxLength="15" onkeypress="return onlyAlphaNumeric(event,this)" required=''></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtVarietyName" />
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-floating mb-3">
                                            <label class="control-label" style="color: #4d5d89">Variety Description</label>
                                            <asp:TextBox ID="txtVarietyDesc" CssClass="form-control border border-primary" runat="server" MaxLength="25" onkeypress="return onlyAlphaNumeric(event,this)" required=''></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row" runat="server" id="idManualButton" visible="false">
                            <div class="widget-body">
                                <div class="widget-main">
                                    <asp:Button ID="btnManualSave" runat="server" Text="Save" class="btn btn-primary rounded-pill px-4" ValidationGroup="id1" OnClick="btnManualSave_Click" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-primary rounded-pill px-4" OnClick="btnCancel_Click" />
                                    <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>




</asp:Content>
