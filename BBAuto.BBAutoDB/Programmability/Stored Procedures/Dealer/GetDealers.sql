create procedure dbo.GetDealers
as
begin
  select
    Id,
    [Name],
    Contacts
  from
    Dealer
  order by
    [Name]
end
