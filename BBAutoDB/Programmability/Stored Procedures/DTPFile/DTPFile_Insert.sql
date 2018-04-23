CREATE PROCEDURE [dbo].[DTPFile_Insert]
@idDtpFile int,
@idDtp int,
@name nvarchar(100),
@file nvarchar(300)
AS
BEGIN
	if (@idDtpFile = 0)
	begin
		INSERT INTO dtpFile VALUES(@idDtp, @name, @file)
		SELECT @idDtpFile = @@IDENTITY
	end
	else
		UPDATE dtpFile SET dtpFile_Name=@name, dtpFile_File=@file WHERE dtpFile_id=@idDtpFile
		
	SELECT @idDtpFile
END
GO
