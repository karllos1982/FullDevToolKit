using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.Helpers;
using FullDevToolKit.Sys.Contracts.Domains;
using FullDevToolKit.Sys.Contracts.Managers;
using FullDevToolKit.Sys.Domains;
using FullDevToolKit.Sys.Models.Identity;
using FullDevToolKit.Sys.Models.Common;

namespace FullDevToolKit.Sys.Manager
{

    public class IdentityModule : IIdentityModule
    {

        public IdentityModule(IContext context)
        {
            Context = context;
            Domainset = new SystemDomainSet(context);
        }

        public IContext Context { get; set; }

        public ISystemDomainSet Domainset { get; set; }


        public async Task<UserResult> Login(UserLogin model)
        {
            UserResult ret = null;

            string errmsg = "";
            bool invalidpassword = false;
            bool activestatus = true;
            int trys = 0;

            UserResult usermatch = null;

            await LocalizationText.LoadDataSync(Domainset.User.Context, true);

            usermatch = await Domainset.User.GetByEmail(model.Email);

            if (Domainset.User.Context.Status.Success)
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
                                    Domainset.User.Context.LocalizationLanguage).Text, trys.ToString());

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
                                        Domainset.User.Context.LocalizationLanguage).Text;
                                }

                            }

                        }
                        else
                        {
                            errmsg = LocalizationText.Get("Login-Inactive-Account", 
                                Domainset.User.Context.LocalizationLanguage).Text;

                        }
                    }
                    else
                    {
                        errmsg =
                            LocalizationText.Get("Login-Locked-Account", 
                                Domainset.User.Context.LocalizationLanguage).Text;
                    }
                }
                else
                {
                    errmsg =
                         LocalizationText.Get("Login-User-NotFound",
                            Domainset.User.Context.LocalizationLanguage).Text;
                }

                if (errmsg == "")
                {
                    usermatch.Permissions
                        = await this.GetUserPermissions(usermatch.Roles[0].RoleID, usermatch.UserID);

                    ret = usermatch;
                }

                if (invalidpassword)
                {
                    await Domainset.User.UpdateLoginFailCounter(new UpdateUserLoginFailCounter()
                    { UserID = usermatch.UserID.ToString(), ActiveStatus = activestatus, Reset = false });
                    Domainset.User.Context.Commit(); 

                }

            }
            else
            {
                errmsg 
                    = LocalizationText.Get("Login-User-NotFound", Domainset.User.Context.LocalizationLanguage).Text;
            }

            if (errmsg != "")
            {
                Domainset.User.Context.Status.SetFailStatus("Error", errmsg);                    
            }

            return ret;
        }

        public async Task<List<UserPermissions>> GetUserPermissions(long roleid, long userid)
        {
            List<UserPermissions> ret = new List<UserPermissions>();
            List<PermissionResult> list
                = await Domainset.Permission.GetPermissionsByRoleUser(roleid, userid);

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

            await Domainset.User.UpdateUserLogin(stateinfo);

            SessionLogEntry session = new SessionLogEntry();
            session.SessionLogID = Helpers.Utilities.GenerateId(); 
            session.UserID = stateinfo.UserID;
            session.Date = DateTime.Now;
            session.IP = model.ClientIP;
            session.BrowserName = model.ClienteBrowserName;
            session.DateLogout = null;

            await Domainset.SessionLog.Set(session, stateinfo.UserID);

        }


        public async Task Logout(long userid)
        {
            await Domainset.User.SetDateLogout(userid);
        }

        public async Task<UserEntry> CreateNewUser(NewUser data, bool gocommit, object userid)
        {
            UserEntry ret = null;

            await LocalizationText.LoadDataSync(Domainset.User.Context, true);

            Domainset.User.Context.Status
                = PrimaryValidation.Execute(data, new List<string>(), Domainset.User.Context.LocalizationLanguage);

            if (!Domainset.User.Context.Status.Success)
            {

                if (data.InstanceID == 0)
                {
                    Domainset.User.Context.Status.SetFailStatus("InstanceID",
                        LocalizationText.Get("Validation-NotNull", Domainset.User.Context.LocalizationLanguage).Text);
                }

                if (data.RoleID == 0)
                {
                    Domainset.User.Context.Status.SetFailStatus("RoleID",
                        LocalizationText.Get("Validation-NotNull", Domainset.User.Context.LocalizationLanguage).Text);               
                }

            }

            if (Domainset.User.Context.Status.Success)
            {
                UserResult old = new UserResult();
                UserEntry obj;

                old = await Domainset.User.GetByEmail(data.Email);

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

                    ret = await Domainset.User.Set(obj, userid);

                    if (Domainset.User.Context.Status.Success)
                    {
                        var aux
                            = await Domainset.User.AddRoleToUser(ret.UserID, data.RoleID);

                        if (aux != null)
                        {
                            var aux2
                            = await Domainset.User.AddInstanceToUser(ret.UserID, data.InstanceID);

                        }

                    }

                }
                else
                {
                    Domainset.User.Context.Status.SetFailStatus("Error",
                       LocalizationText.Get("User-Exists", Domainset.User.Context.LocalizationLanguage).Text + data.Email) ;
                  
                }
            }
            else
            {
                Domainset.User.Context.Status.SetFailStatus("Error",
                     LocalizationText.Get("Validation-Error", Domainset.User.Context.LocalizationLanguage).Text);

            
            }


            return ret;
        }


        public async Task<string> GetTemporaryPassword(ChangeUserPassword model)
        {
            string ret = "";

            ret = await Domainset.User.SetPasswordRecoveryCode(model);

            return ret;
        }

        public async Task<string> GetActiveAccountCode(ActiveUserAccount model)
        {
            string ret = "";

            ret = await Domainset.User.SetPasswordRecoveryCode(new ChangeUserPassword()
                     { Email = model.Email, ToActivate = true });

            return ret;
        }

        public async Task<string> GetChangePasswordCode(ChangeUserPassword model)
        {
            string ret = "";

            ret = await Domainset.User.SetPasswordRecoveryCode(model);

            return ret;
        }

        public async Task ChangeUserProfileImage(ChangeUserImage model)
        {
           
             await Domainset.User.ChangeUserProfileImage(model);
                       
        }

        public async Task<bool> ChangeUserLanguage(ChangeUserLanguage model)
        {
            bool ret = false;

            await Domainset.User.ChangeUserLanguage(model);

            return ret; 
        }

        public async Task<ConfigsResult> GetConfigByName(string name)
        {
            return await Domainset.Configs.GetConfigByName(name);
        }

    }

}
