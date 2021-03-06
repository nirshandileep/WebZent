/****** Object:  StoredProcedure [dbo].[SP_GRN_InsGRNDetails]    Script Date: 07/10/2013 20:31:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Nirshan Ibrahim
-- Create date: 13/04/2011
-- Edited	  : 10/07/2013
-- Description:	Add GRN DETAILS
-- =============================================

ALTER PROCEDURE [dbo].[SP_GRN_InsGRNDetails]
@biGRNId		bigint,
@iItemId 		int,
@iReceivedQty	int,
@iId			bigint,
@mItemValue		money

AS
BEGIN

	declare @mOldCost money
	declare @iQIH int
	declare @mNewCost money

	--history
	declare @sDetails varchar(100), @iQty int,@mUnitCost money
	declare @sRefNoLoc varchar(50)

	set @iQty = @iReceivedQty 

	begin transaction TR_INS_GRN_ITEM
	Begin
	if(@iItemId is not null)
		begin
			insert into dbo.tblGRNDetails
			(
				GRNId,
				ItemId,
				ReceivedQty,
				ItemValue
			)
			values
			(
				@biGRNId,	
				@iItemId, 	
				@iReceivedQty,
				@mItemValue
			)

			/* Set Average Cost */
		
			set @mOldCost = (select isnull((select Cost from dbo.tblItems where ItemId = @iItemId),0));

			set @iQIH = (select isnull((select QuantityInHand from dbo.tblItems where ItemId = @iItemId),0));

			if( (@iQIH + @iReceivedQty) > 0 )
			begin

				set @mNewCost = ( cast(((cast((@mOldCost * @iQIH) as money) + cast((@iReceivedQty * @mItemValue) as money))/
								 (@iQIH + @iReceivedQty)) as money ));

			end
			else
			begin
				set @mNewCost = @mItemValue;
			end

			--get cost from here
			set @mUnitCost = @mNewCost

			if(@mNewCost > 0)
			begin
				update dbo.tblItems 
				set Cost = @mNewCost
				where ItemId = @iItemId

			end

			/* Update the QIH of ITEM */

			update dbo.tblItems
			set QuantityInHand = QuantityInHand + @iReceivedQty
			where ItemId = @iItemId

			/* update Branch Stocks for main store */
			if exists(select 1 from dbo.tblBranchStocks where BranchID = 2 AND
						ItemId = @iItemId )
			begin
				update dbo.tblBranchStocks
				set QuantityInHand = QuantityInHand + @iReceivedQty
				where BranchID = 2 AND
					  ItemId = @iItemId

			end
			else
			begin
				insert into dbo.tblBranchStocks
				(
					BranchID,
					ItemId,
					QuantityInHand
				)
				values
				(
					2,
					@iItemId,
					@iReceivedQty
				)
			end

			/* Update PO Items If its a PO GRN */

			if exists(select 1 from dbo.tblGoodsReceived GRN inner join dbo.tblPOItems POIT
					  On GRN.POId = POIT.POId
					  where GRN.GRNId = @biGRNId AND POIT.ItemId = @iItemId)
			Begin

				declare @vcPONumber varchar(50)
				declare @vcSupInv varchar(50)
				
				Update dbo.tblPOItems 
				Set 
					TotalReceived = TotalReceived + @iReceivedQty,
					TotalRemaining = TotalRemaining - @iReceivedQty
				where POItemId = (select POIT.POItemId from dbo.tblGoodsReceived GRN 
									inner join dbo.tblPOItems POIT 
									On GRN.POId = POIT.POId 
									where GRN.GRNId = @biGRNId AND POIT.ItemId = @iItemId  )
				--set the PO number
				select top 1 @vcPONumber = PO.POCode, @vcSupInv = GR.SuplierInvNo from tblPurchaseOrders PO 
					inner join tblGoodsReceived GR on PO.POId = GR.POId
					where GR.GRNId = @biGRNId
--					where POId = (select top 1 GR.POId from tblGoodsReceived GR where GR.GRNId = @biGRNId)
				select @sDetails = 'Purchase Order - ' + @vcPONumber + ', Supplier Inv. - ' + @vcSupInv + '. Items recieved.'

			End
		End--if itemid is not null
	else if (@iId is not null)
		begin
			declare @itemId bigint
			declare @InvoicNumber varchar(50)
			select @itemId = ItemId from dbo.tblInvoiceDetails where Id = @iId


			insert into dbo.tblGRNDetails
			(
				GRNId,
				ItemId,
				ReceivedQty,
				ItemValue,
				InvDetId
			)
			values
			(
				@biGRNId,	
				@itemId, 	
				@iReceivedQty,
				@mItemValue,
				@iId
			)

			/* Update the QIH of ITEM */

			update dbo.tblItems
			set QuantityInHand = QuantityInHand + @iReceivedQty
			where ItemId = @itemId

			/* update Branch Stocks for main store */
			if exists(select 1 from dbo.tblBranchStocks where BranchID = 2 AND
						ItemId = @itemId )
			begin
				update dbo.tblBranchStocks
				set QuantityInHand = QuantityInHand + @iReceivedQty
				where BranchID = 2 AND
					  ItemId = @itemId

			end
			else
			begin
				insert into dbo.tblBranchStocks
				(
					BranchID,
					ItemId,
					QuantityInHand
				)
				values
				(
					2,
					@itemId,
					@iReceivedQty
				)
			end
		end--if sales return Id is not null
	End
	
	--History updates
	--	declare @sDetails varchar(100), @iQty int,@mUnitCost money
	
	if(@iId is not null)
	begin
		--history ref no		
		select @InvoicNumber = I.InvoiceNo from tblInvoiceHeader I where I.InvoiceId = (select top 1 id.InvoiceId from tblInvoiceDetails id where id.Id = @iId)
		set @sRefNoLoc = @InvoicNumber
		
		select @sDetails = 'Invoice Number - ' + @InvoicNumber + '. Items recieved.'
		
		select top 1 @mUnitCost = ItemCost from dbo.tblInvoiceDetails where Id = @iId
		
		declare @itemIdHist bigint
		select	@itemIdHist = ItemId from dbo.tblInvoiceDetails where Id = @iId
		
		EXEC	[dbo].[SP_Items_InsItemsHistory]
					@iItemId = @itemIdHist,
					@sDescription = @sDetails,
					@iQty = @iReceivedQty,
					@mCost = @mUnitCost,
					@mSellingPrice = null,
					@iUserId = null,
					@sRefNo = @sRefNoLoc,
					@iHistoryTypeId = 4
	end
	else if(@iItemId is not null)
	begin
		--history ref no		
		--select @sRefNoLoc=GR.SuplierInvNo from tblGoodsReceived GR where GRNId=@biGRNId
		set @sRefNoLoc = (cast(@biGRNId as varchar(50)))
		
		EXEC	[dbo].[SP_Items_InsItemsHistory]
					@iItemId = @iItemId,
					@sDescription = @sDetails,
					@iQty = @iReceivedQty,
					@mCost = @mUnitCost,
					@mSellingPrice = null,
					@iUserId = null,
					@sRefNo = @sRefNoLoc,
					@iHistoryTypeId = 3
	end
	
	Commit transaction TR_INS_GRN_ITEM
	
END