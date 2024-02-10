<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegisterPage.aspx.cs" Inherits="Rewards.RegisterPage" %>
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
        <div id="mainContent" class="row">
            <div class="col-7 imageContainer">
                <img class="w-100 h-100" src="/icon/dhlImage.jpg" />
            </div>
            <div class="loginBox col-5 d-flex flex-column justify-content-center align-items-center p-4" >
                <div class="w-75">
                    <h1>Register</h1>
                    <h2>Please register to continue</h2>
                </div>
                 <div class="w-75 loginContainer d-flex flex-column justify-content-between align-items-center">
                     <asp:TextBox placeholder="Username" CssClass="mb-2" ID="username" runat="server" ForeColor="White"></asp:TextBox>
                     <asp:TextBox placeholder="E-mail" CssClass="mb-2" ID="email" runat="server" ForeColor="White"></asp:TextBox>
                     <asp:TextBox placeholder="Boss E-mail" CssClass="mb-2" ID="bossEmail" runat="server" ForeColor="White"></asp:TextBox>
                     <asp:TextBox placeholder="Password" CssClass="mb-2" ID="password" runat="server" ForeColor="White"></asp:TextBox>
                     <asp:Button CssClass="btn btn-success mt-2" ID="Button1" runat="server" Text="Register" />
                    <a href="RegisterPageBoss.aspx">Are you a boss? Register here</a>

                </div>
            </div>
        </div>
    </main>
</asp:Content>
