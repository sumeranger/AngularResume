--------------------
--Seeding Down
--------------------
DECLARE @UserID as int
SET @UserID = (select id
from Users
where Username='Demo')
delete from Users where Username='Demo'
delete from PropertyTypes where LastUpdateBy=@UserID
delete from FurnishingTypes where LastUpdateBy=@UserID
delete from Cities where LastUpdateBy=@UserID
delete from Properties where PostedBy=@UserId