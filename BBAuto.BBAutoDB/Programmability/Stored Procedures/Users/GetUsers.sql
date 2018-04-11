create procedure dbo.GetUsers
as
  select u.Id, u.Login, u.RoleId FROM dbo.Users u
