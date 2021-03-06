/****** Object:  StoredProcedure [dbo].[SP_Items_Adjustment_SelAllByItemId]    Script Date: 05/08/2013 22:57:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Nirshan Dileep
-- Create date: 
-- Description:	Insert Items
-- =============================================

create PROCEDURE [dbo].[SP_Items_Adjustment_SelAllByItemId]
	
	@iItemId int

AS
BEGIN

	select 
		--IA.Id,
		IA.Description,
		--IA.ItemId,
		IA.Qty,
		--I.ItemCode as [Item Code],
		--I.ItemDescription as [Item Description],
		--I.QuantityInHand as [QIH]
		convert(datetime,IA.CreatedDate,101) as Date
	 from tblItemStockAlterations IA 
	 inner join tblItems I on IA.ItemId = I.ItemId 
	 where IA.ItemId = @iItemId
	
END