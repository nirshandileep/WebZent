/****** Object:  StoredProcedure [dbo].[SP_Users_InsUsers]    Script Date: 03/08/2013 12:48:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





/***************************************************************
* SP Name		: 
* Purpose		: 
* Tables		: 
* Author		: Nirshan Dileep
* Date Created	: 08-Feb-2013
*
* Modified by		Date Modified		Description

***************************************************************/
create PROCEDURE [dbo].[SP_User_InsLoginLog] 
	@vcUserName AS VARCHAR(50),
	@vcPassword AS VARCHAR(50),
	@bLogin as bit

AS
BEGIN


	INSERT INTO [CHCP].[dbo].[tblLoginAttemptsLog]
			   ([UserName]
			   ,[Password]
			   ,[Login])
		 VALUES
			   (@vcUserName
			   ,@vcPassword
			   ,@bLogin)

END