using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.Sys.Contracts.Domains;
using FullDevToolKit.Sys.Models.Identity;
using FullDevToolKit.Helpers;
using FullDevToolKit.Sys.Contracts.Repositories;
using FullDevToolKit.Sys.Data.Repositories;


namespace FullDevToolKit.Sys.Domains
{
    public class UserDomain : IUserDomain
    {      

        public UserDomain(IContext context)
        {

            Context = context;
            _repositories = new SystemRepositorySet(context);

        }

        public IContext Context { get; set; }

        private ISystemRepositorySet _repositories { get; set; }


        public async Task<UserResult> FillChields(UserResult obj)
        {
            UserRolesParam param = new UserRolesParam();
            param.pUserID = obj.UserID;

            List<UserRolesResult> roles
                = await  _repositories.UserRoles.Search(param);

            obj.Roles = roles;

            //

            UserInstancesParam param2 = new UserInstancesParam();
            param2.pUserID = obj.UserID;

            List<UserInstancesResult> instances
                = await _repositories.UserInstances.Search(param2);

            obj.Instances = instances; 

            return obj;
        }

        public async Task<UserResult> Get(UserParam param)
        {
            UserResult ret = null;

            ret = await _repositories.User.Read(param); 

            if (ret != null)
            {
                ret = await FillChields(ret); 
            }
            
            return ret;
        }

        public async Task<List<UserList>> List(UserParam param)
        {
            List<UserList> ret = null;

            ret = await _repositories.User.List(param);           

            return ret;
        }

        public async Task<List<UserResult>> Search(UserParam param)
        {
            List<UserResult> ret = null;

            ret = await _repositories.User.Search(param);

            return ret;
        }

        public async Task EntryValidation(UserEntry obj)
        {
            ExecutionStatus ret = null;

            ret = PrimaryValidation.Execute(obj, new List<string>(), Context.LocalizationLanguage);        

            if (!ret.Success)
            {
                ret.SetFailStatus("Error",
                  LocalizationText.Get("Validation-Error",
                      Context.LocalizationLanguage).Text);
            }

            Context.Status = ret;

        }

        public async Task InsertValidation(UserEntry obj)
        {
            ExecutionStatus ret = new ExecutionStatus(true);

            bool check =
                await Context.CheckUniqueValueForInsert(_repositories.User.TableName, "Email", obj.Email) ;

            if (!check)
            {
                PrimaryValidation.AddCheckValidationException(ref ret, "Email",
                    LocalizationText.Get("Email-Exists", Context.LocalizationLanguage).Text);           
            }

            bool check2 =
                await Context.CheckUniqueValueForInsert(_repositories.User.TableName, "UserName", obj.UserName);

            if (!check2)
            {               

                PrimaryValidation.AddCheckValidationException(ref ret, "UserName",
                  LocalizationText.Get("User-Exists", Context.LocalizationLanguage).Text);
            }

            Context.Status = ret;

        }

        public async Task UpdateValidation(UserEntry obj)
        {
            ExecutionStatus ret = new ExecutionStatus(true);

            bool check =
              await Context.CheckUniqueValueForUpdate(_repositories.User.TableName, "Email", 
                    obj.Email, _repositories.User.PKFieldName, obj.UserID.ToString());

            if (!check)
            {
                PrimaryValidation.AddCheckValidationException(ref ret, "Email",
                    LocalizationText.Get("Email-Exists", Context.LocalizationLanguage).Text);            
            }

            //

            bool check2 =
                 await Context.CheckUniqueValueForUpdate(_repositories.User.TableName, "UserName",
                    obj.Email, _repositories.User.PKFieldName, obj.UserName);

            if (!check2)
             {               

                PrimaryValidation.AddCheckValidationException(ref ret, "UserName",
                  LocalizationText.Get("User-Exists", Context.LocalizationLanguage).Text);
            }

            Context.Status = ret;

        }

        public async Task DeleteValidation(UserEntry obj)
        {
            Context.Status = new ExecutionStatus(true);
        }

        public async Task<UserEntry> Set(UserEntry model, object userid)
        {
            UserEntry ret = null;
            OPERATIONLOGENUM operation = OPERATIONLOGENUM.INSERT;

           await EntryValidation(model);

            if ( Context.Status.Success)
            {

                UserResult old 
                    = await _repositories.User.Read(new UserParam() { pUserID = model.UserID });

                if (old == null)
                {
                    await InsertValidation(model);

                    if (Context.Status.Success)
                    {
                        model.CreateDate = DateTime.Now;
                        if (model.UserID == 0) { model.UserID = Utilities.GenerateId(); }
                        await _repositories.User.Create(model);
                    }
                }
                else
                {
                    model.CreateDate = old.CreateDate;
                    operation = OPERATIONLOGENUM.UPDATE;

                    await UpdateValidation(model);

                    if (Context.Status.Success)
                    {
                        await _repositories.User.Update(model);
                    }

                }

                if (model.Roles != null)
                {
                    foreach (UserRolesEntry c in model.Roles)
                    {
                        if (c.RecordState != RECORDSTATEENUM.NONE)
                        {
                            c.UserID = model.UserID;

                            if (c.RecordState == RECORDSTATEENUM.ADD)
                            {
                                c.UserRoleID = Utilities.GenerateId();
                                await _repositories.UserRoles.Create(c);
                            }

                            if (c.RecordState == RECORDSTATEENUM.EDITED)
                            {
                                await _repositories.UserRoles.Update(c);
                            }

                            if (c.RecordState == RECORDSTATEENUM.DELETED)
                            {
                                await _repositories.UserRoles.Delete(c);
                            }

                        }
                    }
                }

                if (model.Instances != null)
                {
                    foreach (UserInstancesEntry c in model.Instances)
                    {
                        if (c.RecordState != RECORDSTATEENUM.NONE)
                        {
                            c.UserID = model.UserID;

                            if (c.RecordState == RECORDSTATEENUM.ADD)
                            {
                                c.UserInstanceID = Utilities.GenerateId();
                                await _repositories.UserInstances.Create(c);
                            }

                            if (c.RecordState == RECORDSTATEENUM.EDITED)
                            {
                                await _repositories.UserInstances.Update(c);
                            }

                            if (c.RecordState == RECORDSTATEENUM.DELETED)
                            {
                                await _repositories.UserInstances.Delete(c);
                            }

                        }
                    }
                }

                if (Context.Status.Success && userid != null)
                {
                    await Context
                        .RegisterDataLogAsync(userid.ToString(), operation, "SYSUSER",
                        model.UserID.ToString(), old, model);

                    ret = model;
                }

            }     

            return ret;
        }

     

        public async Task<UserEntry> Delete(UserEntry model, object userid)
        {
            UserEntry ret = null;

            UserResult old 
                = await this.Get(new UserParam() { pUserID = model.UserID });

            if (old != null)
            {
                await DeleteValidation(model);

                if (Context.Status.Success)
                {
                    if (Context.Status.Success && old.Roles != null)
                    {
                        foreach (UserRolesResult u in old.Roles)
                        {
                            await _repositories.UserRoles.Delete(
                                    new UserRolesEntry() { 
                                        UserRoleID = u.UserRoleID,  
                                        UserID  = u.UserID, 
                                        RoleID = u.RoleID
                                    } 
                                );

                            if (!Context.Status.Success)
                            {
                                break;
                            }
                        }
                    }

                    if (Context.Status.Success && old.Instances != null)
                    {
                        foreach (UserInstancesResult u in old.Instances)
                        {
                           await _repositories.UserInstances.Delete(
                                     new UserInstancesEntry()
                                     {
                                         UserInstanceID = u.UserInstanceID, 
                                         UserID = u.UserID, 
                                         InstanceID =   u.InstanceID

                                     }
                               );

                            if (!Context.Status.Success)
                            {
                                break;
                            }
                        }
                    }

                    if (Context.Status.Success)
                    {
                        await _repositories.User.Delete(model);

                        if (Context.Status.Success && userid != null)
                        {
                            await Context
                                    .RegisterDataLogAsync(userid.ToString(), OPERATIONLOGENUM.DELETE, "SYSUSER",
                                      model.UserID.ToString(), old, model);

                            ret = model;
                        }

                    }                   
                }
            }
            else
            {
                Context.Status
                    .SetFailStatus("Error", LocalizationText.Get("Record-NotFound",
                        Context.LocalizationLanguage).Text);
            }

            return ret;
        }

    
        public async  Task<UserResult> GetByEmail(string email)
        {
            UserResult ret = null;

            ret = await _repositories.User.GetByEmail(email);

            if (ret != null)
            {
                if (Context.Status.Success)
                {
                    ret = await FillChields(ret);
                }
            }       

            return ret;
        }

        public async Task UpdateUserLogin(UpdateUserLogin model)
        {
            
            UserResult obj = null;

            obj = await _repositories.User.Read(new UserParam() { pUserID = model.UserID });

            if (Context.Status.Success)
            {
                if (obj != null)
                {
                    await _repositories.User.UpdateUserLogin(model);
                }
                else
                {
                    Context.Status
                       .SetFailStatus("Login-User-NotFound", LocalizationText.Get("Record-NotFound",
                           Context.LocalizationLanguage).Text);
                }
            }

        }

        public async Task<string> SetPasswordRecoveryCode(ChangeUserPassword model)
        {
            string code = "";
            string errmsg = "";
            
            UserResult usermatch = null;
            usermatch = await _repositories.User.GetByEmail(model.Email);

            if (Context.Status.Success)
            {

                if (usermatch != null)
                {
                    if (!model.ToActivate)
                    {
                        if (usermatch.IsActive)
                        {
                            code = Utilities.GenerateCode(6);

                        }
                        else
                        {
                            errmsg = LocalizationText.Get("Login-Inactive-Account", Context.LocalizationLanguage).Text;

                        }
                    }
                    else
                    {
                        code = Utilities.GenerateCode(6);
                    }

                }
                else
                {
                    errmsg = LocalizationText.Get("Login-User-NotFound", Context.LocalizationLanguage).Text;
                }

                if (errmsg == "")
                {                    
                      await _repositories.User.SetPasswordRecoveryCode(
                        new SetPasswordRecoveryCode()
                        {
                            UserID = usermatch.UserID,
                            Code = code
                        });                   
                }
                else
                {
                    Context.Status.SetFailStatus("Error", errmsg); 
                           
                }

            }                      

            return code;

        }

        public async Task ChangeUserPassword(ChangeUserPassword model)
        {
           
            string errmsg = "";

            UserResult usermatch = null;
            usermatch = await _repositories.User.GetByEmail(model.Email);

            if (Context.Status.Success)
            {

                if (usermatch != null)
                {
                    if (!usermatch.IsActive)
                    {
                        errmsg = LocalizationText.Get("Login-Inactive-Account", Context.LocalizationLanguage).Text;
                    }
                    else
                    {
                        if (usermatch.PasswordRecoveryCode != null)
                        {
                            if (usermatch.PasswordRecoveryCode != model.Code)
                            {
                                errmsg 
                                    = LocalizationText.Get("User-Invalid-Password-Code",Context.LocalizationLanguage).Text;

                            }
                        }
                        else
                        {

                            errmsg
                                = LocalizationText.Get("User-Invalid-Password-Code",Context.LocalizationLanguage).Text;
                        }

                    }

                }
                else
                {
                    errmsg = LocalizationText.Get("Login-User-NotFound", Context.LocalizationLanguage).Text;
                }

                if (errmsg == "")
                {                   
                    string pwd = MD5.BuildMD5(model.NewPassword);
                    pwd = MD5.BuildMD5(pwd + usermatch.Salt);

                    ChangeUserPassword change = new ChangeUserPassword();
                    change.NewPassword = pwd;
                    change.UserID = usermatch.UserID;

                     await _repositories.User.ChangeUserPassword(change);
                    
                }
                else
                {
                    Context.Status.SetFailStatus("Error", errmsg);
                }

            }          
                    
        }

        public async Task ActiveUserAccount(ActiveUserAccount model)
        {
            
            string errmsg = "";
            UserResult usermatch = null;
            usermatch = await _repositories.User.GetByEmail(model.Email);

            if (Context.Status.Success)
            {

                if (usermatch != null)
                {
                    if (usermatch.IsActive)
                    {
                        errmsg = LocalizationText.Get("Account-Active", Context.LocalizationLanguage).Text;
                    }
                    else
                    {
                        if (usermatch.PasswordRecoveryCode != null)
                        {
                            if (usermatch.PasswordRecoveryCode != model.Code)
                            {
                                errmsg = LocalizationText.Get("User-Invalid-Activation-Code", Context.LocalizationLanguage).Text;
                            }
                        }
                        else
                        {
                            errmsg = LocalizationText.Get("User-Invalid-Activation-Code", Context.LocalizationLanguage).Text;
                        }

                    }

                }
                else
                {
                    errmsg = LocalizationText.Get("Login-User-NotFound", Context.LocalizationLanguage).Text;
                }

                if (errmsg == "")
                {                    
                    await _repositories.User.ActiveUserAccount(model);                   

                }
                else
                {
                    Context.Status.SetFailStatus("Error", errmsg);
                }

            }          
               
        }

        public async Task ChangeUserProfileImage(ChangeUserImage model)
        {
            
            string errmsg = "";

            if (model.FileName == "")
            {
                errmsg = LocalizationText.Get("User-No-Image", Context.LocalizationLanguage).Text;
            }
            else
            {
                UserResult usermatch = null;
                usermatch = await _repositories.User.Read(new UserParam() { pUserID=model.UserID });

                if (Context.Status.Success)
                {

                    if (usermatch != null)
                    {
                        if (!usermatch.IsActive)
                        {
                            errmsg = LocalizationText.Get("Login-Inactive-Account", Context.LocalizationLanguage).Text;
                        }

                    }
                    else
                    {
                        errmsg = LocalizationText.Get("Login-User-NotFound", Context.LocalizationLanguage).Text;
                    }

                    if (errmsg == "")
                    {
                         await _repositories.User.ChangeUserProfileImage(model);

                    }
                    else
                    {
                        Context.Status.SetFailStatus("Error", errmsg);
                    }
                }               
            }                    

        }

        public async Task UpdateLoginFailCounter(UpdateUserLoginFailCounter model)
        {
            
             await _repositories.User.UpdateLoginFailCounter(model);                      

        }

        public async Task ChangeState(UserChangeState model)
        {
           
            await _repositories.User.ChangeState(model);              

        }

        public async Task<ExecutionStatus> SetDateLogout(Int64 userid)
        {
            ExecutionStatus ret = new ExecutionStatus(true);

            SessionLogParam param = new SessionLogParam();
          
            param.pUserID = userid;
            ret = await _repositories.SessionLog.SetDateLogout(param);

  
            return ret;

        }

        public async Task<UserRolesEntry> AddRoleToUser(Int64 userid, Int64 roleid)
        {
            UserRolesEntry ret = null;

            List<UserRolesResult> list;
            UserRolesParam param = new UserRolesParam();
            param.pUserID = userid;
            param.pRoleID = roleid;

            list = await _repositories.UserRoles.Search(param);

            if (list != null)
            {
                if (list.Count > 0)
                {
                    Context.Status
                    .SetFailStatus("Error", LocalizationText.Get("User-Role-Exists",
                        Context.LocalizationLanguage).Text);
                 
                }
            }

            if (Context.Status.Success)
            {
                UserRolesEntry obj = new UserRolesEntry();
                obj.UserRoleID = Utilities.GenerateId();
                obj.RoleID = roleid;
                obj.UserID = userid;

                 await _repositories.UserRoles.Create(obj);

                if (Context.Status.Success)
                {
                    ret= obj;
                }

            }
          
            return ret;
        }

        public async Task<UserRolesEntry> RemoveRoleFromUser(Int64 userid, Int64 roleid)
        {
            UserRolesEntry ret = null;

            List<UserRolesResult> list;
            UserRolesParam param = new UserRolesParam();
            param.pUserID = userid;
            param.pRoleID = roleid;

            list = await _repositories.UserRoles.Search(param);

            if (list != null)
            {
                if (list.Count == 0)
                {
                    Context.Status
                      .SetFailStatus("Error", LocalizationText.Get("User-Role-No-Exists",
                        Context.LocalizationLanguage).Text);
                }
            }
            else
            {
                    Context.Status
                        .SetFailStatus("Error", LocalizationText.Get("User-Role-No-Exists",
                            Context.LocalizationLanguage).Text);
            }

            if (Context.Status.Success)
            {
                UserRolesResult obj = list[0];

                await _repositories.UserRoles.Delete(new UserRolesEntry()
                {
                    UserRoleID = obj.UserRoleID,
                    UserID = obj.UserID,
                    RoleID = obj.RoleID
                });
               

                if (Context.Status.Success)
                {
                    ret= new UserRolesEntry()
                        {
                            UserRoleID = obj.UserRoleID,
                            UserID = obj.UserID,
                            RoleID = obj.RoleID
                        };
                }
            }        

            return ret;
        }


        public async Task<UserInstancesEntry> AddInstanceToUser(Int64 userid, Int64 instanceid)
        {
            UserInstancesEntry ret = null;

            List<UserInstancesResult> list;
            UserInstancesParam param = new UserInstancesParam();
            param.pUserID = userid;
            param.pInstanceID = instanceid;

            list = await _repositories.UserInstances.Search(param);

            if (list != null)
            {
                if (list.Count > 0)
                {
                    Context.Status
                      .SetFailStatus("Error", LocalizationText.Get("User-Instance-Exists",
                          Context.LocalizationLanguage).Text);                   
                }
            }

            if (Context.Status.Success)
            {
                UserInstancesEntry obj = new UserInstancesEntry();
                obj.UserInstanceID = Utilities.GenerateId();
                obj.InstanceID = instanceid;
                obj.UserID = userid;

                 await _repositories.UserInstances.Create(obj);

                if (Context.Status.Success)
                {
                    ret = obj;
                }

            }

            return ret;
        }


        public async Task<UserInstancesEntry> RemoveInstanceFromUser(Int64 userid, Int64 instanceid)
        {
            UserInstancesEntry ret = null; ;

            List<UserInstancesResult> list;
            UserInstancesParam param = new UserInstancesParam();
            param.pUserID = userid;
            param.pInstanceID = instanceid;

            list = await _repositories.UserInstances.Search(param);

            if (list != null)
            {
                if (list.Count == 0)
                {
                    Context.Status
                        .SetFailStatus("Error", LocalizationText.Get("User-Instance-No-Exists",
                            Context.LocalizationLanguage).Text);
                }
            }
            else
            {
                Context.Status
                    .SetFailStatus("Error", LocalizationText.Get("User-Instance-No-Exists",
                        Context.LocalizationLanguage).Text);
            }

            if (Context.Status.Success)
            {
                UserInstancesResult obj = list[0];

                await _repositories.UserInstances.Delete(new UserInstancesEntry()
                {
                    UserInstanceID = obj.UserInstanceID,
                    UserID = obj.UserID,
                    InstanceID = obj.InstanceID

                });

                if (Context.Status.Success)
                {
                    ret = new UserInstancesEntry()
                    {
                        UserInstanceID = obj.UserInstanceID,
                        UserID = obj.UserID,
                        InstanceID = obj.InstanceID

                    };
                }
            }         

            return ret;
        }


        public async Task AlterRole(Int64 userroleid, Int64 newroleid)
        {
                        

            if (userroleid == 0)
            {
                Context.Status
                   .SetFailStatus("Error", LocalizationText.Get("Validation-NotNull",
                       Context.LocalizationLanguage).Text);               
            }

            if (newroleid == 0)
            {
                Context.Status
                   .SetFailStatus("Error", LocalizationText.Get("Validation-NotNull",
                       Context.LocalizationLanguage).Text);
            }

            if (Context.Status.Success)
            {
                UserRolesParam obj = new UserRolesParam();
                obj.pUserRoleID = userroleid;
                obj.pRoleID = newroleid;

                await _repositories.UserRoles.AlterRole(obj); 
                
            }
            
        }

        public async Task AlterInstance(Int64 userinstanceid, Int64 newinstanceid)
        {
            
            if (userinstanceid == 0)
            {
                Context.Status
                 .SetFailStatus("Error", LocalizationText.Get("Validation-NotNull",
                     Context.LocalizationLanguage).Text);                
            }

            if (newinstanceid == 0)
            {
                Context.Status
                 .SetFailStatus("Error", LocalizationText.Get("Validation-NotNull",
                     Context.LocalizationLanguage).Text);
            }

            if (Context.Status.Success)
            {
                UserInstancesParam obj = new UserInstancesParam();
                obj.pUserInstanceID= userinstanceid;
                obj.pInstanceID = newinstanceid;

                await _repositories.UserInstances.AlterInstance(obj);

            }

        }

    }
}
