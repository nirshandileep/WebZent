/****** Object:  StoredProcedure [dbo].[SP_Items_InsItems]    Script Date: 05/07/2013 20:39:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Nirshan Dileep
-- Create date: 
-- Description:	Insert Items
-- =============================================

create PROCEDURE [dbo].[SP_Items_Adjustment_SelById]
	
	@iId int

AS
BEGIN

	select 
		I.Id,
		I.Description,
		I.ItemId,
		I.Qty
	 from tblItemStockAlterations I where Id = @iId
	
END



