-- ALTERACOES V-1-0

-- CRIAÇÃO DOS CAMPOS PADRÃO

-- long Seq 
-- DateTime TSCreate 
-- DateTime TSLastUpdate 

alter table sysDataLog add Seq bigint IDENTITY(1,1)
alter table sysDataLog add TSCreate datetime
alter table sysDataLog add TSLastUpdate datetime

update sysDataLog set TSCreate = getdate(), TSLastUpdate = getdate()

--- 

alter table sysExceptionLog add Seq bigint IDENTITY(1,1)
alter table sysExceptionLog add TSCreate datetime
alter table sysExceptionLog add TSLastUpdate datetime

update sysExceptionLog set TSCreate = getdate(), TSLastUpdate = getdate()


--- 

alter table sysGroupParameter add Seq bigint IDENTITY(1,1)
alter table sysGroupParameter add TSCreate datetime
alter table sysGroupParameter add TSLastUpdate datetime

update sysGroupParameter set TSCreate = getdate(), TSLastUpdate = getdate()

-- 

alter table sysInstance add Seq bigint IDENTITY(1,1)
alter table sysInstance add TSCreate datetime
alter table sysInstance add TSLastUpdate datetime

update sysInstance set TSCreate = getdate(), TSLastUpdate = getdate()


-- 

alter table sysLocalizationText add Seq bigint IDENTITY(1,1)
alter table sysLocalizationText add TSCreate datetime
alter table sysLocalizationText add TSLastUpdate datetime

update sysLocalizationText set TSCreate = getdate(), TSLastUpdate = getdate()


-- 

alter table sysObjectPermission add Seq bigint IDENTITY(1,1)
alter table sysObjectPermission add TSCreate datetime
alter table sysObjectPermission add TSLastUpdate datetime

update sysObjectPermission set TSCreate = getdate(), TSLastUpdate = getdate()


-- 

alter table sysParameter add Seq bigint IDENTITY(1,1)
alter table sysParameter add TSCreate datetime
alter table sysParameter add TSLastUpdate datetime

update sysParameter set TSCreate = getdate(), TSLastUpdate = getdate()


-- 

alter table sysPermission add Seq bigint IDENTITY(1,1)
alter table sysPermission add TSCreate datetime
alter table sysPermission add TSLastUpdate datetime

update sysPermission set TSCreate = getdate(), TSLastUpdate = getdate()


-- 

alter table sysRole add Seq bigint IDENTITY(1,1)
alter table sysRole add TSCreate datetime
alter table sysRole add TSLastUpdate datetime

update sysRole set TSCreate = getdate(), TSLastUpdate = getdate()


-- 

alter table sysSessionLog add Seq bigint IDENTITY(1,1)
alter table sysSessionLog add TSCreate datetime
alter table sysSessionLog add TSLastUpdate datetime

update sysSessionLog set TSCreate = getdate(), TSLastUpdate = getdate()


-- 

alter table sysUser add Seq bigint IDENTITY(1,1)
alter table sysUser add TSCreate datetime
alter table sysUser add TSLastUpdate datetime

update sysUser set TSCreate = getdate(), TSLastUpdate = getdate()


-- 

alter table sysUserInstances add Seq bigint IDENTITY(1,1)
alter table sysUserInstances add TSCreate datetime
alter table sysUserInstances add TSLastUpdate datetime

update sysUserInstances set TSCreate = getdate(), TSLastUpdate = getdate()


-- 

alter table sysUserRoles add Seq bigint IDENTITY(1,1)
alter table sysUserRoles add TSCreate datetime
alter table sysUserRoles add TSLastUpdate datetime

update sysUserRoles set TSCreate = getdate(), TSLastUpdate = getdate()


-- CRIAÇÃO DA TABELA DE CONFIGURAÇÕES

-- 1.14: sysConfigs

CREATE TABLE [dbo].[sysConfigs](
	[ConfigID] [bigint] NOT NULL,	
	[ConfigName] [varchar](50) NOT NULL,
	[ConfigValue] [varchar](255) NOT NULL,
	[IsActive] [bit] NOT NULL,	
	[Seq] [bigint] IDENTITY(1,1),
	[TSCreate] [datetime],
	[TSLastUpdate] [datetime]
 CONSTRAINT [pk_sysConfigs] PRIMARY KEY CLUSTERED 
(
	[ConfigID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] 
GO


-- CRIAÇÃO DA TABELA DE LINGUAGENS

-- 1.15: sysLanguages

CREATE TABLE [dbo].[sysLanguage](
	[LanguageID] [bigint] NOT NULL,	
	[LanguageName] [varchar](10) NOT NULL,
	[Description] [varchar](255) NOT NULL,	
	[Seq] [bigint] IDENTITY(1,1),
	[TSCreate] [datetime],
	[TSLastUpdate] [datetime]
 CONSTRAINT [pk_sysLanguage] PRIMARY KEY CLUSTERED 
(
	[LanguageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] 
GO

insert into sysLanguage values (1,'en-US','English',getdate(),getdate())
insert into sysLanguage values (2,'pt-BR','Portuguese',getdate(),getdate())


-- AJUSTAR CAMPO LanguageID nas tabelas

-- tabela sysLocalizationText

alter table sysLocalizationText add LanguageID [bigint] NULL	

alter table [dbo].[sysLocalizationText]  WITH NOCHECK 
ADD  CONSTRAINT [fk_sysLocalizationText_Language] FOREIGN KEY([LanguageID])
REFERENCES [dbo].[sysLanguage] ([LanguageID])


begin transaction

update [sysLocalizationText] set [LanguageID] = 1 where [Language]='en-us'
update [sysLocalizationText] set [LanguageID] = 2 where [Language]='pt-br'

-- commit 
-- rollback

alter table sysLocalizationText drop column [Language]



-- tabela sysUser

alter table sysUser add LanguageID [bigint] NULL	

alter table [dbo].[sysUser]  WITH NOCHECK 
ADD  CONSTRAINT [fk_sysUser_Language] FOREIGN KEY([LanguageID])
REFERENCES [dbo].[sysLanguage] ([LanguageID])


begin transaction

update [sysUser] set [LanguageID] = 1 where [DefaultLanguage]='en-us'
update [sysUser] set [LanguageID] = 2 where [DefaultLanguage]='pt-br'

-- commit 
-- rollback

alter table [sysUser] drop column [DefaultLanguage]