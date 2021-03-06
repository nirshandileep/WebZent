/****** Object:  StoredProcedure [dbo].[SP_Invoice_InsInvoiceHeader]    Script Date: 05/11/2013 10:35:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Dinindu Fernando
-- Create date: 16/05/2011
-- Description:	Add Invoice
-- =============================================

ALTER PROCEDURE [dbo].[SP_Invoice_InsInvoiceHeader]
@sInvoiceNo			varchar(15),
@mGrandTotal		money,
@iBranchId			int,
@iCustomerID		int,
@sPaymentType		varchar(20),
@mDueAmount			money,
@bIsPaid			bit,
@bI_in				bit,
@iCreatedUser		int,
@sTransferNote		varchar(max),
@sChequeNumber		varchar(50),
@dtChequeDate		datetime,
@sRemarks			varchar(max),
@mCustDebitAmount	money,
@sCardType			varchar(50),
@mBankComision		money,
@mBanked_Ammount	money,
@dCardComisionRate	decimal(5,2),
@biGRNId			bigint,
@dDate				datetime,
@iInvoiceId int output

AS
BEGIN

BEGIN TRANSACTION TRN_INS_INVOICE
BEGIN
	insert into dbo.tblInvoiceHeader
	(
		InvoiceNo,
		Date,
		GrandTotal,
		BranchId,
		CustomerID,
		PaymentType,
		DueAmount,
		IsPaid,
		I_in,
		CreatedUser,
		CreatedDate,
		TransferNote,
		ChequeNumber,
		ChequeDate,
		Remarks,
		CustomerDebitUsed,
		CardType,
		Banked_Ammount,
		BankComision,
		CardComisionRate,
		InvoiceStatusID,
		GRNId
	)
	values
	(
		@sInvoiceNo,
		@dDate,
		@mGrandTotal,
		@iBranchId,
		@iCustomerID,
		@sPaymentType,
		@mDueAmount,
		@bIsPaid,
		@bI_in,
		@iCreatedUser,
		getdate(),
		@sTransferNote,
		@sChequeNumber,	
		@dtChequeDate,	
		@sRemarks,		
		@mCustDebitAmount,
@sCardType,
@mBanked_Ammount,
@mBankComision,
@dCardComisionRate,
		1,
		@biGRNId
	)

	set @iInvoiceId = scope_identity();
	
	IF(@biGRNId IS NOT NULL AND ISNULL(@mCustDebitAmount,0) > 0)
	BEGIN
		update dbo.tblGoodsReceived set
			SupplierPaidAmmount = isnull(SupplierPaidAmmount,0) + @mCustDebitAmount
		where GRNId = @biGRNId
	END

	--update customer
	exec SP_Customers_UpdCustomerCreditAmount @iCustomerID, @mDueAmount

	DECLARE @biSuppPaidAmount money
	DECLARE @biReceivedTotal money

	select @biSuppPaidAmount = isnull(SupplierPaidAmmount,0) FROM dbo.tblGoodsReceived WHERE GRNId = @biGRNId
	select @biReceivedTotal = isnull(ReceivedTotal,0) from dbo.tblGoodsReceived WHERE GRNId = @biGRNId

	IF(@biSuppPaidAmount>@biReceivedTotal)
	begin
		rollback tran TRN_INS_INVOICE
	end

END
COMMIT TRAN TRN_INS_INVOICE
END
















