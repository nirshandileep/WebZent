/****** Object:  StoredProcedure [dbo].[SP_Voucher_Receivable_InsVoucher]    Script Date: 05/11/2013 10:47:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER procedure [dbo].[SP_Voucher_Receivable_InsVoucher]

	@iCustomerID	int,
	@mPaymentAmount money,
	@dtPaymentDate	datetime,
	@iPaymentTypeId	int,
	@sChequeNo		varchar(100),
	@dtChequeDate	datetime,
	@sCardType		varchar(20),
	@sComment		varchar(max),
	@dCardCommisionRate	decimal(5,2),
	@iCreatedBy		int,

	@inewPaymentID int output,
	@snewPaymentCode varchar(20) output

As
Begin
	
	declare @newVoucherCode varchar(8);
    set @newVoucherCode = dbo.getNextNumber('R', (select isnull(max(PaymentID),0) + 1 from dbo.tblPayments))
	set @snewPaymentCode = @newVoucherCode

	insert into dbo.tblPayments
	(
		PaymentCode, 
		CustomerID, 
		PaymentAmount, 
		PaymentDate, 
		PaymentTypeId, 
		CreatedBy, 
		CreatedDate, 
		ChequeNo, 
		ChequeDate, 
		CardType, 
		CardCommisionRate, 
		Comment
	)
	values
	(
		@newVoucherCode,
		@iCustomerID	,
		@mPaymentAmount ,
		@dtPaymentDate	,
		@iPaymentTypeId	,
		@iCreatedBy		,
		GETDATE()		,
		@sChequeNo		,
		@dtChequeDate	,
		@sCardType		,
		@dCardCommisionRate,
		@sComment
	)

	set @inewPaymentID = SCOPE_IDENTITY();
	
	set @mPaymentAmount = @mPaymentAmount*-1
	
	exec SP_Customers_UpdCustomerCreditAmount @iCustomerID, @mPaymentAmount

END