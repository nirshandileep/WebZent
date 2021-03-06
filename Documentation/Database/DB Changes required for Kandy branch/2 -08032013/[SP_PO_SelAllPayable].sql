/****** Object:  StoredProcedure [dbo].[SP_PO_SelAllPayable]    Script Date: 05/04/2013 13:50:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



ALTER proc [dbo].[SP_PO_SelAllPayable]
as

begin
		select * from
		(
			select
			PO.POId,
			POCode,
			POAmount,
			GR.SuplierInvNo,
			(GR.ReceivedTotal - (isnull(GR.SupplierPaidAmmount,0)+ISNULL(GR.POReturnTotal,0))) as GRNTotal,
			BalanceAmount,
			POCreatedDate,
			POCreatedUser,
			PO.SupId,
			S.SupplierName,
			POLastModifiedBy,
			POLastModifiedDate,
			(select isnull((select sum(TotalRemaining) from dbo.tblPOItems POIT
				where POIT.POId = PO.POId), 0)) as PendingQty,
			GR.GRNId,
			PO.POStatus
			
			from  dbo.tblPurchaseOrders PO inner join dbo.tblSuppliers S 
			on PO.SupId = S.SupId left outer join dbo.tblGoodsReceived GR
			on PO.POId = GR.POId
			--left outer join dbo.tblVoucherDetails V	on PO.POId = V.POId	where V.POId is null
		) as TMP
		where 
			TMP.BalanceAmount > 0 AND 
			TMP.PendingQty = 0 AND 
			TMP.GRNTotal > 0 AND
			TMP.POStatus <> 2--cancel
		ORDER BY TMP.POId
end


