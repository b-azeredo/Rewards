<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Rewards._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main class="container-fluid p-3">
        <div class="row pb-4">
            <div class="col-xxl-5 col-xl-4 overflow-hidden"> <!-- ACTIVITIES -->
                <div class="content" style="max-height:100%;">
                    <div class="d-flex pb-2">
                        <span class="d-flex align-items-center"><img width="30px" height="30px" src="icon/list-check-solid.svg"/></span> 
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
                        <span class="d-flex align-items-center"><img width="30px" height="30px" src="icon/ranking-star-solid.svg"/></span> 
                        <h1>Leaderboard</h1>
                    </div>
                    <div class="overflow-auto d-flex flex-column align-items-center" style="max-height:85%;">
				            <div class="userCard">
                                <div class="userImgPlaceholder"><img src="https://cdn.pixabay.com/photo/2016/08/08/09/17/avatar-1577909_960_720.png" /></div>
                                <div class="userInfo">
                                    <p class="userName">Username</p>
                                    <p>98 points</p>
                                </div>
				            </div>
                            <div class="userCard">
                                <div class="userImgPlaceholder"><img src="https://cdn.pixabay.com/photo/2016/08/08/09/17/avatar-1577909_960_720.png" /></div>
                                <div class="userInfo">
                                    <p class="userName">Username</p>
                                    <p>98 points</p>
                                </div>
                            </div>
                            <div class="userCard">
                                <div class="userImgPlaceholder"><img src="https://cdn.pixabay.com/photo/2016/08/08/09/17/avatar-1577909_960_720.png" /></div>
                                <div class="userInfo">
                                    <p class="userName">Username</p>
                                    <p>98 points</p>
                                </div>
                            </div>
                            <div class="userCard">
                                <div class="userImgPlaceholder"><img src="https://cdn.pixabay.com/photo/2016/08/08/09/17/avatar-1577909_960_720.png" /></div>
                                <div class="userInfo">
                                    <p class="userName">Username</p>
                                    <p>98 points</p>
                                </div>
                            </div>
                            <div class="userCard">
                                <div class="userImgPlaceholder"><img src="https://cdn.pixabay.com/photo/2016/08/08/09/17/avatar-1577909_960_720.png" /></div>
                                <div class="userInfo">
                                    <p class="userName">Username</p>
                                    <p>98 points</p>
                                </div>
                            </div>
                     </div>
                </div>
            </div>
            <div class="col-xxl-3 col-xl-4">
                <div class="content">
                    <div class="d-flex pb-2">
                        <span class="d-flex align-items-center"><img width="30px" height="30px" src="icon/bars-progress-solid.svg"/></span> 
                        <h1>Your progress</h1>
                    </div>
                      <div class="ag-courses_box d-flex flex-column overflow-auto">

                        <div class="ag-courses_item">
                            <div class="ag-courses-item_bg"></div>

                            <div class="ag-courses-item_title">
                              Activities done
                            </div>

                        </div>

                        <div class="ag-courses_item">
                            <div class="ag-courses-item_bg"></div>

                            <div class="ag-courses-item_title">
                              Awards redeemed
                            </div>

                        </div>

                        <div class="ag-courses_item">
                            <div class="ag-courses-item_bg"></div>

                            <div class="ag-courses-item_title">
                              Points earned
                            </div>

                        </div>

                      </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-4">
                <div class="content">

                </div>
            </div>
            <div class="col-8">
                <div class="content">
                     <div class="d-flex">
                        <span class="d-flex align-items-center"><img width="30px" height="30px" src="icon/award-solid.svg"/></span> 
                        <h1>Awards</h1>
                    </div>
                </div>
            </div>
        </div>
    </main>

</asp:Content>
