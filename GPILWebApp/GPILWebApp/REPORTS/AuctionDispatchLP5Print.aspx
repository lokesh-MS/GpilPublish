<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="AuctionDispatchLP5Print.aspx.cs" Inherits="GPILWebApp.REPORTS.AuctionDispatchLP5Print" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

      <div class="page-header">
        <h1 style="font-weight: bold; font-family: Cambria;">AUCTION DISPATCH LP5 PRINT</h1>
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
                                  

                                    <div class="row mb-0">

            <div class="col-sm-3">
                <div class="form-sm-3-md-4">
                    <label class="control-label"> Date</label>
                    <div class="form-control-sm">
                        <asp:TextBox ID="txtDate" runat="server" class="form-control" TextMode="Date">                            
                        </asp:TextBox>
                       
                    </div>
                </div>
            </div>


            <div class="col-sm-3">
                <div class="form-sm-3">
                    <label class="control-label">Select From Orgn Name </label>
                    <div>
                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlFromOrgnCode">
                             <asp:ListItem Value="0">Select From Orgn Name</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>

            <div class="col-sm-3">
                <div class="form-sm-3">
                    <label class="control-label">Select To Orgn Name </label>
                    <div>
                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlToOrgnCode" OnSelectedIndexChanged="ddlToOrgnCode_SelectedIndexChanged" >
                             <asp:ListItem Value="0">Select To Orgn Name</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>

            <div class="col-sm-3">
                <div class="form-sm-3">
                    <label class="control-label">Select Shipment No</label>
                    <div class="form-control-sm">
                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlShipmentNumber" AutoPostBack="True">
                            <asp:ListItem Value="0">SELECT SHIPMENT NO</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>


                                </div>
                            </div>
                        </div>


                             <div class="row">
                            <div class="widget-body">

                                  </div> </div> 


                          <div class="row">
                            <div class="widget-body">
                                 <label></label>
                            </div> </div> 



                          <div class="row">
                            <div class="widget-body">
                               <label></label>
                            </div> </div> 


                         <div class="row">
                            <div class="widget-body">
                                <div class="widget-main">

                                     <div class="col-sm-4">
                                         </div>
                                    <div class="col-sm-1">
                                         </div>
                                   
                                     <div class="col-sm-2">
                                         
                                    <asp:Button ID="btnView" runat="server" CssClass="btn btn-sm btn-success" Text="View " OnClick="btnView_Click"   BackColor="#0099FF"   />

                                        
                                    <asp:Button ID="btnClose" runat="server" CssClass="btn btn-sm btn-success" Text="Close" OnClick="btnClose_Click"    BackColor="#0099FF"   />
                                
                                     
                                     </div>

                                   </div>
                                    <div class="col-sm-1">
                                         </div>
                                    

                                    <div class="col-sm-4">
                                         </div>
                            </div>
                        </div>
                        </div> 






                </div>
                </div>
            </div>
        </div>

</asp:Content>
