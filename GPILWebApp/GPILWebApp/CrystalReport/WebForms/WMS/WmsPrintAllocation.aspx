<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="WmsPrintAllocation.aspx.cs" Inherits="GPILWebApp.CrystalReport.WebForms.WMS.WmsPrintAllocation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="header">
        <h2 style="text-align: center; font-weight: bold; font-family: 'Times New Roman'; color: #438EB9">PRINT ALLOCATION</h2>
    </div>



    <div class="row">



        <div class="col-sm-6">
            <div class="form-sm-3-md-4">
                <label class="control-label">Running Year</label>
                <div>
                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlRunningYear" AutoPostBack="true" OnTextChanged="ddlRunningYear_TextChanged">
                        <asp:ListItem Value="0">SELECT RUNNING YEAR</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>

        <div class="col-sm-6">
            <div class="form-sm-3-md-4">
                <label class="control-label">Grade Code</label>
                <div>
                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlGradeCode" AutoPostBack="true" OnTextChanged="ddlGradeCode_TextChanged">
                        <asp:ListItem Value="0">SELECT GRADE CODE</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        

    </div>



    <div class="row">
        <label></label>
    </div>


     <div class="row">
       <div class="col-sm-6">
            <div class="form-sm-3-md-4">
                <label class="control-label">Run Number</label>
                <div class="form-control-sm">
                    <asp:TextBox ID="txtRunNo" runat="server" class="form-control" ReadOnly>                            
                    </asp:TextBox>
                </div>
            </div>
        </div>

         <div class="col-sm-6">
            <div class="form-sm-3-md-4">
                <label class="control-label">Location Code</label>
                <div>
                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlLocationCode" AutoPostBack="true" OnTextChanged="ddlLocationCode_TextChanged">
                        <asp:ListItem Value="0">SELECT LOCATION CODE</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>

    </div>

    
    <div class="row">
        <label></label>
    </div>

    <div class="row">
        

        <div class="col-sm-6">
            <div class="form-sm-3-md-4">
                <label class="control-label" id="RunYr">Run Year  :</label>
               <%-- <div>--%>
                    <asp:label id="lblRunYear" runat="server"></asp:label>
               <%-- </div>--%>
            </div>
        </div>
        <div class="col-sm-6">
            <div class="form-sm-3-md-4">
                <label class="control-label">Grade Name :</label>
               <%-- <div>--%>
                    <asp:label id="lblGradeName" runat="server"></asp:label>
               <%-- </div>--%>
            </div>
        </div>
    </div>
    <div class="row">
        <label></label>
    </div>

    <div class="row">
        <div class="col-sm-6">
            <div class="form-sm-3-md-4">
                <label class="control-label">Location Name :</label>
                <%--<div>--%>
                    <asp:label id="lblLocationName"  runat="server"></asp:label>
               <%-- </div>--%>
            </div>
        </div>

       
    </div>


    <div class="row">
        <label></label>
    </div>

     <div class="row">
         <div class="col-sm-6">
            <div class="form-sm-3-md-4">
                <label class="control-label">From</label>
                <div>
                    <asp:TextBox ID="txtFrom" runat="server" class="form-control" ReadOnly>                            
                    </asp:TextBox>
                </div>
            </div>
        </div>
        <div class="col-sm-6">
            <div class="form-sm-3-md-4">
                <label class="control-label">To</label>
                <div>
                    <asp:TextBox ID="txtTo" runat="server" class="form-control" ReadOnly>                            
                    </asp:TextBox>
                </div>
            </div>
        </div>


    </div>

     <div class="row">
        <label></label>
    </div>


    <div class="row">

        <div class="col-sm-6">
            <div class="form-sm-3-md-4">
                <label class="control-label">No Of Cases To Print</label>
                <div class="form-control-sm">
                    <asp:TextBox ID="txtNoOfCasesPrint" runat="server" class="form-control" OnTextChanged="txtNoOfCasesPrint_TextChanged">                            
                    </asp:TextBox>
                </div>
            </div>
        </div>
    <div class="col-sm-6">
            <div class="form-sm-3-md-4">
        <asp:label id="lblTStartCaseNo" runat="server"></asp:label>
    </div>
        </div>
        </div>

    <div class="row">
        <label></label>
    </div>

    <div class="row">
        <div class="col-sm-6">
            <div class="form-sm-3-md-4">
            <div>
                <label></label>
            </div>
            <div class="form-control-sm">
                <asp:Button ID="btnAllocate" runat="server" CssClass="btn btn-sm btn-success" Text="Allocate" OnClick="btnAllocate_Click" />
                <asp:Button ID="btnNewRun" runat="server" CssClass="btn btn-sm btn-success" Text="New Run" OnClick="btnNewRun_Click" />
                 <asp:Button ID="btnClear" runat="server" CssClass="btn btn-sm btn-success" Text="Clear" OnClick="btnClear_Click"/>
               <%-- <asp:Button ID="btnBack" runat="server" CssClass="btn btn-sm btn-success" Text="Back" OnClick="btnBack_Click" />--%>
               
                </div>
            </div>
        </div>

        <div class="col-sm-3">

            <div class="form-control-sm">
            </div>
        </div>
    </div>
</asp:Content>
