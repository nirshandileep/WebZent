/****** Object:  StoredProcedure [dbo].[SP_Voucher_InsVoucher]    Script Date: 05/11/2013 11:55:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
Nirshan		30/10/2011		Update supplier payments and customer payments to 
							suppliers table and customers table
Nirshan		05/11/2011		Added voucher payment date
Nirshan		14/11/2011		Added AccountID
Nirshan		24/04/2012		Cheque Management changes
*/

ALTER procedure [dbo].[SP_Voucher_InsVoucher]

	@sVoucherCode	varchar(8),
	@iCreatedBy		int,
	@sChequeNumber	varchar(100),
	@iChqId			int,
	@dtChequeDate	datetime,
	@sBank			varchar(100),
	@sBankBranch	varchar(100),
	@sDescription	varchar(max),
	@mTotalAmount	money,
	@iVoucherTypeID	int,
	@iPaymentTypeID	int,
	@iSupplierID	int,
	@iCustomerID	int,
	@dtPaymentDate	datetime,
	@iAccountID		int,
	@iBranchId		int,
	@inewVoucherID int output

As
Begin
	
	declare @newVoucherCode varchar(8);

	if not exists(select 1 from dbo.tblVoucherHeader 
				  where VoucherCode = @sVoucherCode)

	begin
		
		set @newVoucherCode = @sVoucherCode;
		
	end
	else
	begin

		set @newVoucherCode = dbo.getNextNumber('VOU', (select isnull(max(VoucherID),0) 
										+ 1 from dbo.tblVoucherHeader))
	end

	insert into dbo.tblVoucherHeader
	(
		VoucherCode,
		CreatedBy,
		CreatedDate,
		ChequeNumber,
		ChqId,
		ChequeDate,
		Bank,
		BankBranch,
		Description,
		TotalAmount,
		VoucherTypeID,
		PaymentTypeID,
		SupplierID,
		VoucherPaymentDate,
		CustomerID,
		AccountID,
		BranchId
		
	)
	values
	(
		@newVoucherCode,
		@iCreatedBy,
		getdate(),
		@sChequeNumber,
		@iChqId,
		@dtChequeDate,
		@sBank,
		@sBankBranch,
		@sDescription,
		@mTotalAmount,
		@iVoucherTypeID,
		@iPaymentTypeID,
		@iSupplierID,
		@dtPaymentDate,
		@iCustomerID,
		@iAccountID,
		@iBranchId
		
	)


	set @inewVoucherID = SCOPE_IDENTITY();

	--Supplier Payment
	IF(@iVoucherTypeID = 2 AND @iSupplierID IS NOT NULL)
	BEGIN
		declare @Amount money
		set @Amount = @mTotalAmount * -1
		
		exec SP_Suppliers_UpdSupplierCreditAmount @iSupplierID, @Amount
	END

	--Customer Payment
	IF(@iVoucherTypeID = 3 AND @iCustomerID IS NOT NULL)
	BEGIN
		declare @CustCredit money
		set @CustCredit = @mTotalAmount * -1
		
		exec SP_Customers_UpdCustomerCreditAmount @iCustomerID, @CustCredit
	END
	
	--Update Cheque status
	if(@iPaymentTypeID<>1)--Cash
	begin
		UPDATE dbo.tblCheques SET 
			ChqStatusId = 2,
			Amount = @mTotalAmount,
			Comment = @newVoucherCode,
			WrittenDate = @dtPaymentDate,
			ChqDate = @dtChequeDate,
			WrittenBy = @iCreatedBy
		WHERE ChqId = @iChqId
	end
END