-- table Person

CREATE TABLE [dbo].[Person](
	[PersonID] [bigint] NOT NULL,
	[PersonName] [varchar](50) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Email] [varchar](255) NOT NULL,
	[PhoneNamber] [varchar](15) NOT NULL,
	[Seq] [bigint] IDENTITY(1,1) NOT NULL,
	[TSCreate] [datetime] NULL,
	[TSLastUpdate] [datetime] NULL,
 CONSTRAINT [pk_Person] PRIMARY KEY CLUSTERED 
(
	[PersonID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


-- table PersonContacts

CREATE TABLE [dbo].[PersonContacts](
	[PersonContactID] [bigint] NOT NULL,
	[PersonID] [bigint] NOT NULL,
	[ContactName] [varchar](50) NOT NULL,
	[Email] [varchar](255) NOT NULL,
	[CellPhoneNumber] [varchar](16) NOT NULL,
 CONSTRAINT [pk_PersonContact] PRIMARY KEY CLUSTERED 
(
	[PersonContactID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[PersonContacts]  WITH NOCHECK ADD  CONSTRAINT [fk_Person_PersonContact] FOREIGN KEY([PersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO

ALTER TABLE [dbo].[PersonContacts] CHECK CONSTRAINT [fk_Person_PersonContact]
GO

