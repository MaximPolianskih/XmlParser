USE [StreamLabs]
GO
/****** Object:  Table [dbo].[LicenseBroadcasting]    Script Date: 18.06.2020 9:02:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LicenseBroadcasting](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ParamId] [int] NULL,
	[LicNum] [int] NULL,
	[LicEisId] [int] NULL,
	[DateStart] [datetime] NULL,
	[DateServiceStart] [datetime] NULL,
	[TypeDateServiceStart] [int] NULL,
	[DateEnd] [datetime] NULL,
	[StatusId] [int] NULL,
	[StatusName] [nvarchar](100) NULL,
	[DateSuspension] [datetime] NULL,
	[DateRenewal] [datetime] NULL,
	[DateAnnulment] [datetime] NULL,
	[Seria] [nvarchar](100) NULL,
	[ActivityKind] [int] NULL,
	[BroadcastArea] [int] NULL,
	[BroadcastAreaName] [nvarchar](100) NULL,
	[MaxTimeWeek] [int] NULL,
	[PerTime] [nvarchar](100) NULL,
	[Place] [nvarchar](1000) NULL,
	[TenderInfo] [nvarchar](1000) NULL,
	[ConceptIsProgram] [bit] NULL,
	[IsAdv] [bit] NULL,
	[OwnerId] [int] NULL,
	[OrderNum] [nvarchar](100) NULL,
	[OrderDate] [datetime] NULL,
	[IsActual] [bit] NULL,
	[LastChangeDate] [datetime] NULL,
 CONSTRAINT [PK_LicenseBroadcasting] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Owner]    Script Date: 18.06.2020 9:02:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Owner](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrgId] [int] NULL,
	[OrgVId] [int] NULL,
	[OrgName] [nvarchar](1000) NULL,
	[RegionId] [int] NULL,
	[RegionName] [nvarchar](1000) NULL,
	[AddressLegal] [nvarchar](1000) NULL,
	[INN] [nvarchar](100) NULL,
	[ORGN] [nvarchar](100) NULL,
	[KPP] [nvarchar](100) NULL,
	[OrgTypeId] [int] NULL,
	[OrgTypeName] [nvarchar](100) NULL,
	[BusinessKindId] [int] NULL,
	[BusinessKindName] [nvarchar](1000) NULL,
 CONSTRAINT [PK_Owner] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[LicenseBroadcasting]  WITH CHECK ADD  CONSTRAINT [FK_LicenseBroadcasting_Owner] FOREIGN KEY([OwnerId])
REFERENCES [dbo].[Owner] ([Id])
GO
ALTER TABLE [dbo].[LicenseBroadcasting] CHECK CONSTRAINT [FK_LicenseBroadcasting_Owner]
GO
