<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Rewards._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main class="container-fluid p-3">
        <div class="row pb-4">
            <div class="col-5"> <!-- ACTIVITIES -->
                <div class="content" style="max-height:100%;">
                    <div class="d-flex pb-3">
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
            <div class="col-4"> <!-- LEADERBOARD -->
                <div class="content">
                    <div class="d-flex pb-3">
                        <span class="d-flex align-items-center"><img width="30px" height="30px" src="icon/ranking-star-solid.svg"/></span> 
                        <h1>Leaderboard</h1>
                    </div>
                    <div class="overflow-auto" style="max-height:85%;">

				                    <table class="table user-list" style="border-spacing: 0px 10px;">
					                    <thead>
						                    <tr>
							                    <th><span>User</span></th>
							                    <th><span>Boss Name</span></th>
							                    <th><span>Points</span></th>
						                    </tr>
					                    </thead>
					                    <tbody>
						                    <tr>
							                    <td>
								                    <img src="https://bootdey.com/img/Content/avatar/avatar1.png" alt="">
								                    <a href="#" class="user-link">Username</a>
								                    <span class="user-subhead">E-mail</span>
							                    </td>
							                    <td>
								                    Bossname
							                    </td>
							                    <td>
								                    <span class="label label-default">120</span>
							                    </td>
						                    </tr>
												<tr>
							                    <td>
								                    <img src="https://bootdey.com/img/Content/avatar/avatar1.png" alt="">
								                    <a href="#" class="user-link">Username</a>
								                    <span class="user-subhead">E-mail</span>
							                    </td>
							                    <td>
								                    Bossname
							                    </td>
							                    <td>
								                    <span class="label label-default">120</span>
							                    </td>
						                    </tr>
                                            <tr>
							                    <td>
								                    <img src="https://bootdey.com/img/Content/avatar/avatar1.png" alt="">
								                    <a href="#" class="user-link">Username</a>
								                    <span class="user-subhead">E-mail</span>
							                    </td>
							                    <td>
								                    Bossname
							                    </td>
							                    <td>
								                    <span class="label label-default">120</span>
							                    </td>
						                    </tr>
											<tr>
							                    <td>
								                    <img src="https://bootdey.com/img/Content/avatar/avatar1.png" alt="">
								                    <a href="#" class="user-link">Username</a>
								                    <span class="user-subhead">E-mail</span>
							                    </td>
							                    <td>
								                    Bossname
							                    </td>
							                    <td>
								                    <span class="label label-default">120</span>
							                    </td>
						                    </tr>
											<tr>
							                    <td>
								                    <img src="https://bootdey.com/img/Content/avatar/avatar1.png" alt="">
								                    <a href="#" class="user-link">Username</a>
								                    <span class="user-subhead">E-mail</span>
							                    </td>
							                    <td>
								                    Bossname
							                    </td>
							                    <td>
								                    <span class="label label-default">120</span>
							                    </td>
						                    </tr>
					                    </tbody>
				                    </table>
                     </div>
                </div>
            </div>
            <div class="col-3">
                <div class="content">
                    <div class="d-flex">
                        <span class="d-flex align-items-center"><img width="30px" height="30px" src="icon/bars-progress-solid.svg"/></span> 
                        <h1>Your progress</h1>
                    </div>
					<div class="ag-format-container">
                      <div class="ag-courses_box d-flex flex-column">
                        <div class="ag-courses_item">
                          <a href="#" class="ag-courses-item_link">
                            <div class="ag-courses-item_bg"></div>

                            <div class="ag-courses-item_title">
                              Activities done
                            </div>

                            <div class="ag-courses-item_date-box">
                              Start:
                            </div>
                          </a>
                        </div>

                        <div class="ag-courses_item">
                          <a href="#" class="ag-courses-item_link">
                            <div class="ag-courses-item_bg"></div>

                            <div class="ag-courses-item_title">
                              Awards redeemed
                            </div>

                            <div class="ag-courses-item_date-box">
                              Start:
                            </div>
                          </a>
                        </div>

                        <div class="ag-courses_item">
                          <a href="#" class="ag-courses-item_link">
                            <div class="ag-courses-item_bg"></div>

                            <div class="ag-courses-item_title">
                              Points earned
                            </div>

                            <div class="ag-courses-item_date-box">
                              Start:
                            </div>
                          </a>
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
