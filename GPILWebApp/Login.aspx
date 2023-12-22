<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" EnableEventValidation="false"  Inherits="GPI.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <%--<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />--%>
    <meta charset="utf-8" />
    <title>Godfrey Phillips India Ltd-Login Page</title>

    <meta name="description" content="User login page" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />

    <!-- bootstrap & fontawesome -->
    <link rel="stylesheet" href="assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="assets/font-awesome/4.5.0/css/font-awesome.min.css" />

    <!-- text fonts -->
    <link rel="stylesheet" href="assets/css/fonts.googleapis.com.css" />

    <!-- ace styles -->
    <link rel="stylesheet" href="assets/css/ace.min.css" />

    <!--[if lte IE 9]>
			<link rel="stylesheet" href="assets/css/ace-part2.min.css" />
		<![endif]-->
    <link rel="stylesheet" href="assets/css/ace-rtl.min.css" />

</head>
<body class="login-layout">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="sm" runat="server" />
        <asp:UpdatePanel ID="up" runat="server">
            <ContentTemplate>



                <div class="main-container">
                    <div class="main-content">

                        <div class="row">
                            <div class="col-sm-10 col-sm-offset-1">
                                <div class="login-container">
                                    <div class="center">
                                        <h4>
                                            <img src="assets/images/avatars/GPIL_logo.png" class="msg-photo" alt="GPI logo" />
                                            <%-- <i class="ace-icon fa fa-leaf green"></i>--%>
                                            <span class="red"></span>
                                            <span class="white" id="id-text2"></span>
                                        </h4>
                                        <h4 class="blue" id="id-company-text">&copy; Green Leaf Traceability System</h4>
                                    </div>

                                    <div class="space-6"></div>

                                    <div class="position-relative">
                                        <div id="login-box" class="login-box visible widget-box no-border">
                                            <div class="widget-body">
                                                <div class="widget-main">
                                                    <h4 class="header blue lighter bigger">
                                                        <i class="ace-icon fa fa-coffee green"></i>
                                                        Please Enter Your Information
                                                    </h4>
                                                    <div class="space-6"></div>

                                                    <form id="loginForm">
                                                        <fieldset>
                                                            <label class="block clearfix">
                                                                <span class="block input-icon input-icon-left">
                                                                    <i class="ace-icon fa fa-user"></i>
                                                                    <input class="form-control" type="text" name="USER_ID" id="logEmail" placeholder="UserName" />

                                                                </span>
                                                            </label>
                                                            <label class="block clearfix">
                                                                <span class="block input-icon input-icon-left">
                                                                    <i class="ace-icon fa fa-lock"></i>

                                                                    <input class="form-control" type="password" name="Password" id="logPssword" placeholder="Password" />
                                                                </span>
                                                            </label>
                                                            <div class="space"></div>
                                                            <div class="clearfix">
                                                                <label class="inline">
                                                                    <input type="checkbox" class="ace" />
                                                                    <span class="lbl">Remember Me</span>
                                                                </label>
                                                            </div>
                                                            <div class="space-4"></div>
                                                        </fieldset>
                                                    </form>
                                                    <div class="social-or-login center">
                                                        <span class="bigger-110">Enter to access</span>
                                                    </div>

                                                    <div class="space-2"></div>

                                                    <div class="social-login center">

                                                        <div class="space"></div>
                                                        <%-- <button id="btnLogin" class="width-35 pull-right btn btn-sm btn-primary">
                                                    <i class="ace-icon fa fa-key"></i>
                                                    <span class="bigger-110">Login</span>
                                                </button>--%>


                                                        <asp:Button ID="btnLogin" runat="server" Text="Login" class="width-35 pull-right btn btn-sm btn-primary" OnClick="btnLogin_Click" />

                                                        <div class="space"></div>
                                                        <div class="space"></div>
                                                        <div class="space"></div>

                                                    </div>
                                                </div>
                                                <!-- /.widget-main -->

                                                <div class="toolbar clearfix">
                                                    <div>
                                                    </div>

                                                    <div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- /.widget-body -->
                                        </div>
                                        <!-- /.login-box -->

                                    </div>
                                    <!-- /.position-relative -->

                                    <div class="navbar-fixed-top align-right">
                                        <br />
                                        &nbsp;
                                <a id="btn-login-dark" href="#">Dark</a>
                                        &nbsp;
                                <span class="blue">/</span>
                                        &nbsp;
                                <a id="btn-login-blur" href="#">Blur</a>
                                        &nbsp;
                                <span class="blue">/</span>
                                        &nbsp;
                                <a id="btn-login-light" href="#">Light</a>
                                        &nbsp; &nbsp; &nbsp;
                                    </div>
                                </div>
                            </div>
                            <!-- /.col -->
                        </div>
                        <!-- /.row -->

                    </div>
                    <!-- /.main-content -->
                </div>
                <!-- /.main-container -->



                <div class="footer">
                    <div class="footer-inner">
                        <div class="footer-content">
                            <span class="bigger-120">
                                <span class="blue bolder">&copy; Godfrey Phillips India Limited </span>
                                <span class="white bolder"></span>
                            </span>

                            <%--  &nbsp; &nbsp;
                    <span class="action-buttons">
                        <a href="#">
                            <i class="ace-icon fa fa-twitter-square light-blue bigger-150"></i>
                        </a>

                        <a href="#">
                            <i class="ace-icon fa fa-facebook-square text-primary bigger-150"></i>
                        </a>

                        <a href="#">
                            <i class="ace-icon fa fa-rss-square orange bigger-150"></i>
                        </a>
                    </span>--%>
                        </div>
                    </div>
                </div>


            </ContentTemplate>
        </asp:UpdatePanel>
        <script src="~/assets/js/jquery-2.1.4.min.js"></script>

        <!-- <![endif]-->
        <!--[if IE]>
    <script src="/assets/js/jquery-1.11.3.min.js"></script>
    <![endif]-->
        <script type="text/javascript">
            if ('ontouchstart' in document.documentElement) document.write("<script src='~/assets/js/jquery.mobile.custom.min.js'>" + "<" + "/script>");
        </script>


    </form>
</body>
</html>
