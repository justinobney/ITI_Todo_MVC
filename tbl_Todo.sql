GO

/****** Object:  Table [dbo].[Todo]    Script Date: 10/3/2012 11:53:43 PM ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Todo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[User_ID] [bigint] NOT NULL,
	[Task_Description] [nvarchar](500) NOT NULL,
	[Task_Complete] [bit] NULL,
	[Timestamp] [datetime] NULL,
 CONSTRAINT [PrimaryKey_f765487f-7b90-4fc2-949e-ef66f037c9a7] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Todo] ADD  CONSTRAINT [ColumnDefault_68e013f1-0e9c-4ec3-89b1-84450edf818b]  DEFAULT (getdate()) FOR [Timestamp]
GO


