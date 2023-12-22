﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="BuyerClassificationReport.aspx.cs" Inherits="GPILWebApp.BuyerClassificationReport" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="page-content" style="background-color: white">

        <div class="row mb-0">


            <div class="col-sm-3">
                <div class="form-sm-3-md-4">
                    <label class="control-label"></label>
                    <div class="form-control-sm">
                     
                    </div>
                </div>
            </div>
            <div class="col-sm-3">
                <div class="form-sm-3-md-4">
                    <label class="control-label"></label>
                    <div class="form-control-sm">
                     <asp:RadioButton runat="server" ID="rdbcomplete"
                    GroupName="VLB" Text="General" ForeColor="Black" Checked="True" />
                    </div>
                </div>
            </div>
            <div class="col-sm-3">
                <div class="form-sm-3-md-4">
                    <label class="control-label"></label>
                    <div class="form-control-sm">
                     <asp:RadioButton runat="server" ID="rdbsummary"
                    GroupName="VLB" Text="Summary" ForeColor="Black" />
                    </div>
                </div>
            </div>
            <div class="col-sm-3">
                <div class="form-sm-3-md-4">
                    <label class="control-label"></label>
                    <div class="form-control-sm">
                     
                    </div>
                </div>
            </div>

            
                
        </div>
        <div class="row mb-0">
            <div class="col-sm-3">
                <div class="form-sm-3-md-4">
                    <label class="control-label">From Date</label>
                    <div class="form-control-sm">
                        <asp:TextBox ID="txt_Report_Date" runat="server" class="form-control" TextMode="Date">                            
                        </asp:TextBox>
                        <%-- <asp:RequiredFieldValidator ID="rfvMDate" runat="server" ControlToValidate="txt_From_Date"
                            ForeColor="Red" ValidationGroup="AddEdit" Display="Dynamic" ErrorMessage="Select Date"
                            Text="*"> </asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RevMDate"
                                runat="server" ControlToValidate="txt_From_Date" ErrorMessage="Date Format Invalid,Enter Valid Date (DD-MM-YYYY)"
                                Text="*" ValidationExpression="^(([1-9])|(0[1-9])|[12][0-9]|3[01])[- /.](([1-9])|(0[1-9])|1[012])[- /.](19|20)\d\d$" Display="Dynamic" ForeColor="Red">
                            </asp:RegularExpressionValidator>--%>
                    </div>
                </div>
            </div>
            <div class="col-sm-3">
                <div class="form-sm-3-md-4">
                    <label class="control-label">To Date</label>
                    <div class="form-control-sm">
                        <asp:TextBox ID="txttodate" runat="server" class="form-control" TextMode="Date">                            
                        </asp:TextBox>
                        <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_To_Date"
                            ForeColor="Red" ValidationGroup="AddEdit" Display="Dynamic" ErrorMessage="Select Date"
                            Text="*"> </asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                runat="server" ControlToValidate="txt_To_Date" ErrorMessage="Date Format Invalid,Enter Valid Date (DD-MM-YYYY)"
                                Text="*" ValidationExpression="^(([1-9])|(0[1-9])|[12][0-9]|3[01])[- /.](([1-9])|(0[1-9])|1[012])[- /.](19|20)\d\d$" Display="Dynamic" ForeColor="Red">
                            </asp:RegularExpressionValidator>--%>
                    </div>
                </div>
            </div>
            <div class="col-sm-3">
                <div class="form-sm-3">
                    <label class="control-label">Crop</label>
                    <div class="form-control-sm">
                        <asp:DropDownList runat="server" CssClass="form-control" ID="cbxcrop">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="col-sm-3">
                <div class="form-sm-3">
                    <label class="control-label">Variety</label>
                    <div class="form-control-sm">
                        <asp:DropDownList runat="server" CssClass="form-control" ID="cbxvariety">
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
                    <label class="control-label">Select Orgn Name </label>
                    <div>
                        <asp:DropDownList runat="server" CssClass="form-control" ID="cbxorgcd" ></asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="col-sm-3">
                <div class="form-sm-3-md-4">
                    <label class="control-label"></label>
                    <div class="form-control-sm">
                    <asp:RadioButton runat="server" ID="rdoWithFreight"
                    GroupName="VLB" Text="With Freight" ForeColor="Black" Checked="True" />
                    </div>
                </div>
            </div>
            <div class="col-sm-3">
                <div class="form-sm-3-md-4">
                    <label class="control-label"></label>
                    <div class="form-control-sm">
                     <asp:RadioButton runat="server" ID="rdoWithoutFreight"
                    GroupName="VLB" Text="Without Freight" ForeColor="Black" />
                    </div>
                </div>
            </div>
            <div class="col-sm-3">
                <div class="form-sm-3-md-4">
                    <label class="control-label"></label>
                    <div class="form-control-sm">
                     
                    </div>
                </div>
            </div>

            
                
        </div>






        <div></div>
        <div class="row">
            <div class="col-sm-3">

                <div>
                    <label class="control-label"> </label>
                </div>
                <div class="form-control-sm">
                    <asp:Button ID="btnview" runat="server" CssClass="btn btn-sm btn-success" Text="View Report"  OnClick="btnview_Click"/>
                     <asp:Button ID="btnclose" runat="server" CssClass="btn btn-sm btn-success" Text="Clear" OnClick="btnclose_Click"/>

                </div>
            </div>

            <div class="col-sm-3">

                <div class="form-control-sm">
                   

                </div>
            </div>

        </div>



        <%-- <h4 class="header green">TapWise Purchase Report</h4>--%>

        <div class="col-sm-12 mb-0" style="width: 100%">
            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" ToolPanelView="None" HasCrystalLogo="False" />
            <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            </CR:CrystalReportSource>
        </div>


        <div class="col-sm-12 mb-0" style="width: 100%">
            <CR:CrystalReportViewer ID="CrystalReportViewer2" runat="server" AutoDataBind="true" ToolPanelView="None" HasCrystalLogo="False" />
            <CR:CrystalReportSource ID="CrystalReportSource2" runat="server">
            </CR:CrystalReportSource>
        </div>


        <div>
            <asp:Label ID="lblMessage" ForeColor="Red" BackColor="Yellow" Font-Size="Large" Font-Bold="true" runat="server" Text=""></asp:Label>
        </div>

    </div>


</asp:Content>
