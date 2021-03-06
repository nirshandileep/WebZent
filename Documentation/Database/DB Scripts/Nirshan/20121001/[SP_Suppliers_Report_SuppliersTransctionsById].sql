/****** Object:  StoredProcedure [dbo].[SP_Voucher_Report_DayBook]    Script Date: 09/30/2012 19:00:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		NIRSHAN IBRAHIM
-- Create date: 30/12/2012
-- Description:	Get day book report details by date range
-- =============================================

create PROCEDURE [dbo].[SP_Suppliers_Report_SuppliersTransctionsById]

	@nSupplierId	int,
	@dFromDate		datetime = '1900.01.01',
	@dToDate		datetime = '1900.01.01'
	
AS
BEGIN

--SELECT * FROM 
(
--GRN Information
	select
		'a' as ID,
		CONVERT(VARCHAR,GR.Rec_Date,101) AS Date,
		'Purchases - ' + PO.POCode AS Reference,--bank - branch
		'Sup InvNo - ' + isnull(SuplierInvNo,'') AS [Description],-- cheque numbers
		'' AS Debit,--payables/
		isnull(ReceivedTotal,0.00) AS Credit,--Recieved
		isnull(ReceivedTotal,0.00) as Total
	from dbo.tblGoodsReceived GR inner join dbo.tblPurchaseOrders PO on GR.POId = PO.POId
	inner join dbo.tblSuppliers S on PO.SupId = S.SupId
	where (CONVERT(VARCHAR,GR.Rec_Date,101) >= @dFromDate and CONVERT(VARCHAR,GR.Rec_Date,101) <= @dToDate)
	and GR.InvoiceId is null
	and S.SupId = @nSupplierId
	--group by CONVERT(VARCHAR,S.Rec_Date,101)


	UNION

--GRN Vouchers/PO Vouchers
	select 
		'b' as ID,
		CONVERT(VARCHAR,ISNULL(VO.VoucherPaymentDate,VO.CreatedDate),101) AS Date,
		'Payment - ' + VO.VoucherCode AS Reference,--bank - branch
		'Sup InvNo - ' + GR.SuplierInvNo + CASE WHEN RTRIM(ISNULL(VO.ChequeNumber,'')) = '' THEN ', CASH' WHEN RTRIM(VO.ChequeNumber) <> '' THEN ', CHQ-#'+VO.ChequeNumber END AS [Description],-- cheque numbers
		VD.Amount AS Debit,--payables
		'' AS Credit,--Recieved
		VD.Amount*-1 as Total
	from dbo.tblVoucherHeader VO inner join dbo.tblVoucherDetails VD on VO.VoucherID = VD.VoucherID
	left outer join dbo.tblGoodsReceived GR on VD.GRNId = GR.GRNId
	inner join dbo.tblPurchaseOrders PO on GR.POId = PO.POId
	where (CONVERT(VARCHAR,ISNULL(VO.VoucherPaymentDate,VO.CreatedDate),101) >= @dFromDate and CONVERT(VARCHAR,ISNULL(VO.VoucherPaymentDate,VO.CreatedDate),101) <= @dToDate)
	--and VO.VoucherTypeID = 2--Purchase Orders
	and PO.SupId = @nSupplierId
	--group by CONVERT(VARCHAR,ISNULL(VO.VoucherPaymentDate,VO.CreatedDate),101)

	UNION

--Purchase Returns
	select 
		'c' as ID,
		CONVERT(VARCHAR,PR.ReturnDate,101) AS Date,
		'Purchase Returns' AS Reference,--bank - branch
		PRCode + ', Sup InvNo - ' + GR.SuplierInvNo AS [Description],-- cheque numbers
		'' AS Debit,--payables
		-1*PR.TotalReturns AS Credit,--Recieved
		-1*PR.TotalReturns as Total
	from dbo.tblPurshaseReturns PR inner join dbo.tblGoodsReceived GR on PR.GRNId = GR.GRNId
	inner join dbo.tblPurchaseOrders P on GR.POId = P.POId
	where (CONVERT(VARCHAR,PR.ReturnDate,101) >= @dFromDate and CONVERT(VARCHAR,PR.ReturnDate,101) <= @dToDate)
	--and PR.IsReimbursed = 0
	--group by CONVERT(VARCHAR,ISNULL(P.PODate,P.POCreatedDate),101)
	and P.SupId = @nSupplierId

	UNION

--Supplier Direct Payments
	select 
		'd' as ID,
		CONVERT(VARCHAR,ISNULL(VO.VoucherPaymentDate,VO.CreatedDate),101) AS Date,
		'Supplier - ' + VO.VoucherCode AS Reference,--bank - branch
		'CHQ#-' + VO.ChequeNumber AS [Description],-- cheque numbers
		VO.TotalAmount AS Debit,--payables
		'' AS Credit,--Recieved
		VO.TotalAmount*-1 as Total
	from dbo.tblVoucherHeader VO-- inner join dbo.tblVoucherDetails VD on VO.VoucherID = VD.VoucherID
	where (CONVERT(VARCHAR,ISNULL(VO.VoucherPaymentDate,VO.CreatedDate),101) >= @dFromDate and CONVERT(VARCHAR,ISNULL(VO.VoucherPaymentDate,VO.CreatedDate),101) <= @dToDate)
	and VO.SupplierID = @nSupplierId
	
) --TMP
order by Date
--GROUP BY TMP.ID,TMP.Date,TMP.Reference,TMP.Description,TMP.Debit,TMP.Credit,TMP.Total
END