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

create PROCEDURE [dbo].[SP_Items_Adjustment_InsItems]
	
	@iItemId			int,
	@sDescription		varchar(500),
	@iQty				int,
	@iCreatedBy			int,
	@iId				int		output

AS
BEGIN

	if exists(select null from dbo.tblItems where ItemId=@iItemId)
	begin
		Insert into dbo.tblItemStockAlterations
		(
			ItemId,
			Description,
			Qty,
			CreatedBy,
			CreatedDate
		)
		Values
		(
			@iItemId,
			@sDescription,
			@iQty,
			@iCreatedBy,
			GETDATE()	
		)
		
		SET @iId = scope_identity();
		
		UPDATE dbo.tblItems
		SET QuantityInHand = (QuantityInHand + @iQty)
		WHERE ItemId = @iItemId
	
	END
	ELSE
	BEGIN
		
		SET @iId = -1

	END

	
END



