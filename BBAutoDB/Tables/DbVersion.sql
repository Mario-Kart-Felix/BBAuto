create table dbo.DbVersion
(
  Id      NVARCHAR(64) not null constraint DF_DbVersion_Id default(N''),
  Created datetime not null constraint DF_DbVersion_Created default getutcdate(), 
  [Type]  nvarchar(32) not null constraint DF_DbVersion_Type default(N'production'),
  DbVersion nvarchar(8) not null constraint DF_DbVersion_DbVersion default(N'20170101')
)
