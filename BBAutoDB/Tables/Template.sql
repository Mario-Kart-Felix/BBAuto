CREATE TABLE [dbo].[Template](
	[Template_id] [int] IDENTITY(1,1) NOT NULL,
	[Template_name] NVARCHAR(50) NOT NULL,
	[Template_path] NVARCHAR(200) NOT NULL,
 CONSTRAINT [PK_Template] PRIMARY KEY CLUSTERED 
(
	[Template_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
