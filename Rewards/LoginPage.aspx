<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="Rewards.LoginPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        @media (max-width: 1200px) {
	        main div.row {
		        height: auto;
                width: 420px;
	        }

	        main > div {
		        height: auto;
                width: 420px;
	        }

	        #mainContent .imageContainer, #mainContent .imageContainer img{
	            background-color: var(--secondary);
	            border-radius: 20px 20px 0px 0px;
	            padding: 0px;
            }
        }

    </style>
    <main class="container-fluid d-flex justify-content-center align-items-center" style="height: 100vh; width: 100vw;">
    <div class="alert alert-warning alert-dismissible fade show" role="alert" style="display:none;"></div>
        <div id="mainContent" class="row">
            <div class="col-7 imageContainer">
                <img class="w-100 h-100" src="/icon/dhlImage.jpg" />
            </div>
            <div class="loginBox col-5 d-flex flex-column justify-content-center align-items-center p-4" >
                <div class="w-75">
                    <h1>Login</h1>
                    <h2>Please login to continue</h2>
                </div>
                 <div class="w-75 loginContainer d-flex flex-column justify-content-between align-items-center">
                     <asp:TextBox placeholder="Username" CssClass="mb-3" ID="txtUsername" runat="server" ForeColor="White"></asp:TextBox>
                     <asp:TextBox placeholder="Password" TextMode="Password" CssClass="mb-3" ID="txtPassword" runat="server" ForeColor="White"></asp:TextBox>
                    <asp:Button CssClass="btn btn-success mt-2" ID="btnLogin" OnClick="btnLogin_Click" runat="server" Text="Login" />
                    <a href="RegisterPage.aspx">Create a new account</a>
                </div>
            </div>
        </div>
        <script>
            function messageAlert(text) {
                $('.alert').html(text);
                $('.alert').append('<button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>');
                $('.alert').show();
                setTimeout(function () {
                    $('.alert').hide();
                }, 5000);
            }
        </script>
    </main>
</asp:Content>
