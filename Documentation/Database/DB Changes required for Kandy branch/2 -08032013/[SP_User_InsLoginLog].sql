/****** Object:  StoredProcedure [dbo].[SP_User_InsLoginLog]    Script Date: 04/28/2013 09:32:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





/***************************************************************
* SP Name		: 
* Purpose		: Log all user login attempts for security reasons
* Tables		: 
* Author		: Nirshan Dileep
* Date Created	: 08-Feb-2013
*
* Modified by		Date Modified		Description

***************************************************************/
ALTER PROCEDURE [dbo].[SP_User_InsLoginLog] 
	@vcUserName AS VARCHAR(50),
	@vcPassword AS VARCHAR(50),
	@bLogin as bit

AS
BEGIN


	INSERT INTO [dbo].[tblLoginAttemptsLog]
			   ([UserName]
			   ,[Password]
			   ,[Login]
			   ,[Date])
		 VALUES
			   (@vcUserName
			   ,@vcPassword
			   ,@bLogin
			   ,GETDATE())

END