alter table [dbo].[Products]
  add [ImageData] VarBinary (MAX) NULL,
      [ImageMimeType] Varchar(50) NULL;