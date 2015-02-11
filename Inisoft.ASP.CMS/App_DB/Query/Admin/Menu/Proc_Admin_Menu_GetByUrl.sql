
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Proc_Admin_Menu_GetByUrl')
	BEGIN
		DROP  Procedure  Proc_Admin_Menu_GetByUrl
	END
GO

CREATE Procedure Proc_Admin_Menu_GetByUrl
	@Url varchar(250)
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
	FROM [Admin_Menu]
	WHERE
		[Admin_Menu].[Url] = @Url	

GO