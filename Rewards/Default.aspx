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
	                        <li class="d-flex justify-content-between">
		                        <h3>Discovery and assessment</h3>
                                <button type="button" class="btn btn-success py-1 px-2">Submit</button>
	                        </li>
	                        <li class="d-flex justify-content-between">
		                        <h3>Information gathering and analysis</h3>
                                <button type="button" class="btn btn-success py-1 px-2">Submit</button>
	                        </li>
	                        <li class="d-flex justify-content-between">
		                        <h3>Creating your claim</h3>
                                <button type="button" class="btn btn-success py-1 px-2">Submit</button>
	                        </li>
	                        <li class="d-flex justify-content-between">
		                        <h3>Approvals and submission</h3>
                                <button type="button" class="btn btn-success py-1 px-2">Submit</button>
	                        </li>
	                        <li class="d-flex justify-content-between">
		                        <h3>Receiving your benefit</h3>
                                <button type="button" class="btn btn-success py-1 px-2">Submit</button>
	                        </li>
                            <li class="d-flex justify-content-between">
		                        <h3>Receiving your benefit</h3>
                                <button type="button" class="btn btn-success py-1 px-2">Submit</button>
	                        </li>
                        </ol>
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
                                            <img src="https://cdn.pixabay.com/photo/2016/08/08/09/17/avatar-1577909_960_720.png" />
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

                            <div class="hiddenNumber">120</div>

                        </div>

                        <div class="ag-courses_item">
                            <div class="ag-courses-item_bg"></div>

                            <div class="ag-courses-item_title">
                              Awards redeemed
                            </div>

                            <div class="hiddenNumber">120</div>
                        </div>

                        <div class="ag-courses_item">
                            <div class="ag-courses-item_bg"></div>

                            <div class="ag-courses-item_title">
                              Points earned
                            </div>

                            <div class="hiddenNumber">120</div>
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
                            <div class="userImgPlaceholder"><img src="https://cdn.pixabay.com/photo/2016/08/08/09/17/avatar-1577909_960_720.png" /></div>
                            <div class="userInfo">
                                <p class="userName unde">Username <br /> <span class="black">Online</span></p>
                                <p class="bold white">98 points</p>
                            </div>
                        </div>
                        <div class="recentTransactions d-flex flex-column overflow-auto">
                              <div class="d-flex justify-content-between align-items-center mb-3">
                                 <h4 class="mb-0">Recent Transactions</h4>
                                <button type="button" class="btn btn-success py-1 px-2 align-self-end mt-0 mx-1">View All</button>
                            </div>
                            <div class="transactionsContainer overflow-auto">
                                <div class="transaction d-flex justify-content-between">
                                    <p class="white w-50">Activity Done</p>
                                    <p class="up">+9 points</p>
                                    <p>22/01</p>
                                </div>
                                <div class="transaction d-flex justify-content-between">
                                    <p class="white w-50">Award Redeemed</p>
                                    <p class="down">-5 points</p>
                                    <p>22/01</p>
                                </div>
                                <div class="transaction d-flex justify-content-between">
                                    <p class="white w-50">Activity Done</p>
                                    <p class="up">+3 points</p>
                                    <p>22/01</p>
                                </div>
                               <div class="transaction d-flex justify-content-between">
                                    <p class="white w-50">Award Redeemed</p>
                                    <p class="down">-4 points</p>
                                    <p>22/01</p>
                                </div>
                                <div class="transaction d-flex justify-content-between">
                                    <p class="white w-50">Award Redeemed</p>
                                    <p class="down">-4 points</p>
                                    <p>22/01</p>
                                </div>
                                <div class="transaction d-flex justify-content-between">
                                    <p class="white w-50">Award Redeemed</p>
                                    <p class="down">-4 points</p>
                                    <p>22/01</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-8">
                <div class="content"> <!-- AWARDS -->
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
                                             <img src="<%# Eval("IMAGE_URL") %>">
                                         </div>

                                         <div class="contentBx">

                                             <h2><%# Eval("NAME") %></h2>

                                             <h4><%# Eval("PRICE") %> Points</h4>

                                             <button type="button" class="btn btn-success py-2 px-3">Claim</button>
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

</asp:Content>
