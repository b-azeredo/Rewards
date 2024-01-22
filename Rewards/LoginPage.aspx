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
        <div id="mainContent" class="row">
            <div class="col-7 imageContainer">
                <img class="w-100 h-100" src="https://images.pexels.com/photos/259915/pexels-photo-259915.jpeg?auto=compress&cs=tinysrgb&w=600" />
            </div>
            <div class="loginBox col-5 d-flex flex-column justify-content-center align-items-center p-4" >
                <div class="w-75">
                    <h1>Login</h1>
                    <h2>Please login to continue</h2>
                </div>
                 <div class="w-75 loginContainer d-flex flex-column justify-content-between align-items-center">
                     <asp:TextBox placeholder="Username" CssClass="mb-3" ID="username" runat="server" ForeColor="White"></asp:TextBox>
                     <asp:TextBox placeholder="Password" CssClass="mb-3" ID="password" runat="server" ForeColor="White"></asp:TextBox>
                     <div class="d-flex justify-content-start align-items-center w-100">
                         <asp:CheckBox BorderColor="#242529" ID="CheckBox1" runat="server" /> <p class="mb-0 mx-1">Keep me logged in</p>
                     </div>
                    <asp:Button CssClass="btn btn-success mt-2" ID="Button1" runat="server" Text="Login" />
                    <a href="RegisterPage.aspx">Create a new account</a>
                </div>
            </div>
        </div>

    </main>
</asp:Content>
