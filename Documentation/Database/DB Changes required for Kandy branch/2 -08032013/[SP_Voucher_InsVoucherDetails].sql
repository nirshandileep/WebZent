/****** Object:  StoredProcedure [dbo].[SP_Voucher_InsVoucherDetails]    Script Date: 05/11/2013 12:41:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER procedure [dbo].[SP_Voucher_InsVoucherDetails]
	
	@sVoucherDetails varchar(max),
	@iVoucherID		 int,
	@iPOId			 int,
	@iGRNId			 bigint,
	@mAmount		 money
	
As
Begin

	insert into dbo.tblVoucherDetails
	(
		VoucherDetails,
		VoucherID,
		POId,
		GRNId,
		Amount
	)
	values
	(
		@sVoucherDetails,
		@iVoucherID,
		@iPOId,
		@iGRNId,
		@mAmount
	)

	update dbo.tblPurchaseOrders
	set BalanceAmount = BalanceAmount - @mAmount
	where POId = @iPOId
	
	declare @SupId int
	select @SupId = SupId from tblPurchaseOrders where POId = @iPOId
	set @mAmount = @mAmount * -1
	
	exec SP_Suppliers_UpdSupplierCreditAmount @SupId, @mAmount

	--update tblgoodsreceived table 
	update dbo.tblGoodsReceived
	set SupplierPaidAmmount = isnull(SupplierPaidAmmount,0) + @mAmount
	where GRNId = @iGRNId

End









