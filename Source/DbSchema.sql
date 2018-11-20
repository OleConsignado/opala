USE [OtcProjectModelDb]
GO
/****** Object:  Table [dbo].[Client]    Script Date: 16/08/2018 17:08:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Cliente](
	[Id] [uniqueidentifier] NOT NULL,
	[Nome] [varchar](100) NOT NULL,
	[Email] [varchar](150) NOT NULL,
	[Telefone] [varchar](20) NOT NULL,
	[Rua] [varchar](150) NOT NULL,
	[Numero] [varchar](10) NOT NULL,
	[Bairro] [varchar](50) NOT NULL,
	[Cidade] [varchar](50) NOT NULL,
	[Estado] [char](2) NOT NULL,
	[Pais] [varchar](30) NOT NULL,
	[Cep] [char](8) NOT NULL,
	[Ativo] [bit] NOT NULL,
	[Excluido] [bit] NOT NULL,
	[DataExclusao] [datetimeoffset](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING ON
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 16/08/2018 17:08:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Pagamento](
	[Id] [uniqueidentifier] NOT NULL,
	[AssinaturaId] [uniqueidentifier] NOT NULL,
	[DataPagamento] [datetimeoffset](7) NOT NULL,
	[DataExpiracao] [datetimeoffset](7) NOT NULL,
	[Total] [decimal](18, 2) NOT NULL,
	[TotalPago] [decimal](18, 2) NOT NULL,
	[Pagador] [varchar](50) NOT NULL,
	[NomeCartao] [varchar](100) NULL,
	[Numero] [varchar](50) NULL,
	[NumeroUltimaTransacao] [varchar](50) NULL,
	[CodigoTransacao] [varchar](50) NULL,
	[FormaPagamento] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING ON
GO
/****** Object:  Table [dbo].[Subscription]    Script Date: 16/08/2018 17:08:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Assinatura](
	[Id] [uniqueidentifier] NOT NULL,
	[ClienteId] [uniqueidentifier] NOT NULL,
	[DataCriacao] [datetime] NOT NULL,
	[DataUltimaAtualizacao] [datetime] NULL,
	[DataExpiracao] [datetime] NULL,
	[Ativa] [bit] NOT NULL,
	[Nome] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO