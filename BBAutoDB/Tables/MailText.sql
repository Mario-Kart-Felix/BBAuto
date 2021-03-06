CREATE TABLE [dbo].[MailText](
	[mailText_id] [int] IDENTITY(1,1) NOT NULL,
	[mailText_name] NVARCHAR(50) NOT NULL,
	[mailText_text] NVARCHAR(500) NOT NULL,
 CONSTRAINT [PK_MailText] PRIMARY KEY CLUSTERED 
(
	[mailText_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
