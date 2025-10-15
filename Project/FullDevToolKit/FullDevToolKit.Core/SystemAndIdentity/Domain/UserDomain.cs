using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.Sys.Contracts.Domains;
using FullDevToolKit.Sys.Models.Identity;
using FullDevToolKit.Helpers;
using FullDevToolKit.Sys.Contracts.Repositories;
using FullDevToolKit.Sys.Data.Repositories;
using System.Security.Cryptography;


namespace FullDevToolKit.Sys.Domains
{
    public class UserDomain
           : BaseDomain<UserParam, UserEntry, UserList, UserResult>, IUserDomain

    {

        public UserDomain(IContext context)
        {

            Context = context;
            _repositories = new SystemRepositorySet(context);
            this.TableName = _repositories.User.TableName;
        }
        
        private ISystemRepositorySet _repositories { get; set; }


        public override async Task<UserResult> FillChields(UserResult obj)
        {
            UserRolesParam param = new UserRolesParam();
            param.pUserID = obj.UserID;

            List<UserRolesResult> roles
                = await  _repositories.UserRoles.ReadSearch(param);

            obj.Roles = roles;

            //

            UserInstancesParam param2 = new UserInstancesParam();
            param2.pUserID = obj.UserID;

            List<UserInstancesResult> instances
                = await _repositories.UserInstances.ReadSearch(param2);

            obj.Instances = instances; 

            return obj;
        }

        public async Task<UserResult> Get(UserParam param)
        {
            UserResult ret = null;

            ret = await _repositories.User.ReadObject(param); 

            if (ret != null)
            {
                ret = await FillChields(ret); 
            }
            
            return ret;
        }

        public async Task<List<UserList>> List(UserParam param)
        {
            List<UserList> ret = null;

            ret = await _repositories.User.ReadList(param);           

            return ret;
        }

        public async Task<List<UserResult>> Search(UserParam param)
        {
            List<UserResult> ret = null;

            ret = await _repositories.User.ReadSearch(param);

            return ret;
        }

   
        public override async Task InsertValidation(UserEntry obj)
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

        public override async Task UpdateValidation(UserEntry obj)
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

        public override async Task DeleteValidation(UserEntry obj)
        {
            Context.Status = new ExecutionStatus(true);        
        }


        public async Task<UserEntry> Set(UserEntry model, object userid)
        {
            UserEntry ret = null;
            
            if (model.UserID == 0)
            {
                model.UserID = Helpers.Utilities.GenerateId();
            }
            this.PKValue = model.UserID.ToString();

            ret = await ExecutionForSet(model, userid,
                      async (model) =>
                      {
                          return
                             await _repositories.User.ReadObject(new UserParam()
                             { pUserID = model.UserID });
                      }
                      ,
                      async (model) =>
                      {
                          await _repositories.User.Create(model);
                      }
                      ,
                      async (model) =>
                      {
                          await _repositories.User.Update(model);
                      }
                      , null,
                      async(model) =>
                      {
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
                      }
                  );

            return ret;
        }


        public async Task<UserEntry> Remove(UserEntry model, object userid)
        {
            UserEntry ret = null;
            this.PKValue = model.UserID.ToString();

            ret = await ExecutionForDelete(model, userid,
                      async (model) =>
                      {
                          return
                             await _repositories.User.ReadObject(new UserParam()
                             { pUserID = model.UserID });
                      }
                      ,
                      async (model) =>
                      {
                          await _repositories.User.Delete(model);
                      }
                      ,
                      async (model) =>
                      {
                          if (model.Roles != null)
                          {
                              foreach (UserRolesResult u in model.Roles)
                              {
                                  await _repositories.UserRoles.Delete(
                                          new UserRolesEntry()
                                          {
                                              UserRoleID = u.UserRoleID,
                                              UserID = u.UserID,
                                              RoleID = u.RoleID
                                          }
                                      );

                                  if (!Context.Status.Success)
                                  {
                                      break;
                                  }
                              }
                          }

                          if (model.Instances != null)
                          {
                              foreach (UserInstancesResult u in model.Instances)
                              {
                                  await _repositories.UserInstances.Delete(
                                            new UserInstancesEntry()
                                            {
                                                UserInstanceID = u.UserInstanceID,
                                                UserID = u.UserID,
                                                InstanceID = u.InstanceID

                                            }
                                      );

                                  if (!Context.Status.Success)
                                  {
                                      break;
                                  }
                              }
                          }
                      }
                  );

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

            obj = await _repositories.User.ReadObject(new UserParam() { pUserID = model.UserID });

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
                    string pwd = FullDevToolKit.Helpers.MD5.BuildMD5(model.NewPassword);
                    pwd = FullDevToolKit.Helpers.MD5.BuildMD5(pwd + usermatch.Salt);

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
                usermatch = await _repositories.User.ReadObject(new UserParam() { pUserID=model.UserID });

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


        public async Task<bool> ChangeUserLanguage(ChangeUserLanguage model)
        {
            bool ret = false; 

            string errmsg = "";

            if (model.NewLanguage == "")
            {
                errmsg = LocalizationText
                    .Get("Validation-Error", Context.LocalizationLanguage).Text;
            }
            else
            {
                UserResult usermatch = null;
                usermatch = await _repositories.User.ReadObject(new UserParam() { pUserID = model.UserID });

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
                        await _repositories.User.ChangeUserLanguage(model);
                        ret = true;
                    }
                    else
                    {
                        Context.Status.SetFailStatus("Error", errmsg);
                    }
                }
            }

            return ret;

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

            UserParam param = new UserParam();
          
            param.pUserID = userid;
            //ret = await _repositories.User.SetDateLogout(param);

  
            return ret;

        }

        public async Task<UserRolesEntry> AddRoleToUser(Int64 userid, Int64 roleid)
        {
            UserRolesEntry ret = null;

            List<UserRolesResult> list;
            UserRolesParam param = new UserRolesParam();
            param.pUserID = userid;
            param.pRoleID = roleid;

            list = await _repositories.UserRoles.ReadSearch(param);

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

            list = await _repositories.UserRoles.ReadSearch(param);

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

            list = await _repositories.UserInstances.ReadSearch(param);

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

            list = await _repositories.UserInstances.ReadSearch(param);

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
