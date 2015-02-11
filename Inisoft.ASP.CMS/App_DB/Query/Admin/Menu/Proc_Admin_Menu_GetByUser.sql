
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Proc_Admin_Menu_GetByUser')
	BEGIN
		DROP  Procedure  Proc_Admin_Menu_GetByUser
	END
GO

CREATE Procedure Proc_Admin_Menu_GetByUser
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
	FROM [dbo].[Admin_Menu]
		

GO