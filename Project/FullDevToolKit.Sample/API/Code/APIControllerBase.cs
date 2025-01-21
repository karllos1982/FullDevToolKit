using Microsoft.AspNetCore.Mvc;
using FullDevToolKit.Common;
using FullDevToolKit.Helpers;
using Newtonsoft.Json;
using Microsoft.Extensions.Caching.Memory;
using FullDevToolKit.Core;
using FullDevToolKit.Sys.Contracts.Managers;
using FullDevToolKit.ApplicationHelpers;
using MyApp.Contracts.Managers;
using MyApp.Managers;

namespace MyApp.API
{
    public class APIControllerBase: ControllerBase
    {
        protected bool IsAllowed = false; 
        protected PERMISSION_STATE_ENUM PermissionState = PERMISSION_STATE_ENUM.NONE;

        public object UserID = 0;
        public object RoleID = 0;
        public object InstanceID = 0;
        public object ret = null; 
        public string ObjectCode = "";        
        public ExecutionStatus opsts = new ExecutionStatus();       
        public int contextindex = 0;       
        public IUserPermissionsManager<UserPermissions> PermissionsManager;

        public IMemoryCache memorycache = null;
        public IContext Context;
        public IMyAppManager Manager;
        public MailManager MailCenter;
        

        protected MemoryCacheEntryOptions GetMemoryCacheOptionsByHour(int time)
        {
            MemoryCacheEntryOptions ret = new MemoryCacheEntryOptions();

            ret.SetSlidingExpiration(TimeSpan.FromHours(time)); 
                
            return ret;
        }

        protected void CheckPermission_()
        {
                       
            IsAllowed = true;
            PermissionState = PERMISSION_STATE_ENUM.NONE;
        }

        protected void Init(IContext context,
                IContextBuilder contextbuilder, string objectcode)
        {
            Context = context;
            contextbuilder.BuilderContext(Context);
            this.Manager = new MyAppManager(context);
            ObjectCode = objectcode; 
        }

        protected void CheckPermission( PERMISSION_CHECK_ENUM? checking = null, bool? allownone = null)
        {
           
            if (checking != null && User.Claims.ToList().Count > 0)
            {
                this.UserID = User.Identity.Name;  
                this.RoleID = User.Claims.ToList()[1].Value;
                this.InstanceID = User.Claims.ToList()[2].Value;

                string permissions_content = User.Claims.ToList()[3].Value;
                string lang = User.Claims.ToList()[4].Value;
                
                if (lang!=null)
                {
                    Context.LocalizationLanguage = lang;
                }
                else
                {
                    Context.LocalizationLanguage = Context.Settings.LocalizationLanguage;
                }

                List<UserPermissions> permissions 
                    = JsonConvert.DeserializeObject<List<UserPermissions>>(permissions_content);               
                                      
                PermissionState = Utilities.CheckPermission(permissions, ObjectCode, checking.Value);
                IsAllowed = false;
                if (PermissionState == PERMISSION_STATE_ENUM.ALLOWED)
                {
                    IsAllowed = true;
                }

                if (allownone.Value && PermissionState == PERMISSION_STATE_ENUM.NONE)
                {
                    IsAllowed = true;
                }

                if (!IsAllowed)
                {
                    Response.StatusCode = 403;
                    Context.Status = new ExecutionStatus(false);
                    Context.Status.SetFailStatus("Access denied",
                        "Access denied to resource: " + ObjectCode + " / " + checking.ToString());
                     
                }
            }

        }


        protected void FinalizeManager()
        {
            Context.End(); 
        }

        protected APIResponse<T> SetReturn<T>(T? data)
        {
            if (Context.Status.Success)
            {
                Response.StatusCode = 200;                
                return new APIResponse<T>(data); 
            }
            else
            {
                Response.StatusCode = 500;
                return new APIResponse<T>(500, Context.Status.Exceptions); 

            }
        }
        protected APIResponse<T> SetReturn<T>(PERMISSION_CHECK_ENUM checking)
        {
           
            Response.StatusCode = 403;
            ExecutionExceptions exps = new ExecutionExceptions();
            exps.AddException("AccessDenied", "Access denied to resource: " + ObjectCode + " / " + checking.ToString());
            return new APIResponse<T>(403, exps);

        }

        protected ExecutionStatus GetDefaultStatus()
        {
            return Context.Status;
        }

       
        protected string SerializeObjectToJSON_String(object obj)
        {
            string ret = "[]";

            if (obj != null)
            {
                ret = JsonConvert.SerializeObject(obj); 
            }

            return ret;
        }

    }
}
