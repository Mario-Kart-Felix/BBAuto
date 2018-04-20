create procedure dbo.FillDatabase
as
  exec dbo.InsertRoles
      
  exec dbo.InsertStatuses
  exec dbo.InsertRegions
  exec dbo.InsertPositions
  exec dbo.InsertDepts
  exec dbo.InsertOwners
  exec dbo.InsertColors
  exec dbo.InsertComps
  exec dbo.InsertCulprits
  exec dbo.InsertDealers
  
  exec dbo.InsertDrivers
