<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DefaultAdmin.aspx.cs" Inherits="Rewards.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
	    h3::before {
		    display: none;
	    }

        .form-control::placeholder{
            color: dimgray;
        }
    </style>

     <main class="container-fluid p-3">
         
          <div class="alert alert-warning alert-dismissible fade show" role="alert" style="display:none;">
        </div>
         <div class="row pb-4">
             <div class="col-5 overflow-hidden">
                 <!-- ACTIVITIES -->
                 <div id="addActivityModal" class="modal fade" role="dialog">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header d-flex flex-column">
                                <div class="d-flex align-items-center mb-1">
                                    <span class="d-flex align-items-center"><img width="30" height="30" src="icon/list-check-solid.svg"/></span>
                                    <h1 class="modal-title">Add Activity</h1>
                                </div>
                            </div>
                            <div class="modal-body">
                                <p class="m-0">Name:</p>
                                <asp:TextBox ID="ActivityName" CssClass="form-control mb-2" placeholder="Name" runat="server"></asp:TextBox>
                                <p class="m-0">Description:</p>
                                <asp:TextBox ID="ActivityDescription" TextMode="MultiLine" Rows="4" CssClass="form-control mb-2" placeholder="Description" runat="server"></asp:TextBox>
                                <p class="m-0">Points:</p>
                                <asp:TextBox ID="ActivityPoints" TextMode="Number" CssClass="form-control mb-2" placeholder="Points earned by doing this activity" runat="server"></asp:TextBox>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-success" data-bs-dismiss="modal">Close</button>
                                <asp:Button ID="btnComfirmAddActivity" OnClick="btnComfirmAddActivity_Click" CssClass="btn btn-success" runat="server" Text="Add Activity" />
                            </div>
                        </div>
                    </div>
                </div>

                 <div id="editActivityModal" class="modal fade" role="dialog">
                    <div class="modal-dialog">
                        <div class="modal-content">
                           <div class="modal-header d-flex flex-column">
                               <div class="d-flex align-items-center mb-1">
                                   <h1 class="modal-title">Edit Activity</h1>
                               </div>
                           </div>
                            <div class="modal-body">
                                <asp:HiddenField ID="hiddenActivityID" runat="server" />
                                <p class="mb-0">Name:</p>
                                <asp:TextBox ID="newActivityName" CssClass="form-control mb-2" placeholder="New Name" runat="server"></asp:TextBox>
                                <p class="mb-0">Description:</p>
                                <asp:TextBox ID="newActivityDescription" TextMode="MultiLine" Rows="4" CssClass="form-control mb-2" placeholder="New Description" runat="server"></asp:TextBox>
                                <p class="mb-0">Points:</p>
                                <asp:TextBox ID="newActivityPoints" TextMode="Number" CssClass="form-control mb-2" placeholder="New Points" runat="server"></asp:TextBox>
                                <p class="mb-0">Activated:</p>
                                <asp:DropDownList ID="dlActivityStatus" CssClass="form-control mb-2" runat="server">
                                    <asp:ListItem Text="True" Value="True"></asp:ListItem>
                                    <asp:ListItem Text="False" Value="False"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-success" data-bs-dismiss="modal">Close</button>
                                <asp:Button ID="btnComfirmActivityChanges" OnClick="btnComfirmActivityChanges_Click" CssClass="btn btn-success" runat="server" Text="Save Changes" />
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
                            <asp:ListView ID="lvActivity" runat="server" OnItemDataBound="lvActivity_ItemDataBound">
                                <ItemTemplate>
                                    <li class="d-flex justify-content-between <%# Eval("ItemClass") %>">
                                        <asp:HiddenField ID="activityIdLiteral" runat="server" />
                                        <h3><asp:Literal runat="server" ID="activityNameLiteral"></asp:Literal> <span class="mx-2 p-1"><asp:Literal runat="server" ID="pointsLiteral"></asp:Literal>  points</span> </h3>
                                        <asp:Button ID="btnEditActivity" OnClick="btnEditActivity_Click" runat="server" CssClass="btn btn-success py-1 px-2 submit-btn" Text="Edit" data-bs-toggle="modal" data-bs-target="#editActivityModal" />
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
                                        <span class="d-flex align-items-center"><img width="30" height="30" src="icon/user-solid.svg"/></span>
                                       <h1 class="modal-title">Add User</h1>
                                   </div>
                               </div>
                                <div class="modal-body">
                                    <p class="m-0">Name:</p>
                                    <asp:TextBox ID="UserName" CssClass="form-control mb-2" placeholder="Name" runat="server"></asp:TextBox>
                                    <p class="m-0">Email:</p>
                                    <asp:TextBox ID="UserEmail" CssClass="form-control mb-2" placeholder="Email" runat="server"></asp:TextBox>
                                    <p class="m-0">Role:</p>
                                    <asp:DropDownList ID="dlRoleUser" onchange="hideManagerEmail2()" CssClass="form-control mb-2" runat="server">
                                        <asp:ListItem Text="EMPLOYEE" Value="EMPLOYEE"></asp:ListItem>
                                        <asp:ListItem Text="MANAGER" Value="MANAGER"></asp:ListItem>
                                        <asp:ListItem Text="ADMIN" Value="ADMIN"></asp:ListItem>
                                    </asp:DropDownList>
                                    <p class="m-0" id="managerEmailLabel2">Manager Email:</p>
                                    <asp:TextBox ID="managerEmailTextBox" CssClass="form-control mb-2" placeholder="Manager Email" runat="server"></asp:TextBox>
                                    <p class="m-0">Image:</p>
                                    <asp:FileUpload ID="FileUpload1" CssClass="form-control" runat="server" accept="image/jpeg, image/png, image/jpg" />
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-success" data-bs-dismiss="modal">Close</button>
                                    <asp:Button ID="btnComfirmAddUser" OnClick="btnComfirmAddUser_Click" CssClass="btn btn-success" runat="server" Text="Add User" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <div id="editUserModal" class="modal fade" role="dialog">
                        <div class="modal-dialog">
                            <div class="modal-content">
                               <div class="modal-header d-flex flex-column">
                                   <div class="d-flex align-items-center mb-1">
                                       <h1 class="modal-title">Edit user</h1>
                                   </div>
                               </div>
                                <div class="modal-body">
                                    <p class="mb-0">ID:</p>
                                    <asp:TextBox ID="ID" CssClass="form-control mb-2" placeholder="ID" ReadOnly="true" runat="server"></asp:TextBox>
                                    <p class="mb-0">Name:</p>
                                    <asp:TextBox ID="newName" CssClass="form-control mb-2" placeholder="New Name" runat="server"></asp:TextBox>
                                    <p class="mb-0">Email:</p>
                                    <asp:TextBox ID="newEmail" CssClass="form-control mb-2" placeholder="New Email" runat="server"></asp:TextBox>
                                    <p class="mb-0">Role:</p>
                                    <asp:DropDownList ID="dlRole" onchange="hideManagerEmail()" CssClass="form-control mb-2" runat="server">
                                        <asp:ListItem Text="EMPLOYEE" Value="EMPLOYEE"></asp:ListItem>
                                        <asp:ListItem Text="MANAGER" Value="MANAGER"></asp:ListItem>
                                        <asp:ListItem Text="ADMIN" Value="ADMIN"></asp:ListItem>
                                    </asp:DropDownList>
                                    <p class="mb-0" runat="server" id="managerEmailLabel">Manager Email:</p>
                                    <asp:TextBox ID="newManagerEmail" CssClass="form-control mb-2" placeholder="New Manager Email" runat="server"></asp:TextBox>
                                    <p class="m-0">Image:</p>
                                    <asp:FileUpload ID="userFileUpload" runat="server" style="display: none;" onchange="previewImage('MainContent_userFileUpload', 'MainContent_UserImagePlaceholder')" accept="image/jpeg, image/png, image/gif, image/jpg" />
                                    <img id="UserImagePlaceholder" runat="server" width="130" height="130" src="none" onclick="document.getElementById('MainContent_userFileUpload').click();" style="cursor: pointer;" />
                                    <p class="m-0">Activated:</p>
                                    <asp:DropDownList ID="dlUserActivated" CssClass="form-control mb-2" runat="server">
                                        <asp:ListItem Text="True" Value="True"></asp:ListItem>
                                        <asp:ListItem Text="False" Value="False"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-success" data-bs-dismiss="modal">Close</button>
                                    <asp:Button ID="btnComfirmUserChanges" OnClick="btnComfirmUserChanges_Click" CssClass="btn btn-success" runat="server" Text="Save Changes" />
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
                            <asp:ListView ID="lvUsers" runat="server" OnItemDataBound="lvUsers_ItemDataBound">
                                <ItemTemplate>
                                    <div class="userCard <%# Eval("ItemClass") %>">
                                        <div class="userImgPlaceholder">
                                             <asp:Image ID="profileImage" runat="server" />
                                        </div>
                                        <div class="userInfo">
                                            <div>
                                                <asp:HiddenField ID="userId" runat="server" />
                                                <p class="userName"><asp:Literal runat="server" ID="litUserName" Text='' /></p>
                                            </div>
                                            <p><asp:Literal runat="server" ID="litPoints" Text='' /> points</p>
                                        </div>
                                        <asp:Button ID="btnEditUser" runat="server" CssClass="btn btn-success py-2 px-3 editUser" Text="Edit"
                                            OnClick="btnEditUser_Click" data-bs-target="#editUserModal" data-bs-toggle="modal"/>
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
                                    <p class="m-0">Name:</p>
                                    <asp:TextBox ID="txbRewardName" CssClass="form-control mb-2" placeholder="Reward Name" runat="server"></asp:TextBox>
                                    <p class="m-0">Price:</p>
                                    <asp:TextBox ID="txbRewardPrice" TextMode="Number" CssClass="form-control mb-2" placeholder="Reward Price" runat="server"></asp:TextBox>
                                    <p class="m-0">Image:</p>
                                    <asp:FileUpload ID="RewardImage" CssClass="form-control" runat="server" accept="image/jpeg, image/png, image/jpg" />
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-success" data-bs-dismiss="modal">Close</button>
                                    <asp:Button ID="btnComfirmAddReward" OnClick="btnComfirmAddReward_Click" CssClass="btn btn-success" runat="server" Text="Add Reward" />
                                </div>
                            </div>
                        </div>
                    </div>


                    <div id="editRewardModal" class="modal fade" role="dialog">
                        <div class="modal-dialog">
                            <div class="modal-content">
                               <div class="modal-header d-flex flex-column">
                                   <div class="d-flex align-items-center mb-1">
                                       <h1 class="modal-title">Edit Reward</h1>
                                   </div>
                               </div>
                                <div class="modal-body">
                                    <asp:HiddenField ID="rewardID" runat="server" />
                                    <p class="m-0">Current Stock: <asp:Label ID="labelCurrentStock" runat="server" Text="0"></asp:Label></p>
                                    <asp:TextBox ID="txtInsertStock" TextMode="Number" CssClass="form-control mb-2" placeholder="Add Stock" runat="server"></asp:TextBox>
                                    <p class="m-0">Name:</p>
                                    <asp:TextBox ID="txtRewardName" CssClass="form-control mb-2" placeholder="Type the name" runat="server"></asp:TextBox>
                                    <p class="m-0">Price:</p>
                                    <asp:TextBox ID="txtRewardPrice" TextMode="Number" CssClass="form-control mb-2" placeholder="Type the price" runat="server"></asp:TextBox>
                                    <p class="m-0">Image</p>
                                    <asp:FileUpload ID="rewardFileUpload" runat="server" style="display: none;" onchange="previewImage('MainContent_rewardFileUpload', 'MainContent_RewardImagePlaceholder')" accept="image/jpeg, image/png, image/jpg" />
                                    <img id="RewardImagePlaceholder" runat="server" width="130" height="130" src="none" onclick="document.getElementById('MainContent_rewardFileUpload').click();" style="cursor: pointer;" />
                                    <p class="m-0">Activated</p>
                                    <asp:DropDownList ID="dlRewardStatus" CssClass="form-control mb-2" runat="server">
                                        <asp:ListItem Text="True" Value="True"></asp:ListItem>
                                        <asp:ListItem Text="False" Value="False"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-success" data-bs-dismiss="modal">Close</button>
                                    <asp:Button ID="btnComfirmRewardChanges" OnClick="btnComfirmRewardChanges_Click" CssClass="btn btn-success" runat="server" Text="Save Changes" />
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
                             <div id="addRewardCard" class="d-flex justify-content-center align-items-end">
                                <asp:Button ID="btnAddReward" CssClass="btn btn-success py-2 px-3 mb-3" OnClientClick="return false;" data-bs-target="#addRewardModal" data-bs-toggle="modal" runat="server" Text="Add Reward" />
                            </div>
                                <asp:ListView ID="lvRewards" runat="server" OnItemDataBound="lvRewards_ItemDataBound">
                                    <ItemTemplate>
                                        <div class="<%# Eval("ItemClass") %> card">
                                            <div class="imgBx">
                                                <asp:Image ID="rewardImage" runat="server" Width="100" Height="100" />
                                            </div>
                                            <div class="contentBx">
                                                <asp:HiddenField ID="rewardIDHiddenField" runat="server" />
                                                <asp:HiddenField ID="rewardStatusHiddenField" runat="server" />
                                                <h2><asp:Literal runat="server" ID="rewardNameLiteral"></asp:Literal></h2>
                                                <h4><asp:Literal runat="server" ID="rewardPriceLiteral"></asp:Literal> Points</h4>
                                                <asp:Button ID="btnEditReward" OnClick="btnEditReward_Click" CssClass="btn btn-success py-2 px-3" runat="server" Text="Edit" data-bs-target="#editRewardModal" data-bs-toggle="modal"  />
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:ListView>
                         </div>
                     </div>
                 </div>
             </div>
         </div>

         <script>

             function showEditActivityModal() {
                 var myModal = new bootstrap.Modal(document.getElementById('editActivityModal'))
                 myModal.show()
             }

             function showEditUserModal() {
                 var myModal = new bootstrap.Modal(document.getElementById('editUserModal'))
                 myModal.show()
             }

             function showEditRewardModal() {
                 var myModal = new bootstrap.Modal(document.getElementById('editRewardModal'))
                 myModal.show()
             }

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

             function previewImage(inputT, imageT) {
                 var input = document.getElementById(inputT);
                 var image = document.getElementById(imageT);

                 if (input.files && input.files[0]) {
                     var reader = new FileReader();

                     reader.onload = function (e) {
                         image.src = e.target.result;
                     };

                     reader.readAsDataURL(input.files[0]);
                 }
             }

             function hideManagerEmail() {
                    var selectedRole = document.getElementById("<%= dlRole.ClientID %>").value;
                     var managerEmailLabel = document.getElementById("<%= managerEmailLabel.ClientID %>");
                     var newManagerEmail = document.getElementById("<%= newManagerEmail.ClientID %>");

                     if (selectedRole !== "EMPLOYEE") {
                         managerEmailLabel.style.display = "none";
                         newManagerEmail.style.display = "none";
                     } else {
                         managerEmailLabel.style.display = "block";
                         newManagerEmail.style.display = "block";
                     }
             }

             function hideManagerEmail2() {
                    var selectedRole = document.getElementById("<%= dlRoleUser.ClientID %>").value;
                    var managerEmailLabel2 = document.getElementById("managerEmailLabel2");
                     var managerEmailTextBox = document.getElementById("<%= managerEmailTextBox.ClientID %>");

                     if (selectedRole !== "EMPLOYEE") {
                         managerEmailLabel2.style.display = "none";
                         managerEmailTextBox.style.display = "none";
                     } else {
                         managerEmailLabel2.style.display = "block";
                         managerEmailTextBox.style.display = "block";
                     }
             }

             function messageAlert(text) {
                 $('.alert').html(text);
                 $('.alert').append('<button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>');
                 $('.alert').show();
                 setTimeout(function () {
                     $('.alert').hide();
                 }, 5000);
             }

             function clearModalFields() {
                     $('#MainContent_txtInsertStock').val('');
                     $('#MainContent_ActivityName').val('');
                     $('#MainContent_ActivityDescription').val('');
                     $('#MainContent_ActivityPoints').val('');

                     $('#MainContent_UserName').val('');
                     $('#MainContent_UserEmail').val('');
                     $('#MainContent_dlRoleUser').val('EMPLOYEE');
                     $('#MainContent_managerEmailTextBox').val('');
                     $('#MainContent_FileUpload1').val('');

                     $('#MainContent_txbRewardName').val('');
                     $('#MainContent_txbRewardPrice').val('');
                     $('#MainContent_RewardImage').val('');
             }
         </script>


     </main>

</asp:Content>
