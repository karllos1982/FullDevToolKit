using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.Helpers;
using FullDevToolKit.Sys.Contracts.Domains;
using FullDevToolKit.Sys.Contracts.Managers;
using FullDevToolKit.Sys.Models.Identity;


namespace FullDevToolKit.Sys.Manager
{

    public class IdentityModule : IIdentityModule
    {

        public IdentityModule(ISystemDomainSet domainset)
        {
            _domainset = domainset;
        }

        private ISystemDomainSet _domainset;


        public async Task<UserResult> Login(UserLogin model)
        {
            UserResult ret = null;

            string errmsg = "";
            bool invalidpassword = false;
            bool activestatus = true;
            int trys = 0;

            UserResult usermatch = null;

            usermatch = await _domainset.User.GetByEmail(model.Email);

            if (_domainset.User.Context.Status.Success)
            {
                if (usermatch != null)
                {
                    if (!usermatch.IsLocked)
                    {
                        if (usermatch.IsActive)
                        {

                            if (usermatch.Password == MD5.BuildMD5(model.Password + usermatch.Salt))
                            {

                            }
                            else
                            {
                                trys = 5 - usermatch.LoginFailCounter;

                                errmsg = string.Format(LocalizationText.Get("Login-Invalid-Password",
                                    _domainset.User.Context.LocalizationLanguage).Text, trys.ToString());

                                invalidpassword = true;

                                if (usermatch.PasswordRecoveryCode != null)
                                {
                                    if (usermatch.PasswordRecoveryCode.Length > 0)
                                    {
                                        if (MD5.BuildMD5(usermatch.PasswordRecoveryCode) == model.Password)
                                        {
                                            errmsg = "";
                                            invalidpassword = false;
                                        }
                                    }
                                }

                                if (usermatch.LoginFailCounter == 5)
                                {
                                    activestatus = false;
                                    errmsg = LocalizationText.Get("Login-Attempts", 
                                        _domainset.User.Context.LocalizationLanguage).Text;
                                }

                            }

                        }
                        else
                        {
                            errmsg = LocalizationText.Get("Login-Inactive-Account", 
                                _domainset.User.Context.LocalizationLanguage).Text;

                        }
                    }
                    else
                    {
                        errmsg =
                            LocalizationText.Get("Login-Locked-Account", 
                                _domainset.User.Context.LocalizationLanguage).Text;
                    }
                }
                else
                {
                    errmsg =
                         LocalizationText.Get("Login-User-NotFound",
                            _domainset.User.Context.LocalizationLanguage).Text;
                }

                if (errmsg == "")
                {
                    usermatch.Permissions
                        = await this.GetUserPermissions(usermatch.Roles[0].RoleID, usermatch.UserID);

                    ret = usermatch;
                }

                if (invalidpassword)
                {
                    await _domainset.User.UpdateLoginFailCounter(new UpdateUserLoginFailCounter()
                    { UserID = usermatch.UserID.ToString(), ActiveStatus = activestatus, Reset = false });
                }

            }
            else
            {
                errmsg 
                    = LocalizationText.Get("Login-User-NotFound", _domainset.User.Context.LocalizationLanguage).Text;
            }

            if (errmsg != "")
            {
                _domainset.User.Context.Status.SetFailStatus("Error", errmsg);                    
            }

            return ret;
        }

        public async Task<List<UserPermissions>> GetUserPermissions(long roleid, long userid)
        {
            List<UserPermissions> ret = new List<UserPermissions>();
            List<PermissionResult> list
                = await _domainset.Permission.GetPermissionsByRoleUser(roleid, userid);

            foreach (PermissionResult item in list)
            {
                ret.Add(new UserPermissions()
                {
                    PermissionID = item.PermissionID,
                    ObjectPermissionID = item.ObjectPermissionID,
                    ObjectCode = item.ObjectCode,
                    ReadStatus = item.ReadStatus,
                    SaveStatus = item.SaveStatus,
                    DeleteStatus = item.DeleteStatus,
                    TypeGrant = item.TypeGrant
                });
            }

            return ret;
        }

        public async Task<PERMISSION_STATE_ENUM> CheckPermission(List<UserPermissions> permissions,
          string objectcode, PERMISSION_CHECK_ENUM type)
        {
            PERMISSION_STATE_ENUM ret = PERMISSION_STATE_ENUM.NONE;
            
            ret = Utilities.CheckPermission(permissions, objectcode, type);
                        
            return ret;
        }

        public async Task<PermissionsState> GetPermissionsState(List<UserPermissions> permissions,
         string objectcode, bool allownone)
        {
            PermissionsState ret = new PermissionsState(false, false, false);

            ret = Utilities.GetPermissionsState(permissions, objectcode, allownone);

            return ret;
        }

        public async Task RegisterLoginState(UserLogin model, UpdateUserLogin stateinfo)
        {

            await _domainset.User.UpdateUserLogin(stateinfo);

            SessionLogEntry session = new SessionLogEntry();
            session.SessionLogID = 0;
            session.UserID = stateinfo.UserID;
            session.Date = DateTime.Now;
            session.IP = model.ClientIP;
            session.BrowserName = model.ClienteBrowserName;
            session.DateLogout = null;

            await _domainset.SessionLog.Set(session, stateinfo.UserID);

        }


        public async Task Logout(long userid)
        {
            await _domainset.User.SetDateLogout(userid);
        }

        public async Task<UserEntry> CreateNewUser(NewUser data, bool gocommit, object userid)
        {
            UserEntry ret = null;

            _domainset.User.Context.Status
                = PrimaryValidation.Execute(data, new List<string>(), _domainset.User.Context.LocalizationLanguage);

            if (!_domainset.User.Context.Status.Success)
            {

                if (data.InstanceID == 0)
                {
                    _domainset.User.Context.Status.SetFailStatus("InstanceID",
                        LocalizationText.Get("Validation-NotNull", _domainset.User.Context.LocalizationLanguage).Text);
                }

                if (data.RoleID == 0)
                {
                    _domainset.User.Context.Status.SetFailStatus("RoleID",
                        LocalizationText.Get("Validation-NotNull", _domainset.User.Context.LocalizationLanguage).Text);               
                }

            }

            if (_domainset.User.Context.Status.Success)
            {
                UserResult old = new UserResult();
                UserEntry obj;

                old = await _domainset.User.GetByEmail(data.Email);

                string pwd = MD5.BuildMD5(data.Password);
                string slt = Utilities.GenerateCode(5);

                if (old == null)
                {
                    obj = new UserEntry();
                    obj.UserID = 0;
                    obj.UserName = data.UserName;
                    obj.ApplicationID = 0;
                    obj.Email = data.Email;
                    obj.Password = MD5.BuildMD5(pwd + slt);
                    obj.Salt = slt;
                    obj.CreateDate = DateTime.Now;
                    obj.IsActive = false;
                    obj.IsLocked = false;
                    obj.DefaultLanguage = data.DefaultLanguage;
                    obj.LastLoginDate = DateTime.Now;
                    obj.LastLoginIP = null;
                    obj.LoginCounter = 0;
                    obj.LoginFailCounter = 0;
                    obj.AuthCode = null;
                    obj.AuthCodeExpires = DateTime.Now;
                    obj.PasswordRecoveryCode = null;
                    obj.ProfileImage = null;
                    obj.AuthUserID = null;

                    ret = await _domainset.User.Set(obj, userid);

                    if (_domainset.User.Context.Status.Success)
                    {
                        var aux
                            = await _domainset.User.AddRoleToUser(ret.UserID, data.RoleID);

                        if (aux != null)
                        {
                            var aux2
                            = await _domainset.User.AddInstanceToUser(ret.UserID, data.InstanceID);

                        }

                    }

                }
                else
                {
                    _domainset.User.Context.Status.SetFailStatus("Error",
                       LocalizationText.Get("User-Exists", _domainset.User.Context.LocalizationLanguage).Text + data.Email) ;
                  
                }
            }
            else
            {
                _domainset.User.Context.Status.SetFailStatus("Error",
                     LocalizationText.Get("Validation-Error", _domainset.User.Context.LocalizationLanguage).Text);

            
            }


            return ret;
        }


        public async Task<string> GetTemporaryPassword(ChangeUserPassword model)
        {
            string ret = "";

            ret = await _domainset.User.SetPasswordRecoveryCode(model);

            return ret;
        }

        public async Task<string> GetActiveAccountCode(ActiveUserAccount model)
        {
            string ret = "";

            ret = await _domainset.User.SetPasswordRecoveryCode(new ChangeUserPassword()
                     { Email = model.Email, ToActivate = true });

            return ret;
        }

        public async Task<string> GetChangePasswordCode(ChangeUserPassword model)
        {
            string ret = "";

            ret = await _domainset.User.SetPasswordRecoveryCode(model);

            return ret;
        }

        public async Task ChangeUserProfileImage(ChangeUserImage model)
        {
           
             await _domainset.User.ChangeUserProfileImage(model);
                       
        }

    }

}
