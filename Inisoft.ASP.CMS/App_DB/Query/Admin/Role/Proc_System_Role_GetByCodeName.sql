
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Proc_System_Role_GetByCodeName')
	BEGIN
		DROP  Procedure  Proc_System_Role_GetByCodeName
	END
GO

CREATE Procedure Proc_System_Role_GetByCodeName
	@UserID int
AS
	SET NOCOUNT ON;

	SELECT [Id]
      ,[CreateDate]
      ,[CreateUser]
      ,[CreateUserId]
      ,[UpdateDate]
      ,[UpdateUser]
      ,[UpdateUserId]
      ,[Version]
      ,[ParentMenuId]
      ,[Title]
      ,[Url]
      ,[CssClass]
      ,[MenuBar]
	  ,[ApplicationPath]
	FROM [dbo].[Admin_Menu]
		

GO