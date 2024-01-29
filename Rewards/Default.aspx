<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Rewards._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main class="container-fluid p-3">





        <div class="row pb-4">
            <div class="col-xxl-5 col-xl-4 overflow-hidden"> <!-- ACTIVITIES -->
                <div class="content" style="max-height:100%;">
                    <div class="d-flex pb-2">
                        <span class="d-flex align-items-center"><img width="30" height="30" src="icon/list-check-solid.svg"/></span> 
                        <h1>Activities</h1>
                    </div>
                    <div class="overflow-auto" style="max-height:85%;">
                        <ol style="--length: 0" role="list">
                            <asp:ListView ID="lvActivity" runat="server">
                                <ItemTemplate>
                                    <li class="d-flex justify-content-between">
                                        <h3><%# Eval("NAME") %> <span class="mx-2 p-1"><%# Eval("POINTS") %>  points</span> </h3>
                                        <button type="button" class="btn btn-success py-1 px-2 submit-btn" data-bs-toggle="modal" data-bs-target="#myModal" data-name='<%# Eval("NAME") %>' data-id="<%# Eval("ACTIVITY_ID") %>">Submit</button>
                                    </li>
                                </ItemTemplate>
                            </asp:ListView>

                        </ol>
                    </div>
                    <div id="myModal" class="modal fade" role="dialog">
                        <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title">Modal title</h5>
                            </div>
                            <div class="modal-body">
                                <asp:HiddenField ID="activityID" runat="server" />
                                <asp:TextBox ID="description" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" placeholder="Write down in detail the activity you did"></asp:TextBox>

                                <input type="file" class="form-control mt-2">
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-danger" data-bs-dismiss="modal">Close</button>
                                <asp:Button ID="btnSubmitForm" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="btnSubmitForm_Click"/>
                            </div>
                        </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-xxl-4 col-xl-4 overflow-hidden"> <!-- LEADERBOARD -->
                <div class="content">
                    <div class="d-flex pb-2">
                        <span class="d-flex align-items-center"><img width="30" height="30" src="icon/ranking-star-solid.svg"/></span> 
                        <h1>Leaderboard</h1>
                    </div>
                    <div class="overflow-auto d-flex flex-column align-items-center" style="max-height:85%;">
				            <asp:ListView ID="lvLeaderboard" runat="server">
                                <ItemTemplate>
                                    <div class="userCard">
                                        <div class="userImgPlaceholder">
                                            <img src='<%# "data:image;base64," + Convert.ToBase64String(Eval("PROFILE_IMAGE") as byte[]) %>' />
                                        </div>
                                        <div class="userInfo">
                                            <p class="userName"><%# Eval("UserName") %></p>
                                            <p><%# Eval("Points") %> points</p>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:ListView>
                     </div>
                </div>
            </div>
            <div class="col-xxl-3 col-xl-4"> <!-- YOUR PROGRESS -->
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

                            <div class="hiddenNumber" runat="server" id="activitiesDone"></div>

                        </div>

                        <div class="ag-courses_item">
                            <div class="ag-courses-item_bg"></div>

                            <div class="ag-courses-item_title">
                              Rewards redeemed
                            </div>

                            <div class="hiddenNumber" runat="server" id="redeemedRewards"></div>
                        </div>

                        <div class="ag-courses_item">
                            <div class="ag-courses-item_bg"></div>

                            <div class="ag-courses-item_title">
                              Points earned
                            </div>

                            <div class="hiddenNumber" runat="server" id="lifetimePoints"></div>
                        </div>
                      </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-4"> <!-- YOUR PROFILE -->
                <div class="content">
                     <div class="d-flex pb-2">
                        <span class="d-flex align-items-center"><img width="30" height="30" src="icon/user-solid.svg"/></span> 
                        <h1>Your Profile</h1>
                    </div>
                    <div class="d-flex flex-column align-items-center" style="height: 85%;">
                        <div class="userCard row-cols-2">
                            <div class="userImgPlaceholder"><img runat="server" id="profileImage" src ="" /></div>
                            <div class="userInfo">
                                <p class="userName" runat="server" ID="profileUsername"></p>
                                <p class="bold white" runat="server" ID="profilePoints"></p>
                            </div>
                        </div>
                        <div class="recentTransactions d-flex flex-column overflow-auto">
                              <div class="d-flex justify-content-between align-items-center mb-3">
                                 <h4 class="mb-0">Recent Transactions</h4>
                                <button type="button" class="btn btn-success py-1 px-2 align-self-end mt-0 mx-1">View All</button>
                            </div>
                            <div class="transactionsContainer overflow-auto">
                            <asp:ListView ID="lvTransactions" runat="server">
                                <ItemTemplate>
                                    <div class="transaction d-flex justify-content-between">
                                        <p class="white w-50"> <%# Eval("NAME") %></p>
                                        <p class="<%# Eval("ItemClass") %>"><%# Eval("POINTS") %> points</p>
                                        <p class="TransactionDate"><%# Eval("DATE") %></p>
                                    </div>
                                </ItemTemplate>
                            </asp:ListView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-8">
                <div class="content"> <!-- REWARDS -->
                     <div class="d-flex pb-2">
                        <span class="d-flex align-items-center"><img width="30" height="30" src="icon/award-solid.svg"/></span> 
                        <h1>Rewards</h1>
                    </div>
                    <div class="rewards overflow-auto d-flex justify-content-center container">
                        <div class="cardsContainer container">


                          <asp:ListView ID="lvRewards" runat="server">
                                <ItemTemplate>
                                     <div class="card">
                                         <div class="imgBx">
                                             <img src="<%# "data:image;base64," + Convert.ToBase64String(Eval("IMAGE") as byte[]) %>">
                                         </div>

                                         <div class="contentBx">

                                             <h2><%# Eval("NAME") %></h2>

                                             <h4><%# Eval("PRICE") %> Points</h4>

                                             <asp:Button ID="btnClaim" CssClass="btn btn-success py-2 px-3" runat="server" Text="Claim" CommandName="Claim" CommandArgument='<%# Eval("ID") %>' OnCommand="btnClaim_Command" />
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
            $('#myModal').on('show.bs.modal', function (event) {
                var button = $(event.relatedTarget);
                var activityName = button.data('name');
                var activityId = button.data('id'); // Capturando o ID da atividade
                document.querySelectorAll('.submit-btn').forEach(button => {
                    button.addEventListener('click', function () {
                        document.getElementById('MainContent_activityID').value = activityId; 
                    });
                });

                var modal = $(this);
                modal.find('.modal-title').text(activityName);
            });
        </script>

</asp:Content>
