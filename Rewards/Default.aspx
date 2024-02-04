<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Rewards._Default" %>

    <asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

        <main class="container-fluid p-3">
            <div class="row pb-4">
                <div class="col-4 overflow-hidden">
                    <!-- ACTIVITIES -->
                    <div class="content" style="max-height:100%;">
                        <div class="d-flex pb-2">
                            <span class="d-flex align-items-center"><img width="30" height="30" src="icon/list-check-solid.svg"/></span>
                            <h1>Activities</h1>
                        </div>
                        <div class="overflow-auto" style="max-height:85%;">
                            <ol style="--length: 0" role="list">
                                <asp:ListView ID="lvActivity" runat="server" OnItemDataBound="lvActivity_ItemDataBound">
                                    <ItemTemplate>
                                        <li class="d-flex justify-content-between">
                                            <asp:HiddenField runat="server" ID="activityIdLiteral"></asp:HiddenField>
                                            <asp:HiddenField runat="server" ID="activityDescriptionLiteral"></asp:HiddenField>
                                            <h3><asp:Literal runat="server" ID="activityNameLiteral"></asp:Literal> <span class="mx-2 p-1"><asp:Literal runat="server" ID="pointsLiteral"></asp:Literal>  points</span> </h3>
                                           <asp:Button ID="btnSubmitActivity" OnClick="btnSubmitActivity_Click" runat="server" CssClass="btn btn-success py-1 px-2 submit-btn" Text="Submit" />
                                        </li>
                                    </ItemTemplate>
                                </asp:ListView>
                            </ol>
                        </div>
                        <div class="modal fade" id="submitFormModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header d-flex flex-column align-items-start">
                                        <h5 id="activityNAME" class="modal-title" runat="server">Label</h5>
                                        <p runat="server" id="activityDESCRIPTION" class="mb-0"></p>
                                    </div>
                                    <div class="modal-body">
                                        <asp:HiddenField ID="activityID" runat="server" />
                                        <asp:TextBox ID="txtActivityDESCRIPTION" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" placeholder="Write down in detail the activity you did"></asp:TextBox>
                                        <asp:FileUpload ID="fileUpload1" runat="server" CssClass="form-control mt-3" Multiple="multiple" />
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-success" data-bs-dismiss="modal">Close</button>
                                        <asp:Button ID="btnSubmitForm" runat="server" CssClass="btn btn-success" Text="Submit" OnClick="btnSubmitForm_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-5 overflow-hidden">
                    <!-- LEADERBOARD -->
                    <div class="content">
                        <div class="d-flex pb-2">
                            <span class="d-flex align-items-center"><img width="30" height="30" src="icon/ranking-star-solid.svg"/></span>
                            <h1>Leaderboard</h1>
                        </div>
                        <div class="overflow-auto d-flex flex-column align-items-center" style="max-height:85%;">
                            <asp:ListView ID="lvLeaderboard" runat="server" OnItemDataBound="lvLeaderboard_ItemDataBound">
                                <ItemTemplate>
                                    <div class="userCard">
                                        <div class="userImgPlaceholder">
                                            <asp:Image ID="profileImage" runat="server" />
                                        </div>
                                        <div class="userInfo">
                                            <p class="userName"><asp:Literal runat="server" ID="usernameLiteral"></asp:Literal></p>
                                            <p><asp:Literal runat="server" ID="pointsLiteral"></asp:Literal> points</p>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:ListView>
                        </div>
                    </div>
                </div>
                <div class="col-3">
                    <!-- YOUR PROGRESS -->
                    <div class="content">
                        <div class="d-flex pb-2">
                            <span class="d-flex align-items-center"><img width="30" height="30" src="icon/bars-progress-solid.svg"/></span>
                            <h1>Your progress</h1>
                        </div>
                        <div class="ag-courses_box d-flex flex-column justify-content-center align-items-center overflow-auto">

                            <div class="ag-courses_item">
                                <div class="ag-courses-item_bg"></div>

                                <div class="ag-courses-item_title">
                                    Activities done
                                </div>

                                <div class="number" runat="server" id="activitiesDone"></div>

                            </div>

                            <div class="ag-courses_item">
                                <div class="ag-courses-item_bg"></div>

                                <div class="ag-courses-item_title">
                                    Rewards redeemed
                                </div>

                                <div class="number" runat="server" id="redeemedRewards"></div>
                            </div>

                            <div class="ag-courses_item">
                                <div class="ag-courses-item_bg"></div>

                                <div class="ag-courses-item_title">
                                    Points earned
                                </div>

                                <div class="number" runat="server" id="lifetimePoints"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-4">
                    <!-- YOUR PROFILE -->
                    <div id="profileModal" class="modal fade" role="dialog">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header d-flex flex-column">
                                    <div class="d-flex align-items-center mb-3">
                                        <span class="d-flex align-items-center"><img width="30" height="30" src="icon/user-solid.svg"/></span>
                                        <h1 class="modal-title">Your Profile</h1>
                                    </div>
                                    <div class="d-flex justify-content-around w-100 align-items-center pt-4" style="border-top: 1px solid white;">
                                        <div class="d-flex flex-column align-items-center">
                                            <asp:FileUpload ID="fileUpload" runat="server" style="display: none;" onchange="previewImage()" accept="image/*" />
                                            <img id="profileImage2" runat="server" width="130" height="130" src="none" onclick="document.getElementById('MainContent_fileUpload').click();" style="cursor: pointer;" />
                                            <p class="userName" runat="server" ID="profileUsername2" onclick="document.getElementById('changeUsernameBtn').click();"></p>
                                        </div>
                                        <div class="d-flex flex-column justify-content-center align-items-start">
                                            <h2>Your manager: </h2>
                                            <p class="bold white" runat="server" ID="managerEmail"></p>
                                            <h2>Your points: </h2>
                                            <p class="bold white" runat="server" ID="profilePoints2"></p>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-body" style="	background: rgb(36,37,41); background: linear-gradient(90deg, var(--primary) 0%, var(--secondary) 100%);">
                                    <div class="recentTransactions d-flex flex-column overflow-auto" style="background: transparent;">
                                        <div class="d-flex justify-content-between align-items-center mb-3">
                                            <h4 class="mb-0">Your Transactions</h4>
                                        </div>
                                        <div class="transactionsContainer overflow-auto" style="height: 160px">
                                        <asp:ListView ID="lvProfileTransactions" runat="server" OnItemDataBound="lvProfileTransactions_ItemDataBound">
                                            <ItemTemplate>
                                                <div class="transaction d-flex justify-content-between">
                                                    <p class="white w-50"><asp:Literal runat="server" ID="transactionNameLiteral"></asp:Literal></p>
                                                    <p class="<asp:Literal runat='server' ID='itemClassLiteral'></asp:Literal>"><asp:Literal runat="server" ID="pointsLiteral"></asp:Literal> points</p>
                                                    <p class="TransactionDate"><asp:Literal runat="server" ID="transactionDateLiteral"></asp:Literal></p>
                                                </div>
                                            </ItemTemplate>
                                        </asp:ListView>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-success" data-bs-dismiss="modal">Close</button>
                                    <asp:Button ID="btnSaveProfileChanges" OnClick="btnSaveProfileChanges_Click" CssClass="btn btn-success" runat="server" Text="Save Changes" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="content">
                        <div class="d-flex pb-2">
                            <span class="d-flex align-items-center"><img width="30" height="30" src="icon/user-solid.svg"/></span>
                            <h1>Your Profile</h1>
                        </div>
                        <div class="d-flex flex-column align-items-center" style="height: 85%;">
                            <div class="userCard row-cols-2" data-bs-toggle="modal" data-bs-target="#profileModal" id="divProfile" runat="server">
                                <div class="userImgPlaceholder"><img runat="server" id="profileImage" src="none" /></div>
                                <div class="userInfo">
                                    <p class="userName" runat="server" ID="profileUsername"></p>
                                    <p class="bold white" runat="server" ID="profilePoints"></p>
                                </div>
                            </div>
                            <div class="recentTransactions d-flex flex-column overflow-auto">
                                <div class="d-flex justify-content-between align-items-center mb-3">
                                    <h4 class="mb-0">Recent Transactions</h4>
                                    <asp:Button ID="btnViewAll" runat="server" Text="View All" CssClass="btn btn-success py-1 px-2 align-self-end mt-0 mx-1" OnClientClick="return false;" data-bs-toggle="modal" data-bs-target="#profileModal" />
                                </div>
                                <div class="transactionsContainer overflow-auto">
                                    <asp:ListView ID="lvTransactions" runat="server" OnItemDataBound="lvTransactions_ItemDataBound">
                                        <ItemTemplate>
                                            <div class="transaction d-flex justify-content-between">
                                                <p class="white w-50"><asp:Literal runat="server" ID="transactionNameLiteral"></asp:Literal></p>
                                                <p class="<asp:Literal runat='server' ID='itemClassLiteral'></asp:Literal>"><asp:Literal runat="server" ID="pointsLiteral"></asp:Literal> points</p>
                                                <p class="TransactionDate"><asp:Literal runat="server" ID="transactionDateLiteral"></asp:Literal></p>
                                            </div>
                                        </ItemTemplate>
                                    </asp:ListView>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-8">
                    <div class="content">
                        <!-- REWARDS -->
                        <div class="d-flex pb-2">
                            <span class="d-flex align-items-center"><img width="30" height="30" src="icon/award-solid.svg"/></span>
                            <h1>Rewards</h1>
                        </div>
                        <div class="rewards overflow-auto d-flex justify-content-center container">
                            <div class="cardsContainer container">
                                <asp:ListView ID="lvRewards" runat="server" OnItemDataBound="lvRewards_ItemDataBound">
                                    <ItemTemplate>
                                        <div class="card">
                                            <div class="imgBx">
                                                <asp:Image ID="rewardImage" runat="server" Width="100" Height="100" />
                                            </div>
                                            <div class="contentBx">
                                                <h2><asp:Literal runat="server" ID="rewardNameLiteral"></asp:Literal></h2>
                                                <h4><asp:Literal runat="server" ID="rewardPriceLiteral"></asp:Literal> Points</h4>
                                                <asp:Button ID="btnClaim" CssClass="btn btn-success py-2 px-3" runat="server" Text="Claim" CommandName="Claim" CommandArgument='<%# Eval("Id") %>' OnCommand="btnClaim_Command" />
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </main>
        <script>
            function previewImage() {
                var input = document.getElementById('MainContent_fileUpload');
                var image = document.getElementById('MainContent_profileImage2');

                if (input.files && input.files[0]) {
                    var reader = new FileReader();

                    reader.onload = function (e) {
                        image.src = e.target.result;
                    };

                    reader.readAsDataURL(input.files[0]);
                }
            }

            function showModal() {
                var myModal = new bootstrap.Modal(document.getElementById('submitFormModal'))
                myModal.show()
            }
        </script>

    </asp:Content>