CREATE PROCEDURE [dbo].[DTP_Insert]
@idDTP int,
@idCar int,
@date datetime,
@idRegion int,
@dateCallInsure datetime,
@idCulprit int,
@idStatusAfterDTP nvarchar(50),
@numberLoss nvarchar(50),
@sum float,
@damage nvarchar(300),
@facts nvarchar(500),
@comm nvarchar(100),
@idCurrentStatusAfterDTP int
AS
BEGIN		
	if (@idDTP = 0)
	begin		
		Declare @number int
		SELECT @number=max(DTP_number)+1 FROM DTP
		if (@number is null)
			SET @number = 1
		
		INSERT INTO DTP VALUES(@idCar, @number, @date, @idRegion, @dateCallInsure, @idCulprit,
			@idStatusAfterDTP, @numberLoss, @sum, @damage, @facts, @comm,
			@idCurrentStatusAfterDTP)
			
		SET @idDTP = SCOPE_IDENTITY()
	end
	else
	begin
		UPDATE DTP
		SET dtp_date=@date, region_id=@idRegion, dtp_dateCallInsure=@dateCallInsure,
			culprit_id=@idCulprit, StatusAfterDTP_id=@idStatusAfterDTP, dtp_numberLoss=@numberLoss,
			dtp_sum=@sum, dtp_damage=@damage, dtp_facts=@facts, dtp_comm=@comm,
			currentStatusAfterDTP_id=@idCurrentStatusAfterDTP
		WHERE dtp_id=@idDTP
	end
	
	SELECT @idDTP
END
GO
