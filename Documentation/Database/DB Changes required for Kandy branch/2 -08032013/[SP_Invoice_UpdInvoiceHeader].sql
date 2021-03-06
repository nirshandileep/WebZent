/****** Object:  StoredProcedure [dbo].[SP_Invoice_UpdInvoiceHeader]    Script Date: 05/11/2013 11:28:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Dinindu Fernando
-- Create date: 16/05/2011
-- Description:	Update Invoice
-- =============================================

ALTER PROCEDURE [dbo].[SP_Invoice_UpdInvoiceHeader]
@iInvoiceId			int,
@mGrandTotal		money,
@mDueAmount			money,
@bIsPaid			bit,
@iModifiedUser		int,
@sChequeNumber		varchar(50),
@dtChequeDate		datetime,
@sRemarks			varchar(max),
@iStatus			int,
@sCardType			varchar(50),
@mBankComision		money,
@mBanked_Ammount	money,
@dCardComisionRate	decimal(5,2)

AS
BEGIN

	declare @InitialDueAmount money, @Difference money
	
	select @InitialDueAmount = isnull(DueAmount,0) from tblInvoiceHeader where InvoiceId = @iInvoiceId

	update dbo.tblInvoiceHeader
	set 
		DueAmount = @mDueAmount,
		GrandTotal = @mGrandTotal,
		IsPaid	  = @bIsPaid,
		ModifiedUser = @iModifiedUser,
		ModifiedDate = getdate()
		,ChequeNumber = @sChequeNumber
		,ChequeDate=@dtChequeDate
		,Remarks=@sRemarks
		,InvoiceStatusID=@iStatus
		,CardType=@sCardType
		,Banked_Ammount=@mBanked_Ammount
		,BankComision=@mBankComision
		,CardComisionRate=@dCardComisionRate
	where InvoiceId	= @iInvoiceId	
	
	declare @CustId int
	select @CustId= CustomerID from tblInvoiceHeader WHERE InvoiceId = @iInvoiceId
	
	set @Difference = @mDueAmount - @InitialDueAmount
	exec SP_Customers_UpdCustomerCreditAmount @CustId, @Difference

END













