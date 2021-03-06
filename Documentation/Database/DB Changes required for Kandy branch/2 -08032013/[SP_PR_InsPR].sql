/****** Object:  StoredProcedure [dbo].[SP_PR_InsPR]    Script Date: 05/11/2013 12:57:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Nirshan Ibrahim
-- Create date: 21/02/2012
-- Description:	Add PR
-- Nirshan		30/11/2011		Added PO Date
-- =============================================

ALTER PROCEDURE [dbo].[SP_PR_InsPR]

@iGRNId bigint,
@iCreatedBy int,
@mTotalReturns money,
@sComment varchar(250),
@dReturnDate smalldatetime,
@iPRId	int output,
@sPRCode varchar(10) output

AS
BEGIN

declare @sNextCode varchar(8)
select @sNextCode = dbo.getNextNumber('PR', (select isnull(max(PRId),0) + 1 from dbo.tblPurshaseReturns))

	--Set next pr code
	if not exists (select 1 from dbo.tblPurshaseReturns where rtrim(PRCode) = rtrim(@sNextCode))
	begin
		set @sPRCode = @sNextCode;
	end
	else
	begin
		set @sPRCode = dbo.getNextNumber('PR', (select isnull(max(PRId),0) + 1 from dbo.tblPurshaseReturns));
	end

	set @sPRCode = RTRIM(@sPRCode)

	insert into dbo.tblPurshaseReturns
	(
		PRCode,
		GRNId,
		CreatedBy,
		CreatedDate,
		TotalReturns,
		Comment,
		ReturnDate
	)
	values
	(
		@sPRCode,
		@iGRNId,
		@iCreatedBy,
		getdate(),
		@mTotalReturns,
		@sComment,
		@dReturnDate
	)
	
	Update tblPurchaseOrders
		set BalanceAmount = BalanceAmount - @mTotalReturns,
			POLastModifiedBy = @iCreatedBy,
			POLastModifiedDate = getdate()
		where POId = 
			(select GR.POId from tblGoodsReceived GR 
				where GR.GRNId = @iGRNId
			)
			
	update tblGoodsReceived 
		set POReturnTotal = (POReturnTotal+@mTotalReturns)
		where GRNId = @iGRNId
	
	set @iPRId = scope_identity();
	
	declare @SupId int
	declare @SupBal money
	
	select @SupId = SupId from tblPurchaseOrders 
		where POId = (select GR.POId 
			from tblGoodsReceived GR 
			where GR.GRNId = @iGRNId)
	
	set @SupBal = @mTotalReturns * -1
	
	exec SP_Suppliers_UpdSupplierCreditAmount @SupId, @SupBal
	
END
















