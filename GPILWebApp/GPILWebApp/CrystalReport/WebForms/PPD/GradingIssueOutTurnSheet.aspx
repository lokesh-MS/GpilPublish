<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" EnableEventValidation="false"  AutoEventWireup="true" CodeBehind="GradingIssueOutTurnSheet.aspx.cs" Inherits="GPILWebApp.GradingIssueOutTurnSheet" %>

 

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


   <div class="page-header">
        <h1>Grading Issue / Out Turn Sheet
								
        </h1>
    </div>

    

    <div class="page-content" style="background-color: white">

        <div class="row mb-0">


            <div class="col-sm-3">
                <div class="form-sm-3-md-3">
                    <label class="control-label"></label>
                    <div class="form-control-sm">
                    </div>
                </div>
            </div>
            <div class="col-sm-1">
                <div class="form-sm-3-md-4">
                    <label class="control-label"></label>
                    <div class="form-control-sm">
                        <asp:RadioButton runat="server" ID="rdbIssuedReport"
                            GroupName="GradingIssue" Text="Issued Report" ForeColor="Black" Checked="True" />
                    </div>
                </div>
            </div>
            <div class="col-sm-1">
                <div class="form-sm-3-md-4">
                    <label class="control-label"></label>
                    <div class="form-control-sm">
                        <asp:RadioButton runat="server" ID="rdbOutTurnReport"
                            GroupName="GradingIssue" Text="Out Turn Report" ForeColor="Black" />
                    </div>
                </div>
            </div>

             <div class="col-sm-1">
                <div class="form-sm-3-md-4">
                    <label class="control-label"></label>
                    <div class="form-control-sm">
                        <asp:RadioButton runat="server" ID="rdbOperationReport"
                            GroupName="GradingIssue" Text="Operation Report" ForeColor="Black" />
                    </div>
                </div>
            </div>


            <div class="col-sm-3">
                <div class="form-sm-3-md-3">
                    <label class="control-label"></label>
                    <div class="form-control-sm">
                    </div>
                </div>
            </div>



        </div>
        <div class="row mb-0">
            <div class="col-sm-3">
                <div class="form-sm-3-md-4">
                    <label class="control-label">Grading Out Turn Date</label>
                    <div class="form-control-sm">
                        <asp:TextBox ID="txt_GradingOutTurn_Date" runat="server" CssClass="form-control" TextMode="Date" EnableViewState="true">                            
                        </asp:TextBox>
                        <%--<asp:RegularExpressionValidator ID="RevMDate"
                                runat="server" ControlToValidate="txt_From_Date" ErrorMessage="Date Format Invalid,Enter Valid Date (DD-MM-YYYY)"
                                Text="*" ValidationExpression="^(([1-9])|(0[1-9])|[12][0-9]|3[01])[- /.](([1-9])|(0[1-9])|1[012])[- /.](19|20)\d\d$" Display="Dynamic" ForeColor="Red">
                            </asp:RegularExpressionValidator>--%>
                    </div>
                </div>
            </div>
            
            <div class="col-sm-3">
                <div class="form-sm-3">
                    <label class="control-label">Location Code</label>
                    <div class="form-control-sm">
                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlLocationCode">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="col-sm-3">
                <div class="form-sm-3">
                    <label class="control-label">Crop Year</label>
                    <div class="form-control-sm">
                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlCropYear">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>






            <%--<div class="col-sm-7">
                <div>
                    <label></label></div>

               



            </div>--%>
        </div>

        <div class="row mb-0">


            <div class="col-sm-3">
                <div class="form-sm-3">
                    <label class="control-label">Variety</label>
                    <div>
                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlVariety"></asp:DropDownList>
                    </div>
                </div>
            </div>
           
            <div class="col-sm-3">
                <div class="form-sm-3">
                    <label class="control-label">Batch Number</label>
                    <div>
                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlBatchNo"></asp:DropDownList>
                    </div>
                </div>
            </div>


        </div>


        



        <div></div>
        <div class="row">
            <div class="col-sm-3">

                <div>
                    <label class="control-label"></label>
                </div>
                <div class="form-control-sm">
                    <asp:Button ID="btnview" runat="server" CssClass="btn btn-sm btn-success" Text="View Report" OnClick="btnview_Click" />
                    <asp:Button ID="btnclose" runat="server" CssClass="btn btn-sm btn-success" Text="Clear" OnClick="btnclose_Click" />

                </div>
            </div>

            <div class="col-sm-3">

                <div class="form-control-sm">
                </div>
            </div>

        </div>



         <h4 class="header green">Grading Operation Report</h4>

        <div class="col-sm-12 mb-0" style="width: 100%">
            <CR:CrystalReportViewer ID="GradingReport" runat="server" AutoDataBind="true" ToolPanelView="None" HasCrystalLogo="False" />
            <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            </CR:CrystalReportSource>
        </div>


        


        <div>
            <asp:Label ID="lblMessage" ForeColor="Red" BackColor="Yellow" Font-Size="Large" Font-Bold="true" runat="server" Text=""></asp:Label>
        </div>

    </div>
   







</asp:Content>
