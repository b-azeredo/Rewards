<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DefaultAdmin.aspx.cs" Inherits="Rewards.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <main class="container-fluid p-3">
         <div class="row pb-4">
             <div class="col-5 overflow-hidden">
                 <!-- ACTIVITIES -->

                 <div id="addActivityModal" class="modal fade" role="dialog">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header d-flex flex-column">
                                <div class="d-flex align-items-center mb-1">
                                    <h1 class="modal-title">Add Activity</h1>
                                </div>
                            </div>
                            <div class="modal-body">
                                <asp:TextBox ID="ActivityName" CssClass="form-control mb-2" placeholder="Name" runat="server"></asp:TextBox>
                                <asp:TextBox ID="ActivityDescription" CssClass="form-control mb-2" placeholder="Description" runat="server"></asp:TextBox>
                                <asp:TextBox ID="ActivityPoints" CssClass="form-control mb-2" placeholder="Points earned by doing this activity" runat="server"></asp:TextBox>
                                <asp:TextBox ID="ActivityLimit" CssClass="form-control mb-2" placeholder="Limit per week" runat="server"></asp:TextBox>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-success" data-bs-dismiss="modal">Close</button>
                                <asp:Button ID="btnComfirmAddActivity" OnClick="btnComfirmAddActivity_Click" CssClass="btn btn-success" runat="server" Text="Add Activity" />
                            </div>
                        </div>
                    </div>
                </div>

                 <div class="content" style="max-height:100%;">
                     <div class="d-flex pb-2">
                         <span class="d-flex align-items-center"><img width="30" height="30" src="icon/list-check-solid.svg"/></span>
                         <h1>Activities</h1>
                     </div>
                     <div class="overflow-auto" style="max-height:85%;">
                         <ol style="--length: 0" role="list">
                            <li class="d-flex justify-content-between align-items-center">
                                <h3 id="h3AddActivity"> Add Activity </h3>
                                <asp:Button ID="btnAddActivity" CssClass="btn btn-success py-2 px-3" OnClientClick="return false;" data-bs-target="#addActivityModal" data-bs-toggle="modal" runat="server" Text="+" />
                            </li>
                             <asp:ListView ID="lvActivity" runat="server">
                                 <ItemTemplate>
                                     <li class="d-flex justify-content-between">
                                         <h3><%# Eval("NAME") %> <span class="mx-2 p-1"><%# Eval("POINTS") %>  points</span> </h3>
                                        <asp:Button ID="btnEditActivity" CssClass="btn btn-success py-2 px-3" OnClientClick="return false;" data-bs-target="#" data-bs-toggle="modal" runat="server" Text="Edit" />
                                     </li>
                                 </ItemTemplate>
                             </asp:ListView>
                         </ol>
                     </div>
                 </div>
             </div>
             <div class="col-7 overflow-hidden">
                 <!-- USERS -->
                    <div id="addUserModal" class="modal fade" role="dialog">
                        <div class="modal-dialog">
                            <div class="modal-content">
                               <div class="modal-header d-flex flex-column">
                                   <div class="d-flex align-items-center mb-1">
                                       <h1 class="modal-title">Add User</h1>
                                   </div>
                               </div>
                                <div class="modal-body">
                                    <asp:TextBox ID="UserName" CssClass="form-control mb-2" placeholder="Name" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="UserEmail" CssClass="form-control mb-2" placeholder="Email" runat="server"></asp:TextBox>
                                        <asp:DropDownList ID="dlRoleUser" CssClass="form-control mb-2" runat="server">
                                            <asp:ListItem Text="EMPLOYEE" Value="EMPLOYEE"></asp:ListItem>
                                            <asp:ListItem Text="MANAGER" Value="MANAGER"></asp:ListItem>
                                            <asp:ListItem Text="ADMIN" Value="ADMIN"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:TextBox ID="managerEmailTextBox" CssClass="form-control mb-2" placeholder="Manager Email" runat="server"></asp:TextBox>
                                        <asp:FileUpload ID="FileUpload1" CssClass="form-control" runat="server" />
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-success" data-bs-dismiss="modal">Close</button>
                                    <asp:Button ID="btnComfirmAddUser" OnClick="btnComfirmAddUser_Click" CssClass="btn btn-success" runat="server" Text="Add User" />
                                </div>
                            </div>
                        </div>
                    </div>
                    

                 <div class="content">
                     <div class="d-flex pb-2">
                         <span class="d-flex align-items-center"><img width="30" height="30" src="icon/user-solid.svg"/></span>
                         <h1>Users</h1>
                     </div>
                     <div class="overflow-auto d-flex flex-column align-items-center" style="height: 85%;">
                            <div class="userCard justify-content-between align-items-center">
                                <p class="m-0">Add user</p>
                                <asp:Button ID="btnAddUser" CssClass="btn btn-success py-2 px-3" OnClientClick="return false;" data-bs-target="#addUserModal" data-bs-toggle="modal" runat="server" Text="+" />
                            </div>
                             <asp:ListView ID="lvUsers" runat="server">
                                 <ItemTemplate>
                                     <div class="userCard">
                                         <div class="userImgPlaceholder">
                                             <img src='<%# "data:image;base64," + Convert.ToBase64String(Eval("PROFILE_IMAGE") as byte[]) %>' />
                                         </div>
                                         <div class="userInfo">
                                               <div>
                                                     <p>ID: <%# Eval("ID") %></p>
                                                     <p class="userName"><%# Eval("USERNAME") %></p>
                                               </div>
                                             <p>
                                                 <%# Eval("POINTS") %> points</p>
                                         </div>
                                        <asp:Button ID="btnEditUser" CssClass="btn btn-success py-2 px-3" OnClientClick="return false;" data-bs-target="#" data-bs-toggle="modal" runat="server" Text="Edit" />
                                     </div>
                                 </ItemTemplate>
                             </asp:ListView>
                     </div>
                 </div>
             </div>
         </div>
         <div class="row">
             <div class="col-5">
                 <!-- LEADERBOARD -->
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
                                         <p class="userName">
                                             <%# Eval("USERNAME") %>
                                         </p>
                                         <p>
                                             <%# Eval("POINTS") %> points</p>
                                     </div>
                                 </div>
                             </ItemTemplate>
                         </asp:ListView>
                     </div>
                 </div>

             </div>
             <div class="col-7">
                 <div class="content">
                     <!-- REWARDS -->
                    <div id="addRewardModal" class="modal fade" role="dialog">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header d-flex flex-column">
                                    <div class="d-flex align-items-center mb-3">
                                        <span class="d-flex align-items-center"><img width="30" height="30" src="icon/award-solid.svg"/></span>
                                        <h1 class="modal-title">Add Reward</h1>
                                    </div>
                                </div>
                                <div class="modal-body">
                                    <asp:TextBox ID="txbRewardName" CssClass="form-control mb-2" placeholder="Reward Name" runat="server"></asp:TextBox>
                                    <asp:TextBox ID="txbRewardPrice" CssClass="form-control mb-2" placeholder="Reward Price" runat="server"></asp:TextBox>
                                    <asp:FileUpload ID="RewardImage" CssClass="form-control" runat="server" />
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-success" data-bs-dismiss="modal">Close</button>
                                    <asp:Button ID="btnComfirmAddReward" OnClick="btnComfirmAddReward_Click" OnClientClick="return validateFileUpload();" CssClass="btn btn-success" runat="server" Text="Add Reward" />
                                </div>
                            </div>
                        </div>
                    </div>

                     <div class="d-flex pb-2">
                         <span class="d-flex align-items-center"><img width="30" height="30" src="icon/award-solid.svg"/></span>
                         <h1>Rewards</h1>
                     </div>
                     <div class="rewards overflow-auto d-flex justify-content-center container">
                         <div class="cardsContainer container">
                             <asp:ListView ID="lvRewards" runat="server">
                                 <ItemTemplate>
                                     <div class="card <%# Eval("ItemClass") %>">
                                         <div class="imgBx">
                                             <img src="<%# " data:image;base64, " + Convert.ToBase64String(Eval("IMAGE ") as byte[]) %>">
                                         </div>

                                         <div class="contentBx">
                                             <h2><%# Eval("NAME") %></h2>
                                             <h4><%# Eval("PRICE") %> Points</h4>
                                             <asp:Button ID="btnEditReward" CssClass="btn btn-success py-2 px-3" runat="server" Text="Edit" />
                                         </div>
                                     </div>
                                 </ItemTemplate>
                             </asp:ListView>
                                <div id="addRewardCard" class="d-flex justify-content-center align-items-end">
                                    <asp:Button ID="btnAddReward" CssClass="btn btn-success py-2 px-3 mb-3" OnClientClick="return false;" data-bs-target="#addRewardModal" data-bs-toggle="modal" runat="server" Text="Add" />
                                </div>
                         </div>
                     </div>
                 </div>
             </div>
         </div>

         <script>

            $(document).ready(function () {
                $('#MainContent_dlRoleUser').change(function () {
                    if ($(this).val() === "EMPLOYEE") {
                        $('#MainContent_managerEmailTextBox').show();
                    } else {
                        $('#MainContent_managerEmailTextBox').hide();
                    }
                });
            });

             function validateFileUpload() {
                 var fuData = document.getElementById('<%=RewardImage.ClientID%>');
                 var FileUploadPath = fuData.value;

                 if (FileUploadPath == '') {
                     return false;
                 } else {
                     var Extension = FileUploadPath.substring(FileUploadPath.lastIndexOf('.') + 1).toLowerCase();
                     if (Extension == "png" || Extension == "jpeg" || Extension == "jpg") {
                         return true;
                     } else {
                         fuData.value = '';
                         return false;
                     }
                 }
             }
         </script>


     </main>

</asp:Content>
