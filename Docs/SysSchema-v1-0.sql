-- 1. CREATE SYS TABLES

-- 1.1: Table Instance:

CREATE TABLE [dbo].[sysInstance](
	[InstanceID] [bigint] NOT NULL,
	[InstanceTypeName] [varchar](50) NOT NULL,
	[InstanceName] [varchar](100) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_sysInstance] PRIMARY KEY CLUSTERED 
(
	[InstanceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


-- 1.2: Table Role:

CREATE TABLE [dbo].[sysRole](
	[RoleID] [bigint] NOT NULL,
	[RoleName] [varchar](30) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [pk_sysRole] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


-- 1.3: Table Role:

CREATE TABLE [dbo].[sysObjectPermission](
	[ObjectPermissionID] [bigint] NOT NULL,
	[ObjectName] [varchar](100) NOT NULL,
	[ObjectCode] [varchar](25) NOT NULL,
 CONSTRAINT [PK_sysObjectPermission] PRIMARY KEY CLUSTERED 
(
	[ObjectPermissionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


-- 1.4: Table User:

CREATE TABLE [dbo].[sysUser](
	[UserID] [bigint] NOT NULL,
	[ApplicationID] [bigint] NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Password] [varchar](100) NOT NULL,
	[Salt] [varchar](10) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsLocked] [bit] NOT NULL,
	[DefaultLanguage] [varchar](5) NOT NULL,
	[LastLoginDate] [datetime] NULL,
	[LastLoginIP] [varchar](30) NULL,
	[LoginCounter] [int] NULL,
	[LoginFailCounter] [int] NULL,
	[Avatar] [image] NULL,
	[AuthCode] [varchar](max) NULL,
	[AuthCodeExpires] [datetime] NULL,
	[PasswordRecoveryCode] [varchar](45) NULL,
	[ProfileImage] [varchar](255) NULL,
	[AuthUserID] [varchar](50) NULL,
 CONSTRAINT [pk_sysUser] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


-- 1.5: Table UserInstances

CREATE TABLE [dbo].[sysUserInstances](
	[UserInstanceID] [bigint] NOT NULL,
	[UserID] [bigint] NOT NULL,
	[InstanceID] [bigint] NOT NULL,
 CONSTRAINT [PK_sysUserInstances] PRIMARY KEY CLUSTERED 
(
	[UserInstanceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[sysUserInstances]  WITH NOCHECK ADD  CONSTRAINT [fk_sysUserInstances_instance] FOREIGN KEY([InstanceID])
REFERENCES [dbo].[sysInstance] ([InstanceID])
GO

ALTER TABLE [dbo].[sysUserInstances]  WITH NOCHECK ADD  CONSTRAINT [fk_sysUserInstances_user] FOREIGN KEY([UserID])
REFERENCES [dbo].[sysUser] ([UserID])
GO


-- 1.6: UserRoles

CREATE TABLE [dbo].[sysUserRoles](
	[UserRoleID] [bigint] NOT NULL,
	[UserID] [bigint] NOT NULL,
	[RoleID] [bigint] NOT NULL,
 CONSTRAINT [PK_sysUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserRoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[sysUserRoles]  WITH NOCHECK ADD  CONSTRAINT [fk_sysUserRoles_role] FOREIGN KEY([RoleID])
REFERENCES [dbo].[sysRole] ([RoleID])
GO

ALTER TABLE [dbo].[sysUserRoles]  WITH NOCHECK ADD  CONSTRAINT [fk_sysUserRoles_user] FOREIGN KEY([UserID])
REFERENCES [dbo].[sysUser] ([UserID])
GO


-- 1.7: Permissions

CREATE TABLE [dbo].[sysPermission](
	[PermissionID] [bigint] NOT NULL,
	[ObjectPermissionID] [bigint] NOT NULL,
	[RoleID] [bigint] NULL,
	[UserID] [bigint] NULL,
	[ReadStatus] [int] NOT NULL,
	[SaveStatus] [int] NOT NULL,
	[DeleteStatus] [int] NOT NULL,
	[TypeGrant] [varchar](1) NULL,
 CONSTRAINT [PK_sysPermission] PRIMARY KEY CLUSTERED 
(
	[PermissionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[sysPermission]  WITH NOCHECK ADD  CONSTRAINT [fk_sysPermission_object] FOREIGN KEY([ObjectPermissionID])
REFERENCES [dbo].[sysObjectPermission] ([ObjectPermissionID])
GO

ALTER TABLE [dbo].[sysPermission]  WITH NOCHECK ADD  CONSTRAINT [fk_sysPermission_role] FOREIGN KEY([RoleID])
REFERENCES [dbo].[sysRole] ([RoleID])
GO

ALTER TABLE [dbo].[sysPermission]  WITH NOCHECK ADD  CONSTRAINT [fk_sysPermission_user] FOREIGN KEY([UserID])
REFERENCES [dbo].[sysUser] ([UserID])
GO


-- 1.8: SessionLog

CREATE TABLE [dbo].[sysSessionLog](
	[SessionLogID] [bigint] NOT NULL,
	[UserID] [bigint] NOT NULL,
	[Date] [datetime] NOT NULL,
	[IP] [varchar](25) NULL,
	[BrowserName] [varchar](100) NULL,
	[DateLogout] [datetime] NULL,
 CONSTRAINT [pk_sysSessionLog] PRIMARY KEY CLUSTERED 
(
	[SessionLogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


-- 1.9: DataLog

CREATE TABLE [dbo].[sysDataLog](
	[DataLogID] [bigint] NOT NULL,
	[UserID] [bigint] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Operation] [varchar](1) NOT NULL,
	[TableName] [varchar](50) NOT NULL,
	[ID] [bigint] NULL,
	[LogOldData] [varchar](max) NULL,
	[LogCurrentData] [varchar](max) NULL,
 CONSTRAINT [pk_sysDataLog] PRIMARY KEY CLUSTERED 
(
	[DataLogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[sysDataLog]  WITH CHECK ADD  CONSTRAINT [fk_sysDataLog_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[sysUser] ([UserID])
GO


-- 1.10: LocalizationText

CREATE TABLE [dbo].[sysLocalizationText](
	[LocalizationTextID] [bigint] NOT NULL,
	[Language] [varchar](5) NOT NULL,
	[Code] [varchar](10) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Text] [varchar](255) NOT NULL,
 CONSTRAINT [PK_sysLocalizationText] PRIMARY KEY CLUSTERED 
(
	[LocalizationTextID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


-- 1.11: GroupParameter

CREATE TABLE [dbo].[sysGroupParameter](
	[GroupParameterID] [bigint] NOT NULL,
	[GroupParameterName] [varchar](100) NOT NULL,
	[IsActive] [bit] NOT NULL,	
 CONSTRAINT [PK_sysGroupParameter] PRIMARY KEY CLUSTERED 
(
	[GroupParameterID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


-- 1.12: GroupParameter

CREATE TABLE [dbo].[sysParameter](
	[ParameterID] [bigint] NOT NULL,
	[GroupParameterID] [bigint] NOT NULL,
	[ParameterName] [varchar](100) NOT NULL,
	[IsActive] [bit] NOT NULL,	
 CONSTRAINT [pk_sysParameter] PRIMARY KEY CLUSTERED 
(
	[ParameterID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] 
GO

ALTER TABLE [dbo].[sysParameter]  WITH CHECK ADD  CONSTRAINT [fk_sysParameter_GroupParameter] FOREIGN KEY([GroupParameterID])
REFERENCES [dbo].[sysGroupParameter] ([GroupParameterID])
GO



-- TABELAS DO TEMPLATE

CREATE TABLE [dbo].[Person](
	[PersonID] [bigint] NOT NULL,
	[PersonName] [varchar](50) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Email] [varchar](255) NOT NULL,
	[PhoneNamber] [varchar](15) NOT NULL,
 CONSTRAINT [pk_Person] PRIMARY KEY CLUSTERED 
(
	[PersonID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[PersonContacts](
	[PersonContactID] [bigint] NOT NULL,
	[PersonID] [bigint] NOT NULL,
	[ContactName] [varchar](50) NOT NULL,
	[Email] [varchar](255) NOT NULL,
	[CellPhoneNumber] [varchar](16) NOT NULL,
 CONSTRAINT [pk_PersonContact] PRIMARY KEY CLUSTERED 
(
	[PersonContactID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[PersonContacts]  WITH NOCHECK ADD  CONSTRAINT [fk_Person_PersonContact] FOREIGN KEY([PersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO




-- 2. CREATE PROCS

-- 2.1: Procedure CheckUniqueValueForInsert

CREATE PROCEDURE CheckUniqueValueForInsert
    @tablename varchar(50),
	@fieldname varchar(50),
	@fieldvalue varchar(100)
AS
BEGIN
    
	DECLARE @SQLString NVARCHAR(500);  
	DECLARE @ParmDefinition NVARCHAR(500);  

	set @SQLString = 
		N'select count(*) cnt from ' 
		+ @tablename + 
		' where ' + @fieldname + '=@value';
	
	set @ParmDefinition = N'@value varchar(255)';

	print @SQLString;

	EXECUTE sp_executesql @SQLString, @ParmDefinition,@value=@fieldvalue;  
END
go


-- 2.2: Procedure CheckUniqueValueForUpdate

CREATE PROCEDURE CheckUniqueValueForUpdate
    @tablename varchar(50),
	@fieldname varchar(50),
	@fieldvalue varchar(100),
	@recordfieldname varchar(100),
	@recordID bigint
AS
BEGIN
    
	DECLARE @SQLString NVARCHAR(500);  
	DECLARE @ParmDefinition NVARCHAR(500);  

	set @SQLString = 
		N'select count(*) cnt from ' 
		+ @tablename + 
		' where ' + @fieldname + '=@value' 
		+ ' and ' + @recordfieldname + '<>@id' ;
	
	set @ParmDefinition = N'@value varchar(255), @id bigint';

	print @SQLString;

	EXECUTE sp_executesql @SQLString, @ParmDefinition, @value=@fieldvalue, @id=@recordID;  
END

go


-- 2.3: Procedure ResetSysTables

CREATE PROCEDURE [dbo].[sp_ResetSysTables]
AS
BEGIN
		
	delete from sysDataLog
	delete from sysSessionLog	
	delete from sysPermission
	delete from sysObjectPermission
	delete from sysUserRoles
	delete from  sysUserInstances
	delete from sysRole
	delete from sysInstance
	delete from sysUser
	

	insert into [sysInstance] values (1,'Default','Default',1,GetDate())

	insert into [sysRole] values (1,'SuperAdmin',GetDate(),1)
	insert into [sysRole] values (2,'Admin',GetDate(),1)

	insert into [sysUser] values (1001,1,'master.admin','master.user@sys.com',
		'03273b3cc1a8e4c9811ac88ee757275f','GWSLT',GetDate(),1,0,'en-us',null,null,0,0,null,null,null,null,null,null)

	insert into [sysUserRoles] values (1,1001,1)
	insert into [sysUserInstances] values (1,1001,1)

	-- 

	insert into [sysUser] values (1002,1,'deleted.user','deleted.user@sys.com',
		'03273b3cc1a8e4c9811ac88ee757275f','GWSLT',GetDate(),1,0,'en-us',null,null,0,0,null,null,null,null,null,null)

	insert into [sysUserRoles] values (2,1002,1)
	insert into [sysUserInstances] values (2,1002,1)

	-- 

	insert into [sysUser] values (1003,1,'simple.user','simple.user@sys.com',
		'03273b3cc1a8e4c9811ac88ee757275f','GWSLT',GetDate(),1,0,'en-us',null,null,0,0,null,null,null,null,null,null)

	insert into [sysUserRoles] values (3,1003,2)
	insert into [sysUserInstances] values (3,1003,1)

	--

	insert into sysObjectPermission values (10001, 'Table.SysUser.Basic', 'SYSUSER')
	insert into sysObjectPermission values (10002, 'Table.SysRole.Basic', 'SYSROLE')
	insert into sysObjectPermission values (10003, 'Table.SysObjectPermission.Basic', 'SYSOBJECTPERMISSION')
	insert into sysObjectPermission values (10004, 'Table.SysPermission.Basic', 'SYSPERMISSION')
	insert into sysObjectPermission values (10005, 'Table.sysSession.Basic', 'SYSSESSION')
	insert into sysObjectPermission values (10006, 'Table.sysDataLog.Basic', 'SYSDATALOG')
	insert into sysObjectPermission values (10007, 'Table.SysInstance.Basic', 'SYSINSTANCE')
	insert into sysObjectPermission values (10008, 'Table.SysLocalizationText.Basic', 'SYSLOCALIZATIONTEXT')
    	insert into sysObjectPermission values (10009, 'Table.SysGroupParameter.Basic', 'SYSGROUPPARAMETER')
	insert into sysObjectPermission values (10010, 'Table.SysParameter.Basic', 'SYSPARAMETER')
	insert into sysObjectPermission values (10011, 'Table.SysPerson.Basic', 'PERSON')

	insert into sysPermission values (10001, 10001, 1, null, 1,1,1,'R')
	insert into sysPermission values (10002, 10002, 1, null, 1,1,1,'R')
	insert into sysPermission values (10003, 10003, 1, null, 1,1,1,'R')
	insert into sysPermission values (10004, 10004, 1, null, 1,1,1,'R')
	insert into sysPermission values (10005, 10005, 1, null, 1,1,1,'R')
	insert into sysPermission values (10006, 10006, 1, null, 1,1,1,'R')
	insert into sysPermission values (10007, 10007, 1, null, 1,1,1,'R')
	insert into sysPermission values (10008, 10008, 1, null, 1,1,1,'R')
	insert into sysPermission values (10009, 10009, 1, null, 1,1,1,'R')
	insert into sysPermission values (10010, 10010, 1, null, 1,1,1,'R')
	insert into sysPermission values (10011, 10011, 1, null, 1,1,1,'R')
	insert into sysPermission values (10012, 10011, 2, null, 1,1,1,'R')

	-- inserts na tabela LocalizationTexts

	delete from sysLocalizationText

	insert into sysLocalizationText Values(1001,      'en-us','1001',     'Execution-Error',    'Execution error')
	insert into sysLocalizationText Values(1002,      'en-us','1002',     'Validation-Error',    'Data validation error.')
	insert into sysLocalizationText Values(1003,      'en-us','1003',     'Record-NotFound',    'The requested record was not found.')
	insert into sysLocalizationText Values(1004,      'en-us','1004',     'Login-Invalid-Password',    'Invalid password. You still have 0 attempts before the account is deactivated.')
	insert into sysLocalizationText Values(1005,      'en-us','1005',     'Login-Attempts',    'You have already used access attempts and the account has been disabled. Request activation.')
	insert into sysLocalizationText Values(1006,      'en-us','1006',     'Login-Inactive-Account',    'The account associated with the User is not active. Request account activation.')
	insert into sysLocalizationText Values(1007,      'en-us','1007',     'Login-Locked-Account',    'The account associated with the User is locked out. Contact your system administrator.')
	insert into sysLocalizationText Values(1008,      'en-us','1008',     'Login-User-NotFound',    'User not found.')
	insert into sysLocalizationText Values(1009,      'en-us','1009',     'User-Exists',    'There is already a user with the email')
	insert into sysLocalizationText Values(1010,      'en-us','1010',     'Email-Exists',    'The email you entered already exists.')
	insert into sysLocalizationText Values(1011,      'en-us','1011',     'Profile-NotBe-Null',    'The Profile cannot be null.')
	insert into sysLocalizationText Values(1012,      'en-us','1012',     'User-Error-Exclude-Childs',    'There was an error deleting child items (Roles).')
	insert into sysLocalizationText Values(1013,      'en-us','1013',     'User-Invalid-Password-Code',    'The password exchange authorization code is invalid.')
	insert into sysLocalizationText Values(1014,      'en-us','1014',     'Account-Active',    'The account associated with the User is already active.')
	insert into sysLocalizationText Values(1015,      'en-us','1015',     'User-Invalid-Activation-Code',    'The activation authorization code is invalid.')
	insert into sysLocalizationText Values(1016,      'en-us','1016',     'User-No-Image',    'Send the image file.')
	insert into sysLocalizationText Values(1017,      'en-us','1017',     'User-Role-Exists',    'This Role is already associated with the user.')
	insert into sysLocalizationText Values(1018,      'en-us','1018',     'User-Role-No-Exists',    'This Role does not belong to the user.')
	insert into sysLocalizationText Values(1019,      'en-us','1019',     'Http-Unauthorized',    'Unauthorized access')
	insert into sysLocalizationText Values(1020,      'en-us','1020',     'Http-NotFound',    'The resource could not be found')
	insert into sysLocalizationText Values(1021,      'en-us','1021',     'Http-Forbidden',    'User profile without access permission.')
	insert into sysLocalizationText Values(1022,      'en-us','1022',     'Http-500Error',    'An error occurred in the processing of the request.')
	insert into sysLocalizationText Values(1023,      'en-us','1023',     'Http-ServiceUnavailable',    'The requested service is unavailable.')
	insert into sysLocalizationText Values(1024,      'en-us','1024',     'API-Unexpected-Exception',    'Unexpected error not identified GetInnerExceptions@f2]')
	insert into sysLocalizationText Values(1025,      'en-us','1025',     'ShortDayName-1',    'Sun')
	insert into sysLocalizationText Values(1026,      'en-us','1026',     'ShortDayName-2',    'Mon')
	insert into sysLocalizationText Values(1027,      'en-us','1027',     'ShortDayName-3',    'Tue')
	insert into sysLocalizationText Values(1028,      'en-us','1028',     'ShortDayName-4',    'Wed')
	insert into sysLocalizationText Values(1029,      'en-us','1029',     'ShortDayName-5',    'Thu')
	insert into sysLocalizationText Values(1030,      'en-us','1030',     'ShortDayName-6',    'Fri')
	insert into sysLocalizationText Values(1031,      'en-us','1031',     'ShortDayName-7',    'Sat'  )
	insert into sysLocalizationText Values(1032,      'en-us','1032',     'MonthName-1',    'JANUARY')
	insert into sysLocalizationText Values(1033,      'en-us','1033',     'MonthName-2',    'FEBRUARY')
	insert into sysLocalizationText Values(1034,      'en-us','1034',     'MonthName-3',    'MARCH')
	insert into sysLocalizationText Values(1035,      'en-us','1035',     'MonthName-4',    'APRIL')
	insert into sysLocalizationText Values(1036,      'en-us','1036',     'MonthName-5',    'MAY')
	insert into sysLocalizationText Values(1037,      'en-us','1037',     'MonthName-6',    'JUNE')
	insert into sysLocalizationText Values(1038,      'en-us','1038',     'MonthName-7',    'JULY')
	insert into sysLocalizationText Values(1039,      'en-us','1039',     'MonthName-8',    'AUGUST')
	insert into sysLocalizationText Values(1040,      'en-us','1040',     'MonthName-9',    'SEPTEMBER')
	insert into sysLocalizationText Values(1041,      'en-us','1041',     'MonthName-10',    'OCTOBER')
	insert into sysLocalizationText Values(1042,      'en-us','1042',     'MonthName-11',    'NOVEMBER')
	insert into sysLocalizationText Values(1043,      'en-us','1043',     'MonthName-12',    'DECEMBER')
	insert into sysLocalizationText Values(1044,      'en-us','1044',     'Validation-NotNull',    'cannot be null.')
	insert into sysLocalizationText Values(1045,      'en-us','1045',     'Validation-Max-Characters',    'The {0} field cannot have more than 1 characters.')
	insert into sysLocalizationText Values(1046,      'en-us','1046',     'Validation-Invalid-Field',    'The {0} field is invalid.')
	insert into sysLocalizationText Values(1047,      'en-us','1047',     'Validation-Invalid-UserName',    'The {0} field is invalid. Do not use special characters or spaces.')
	insert into sysLocalizationText Values(1048,      'en-us','1048',     'Validation-Unique-Value',    'The {0} field is invalid. Value must be unique.')
	insert into sysLocalizationText Values(1049,      'en-us','1049',     'User-Instance-Exists',    'This Instance is already associated with the user.')
	insert into sysLocalizationText Values(1050,      'en-us','1050',     'User-Instance-No-Exists',    'This Instance does not belong to the user.')
	insert into sysLocalizationText Values(1051,      'en-us','1051','User-PageTitle','User Management')
	insert into sysLocalizationText Values(1052,      'en-us','1052','SearchButtonLabel','Search')
	insert into sysLocalizationText Values(1053,      'en-us','1053','SearchingLabel','Searching...')
	insert into sysLocalizationText Values(1054,      'en-us','1054','InsertingLoadingLabel','Inserting...')
	insert into sysLocalizationText Values(1055,      'en-us','1055','SearchResultLabel','Search Result')
	insert into sysLocalizationText Values(1056,      'en-us','1056','DetailsLabel','Details')
	insert into sysLocalizationText Values(1057,      'en-us','1057','NoRecordsFound','No records was found')
	insert into sysLocalizationText Values(1058,      'en-us','1058','LoadingPage','Loading. Wait...')
	insert into sysLocalizationText Values(1059,      'en-us','1059','LoadingData','Loading Page. Wait...')
	insert into sysLocalizationText Values(1060,      'en-us','1060','ErrorOnExecuteSearch','Error on execute search')
	insert into sysLocalizationText Values(1061,      'en-us','1061','ErrorOnReturnData','Error on return data')
	insert into sysLocalizationText Values(1062,      'en-us','1062','ErrorOnCreateNewRecord','Error on create record')
	insert into sysLocalizationText Values(1063,      'en-us','1063','AfterSaveAnswering','The new record was created successfuly. Do you want inserting ?')
	insert into sysLocalizationText Values(1064,      'en-us','1064','NoticeLabel','Notice')
	insert into sysLocalizationText Values(1065,      'en-us','1065','SuccessLabel','Success')
	insert into sysLocalizationText Values(1066,      'en-us','1066','SuccessSaveMessage','The record was saved successfuly.')
	insert into sysLocalizationText Values(1067,      'en-us','1067','Email-Label','E-mail')
	insert into sysLocalizationText Values(1068,      'en-us','1068','UserName-Label','User Name')
	insert into sysLocalizationText Values(1069,      'en-us','1069','Password-Label','Password')
	insert into sysLocalizationText Values(1070,      'en-us','1070','Instance-Label','Instance')
	insert into sysLocalizationText Values(1071,      'en-us','1071','Role-Label','Role')
	insert into sysLocalizationText Values(1072,      'en-us','1072','Yes-Text','Yes')
	insert into sysLocalizationText Values(1073,      'en-us','1073','No-Text','No')
	insert into sysLocalizationText Values(1074,      'en-us','1074','Saving-Label','Saving...')
	insert into sysLocalizationText Values(1075,      'en-us','1075','Edit-Label','Edit')
	insert into sysLocalizationText Values(1076,      'en-us','1076','Date-Label','Date')
	insert into sysLocalizationText Values(1077,      'en-us','1077','Field-Label','Field')
	insert into sysLocalizationText Values(1078,      'en-us','1078','Value-Label','Value')
	insert into sysLocalizationText Values(1079,      'en-us','1079','SelectItem-Description','Select a item')
	insert into sysLocalizationText Values(1080,      'en-us','1080','AllItem-Description','All')
	insert into sysLocalizationText Values(1081,      'en-us','1081','Welcome-Label','Welcome to GW Template')
	insert into sysLocalizationText Values(1082,      'en-us','1082','LoginTitle-Label','Sign In')
	insert into sysLocalizationText Values(1083,      'en-us','1083','LoginTitle-Description','Enter your login and password')
	insert into sysLocalizationText Values(1084,      'en-us','1084','InputEmail-Description','Input your account e-mail')
	insert into sysLocalizationText Values(1085,      'en-us','1085','InputPassword-Description','Input your password')
	insert into sysLocalizationText Values(1086,      'en-us','1086','ForgetPassword-Description','Forgot your password ?')
	insert into sysLocalizationText Values(1087,      'en-us','1087','LoginButton-Label','Confirm')
	insert into sysLocalizationText Values(1088,      'en-us','1088','LoginLoading-Label','Entering...')
	insert into sysLocalizationText Values(1089,      'en-us','1089','SendText-Description','Sending...')
	insert into sysLocalizationText Values(1090,      'en-us','1090','ActiveAccountButton-Label','Activate Account')
	insert into sysLocalizationText Values(1091,      'en-us','1091','ActiveAccount-Label','Account Activation')
	insert into sysLocalizationText Values(1092,      'en-us','1092','ActiveAccount-Description','Cannot access ?')
	insert into sysLocalizationText Values(1093,      'en-us','1093','ActiveAccount-Step1','Send the activation code to your registration email')
	insert into sysLocalizationText Values(1094,      'en-us','1094','ActiveAccount-Step2','Enter the activation code received')
	insert into sysLocalizationText Values(1095,      'en-us','1095','SendCodeButton-Label','Send code')
	insert into sysLocalizationText Values(1096,      'en-us','1096','ActiveLoading-Label','Request Activation')
	insert into sysLocalizationText Values(1097,      'en-us','1097','InputCode-Description','Input code received on e-mail')
	insert into sysLocalizationText Values(1098,      'en-us','1098','Unlogged-Label','Not Logged')
	insert into sysLocalizationText Values(1099,      'en-us','1099','MyProfile-Label','My Profile')
	insert into sysLocalizationText Values(1100,      'en-us','1100','MainProfileData-Label','Main Info')
	insert into sysLocalizationText Values(1101,      'en-us','1101','AlterPassword-Label','Change Password')
	insert into sysLocalizationText Values(1102,      'en-us','1102','LanguageRole-Label','Language')
	insert into sysLocalizationText Values(1103,      'en-us','1103','AlterProfileImage-Label','Change Profile Image')
	insert into sysLocalizationText Values(1104,      'en-us','1104','AlterPasswordStep1-Label','Click on the link below to receive an email with the security code to change your password')
	insert into sysLocalizationText Values(1105,      'en-us','1105','AlterPasswordStep2-Label','After receiving the code, fill in the information below and click Change Password')
	insert into sysLocalizationText Values(1106,      'en-us','1106','InputNewPassword-Label','Input the new password')
	insert into sysLocalizationText Values(1107,      'en-us','1107','AlterPasswordButton-Label','Change Password')
	insert into sysLocalizationText Values(1108,      'en-us','1108','AlterPasswordButton-Loading','Changing password...')
	insert into sysLocalizationText Values(1109,      'en-us','1109','InvalidCredentials-Title','Invalid Credentiais')
	insert into sysLocalizationText Values(1110,      'en-us','1110','InvalidCredentials-Message','E-mail or password invalid.')
	insert into sysLocalizationText Values(1111,      'en-us','1111','TemporaryPassword-Title','Temporary Password Sent')
	insert into sysLocalizationText Values(1112,      'en-us','1112','TemporaryPassword-Message','A temporary password has been sent to your registration e-mail. When logging in, ask to change your password.')
	insert into sysLocalizationText Values(1113,      'en-us','1113','SuccessActivated-Title','Activation Successfuly')
	insert into sysLocalizationText Values(1114,      'en-us','1114','SuccessActivated-Message','The account has been successfully activated. You can now log in.')
	insert into sysLocalizationText Values(1115,      'en-us','1115','ActivateCode-Title','Code was Sent .')
	insert into sysLocalizationText Values(1116,      'en-us','1116','ActivateCode-Message','A security code has been sent by e-mail.')
	insert into sysLocalizationText Values(1117,      'en-us','1117','PasswordChanged-Title','Changed Password')
	insert into sysLocalizationText Values(1118,      'en-us','1118','PasswordChanged-Message','The password has been successfully changed. Log in again.')
	insert into sysLocalizationText Values(1119,      'en-us','1119','ImageChanged-Title','Altered Image')
	insert into sysLocalizationText Values(1120,      'en-us','1120','ImageChanged-Message','The profile picture has been changed. At the next login the new image will be displayed.')
	insert into sysLocalizationText Values(1121,      'en-us','1121','SearchByEmail-Label','By E-mail')
	insert into sysLocalizationText Values(1122,      'en-us','1122','SearchByUserName-Label','By User Name')
	insert into sysLocalizationText Values(1123,      'en-us','1123','SearchByEmail-Description','Search By E-mail')
	insert into sysLocalizationText Values(1124,      'en-us','1124','SearchByUserName-Description','Search By User Name')
	insert into sysLocalizationText Values(1125,      'en-us','1125','SearchByInstance-Label','By Instance')
	insert into sysLocalizationText Values(1126,      'en-us','1126','SearchByRole-Label','By Role')
	insert into sysLocalizationText Values(1127,      'en-us','1127','NewUser-Label','New User')
	insert into sysLocalizationText Values(1128,      'en-us','1128','NewUser-Description','Click here to create new User')
	insert into sysLocalizationText Values(1129,      'en-us','1129','Active-Label','Active')
	insert into sysLocalizationText Values(1130,      'en-us','1130','Locked-Label','Locked')
	insert into sysLocalizationText Values(1131,      'en-us','1131','MainData-Label','Main Data')
	insert into sysLocalizationText Values(1132,      'en-us','1132','User-SecondTabLabel','Instances & Roles')
	insert into sysLocalizationText Values(1133,      'en-us','1133','CreateDate-Label','Create Date')
	insert into sysLocalizationText Values(1134,      'en-us','1134','LastLoginDate-Label','Last Login Date')
	insert into sysLocalizationText Values(1135,      'en-us','1135','DefaultLanguage-Label','Default Language')
	insert into sysLocalizationText Values(1136,      'en-us','1136','LastLoginIP-Label','Last Login IP')
	insert into sysLocalizationText Values(1137,      'en-us','1137','LoginCounter-Label','Login Counter')
	insert into sysLocalizationText Values(1138,      'en-us','1138','PasswordRecovery-Label','Password Recovery Code')
	insert into sysLocalizationText Values(1139,      'en-us','1139','AlterInstance-Label','Alter Instance')
	insert into sysLocalizationText Values(1140,      'en-us','1140','AlterInstance-Description','Select a Instance to change')
	insert into sysLocalizationText Values(1141,      'en-us','1141','Altering-Label','Altering...')
	insert into sysLocalizationText Values(1142,      'en-us','1142','AlterRole-Label','Alter Role')
	insert into sysLocalizationText Values(1143,      'en-us','1143','AlterRole-Description','Select a Role to change')
	insert into sysLocalizationText Values(1144,      'en-us','1144','UserStatus-Label','User Status')
	insert into sysLocalizationText Values(1145,      'en-us','1145','ChangeUserState-Description','Click here to change status')
	insert into sysLocalizationText Values(1146,      'en-us','1146','ChangeUserState-Label','Update Status')
	insert into sysLocalizationText Values(1147,      'en-us','1147','CreateUser-Label','Creating New User')
	insert into sysLocalizationText Values(1148,      'en-us','1148','CreateUser-Description','Click here to create new user record.')
	insert into sysLocalizationText Values(1149,      'en-us','1149','CreateUserButton-Label','Save')
	insert into sysLocalizationText Values(1150,      'en-us','1150','AlterStatus-Error','Error on alter user status')
	insert into sysLocalizationText Values(1151,      'en-us','1151','AlterStatus-Success','User status altered successfuly')
	insert into sysLocalizationText Values(1152,      'en-us','1152','AlterInstance-Error','Error on alter instance')
	insert into sysLocalizationText Values(1153,      'en-us','1153','AlterInstance-Success','Instance altered successfuly')
	insert into sysLocalizationText Values(1154,      'en-us','1154','AlterRole-Error','Error on alter user role')
	insert into sysLocalizationText Values(1155,      'en-us','1155','AlterRole-Success','Role altered successfuly')
	insert into sysLocalizationText Values(1156,      'en-us','1156','Instance-PageTitle','Instance')
	insert into sysLocalizationText Values(1157,      'en-us','1157','SearchByInstanceName-Label','By Instance Name')
	insert into sysLocalizationText Values(1158,      'en-us','1158','SearchByInstanceName-Description','Search by Instance Name')
	insert into sysLocalizationText Values(1159,      'en-us','1159','SearchByInstanceTypeName-Label','By Type Name')
	insert into sysLocalizationText Values(1160,      'en-us','1160','SearchByInstanceTypeName-Description','Search by Type Name')
	insert into sysLocalizationText Values(1161,      'en-us','1161','NewInstance-Label','New Instance')
	insert into sysLocalizationText Values(1162,      'en-us','1162','NewInstance-Description','Click here to create new Instance')
	insert into sysLocalizationText Values(1163,      'en-us','1163','InstanceTypeName-Label','Instance Type Name')
	insert into sysLocalizationText Values(1164,      'en-us','1164','InstanceName-Label','Instance Name')
	insert into sysLocalizationText Values(1165,      'en-us','1165','InstanceRecord-Label','Instance Record')
	insert into sysLocalizationText Values(1166,      'en-us','1166','SaveInstanceButton-Label','Save Instance')
	insert into sysLocalizationText Values(1167,      'en-us','1167','SaveInstanceButton-Description','Click here to save Instance')
	insert into sysLocalizationText Values(1168,      'en-us','1168','DataLog-PageTitle','Data Log')
	insert into sysLocalizationText Values(1169,      'en-us','1169','SearchByOperationType-Label','By Operation Type')
	insert into sysLocalizationText Values(1170,      'en-us','1170','SearchByObject-Label','By Object')
	insert into sysLocalizationText Values(1171,      'en-us','1171','SearchByIntervalDate-Label','By Date Range')
	insert into sysLocalizationText Values(1172,      'en-us','1172','SearchByInicialDate-Label','Start Date')
	insert into sysLocalizationText Values(1173,      'en-us','1173','SearchByFinalDate-Label','End Date')
	insert into sysLocalizationText Values(1174,      'en-us','1174','SearchByRecordID-Label','Search Record ID')
	insert into sysLocalizationText Values(1175,      'en-us','1175','TableName-Label','Table Name')
	insert into sysLocalizationText Values(1176,      'en-us','1176','OperationText-Label','Operation Text')
	insert into sysLocalizationText Values(1177,      'en-us','1177','LogID-Label','Log ID')
	insert into sysLocalizationText Values(1178,      'en-us','1178','LogInformation-Label','Log Information')
	insert into sysLocalizationText Values(1179,      'en-us','1179','ShowTimeLine-Label','Show Time Line')
	insert into sysLocalizationText Values(1180,      'en-us','1180','OldVersionData-Label','Old Version')
	insert into sysLocalizationText Values(1181,      'en-us','1181','HasNoOldVersion-Label','No data was found')
	insert into sysLocalizationText Values(1182,      'en-us','1182','CurrentVersionData-Label','Current Version')
	insert into sysLocalizationText Values(1183,      'en-us','1183','HasNoCurrentVersion-Label','No data was found')
	insert into sysLocalizationText Values(1184,      'en-us','1184','RecordTimeLine-Label','Record Time Line')
	insert into sysLocalizationText Values(1185,      'en-us','1185','HasNoTimeLine-Label','No data on time line')
	insert into sysLocalizationText Values(1186,      'en-us','1186','Localization-PageTitle','Localization Text')
	insert into sysLocalizationText Values(1187,      'en-us','1187','SearchByLanguage-Label','By Language')
	insert into sysLocalizationText Values(1188,      'en-us','1188','SearchByLanguage-Description','Search by Language')
	insert into sysLocalizationText Values(1189,      'en-us','1189','SearchByLocalizationCode-Label','By Localization Code')
	insert into sysLocalizationText Values(1190,      'en-us','1190','SearchByLocalizationCode-Description','Search by Localization Code')
	insert into sysLocalizationText Values(1191,      'en-us','1191','SearchByLocalizationName-Label','By Localization Name')
	insert into sysLocalizationText Values(1192,      'en-us','1192','SearchByLocalizationName-Description','Search by Localization Name')
	insert into sysLocalizationText Values(1193,      'en-us','1193','SearchByLocalizationText-Label','By Localization Text')
	insert into sysLocalizationText Values(1194,      'en-us','1194','SearchByLocalizationText-Description','Search by Localization Text')
	insert into sysLocalizationText Values(1195,      'en-us','1195','Language-Label','Language')
	insert into sysLocalizationText Values(1196,      'en-us','1196','LocalizationCode-Label','Localization Code')
	insert into sysLocalizationText Values(1197,      'en-us','1197','LocalizationName-Label','Localization Name')
	insert into sysLocalizationText Values(1198,      'en-us','1198','LocalizationRecord-Label','Localization Text Record')
	insert into sysLocalizationText Values(1199,      'en-us','1199','LocalizationText-Label','Localization Text')
	insert into sysLocalizationText Values(1200,      'en-us','1200','SaveLocalizationButton-Label','Save Localization')
	insert into sysLocalizationText Values(1201,      'en-us','1201','SaveLocalizationButton-Description','Click here to save localization')
	insert into sysLocalizationText Values(1202,      'en-us','1202','NewLocalization-Label','New Localization')
	insert into sysLocalizationText Values(1203,      'en-us','1203','ObjectPermission-PageTitle','Object Permission')
	insert into sysLocalizationText Values(1204,      'en-us','1204','SearchByObjectName-Label','By Object Name')
	insert into sysLocalizationText Values(1205,      'en-us','1205','SearchByObjectName-Description','Search by Object Name')
	insert into sysLocalizationText Values(1206,      'en-us','1206','SearchByObjectCode-Label','By Object Code')
	insert into sysLocalizationText Values(1207,      'en-us','1207','SearchByObjectCode-Description','Search by Object Code')
	insert into sysLocalizationText Values(1208,      'en-us','1208','NewObject-Label','New Object')
	insert into sysLocalizationText Values(1209,      'en-us','1209','ObjectName-Label','Object Name')
	insert into sysLocalizationText Values(1210,      'en-us','1210','ObjectCode-Label','Object Code')
	insert into sysLocalizationText Values(1211,      'en-us','1211','ObjectPermissionRecord-Label','Object Permission Record')
	insert into sysLocalizationText Values(1212,      'en-us','1212','SaveObjectPermissionButton-Label','Save Object Permission')
	insert into sysLocalizationText Values(1213,      'en-us','1213','SaveObjectPermissionButton-Description','Click here to save Object Permission')
	insert into sysLocalizationText Values(1214,      'en-us','1214','Permission-PageTitle','Permission')
	insert into sysLocalizationText Values(1215,      'en-us','1215','SearchByObjectPermission-Label','By Object Permission')
	insert into sysLocalizationText Values(1216,      'en-us','1216','SearchByRole-Label','By Role')
	insert into sysLocalizationText Values(1217,      'en-us','1217','SearchByUser-Label','By User')
	insert into sysLocalizationText Values(1218,      'en-us','1218','NewPermission-Label','New Permission')
	insert into sysLocalizationText Values(1219,      'en-us','1219','NewPermission-Description','Click here to create new permission')
	insert into sysLocalizationText Values(1220,      'en-us','1220','ObjectName-Label','Object Name')
	insert into sysLocalizationText Values(1221,      'en-us','1221','RoleName-Label','Role Name')
	insert into sysLocalizationText Values(1222,      'en-us','1222','PermissionRecord-Label','Permission Record')
	insert into sysLocalizationText Values(1223,      'en-us','1223','PermissionType-Label','Permission Type')
	insert into sysLocalizationText Values(1224,      'en-us','1224','ReadStatus-Label','Read Status')
	insert into sysLocalizationText Values(1225,      'en-us','1225','SaveStatus-Label','Save Status')
	insert into sysLocalizationText Values(1226,      'en-us','1226','DeleteStatus-Label','Delete Status')
	insert into sysLocalizationText Values(1227,      'en-us','1227','SavePermissionButton-Label','Save Permission')
	insert into sysLocalizationText Values(1228,      'en-us','1228','SavePermissionButton-Description','Click here to save Permission')
	insert into sysLocalizationText Values(1229,      'en-us','1229','Role-PageTitle','Role')
	insert into sysLocalizationText Values(1230,      'en-us','1230','SearchByRoleName-Label','By Role Name')
	insert into sysLocalizationText Values(1231,      'en-us','1231','SearchByRoleName-Description','Search by Role Name')
	insert into sysLocalizationText Values(1232,      'en-us','1232','NewRole-Label','New Role')
	insert into sysLocalizationText Values(1233,      'en-us','1233','NewRole-Description','Click here to create new Role')
	insert into sysLocalizationText Values(1234,      'en-us','1234','RoleName-Label','Role Name')
	insert into sysLocalizationText Values(1235,      'en-us','1235','RoleRecord-Label','Role Record')
	insert into sysLocalizationText Values(1236,      'en-us','1236','SaveRoleButton-Label','Save Role')
	insert into sysLocalizationText Values(1237,      'en-us','1237','SaveRoleButton-Description','Click here to save Role')
	insert into sysLocalizationText Values(1238,      'en-us','1238','SessionLog-PageTitle','Session Log')
	insert into sysLocalizationText Values(1239,      'en-us','1239','SearchByEmail-Description','Search by E-mail')
	insert into sysLocalizationText Values(1240,      'en-us','1240','SearchByDateInterval-Label','By Date Range')
	insert into sysLocalizationText Values(1241,      'en-us','1241','SearchByInicialDate-Label','Start Date')
	insert into sysLocalizationText Values(1242,      'en-us','1242','SearchByFinalDate-Label','End Date')
	insert into sysLocalizationText Values(1243,      'en-us','1243','AccessDate-Label','Access Date')
	insert into sysLocalizationText Values(1244,      'en-us','1244','IP-Label','IP')
	insert into sysLocalizationText Values(1245,      'en-us','1245','SuperAdmin-MenuText','Super Admin')
	insert into sysLocalizationText Values(1246,      'en-us','1246','BasicsData-MenuText','Management')
	insert into sysLocalizationText Values(1247,      'en-us','1247','Monitoring-MenuText','Monitoring')
	insert into sysLocalizationText Values(1248,      'en-us','1248','Instance-MenuText','Instances')
	insert into sysLocalizationText Values(1249,      'en-us','1249','Role-MenuText','Roles')
	insert into sysLocalizationText Values(1250,      'en-us','1250','Users-MenuText','Users')
	insert into sysLocalizationText Values(1251,      'en-us','1251','ObjectPermission-MenuText','Objects Permission')
	insert into sysLocalizationText Values(1252,      'en-us','1252','Permissions-MenuText','Permissions')
	insert into sysLocalizationText Values(1253,      'en-us','1253','Localization-MenuText','Localization Texts')
	insert into sysLocalizationText Values(1254,      'en-us','1254','DataLog-MenuText','Data Log')
	insert into sysLocalizationText Values(1255,      'en-us','1255','SessionLog-MenuText','Session Log')
	insert into sysLocalizationText Values(1256,      'en-us','1256','Save-Label','Save')
	insert into sysLocalizationText Values(1257,      'en-us','1257','New-Label','New Record')

	insert into sysLocalizationText Values(1258,      'en-us','1258','SearchByGroupParameterName-Label','Group Parameter Name')
	insert into sysLocalizationText Values(1259,      'en-us','1259','SearchByGroupParameterName-Description','Search by Group Parameter Name')
	insert into sysLocalizationText Values(1260,      'en-us','1260','NewGroupParameter-Label','New Group Parameter')
	insert into sysLocalizationText Values(1261,      'en-us','1261','NewGroupParameter-Description','Click here to create new Group Parameter ')
	insert into sysLocalizationText Values(1262,      'en-us','1262','GroupParameterRecord-Label','Group Parameter Record')
	insert into sysLocalizationText Values(1263,      'en-us','1263','GroupParameterName-Label','Group Parameter Name ')
	insert into sysLocalizationText Values(1264,      'en-us','1264','SaveGroupParameterButton-Label','Save Group Parameter')
	insert into sysLocalizationText Values(1265,      'en-us','1265','SaveGroupParameterButton-Description','Click here to save Group Parameter')
	insert into sysLocalizationText Values(1266,      'en-us','1266','GroupParameter-PageTitle','Group Parameters')
	insert into sysLocalizationText Values(1267,      'en-us','1267','SearchByParameterName-Label','Parameter Name')
	insert into sysLocalizationText Values(1268,      'en-us','1268','SearchByParameterName-Description','Search by Parameter Name')
	insert into sysLocalizationText Values(1269,      'en-us','1269','NewParameter-Label','New Parameter')
	insert into sysLocalizationText Values(1270,      'en-us','1270','NewParameter-Description','Click here to create new Parameter ')
	insert into sysLocalizationText Values(1271,      'en-us','1271','ParameterRecord-Label','Parameter Record')
	insert into sysLocalizationText Values(1272,      'en-us','1272','ParameterName-Label','Parameter Name ')
	insert into sysLocalizationText Values(1273,      'en-us','1273','SaveParameterButton-Label','Save Parameter')
	insert into sysLocalizationText Values(1274,      'en-us','1274','SaveParameterButton-Description','Click here to save Parameter')
	insert into sysLocalizationText Values(1275,      'en-us','1275','Parameter-PageTitle','Parameters')
	insert into sysLocalizationText Values(1276,      'en-us','1276','ChangeUserLanguage-Title','Select a language to change')
	insert into sysLocalizationText Values(1277,      'en-us','1277','ChangeUserLanguage-Message','The User Language has been successfully changed. Log in again.')

	-- 


	insert into sysLocalizationText Values(2001,      'pt-br','2001',     'Execution-Error',    'Erro de execu��o ')
	insert into sysLocalizationText Values(2002,      'pt-br','2002',     'Validation-Error',    'Erro na valida��o de dados.')
	insert into sysLocalizationText Values(2003,      'pt-br','2003',     'Record-NotFound',    'O registro solicitado n�o foi encontrado.')
	insert into sysLocalizationText Values(2004,      'pt-br','2004',     'Login-Invalid-Password',    'Senha inv�lida. Voc� ainda tem {0} tentativas antes da conta ser desativada.')
	insert into sysLocalizationText Values(2005,      'pt-br','2005',     'Login-Attempts',    'Voc� j� utilizou as tentativas de acesso e a conta foi desativada. Solicite a ativa��o.')
	insert into sysLocalizationText Values(2006,      'pt-br','2006',     'Login-Inactive-Account',    'A conta associada ao Usu�rio n�o est� ativa. Solicite a ativa��o da conta.')
	insert into sysLocalizationText Values(2007,      'pt-br','2007',     'Login-Locked-Account',    'A conta associada ao Usu�rio est� bloqueada. Contate o administrador do sistema.')
	insert into sysLocalizationText Values(2008,      'pt-br','2008',     'Login-User-NotFound',    'Usu�rio n�o encontrado.')
	insert into sysLocalizationText Values(2009,      'pt-br','2009',     'User-Exists',    'J� existe um usu�rio com o e-mail ')
	insert into sysLocalizationText Values(2010,      'pt-br','2010',     'Email-Exists',    'O e-mail informado j� existe.')
	insert into sysLocalizationText Values(2011,      'pt-br','2011',     'Profile-NotBe-Null',    'O Perfil n�o pode ser nulo.')
	insert into sysLocalizationText Values(2012,      'pt-br','2012',     'User-Error-Exclude-Childs',    'Houve um erro ao excluir os itens filhos (Roles).')
	insert into sysLocalizationText Values(2013,      'pt-br','2013',     'User-Invalid-Password-Code',    'O c�digo de autoriza��o de troca de senha � inv�lido.')
	insert into sysLocalizationText Values(2014,      'pt-br','2014',     'Account-Active',    'A conta associada ao Usu�rio j� est� ativa.')
	insert into sysLocalizationText Values(2015,      'pt-br','2015',     'User-Invalid-Activation-Code',    'O c�digo de autoriza��o de ativa��o � inv�lido.')
	insert into sysLocalizationText Values(2016,      'pt-br','2016',     'User-No-Image',    'Envie o arquivo da imagem.')
	insert into sysLocalizationText Values(2017,      'pt-br','2017',     'User-Role-Exists',    'Esta Role j� est� associada ao usu�rio.')
	insert into sysLocalizationText Values(2018,      'pt-br','2018',     'User-Role-No-Exists',    'Esta Role n�o pertence ao usu�rio.')
	insert into sysLocalizationText Values(2019,      'pt-br','2019',     'Http-Unauthorized',    'Acesso n�o autorizado')
	insert into sysLocalizationText Values(2020,      'pt-br','2020',     'Http-NotFound',    'O recurso n�o foi encontrado')
	insert into sysLocalizationText Values(2021,      'pt-br','2021',     'Http-Forbidden',    'Perfil do usu�rio sem permiss�o de acesso')
	insert into sysLocalizationText Values(2022,      'pt-br','2022',     'Http-500Error',    'Ocorreu um erro no processamento da requisi��o.')
	insert into sysLocalizationText Values(2023,      'pt-br','2023',     'Http-ServiceUnavailable',    'O servi�o solicitado est� indispon�vel.')
	insert into sysLocalizationText Values(2024,      'pt-br','2024',     'API-Unexpected-Exception',    'Erro inesperado n�o identificado GetInnerExceptions@f2]')
	insert into sysLocalizationText Values(2025,      'pt-br','2025',     'ShortDayName-1',    'Dom')
	insert into sysLocalizationText Values(2026,      'pt-br','2026',     'ShortDayName-2',    'Seg')
	insert into sysLocalizationText Values(2027,      'pt-br','2027',     'ShortDayName-3',    'Ter')
	insert into sysLocalizationText Values(2028,      'pt-br','2028',     'ShortDayName-4',    'Qua')
	insert into sysLocalizationText Values(2029,      'pt-br','2029',     'ShortDayName-5',    'Qui')
	insert into sysLocalizationText Values(2030,      'pt-br','2030',     'ShortDayName-6',    'Sex')
	insert into sysLocalizationText Values(2031,      'pt-br','2031',     'ShortDayName-7',    'S�b')
	insert into sysLocalizationText Values(2032,      'pt-br','2032',     'MonthName-1',    'JANEIRO')
	insert into sysLocalizationText Values(2033,      'pt-br','2033',     'MonthName-2',    'FEVEREIRO')
	insert into sysLocalizationText Values(2034,      'pt-br','2034',     'MonthName-3',    'MAR�O')
	insert into sysLocalizationText Values(2035,      'pt-br','2035',     'MonthName-4',    'ABRIL')
	insert into sysLocalizationText Values(2036,      'pt-br','2036',     'MonthName-5',    'MAIO')
	insert into sysLocalizationText Values(2037,      'pt-br','2037',     'MonthName-6',    'JUNHO')
	insert into sysLocalizationText Values(2038,      'pt-br','2038',     'MonthName-7',    'JULHO')
	insert into sysLocalizationText Values(2039,      'pt-br','2039',     'MonthName-8',    'AGOSTO')
	insert into sysLocalizationText Values(2040,      'pt-br','2040',     'MonthName-9',    'SETEMBRO')
	insert into sysLocalizationText Values(2041,      'pt-br','2041',     'MonthName-10',    'OUTUBRO')
	insert into sysLocalizationText Values(2042,      'pt-br','2042',     'MonthName-11',    'NOVEMBRO')
	insert into sysLocalizationText Values(2043,      'pt-br','2043',     'MonthName-12',    'DEZEMBRO')
	insert into sysLocalizationText Values(2044,      'pt-br','2044',     'Validation-NotNull',    'n�o pode ser nulo.')
	insert into sysLocalizationText Values(2045,      'pt-br','2045',     'Validation-Max-Characters',    'O campo {0} n�o pode ter mais de maxlength caracteres.')
	insert into sysLocalizationText Values(2046,      'pt-br','2046',     'Validation-Invalid-Field',    'O campo {0} � inv�lido.')
	insert into sysLocalizationText Values(2047,      'pt-br','2047',     'Validation-Invalid-UserName',    'O campo {0} � inv�lido. N�o use caracteres especiais ou espa�os.')
	insert into sysLocalizationText Values(2048,      'pt-br','2048',     'Validation-Unique-Value',    'O campo {0} � inv�lido. O valor informado deve ser �nico.')
	insert into sysLocalizationText Values(2049,      'pt-br','2049',     'User-Instance-Exists',    'Esta Inst�ncia j� est� associada ao Usu�rio.')
	insert into sysLocalizationText Values(2050,      'pt-br','2050',     'User-Instance-No-Exists',    'Esta Inst�ncia n�o pertence ao Usu�rio.')
	insert into sysLocalizationText Values(2051,      'pt-br','2051','User-PageTitle','Gerenciamento de Usu�rios')
	insert into sysLocalizationText Values(2052,      'pt-br','2052','SearchButtonLabel','Pesquisar')
	insert into sysLocalizationText Values(2053,      'pt-br','2053','SearchingLabel','Pesquisando...')
	insert into sysLocalizationText Values(2054,      'pt-br','2054','InsertingLoadingLabel','Inserindo...')
	insert into sysLocalizationText Values(2055,      'pt-br','2055','SearchResultLabel','Resultado da Busca')
	insert into sysLocalizationText Values(2056,      'pt-br','2056','DetailsLabel','Detalhes')
	insert into sysLocalizationText Values(2057,      'pt-br','2057','NoRecordsFound','Nenhum registro encontrado')
	insert into sysLocalizationText Values(2058,      'pt-br','2058','LoadingPage','Carregando. Aguarde...')
	insert into sysLocalizationText Values(2059,      'pt-br','2059','LoadingData','Carregando a p�gina. Aguarde...')
	insert into sysLocalizationText Values(2060,      'pt-br','2060','ErrorOnExecuteSearch','Erro ao efetuar a busca')
	insert into sysLocalizationText Values(2061,      'pt-br','2061','ErrorOnReturnData','Erro ao retornar dados')
	insert into sysLocalizationText Values(2062,      'pt-br','2062','ErrorOnCreateNewRecord','Erro ao criar registro')
	insert into sysLocalizationText Values(2063,      'pt-br','2063','AfterSaveAnswering','O novo registro foi criado com sucesso. Deseja continuar inserindo ?')
	insert into sysLocalizationText Values(2064,      'pt-br','2064','NoticeLabel','Aviso')
	insert into sysLocalizationText Values(2065,      'pt-br','2065','SuccessLabel','Sucesso')
	insert into sysLocalizationText Values(2066,      'pt-br','2066','SuccessSaveMessage','O registro foi salvo com sucesso.')
	insert into sysLocalizationText Values(2067,      'pt-br','2067','Email-Label','E-mail')
	insert into sysLocalizationText Values(2068,      'pt-br','2068','UserName-Label','Nome de Usu�rio')
	insert into sysLocalizationText Values(2069,      'pt-br','2069','Password-Label','Senha')
	insert into sysLocalizationText Values(2070,      'pt-br','2070','Instance-Label','Inst�ncia')
	insert into sysLocalizationText Values(2071,      'pt-br','2071','Role-Label','Perfim')
	insert into sysLocalizationText Values(2072,      'pt-br','2072','Yes-Text','Sim')
	insert into sysLocalizationText Values(2073,      'pt-br','2073','No-Text','N�o')
	insert into sysLocalizationText Values(2074,      'pt-br','2074','Saving-Label','Salvando...')
	insert into sysLocalizationText Values(2075,      'pt-br','2075','Edit-Label','Editar')
	insert into sysLocalizationText Values(2076,      'pt-br','2076','Date-Label','Data')
	insert into sysLocalizationText Values(2077,      'pt-br','2077','Field-Label','Campo')
	insert into sysLocalizationText Values(2078,      'pt-br','2078','Value-Label','Valor')
	insert into sysLocalizationText Values(2079,      'pt-br','2079','SelectItem-Description','Selecione um Item')
	insert into sysLocalizationText Values(2080,      'pt-br','2080','AllItem-Description','Todos')
	insert into sysLocalizationText Values(2081,      'pt-br','2081','Welcome-Label','Bem-vindo ao GW Template')
	insert into sysLocalizationText Values(2082,      'pt-br','2082','LoginTitle-Label','Entrar')
	insert into sysLocalizationText Values(2083,      'pt-br','2083','LoginTitle-Description','Insira seu login e senha')
	insert into sysLocalizationText Values(2084,      'pt-br','2084','InputEmail-Description','Digite seu e-mail de cadastro')
	insert into sysLocalizationText Values(2085,      'pt-br','2085','InputPassword-Description','Digite sua senha')
	insert into sysLocalizationText Values(2086,      'pt-br','2086','ForgetPassword-Description','Esqueceu a senha ?')
	insert into sysLocalizationText Values(2087,      'pt-br','2087','LoginButton-Label','Confirmar')
	insert into sysLocalizationText Values(2088,      'pt-br','2088','LoginLoading-Label','Entrando...')
	insert into sysLocalizationText Values(2089,      'pt-br','2089','SendText-Description','Enviando...')
	insert into sysLocalizationText Values(2090,      'pt-br','2090','ActiveAccountButton-Label','Ativar Conta')
	insert into sysLocalizationText Values(2091,      'pt-br','2091','ActiveAccount-Label','Ativa��o de Conta')
	insert into sysLocalizationText Values(2092,      'pt-br','2092','ActiveAccount-Description','N�o consegue acessar ?')
	insert into sysLocalizationText Values(2093,      'pt-br','2093','ActiveAccount-Step1','Envie o c�digo de ativa��o para o seu e-mail de cadastro')
	insert into sysLocalizationText Values(2094,      'pt-br','2094','ActiveAccount-Step2','Informe o c�digo de ativa��o recebido')
	insert into sysLocalizationText Values(2095,      'pt-br','2095','SendCodeButton-Label','Enviar C�digo')
	insert into sysLocalizationText Values(2096,      'pt-br','2096','ActiveLoading-Label','Solicitar Ativa��o')
	insert into sysLocalizationText Values(2097,      'pt-br','2097','InputCode-Description','Digite o c�digo recebido por e-mail')
	insert into sysLocalizationText Values(2098,      'pt-br','2098','Unlogged-Label','Voc� n�o est� logado')
	insert into sysLocalizationText Values(2099,      'pt-br','2099','MyProfile-Label','Meu Perfil')
	insert into sysLocalizationText Values(2100,      'pt-br','2100','MainProfileData-Label','Dados do Perfil')
	insert into sysLocalizationText Values(2101,      'pt-br','2101','AlterPassword-Label','Trocar Senha')
	insert into sysLocalizationText Values(2102,      'pt-br','2102','LanguageRole-Label','Linguagem')
	insert into sysLocalizationText Values(2103,      'pt-br','2103','AlterProfileImage-Label','Alterar a imagem de perfil')
	insert into sysLocalizationText Values(2104,      'pt-br','2104','AlterPasswordStep1-Label','Clique no link abaixo para receber um email com o c�digo de seguran�a para troca de senha')
	insert into sysLocalizationText Values(2105,      'pt-br','2105','AlterPasswordStep2-Label','Ap�s receber c�digo, preencha as informa��es abaixo e clique em Altera Senha')
	insert into sysLocalizationText Values(2106,      'pt-br','2106','InputNewPassword-Label','Digite a nova senha')
	insert into sysLocalizationText Values(2107,      'pt-br','2107','AlterPasswordButton-Label','Alterar a Senha')
	insert into sysLocalizationText Values(2108,      'pt-br','2108','AlterPasswordButton-Loading','Alterando a senha....')
	insert into sysLocalizationText Values(2109,      'pt-br','2109','InvalidCredentials-Title','Credenciais Inv�lidas')
	insert into sysLocalizationText Values(2110,      'pt-br','2110','InvalidCredentials-Message','E-mail ou senha inv�lidos!')
	insert into sysLocalizationText Values(2111,      'pt-br','2111','TemporaryPassword-Title','Senha Enviada')
	insert into sysLocalizationText Values(2112,      'pt-br','2112','TemporaryPassword-Message','Uma senha tempor�ria foi enviada para o seu e-mail de cadastro. Ao logar, solicite a troca de senha.')
	insert into sysLocalizationText Values(2113,      'pt-br','2113','SuccessActivated-Title','Conta Ativada com Sucesso')
	insert into sysLocalizationText Values(2114,      'pt-br','2114','SuccessActivated-Message','A conta foi ativada com sucesso. Voc� j� pode efetuar login.')
	insert into sysLocalizationText Values(2115,      'pt-br','2115','ActivateCode-Title','C�digo Enviado')
	insert into sysLocalizationText Values(2116,      'pt-br','2116','ActivateCode-Message','O c�digo de seguran�a foi enviado por e-mail.')
	insert into sysLocalizationText Values(2117,      'pt-br','2117','PasswordChanged-Title','Senha Alterada')
	insert into sysLocalizationText Values(2118,      'pt-br','2118','PasswordChanged-Message','A senha foi alterada com sucesso. Efetue login novamente.')
	insert into sysLocalizationText Values(2119,      'pt-br','2119','ImageChanged-Title','Imagem Alterada')
	insert into sysLocalizationText Values(2120,      'pt-br','2120','ImageChanged-Message','A imagem de perfil foi alterada. No pr�ximo login a nova imagem ser� exibida.')
	insert into sysLocalizationText Values(2121,      'pt-br','2121','SearchByEmail-Label','Por E-mail')
	insert into sysLocalizationText Values(2122,      'pt-br','2122','SearchByUserName-Label','Por Nome de Usu�rio')
	insert into sysLocalizationText Values(2123,      'pt-br','2123','SearchByEmail-Description','Pesquisar por E-mail')
	insert into sysLocalizationText Values(2124,      'pt-br','2124','SearchByUserName-Description','Pesquisar por Nome de Usu�rio')
	insert into sysLocalizationText Values(2125,      'pt-br','2125','SearchByInstance-Label','Por Inst�ncia')
	insert into sysLocalizationText Values(2126,      'pt-br','2126','SearchByRole-Label','Por Perfil')
	insert into sysLocalizationText Values(2127,      'pt-br','2127','NewUser-Label','Novo Usu�ro')
	insert into sysLocalizationText Values(2128,      'pt-br','2128','NewUser-Description','Clique aqui para criar novo usu�rio')
	insert into sysLocalizationText Values(2129,      'pt-br','2129','Active-Label','Ativo')
	insert into sysLocalizationText Values(2130,      'pt-br','2130','Locked-Label','Bloqueado')
	insert into sysLocalizationText Values(2131,      'pt-br','2131','MainData-Label','Dados Principais')
	insert into sysLocalizationText Values(2132,      'pt-br','2132','User-SecondTabLabel','Inst�ncia e Perfil')
	insert into sysLocalizationText Values(2133,      'pt-br','2133','CreateDate-Label','Data de Cadastro')
	insert into sysLocalizationText Values(2134,      'pt-br','2134','LastLoginDate-Label','Data do �ltimo Acesso')
	insert into sysLocalizationText Values(2135,      'pt-br','2135','DefaultLanguage-Label','Idioma Padr�o')
	insert into sysLocalizationText Values(2136,      'pt-br','2136','LastLoginIP-Label','IP do �ltimo Acesso')
	insert into sysLocalizationText Values(2137,      'pt-br','2137','LoginCounter-Label','Total de Acessos')
	insert into sysLocalizationText Values(2138,      'pt-br','2138','PasswordRecovery-Label','C�digo de Recupera��o de Senha')
	insert into sysLocalizationText Values(2139,      'pt-br','2139','AlterInstance-Label','Alterar a Inst�ncia')
	insert into sysLocalizationText Values(2140,      'pt-br','2140','AlterInstance-Description','Selecione uma Inst�ncia para alterar')
	insert into sysLocalizationText Values(2141,      'pt-br','2141','Altering-Label','Alterando...')
	insert into sysLocalizationText Values(2142,      'pt-br','2142','AlterRole-Label','Alterar Perfil')
	insert into sysLocalizationText Values(2143,      'pt-br','2143','AlterRole-Description','Selecione um Perfil para alterar')
	insert into sysLocalizationText Values(2144,      'pt-br','2144','UserStatus-Label','Status do Usu�rio')
	insert into sysLocalizationText Values(2145,      'pt-br','2145','ChangeUserState-Description','Clique aqui para alterar o status')
	insert into sysLocalizationText Values(2146,      'pt-br','2146','ChangeUserState-Label','Atualizar Status')
	insert into sysLocalizationText Values(2147,      'pt-br','2147','CreateUser-Label','Novo Usu�rio')
	insert into sysLocalizationText Values(2148,      'pt-br','2148','CreateUser-Description','Clique aqui para cciar o novo usu�rio')
	insert into sysLocalizationText Values(2149,      'pt-br','2149','CreateUserButton-Label','Salvar')
	insert into sysLocalizationText Values(2150,      'pt-br','2150','AlterStatus-Error','Error ao alterar o status do usu�rio')
	insert into sysLocalizationText Values(2151,      'pt-br','2151','AlterStatus-Success','O status do usu�rio foi alterado com sucesso')
	insert into sysLocalizationText Values(2152,      'pt-br','2152','AlterInstance-Error','Erro ao alterar inst�ncia')
	insert into sysLocalizationText Values(2153,      'pt-br','2153','AlterInstance-Success','A inst�ncia foi alterada com sucesso')
	insert into sysLocalizationText Values(2154,      'pt-br','2154','AlterRole-Error','Erro ao alterar o perfil.')
	insert into sysLocalizationText Values(2155,      'pt-br','2155','AlterRole-Success','O status foi alterado com sucesso')
	insert into sysLocalizationText Values(2156,      'pt-br','2156','Instance-PageTitle','Inst�ncia')
	insert into sysLocalizationText Values(2157,      'pt-br','2157','SearchByInstanceName-Label','Por Nome da Inst�ncia')
	insert into sysLocalizationText Values(2158,      'pt-br','2158','SearchByInstanceName-Description','Pesquisar por Nome da Inst�ncia')
	insert into sysLocalizationText Values(2159,      'pt-br','2159','SearchByInstanceTypeName-Label','Por Tipo da Inst�ncia')
	insert into sysLocalizationText Values(2160,      'pt-br','2160','SearchByInstanceTypeName-Description','Pesquisar por Tipo da Inst�ncia')
	insert into sysLocalizationText Values(2161,      'pt-br','2161','NewInstance-Label','Nova Inst�ncia')
	insert into sysLocalizationText Values(2162,      'pt-br','2162','NewInstance-Description','Clique aqui para criar uma nova Inst�ncia')
	insert into sysLocalizationText Values(2163,      'pt-br','2163','InstanceTypeName-Label','Tipo de Inst�ncia')
	insert into sysLocalizationText Values(2164,      'pt-br','2164','InstanceName-Label','Nome da Inst�ncia')
	insert into sysLocalizationText Values(2165,      'pt-br','2165','InstanceRecord-Label','Dados da Inst�ncia')
	insert into sysLocalizationText Values(2166,      'pt-br','2166','SaveInstanceButton-Label','Salvar Inst�ncia')
	insert into sysLocalizationText Values(2167,      'pt-br','2167','SaveInstanceButton-Description','Clque aqui para salvar a Inst�ncia')
	insert into sysLocalizationText Values(2168,      'pt-br','2168','DataLog-PageTitle','Log de Dados')
	insert into sysLocalizationText Values(2169,      'pt-br','2169','SearchByOperationType-Label','Por Tipo de Opera��o')
	insert into sysLocalizationText Values(2170,      'pt-br','2170','SearchByObject-Label','Por Objeto')
	insert into sysLocalizationText Values(2171,      'pt-br','2171','SearchByIntervalDate-Label','Por Intervalo de Datas')
	insert into sysLocalizationText Values(2172,      'pt-br','2172','SearchByInicialDate-Label','Data Inicial')
	insert into sysLocalizationText Values(2173,      'pt-br','2173','SearchByFinalDate-Label','Data Final')
	insert into sysLocalizationText Values(2174,      'pt-br','2174','SearchByRecordID-Label','Por ID do Registro')
	insert into sysLocalizationText Values(2175,      'pt-br','2175','TableName-Label','Tabela')
	insert into sysLocalizationText Values(2176,      'pt-br','2176','OperationText-Label','Opera��o')
	insert into sysLocalizationText Values(2177,      'pt-br','2177','LogID-Label','Log ID')
	insert into sysLocalizationText Values(2178,      'pt-br','2178','LogInformation-Label','Informa��o do Log')
	insert into sysLocalizationText Values(2179,      'pt-br','2179','ShowTimeLine-Label','Exibir Linha do Tempo')
	insert into sysLocalizationText Values(2180,      'pt-br','2180','OldVersionData-Label','Vers�o Antiga')
	insert into sysLocalizationText Values(2181,      'pt-br','2181','HasNoOldVersion-Label','N�o existe vers�o antiga')
	insert into sysLocalizationText Values(2182,      'pt-br','2182','CurrentVersionData-Label','Vers�o Atual')
	insert into sysLocalizationText Values(2183,      'pt-br','2183','HasNoCurrentVersion-Label','N�o existe vers�o atual')
	insert into sysLocalizationText Values(2184,      'pt-br','2184','RecordTimeLine-Label','Linha do Tempo do Registro')
	insert into sysLocalizationText Values(2185,      'pt-br','2185','HasNoTimeLine-Label','N�o existe dados da Linha do Tempo')
	insert into sysLocalizationText Values(2186,      'pt-br','2186','Localization-PageTitle','Textos de Localiza��o')
	insert into sysLocalizationText Values(2187,      'pt-br','2187','SearchByLanguage-Label','Por Linguagem')
	insert into sysLocalizationText Values(2188,      'pt-br','2188','SearchByLanguage-Description','Pesquisar por Linguagem')
	insert into sysLocalizationText Values(2189,      'pt-br','2189','SearchByLocalizationCode-Label','Por C�digo')
	insert into sysLocalizationText Values(2190,      'pt-br','2190','SearchByLocalizationCode-Description','Pesquisar por C�digo')
	insert into sysLocalizationText Values(2191,      'pt-br','2191','SearchByLocalizationName-Label','Por Nome')
	insert into sysLocalizationText Values(2192,      'pt-br','2192','SearchByLocalizationName-Description','Pesquisar por Nome')
	insert into sysLocalizationText Values(2193,      'pt-br','2193','SearchByLocalizationText-Label','Por Texto')
	insert into sysLocalizationText Values(2194,      'pt-br','2194','SearchByLocalizationText-Description','Pesquisar por Texto')
	insert into sysLocalizationText Values(2195,      'pt-br','2195','Language-Label','Linguagem')
	insert into sysLocalizationText Values(2196,      'pt-br','2196','LocalizationCode-Label','C�digo')
	insert into sysLocalizationText Values(2197,      'pt-br','2197','LocalizationName-Label','Nome')
	insert into sysLocalizationText Values(2198,      'pt-br','2198','LocalizationRecord-Label','Dados do Texto de Localiza��o')
	insert into sysLocalizationText Values(2199,      'pt-br','2199','LocalizationText-Label','Texto ')
	insert into sysLocalizationText Values(2200,      'pt-br','2200','SaveLocalizationButton-Label','Salvar Texto de Localiza��o')
	insert into sysLocalizationText Values(2201,      'pt-br','2201','SaveLocalizationButton-Description','Clique aqui para salvar os dados.')
	insert into sysLocalizationText Values(2202,      'pt-br','2202','NewLocalization-Label','Novo Texto de Localiza��o')
	insert into sysLocalizationText Values(2203,      'pt-br','2203','ObjectPermission-PageTitle','Objeto de Permiss�o')
	insert into sysLocalizationText Values(2204,      'pt-br','2204','SearchByObjectName-Label','Por Nome')
	insert into sysLocalizationText Values(2205,      'pt-br','2205','SearchByObjectName-Description','Pesquisar por Nome')
	insert into sysLocalizationText Values(2206,      'pt-br','2206','SearchByObjectCode-Label','Por C�digo')
	insert into sysLocalizationText Values(2207,      'pt-br','2207','SearchByObjectCode-Description','Pesquisar por C�digo')
	insert into sysLocalizationText Values(2208,      'pt-br','2208','NewObject-Label','Novo Objeto de Permiss�o')
	insert into sysLocalizationText Values(2209,      'pt-br','2209','ObjectName-Label','Nome')
	insert into sysLocalizationText Values(2210,      'pt-br','2210','ObjectCode-Label','C�digo')
	insert into sysLocalizationText Values(2211,      'pt-br','2211','ObjectPermissionRecord-Label','Dados do Objeto de Permiss�o')
	insert into sysLocalizationText Values(2212,      'pt-br','2212','SaveObjectPermissionButton-Label','Salvar Objeto de Permiss�o')
	insert into sysLocalizationText Values(2213,      'pt-br','2213','SaveObjectPermissionButton-Description','Clique aqui para salvar o Objeto de Permiss�o')
	insert into sysLocalizationText Values(2214,      'pt-br','2214','Permission-PageTitle','Permiss�es')
	insert into sysLocalizationText Values(2215,      'pt-br','2215','SearchByObjectPermission-Label','Por Objeto de Permiss�o')
	insert into sysLocalizationText Values(2216,      'pt-br','2216','SearchByRole-Label','Por Perfil')
	insert into sysLocalizationText Values(2217,      'pt-br','2217','SearchByUser-Label','Por Usu�rio')
	insert into sysLocalizationText Values(2218,      'pt-br','2218','NewPermission-Label','Nova Permiss�o')
	insert into sysLocalizationText Values(2219,      'pt-br','2219','NewPermission-Description','Clique aqui para inserir uma nova Permiss�o')
	insert into sysLocalizationText Values(2220,      'pt-br','2220','ObjectName-Label','Objeto de Permiss�o')
	insert into sysLocalizationText Values(2221,      'pt-br','2221','RoleName-Label','Perfil')
	insert into sysLocalizationText Values(2222,      'pt-br','2222','PermissionRecord-Label','Dados da Permiss�o')
	insert into sysLocalizationText Values(2223,      'pt-br','2223','PermissionType-Label','Tipo de Permiss�o')
	insert into sysLocalizationText Values(2224,      'pt-br','2224','ReadStatus-Label','Status de Leitura')
	insert into sysLocalizationText Values(2225,      'pt-br','2225','SaveStatus-Label','Status de Grava��o')
	insert into sysLocalizationText Values(2226,      'pt-br','2226','DeleteStatus-Label','Status de Exclus�o')
	insert into sysLocalizationText Values(2227,      'pt-br','2227','SavePermissionButton-Label','Salvar Permiss�o')
	insert into sysLocalizationText Values(2228,      'pt-br','2228','SavePermissionButton-Description','Clique aqui para salvar a Permiss�o')
	insert into sysLocalizationText Values(2229,      'pt-br','2229','Role-PageTitle','Perfil')
	insert into sysLocalizationText Values(2230,      'pt-br','2230','SearchByRoleName-Label','Por Nome do Perfil')
	insert into sysLocalizationText Values(2231,      'pt-br','2231','SearchByRoleName-Description','Pesquisar por Nome do Perfil')
	insert into sysLocalizationText Values(2232,      'pt-br','2232','NewRole-Label','Novo Perfil')
	insert into sysLocalizationText Values(2233,      'pt-br','2233','NewRole-Description','Clique aqui para criar um novo Perfil')
	insert into sysLocalizationText Values(2234,      'pt-br','2234','RoleName-Label','Nome do Perfil')
	insert into sysLocalizationText Values(2235,      'pt-br','2235','RoleRecord-Label','Dados do Perfil')
	insert into sysLocalizationText Values(2236,      'pt-br','2236','SaveRoleButton-Label','Salvar Perfil')
	insert into sysLocalizationText Values(2237,      'pt-br','2237','SaveRoleButton-Description','Clique aqui para salvar o Perfil')
	insert into sysLocalizationText Values(2238,      'pt-br','2238','SessionLog-PageTitle','Dados de Sess�o')
	insert into sysLocalizationText Values(2239,      'pt-br','2239','SearchByEmail-Description','Pesquisar por E-mail')
	insert into sysLocalizationText Values(2240,      'pt-br','2240','SearchByDateInterval-Label','Por Intervalo de Dados')
	insert into sysLocalizationText Values(2241,      'pt-br','2241','SearchByInicialDate-Label','Data Inicial')
	insert into sysLocalizationText Values(2242,      'pt-br','2242','SearchByFinalDate-Label','Data Final')
	insert into sysLocalizationText Values(2243,      'pt-br','2243','AccessDate-Label','Data de Acesso')
	insert into sysLocalizationText Values(2244,      'pt-br','2244','IP-Label','IP')
	insert into sysLocalizationText Values(2245,      'pt-br','2245','SuperAdmin-MenuText','Super Admin')
	insert into sysLocalizationText Values(2246,      'pt-br','2246','BasicsData-MenuText','Gerenciamento')
	insert into sysLocalizationText Values(2247,      'pt-br','2247','Monitoring-MenuText','Monitoramento')
	insert into sysLocalizationText Values(2248,      'pt-br','2248','Instance-MenuText','Inst�ncias')
	insert into sysLocalizationText Values(2249,      'pt-br','2249','Role-MenuText','Perfils')
	insert into sysLocalizationText Values(2250,      'pt-br','2250','Users-MenuText','Usu�rios')
	insert into sysLocalizationText Values(2251,      'pt-br','2251','ObjectPermission-MenuText','Objetos de Permiss�es')
	insert into sysLocalizationText Values(2252,      'pt-br','2252','Permissions-MenuText','Permiss�es')
	insert into sysLocalizationText Values(2253,      'pt-br','2253','Localization-MenuText','Textos de Localiza��o')
	insert into sysLocalizationText Values(2254,      'pt-br','2254','DataLog-MenuText','Log de Dados')
	insert into sysLocalizationText Values(2255,      'pt-br','2255','SessionLog-MenuText','Log de Sess�o')
	insert into sysLocalizationText Values(2256,      'pt-br','2256','Save-Label','Salvar')
	insert into sysLocalizationText Values(2257,      'pt-br','2257','New-Label','Novo Registro')

	insert into sysLocalizationText Values(2258,      'pt-br','2258','SearchByGroupParameterName-Label','Nome do Grupo de Par�metro')
	insert into sysLocalizationText Values(2259,      'pt-br','2259','SearchByGroupParameterName-Description','Pesquisar por Nome de Grupo de Par�metro')
	insert into sysLocalizationText Values(2260,      'pt-br','2260','NewGroupParameter-Label','Novo Grupo de Par�metro')
	insert into sysLocalizationText Values(2261,      'pt-br','2261','NewGroupParameter-Description','Clique aqui para criar um novo Grupo de Par�metro ')
	insert into sysLocalizationText Values(2262,      'pt-br','2262','GroupParameterRecord-Label','Dados do Grupo de Par�metro')
	insert into sysLocalizationText Values(2263,      'pt-br','2263','GroupParameterName-Label','Nome do Grupo de Par�metro')
	insert into sysLocalizationText Values(2264,      'pt-br','2264','SaveGroupParameterButton-Label','Save Group Parameter')
	insert into sysLocalizationText Values(2265,      'pt-br','2265','SaveGroupParameterButton-Description','Clique aqui para salvar o Grupo de Par�metro')
	insert into sysLocalizationText Values(2266,      'pt-br','2266','GroupParameter-PageTitle','Grupo de Par�metros')
	insert into sysLocalizationText Values(2267,      'pt-br','2267','SearchByParameterName-Label','Nome do Par�metro')
	insert into sysLocalizationText Values(2268,      'pt-br','2268','SearchByParameterName-Description','Pesquisar por Nome do Par�metro')
	insert into sysLocalizationText Values(2269,      'pt-br','2269','NewParameter-Label','Novo Par�metro')
	insert into sysLocalizationText Values(2270,      'pt-br','2270','NewParameter-Description','Clique aqui par acriar um novo Par�metro')
	insert into sysLocalizationText Values(2271,      'pt-br','2271','ParameterRecord-Label','Dados do Par�metro')
	insert into sysLocalizationText Values(2272,      'pt-br','2272','ParameterName-Label','Nome do Par�metro')
	insert into sysLocalizationText Values(2273,      'pt-br','2273','SaveParameterButton-Label','Salvar Par�metro')
	insert into sysLocalizationText Values(2274,      'pt-br','2274','SaveParameterButton-Description','Clique aqui para salvar o Par�metro')
	insert into sysLocalizationText Values(2275,      'pt-br','2275','Parameter-PageTitle','Par�metros')
	insert into sysLocalizationText Values(2276,      'pt-br','2276','ChangeUserLanguage-Title','Selecione um item para trocar a linguagem padr�o')
	insert into sysLocalizationText Values(2277,      'pt-br','2277','ChangeUserLanguage-Message','A linguagem foi trocada com sucesso. � preciso logar novamente para refletir as altera��es.')


END


GO
