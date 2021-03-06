/****** Object:  StoredProcedure [dbo].[SP_GRN_InsGRN]    Script Date: 05/11/2013 11:46:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Dinindu Fernando
-- Create date: 13/04/2011
-- Description:	Add GRN
-- =============================================

ALTER PROCEDURE [dbo].[SP_GRN_InsGRN]
@iPOId				int,
@iSalesReturnID 	int,
@iRec_By			int,
@sSuplierInvNo		varchar(50),
@iInvId				int,
@mReceivedTotal		money,
@sCreditNote		varchar(max),
@dtRec_Date			DateTime,
@iGRNId bigint output

AS
BEGIN

	insert into dbo.tblGoodsReceived
	(
		POId,
		SalesReturnID,
		Rec_Date,
		Rec_By,
		SuplierInvNo,
		InvoiceId,
		ReceivedTotal
	)
	values
	(
		@iPOId,			
		@iSalesReturnID, 
		@dtRec_Date,
		@iRec_By,		
		@sSuplierInvNo,
		@iInvId,
		@mReceivedTotal
	)

	set @iGRNId = scope_identity();
		
	if(@iInvId is not null)
	begin
	INSERT INTO [dbo].[tblCreditNote]
           ([GRNId]
           ,[InvoiceId]
           ,[CreditAmount]
           ,[CreditNote]
           ,[Date])
     VALUES
           (@iGRNId
           ,@iInvId
           ,@mReceivedTotal
           ,@sCreditNote
           ,getdate())
	
		declare @CustomerId int
		select @CustomerId = CustomerID from dbo.tblInvoiceHeader where InvoiceId = @iInvId

		set @mReceivedTotal = @mReceivedTotal* -1
		exec SP_Customers_UpdCustomerCreditAmount @CustomerId, @mReceivedTotal
	end
	
	if (@iPOId is not null)
	begin
		declare @SupId int
		select @SupId = SupId from tblPurchaseOrders where POId = @iPOId
		
		exec SP_Suppliers_UpdSupplierCreditAmount @SupId, @mReceivedTotal
	end
END



