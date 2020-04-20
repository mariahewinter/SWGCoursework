IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'DbReset')
		DROP PROCEDURE DbReset
GO

CREATE PROCEDURE DbReset AS
BEGIN
	DELETE FROM Contact WHERE ContactID > 3;

	INSERT INTO AspNetUsers(Id, EmailConfirmed, PhoneNumber, PhoneNumberConfirmed, Email, FirstName, LastName, TwoFactorEnabled, LockoutEnabled, AccessFailedCount, UserName)
	VALUES ('00000000-0000-0000-0000-000000000000', 0, '507-500-5252', 0, 'test@test.com', 'Tester', 'Test', 0, 0, 0, 'Test1')

END

SELECT * FROM Contact