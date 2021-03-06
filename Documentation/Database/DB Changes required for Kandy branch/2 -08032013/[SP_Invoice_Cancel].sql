/****** Object:  StoredProcedure [dbo].[SP_Invoice_Cancel]    Script Date: 05/11/2013 11:25:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Dinindu Fernando
-- Create date: 01/07/2011
-- Description:	Cancel Invoice
-- =============================================

ALTER PROCEDURE [dbo].[SP_Invoice_Cancel]

@iInvoiceId			int,
@iInvoiceStatusID	int,
@sRemarks			varchar(max)

AS
BEGIN
	
	begin transaction TR_Cancel_Invoice
	begin
		
		/* Update the invoice for cancellation */
		update dbo.tblInvoiceHeader
		set 
			InvoiceStatusID = @iInvoiceStatusID,
			Remarks = @sRemarks,
			DueAmount = 0

		where InvoiceId	= @iInvoiceId	

		/* Update Item table (Invoiced Qty) for Invoiced items */

		declare @itmpItemID int, @itmpQuantity int;

		declare CUR_CAN_ITM cursor for 
		select 
			ItemId,
			Quantity
		from dbo.tblInvoiceDetails
		where InvoiceId = @iInvoiceId;

		open CUR_CAN_ITM;

		fetch next from CUR_CAN_ITM into 
			@itmpItemID,
			@itmpQuantity

		while (@@Fetch_Status = 0)
		begin

			update dbo.tblItems
			set 
				InvoicedQty = InvoicedQty - @itmpQuantity
			where ItemId = @itmpItemID

			fetch next from CUR_CAN_ITM into 
				@itmpItemID,
				@itmpQuantity
		end
		
		close CUR_CAN_ITM;
		deallocate CUR_CAN_ITM;

		/* update customer credit total if paid*/

		declare @mGrandTotal money, 
				@mDueAmount money

		set @mGrandTotal = (select GrandTotal from dbo.tblInvoiceHeader 
							where InvoiceId = @iInvoiceId );

		set @mDueAmount = (select DueAmount from dbo.tblInvoiceHeader 
							where InvoiceId = @iInvoiceId );

		if(@mDueAmount < @mGrandTotal)
		begin
									
			update dbo.tblCustomers 
			set 
				Cus_CreditTotal = Cus_CreditTotal - ( @mGrandTotal - @mDueAmount )

			where
			( CustomerID = ( select distinct isnull(CustomerID,0) from dbo.tblInvoiceHeader
								 where InvoiceId = @iInvoiceId) ) AND
				  
			( CustomerID != 1 ) -- 1 - cash customer
			

		end
		
	end
	Commit transaction TR_Cancel_Invoice
	
	
END















