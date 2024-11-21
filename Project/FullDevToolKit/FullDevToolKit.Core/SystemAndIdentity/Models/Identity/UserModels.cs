using FullDevToolKit.Helpers;
using FullDevToolKit.Common;

namespace FullDevToolKit.Sys.Models.Identity
{

    public class UserLogin
    {
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string TargetRole { get; set; } = string.Empty;

        public bool KeepConnection { get; set; }

        public string SessionTimeOut { get; set; } = string.Empty;

        public string ClientIP { get; set; } = string.Empty;

        public string ClienteBrowserName { get; set; } = string.Empty;

        public string AuthToken { get; set; } = string.Empty;

        public DateTime AuthTokenExpires { get; set; }

    }

    public class UserAuthenticated
    {
        public string UserID { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string RoleName { get; set; } = string.Empty;

        public string InstanceName { get; set; } = string.Empty;

        public string InstanceID { get; set; } = string.Empty;

        public string HomeURL { get; set; } = string.Empty;

        public string ProfileImageURL { get; set; } = string.Empty;

        public string Token { get; set; } = string.Empty;

        public DateTime Expires { get; set; }

        public string Status { get; set; } = string.Empty;

        public List<UserPermissions> Permissions { get; set; }  = new List<UserPermissions>();

        public string LocalizationLanguage { get; set; } = string.Empty;

    }
   

    public class UserParam
    {
        public UserParam()
        {
            pUserID = 0;
            pRoleID = 0;
            pInstanceID = 0;
            pUserName = "";
            pEmail = ""; 
        }

        public long pUserID { get; set; }

        public string pEmail { get; set; }

        public string pUserName { get; set; }

        public long pRoleID { get; set; }

        public long pInstanceID { get; set; }

    }

    public class NewUser
    {

        [PrimaryValidationConfig("UserName", "User Name", FieldType.USERNAME, false, 50)]
        public string UserName { get; set; } = string.Empty;

        [PrimaryValidationConfig("Email", "E-mail", FieldType.EMAIL, false, 100)]
        public string Email { get; set; } = string.Empty;

        [PrimaryValidationConfig("RoleID", "Role ID", FieldType.NUMERIC, false, 0)]
        public long RoleID { get; set; } 

        [PrimaryValidationConfig("InstanceID", "LocalizationText ID", FieldType.NUMERIC, false, 0)]
        public long InstanceID { get; set; } 

        [PrimaryValidationConfig("DefaultLanguage", "Default Language", FieldType.TEXT, false, 5)]
        public string DefaultLanguage { get; set; } = string.Empty;

        [PrimaryValidationConfig("Password", "Password", FieldType.TEXT, false, 8)]
        public string Password { get; set; } = string.Empty;

    }

    public class EmailConfirmation
    {
        public string UserName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Code { get; set; } = string.Empty;
    }

    public class UserEntry
    {
        public UserEntry()
        {
            
        }
        public UserEntry(UserResult fromobj)
        {
            UserID = fromobj.UserID;
            ApplicationID = fromobj.ApplicationID;
            UserName = fromobj.UserName;
            Email = fromobj.Email;
            Password = fromobj.Password;
            Salt   = fromobj.Salt;  
            CreateDate = fromobj.CreateDate;
            IsActive = fromobj.IsActive;
            IsLocked    = fromobj.IsLocked;
            DefaultLanguage = fromobj.DefaultLanguage;  
            LastLoginDate = fromobj.LastLoginDate;
            LastLoginIP = fromobj.LastLoginIP;
            LoginCounter = fromobj.LoginCounter;
            LoginFailCounter = fromobj.LoginFailCounter;
            Avatar  = fromobj.Avatar;
            AuthCode = fromobj.AuthCode;
            AuthCodeExpires = fromobj.AuthCodeExpires;
            PasswordRecoveryCode   = fromobj.PasswordRecoveryCode;  
            ProfileImage = fromobj.ProfileImage;
            AuthUserID  = fromobj.AuthUserID;



        }

        [PrimaryValidationConfig("UserID", "User ID", FieldType.NUMERIC, false, 0)]
        public long UserID { get; set; }

        public long ApplicationID { get; set; }

        [PrimaryValidationConfig("UserName", "User Name", FieldType.USERNAME, false, 50)]
        public string UserName { get; set; } = string.Empty;

        [PrimaryValidationConfig("Email", "E-mail", FieldType.EMAIL, false, 100)]
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string Salt { get; set; } = string.Empty;

        public DateTime CreateDate { get; set; }

        public bool IsActive { get; set; }

        public bool IsLocked { get; set; }

        public string? DefaultLanguage { get; set; }

        public DateTime LastLoginDate { get; set; }

        public string? LastLoginIP { get; set; }

        public int LoginCounter { get; set; }

        public int LoginFailCounter { get; set; }

        public string? Avatar { get; set; }

        public string? AuthCode { get; set; }

        public DateTime AuthCodeExpires { get; set; }

        public string? PasswordRecoveryCode { get; set; }

        public string? ProfileImage { get; set; }

        public string? AuthUserID { get; set; }

        public List<UserRolesEntry> Roles { get; set; }    

        public List<UserInstancesEntry> Instances { get; set; }

    }

    public class UserList
    {
        public long UserID { get; set; }

        public string? UserName { get; set; }       

        public string? Email { get; set; }

    }

    public class UserResult
    {
        
        public long UserID { get; set; }

        public long ApplicationID { get; set; }
        
        public string UserName { get; set; } = string.Empty;
        
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string Salt { get; set; } = string.Empty;

        public DateTime CreateDate { get; set; }

        public bool IsActive { get; set; }

        public bool IsLocked { get; set; }

        public string DefaultLanguage { get; set; } = string.Empty;

        public DateTime LastLoginDate { get; set; }

        public string LastLoginIP { get; set; } = string.Empty;

        public Int32 LoginCounter { get; set; }

        public Int32 LoginFailCounter { get; set; }

        public string Avatar { get; set; } = string.Empty;

        public string AuthCode { get; set; } = string.Empty;

        public DateTime AuthCodeExpires { get; set; }

        public string PasswordRecoveryCode { get; set; } = string.Empty;

        public string ProfileImage { get; set; } = string.Empty;

        public string AuthUserID { get; set; } = string.Empty;

        public string ProfileImageURL { get; set; } = string.Empty;

        //
      
        public List<UserRolesResult> Roles { get; set; } = new List<UserRolesResult>();

        public List<UserInstancesResult> Instances { get; set; } = new List<UserInstancesResult>();

        //

        public List<UserPermissions> Permissions { get; set; } = new List<UserPermissions>();   

    }


    public class UpdateUserLogin
    {
        public long UserID { get; set; }

        public DateTime LastLoginDate { get; set; }

        public string AuthToken { get; set; } = string.Empty;   

        public DateTime AuthTokenExpires { get; set; }

    }
   

    public class ChangeUserPassword
    {
        public long UserID { get; set; }

        public string Email { get; set; } = string.Empty;   

        public string NewPassword { get; set; } = string.Empty;

        public string Code { get; set; } = string.Empty;

        public bool ToActivate { get; set; }
    }

    public class SetPasswordRecoveryCode
    {
        public long UserID { get; set; }

        public string Code { get; set; } = string.Empty;
    }

    public class ActiveUserAccount
    {
        public string Email { get; set; } = string.Empty;

        public string Code { get; set; } = string.Empty;
    }

    public class ChangeUserImage
    {
        public long UserID { get; set; }

        public string FileName { get; set; } = string.Empty;
    }

    public class UpdateUserLoginFailCounter
    {
        public string UserID { get; set; } = string.Empty;

        public bool Reset { get; set; }

        public bool ActiveStatus { get; set; }

    }

    public class UserChangeState
    {
        public long UserID { get; set; }

        public bool ActiveValue { get; set; }

        public bool LockedValue { get; set; }
    }

    public class RegisterSession
    {
        public long SessionID { get; set; }

        public long UserID { get; set; }

        public DateTime Date { get; set; }

        public string IP { get; set; } = string.Empty;

        public string BrowserName { get; set; } = string.Empty;
    }


   


}
