
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Proc_System_User_GetByRightId')
	BEGIN
		DROP  Procedure  Proc_System_User_GetByRightId
	END
GO

CREATE Procedure Proc_System_User_GetByRightId
	@RightId int
AS
	SET NOCOUNT ON;

	SELECT Id
		, CreateDate
		, CreateUser
		, CreateUserId
		, UpdateDate
		, UpdateUser
		, UpdateUserId
		, Version
		, FirstName
		, LastName
		, Email
		, Password
		, Nick
		, ApplicationName
		, PasswordQuestion
		, PasswordAnswer
		, LastPasswordChangedDate
		, IsApproved
		, IsLockedOut
		, Comment
		, LastLoginDate
		, LastLockedOutDate
		, LastActivityDate 
	FROM [dbo].[System_User]
		
		

GO