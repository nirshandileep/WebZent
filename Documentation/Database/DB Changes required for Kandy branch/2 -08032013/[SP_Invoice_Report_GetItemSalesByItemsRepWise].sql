
/****** Object:  StoredProcedure [dbo].[SP_Invoice_Report_GetItemSalesByItemsRepWise]    Script Date: 04/27/2013 12:35:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		NIRSHAN IBRAHIM
-- Create date: 18/01/2012
-- Description:	Get item wise sales rep wise
-- =============================================

ALTER PROCEDURE [dbo].[SP_Invoice_Report_GetItemSalesByItemsRepWise]

	@dFromDate		datetime = '1900.01.01',
	@dToDate		datetime = '1900.01.01'
	
AS
BEGIN

	if (@dFromDate <> '1900.01.01' AND @dToDate <> '1900.01.01')--date range given
	begin
		select 
			IT.ItemId,
			I.ItemCode,
			I.ItemDescription,
			C.CategoryType,
			B.BrandName,
			T.TypeName,
			U.FirstName,
			BR.BranchCode,
			--BR.InvPrefix,
			sum(isnull(IT.Quantity,0)) as SoldQuantity,
			sum(isnull(IT.TotalPrice,0)) as TotalSales,
			IT.Price,
			sum(isnull(IT.ItemCost,0) * isnull(IT.Quantity,0)) as ItemCost,
			(sum(isnull(IT.TotalPrice,0)) - (sum(isnull(IT.ItemCost,0) * isnull(IT.Quantity,0)))) as Profit,
			IH.InvoiceNo,
			IH.Date
		from dbo.tblInvoiceDetails IT inner join dbo.tblInvoiceHeader IH 
			on IT.InvoiceId = IH.InvoiceId 
			inner join dbo.tblItems I on IT.ItemId = I.ItemId
			left outer join dbo.tblItemCategory C on I.CategoryId = C.CategoryId
			inner join dbo.tblBrands B on I.BrandId = B.BrandId
			left outer join dbo.tblTypes T on I.TypeId = T.TypeId
			LEFT OUTER JOIN dbo.tblUsers U ON IH.CreatedUser = U.UserId
			LEFT OUTER JOIN dbo.tblBranches BR ON IH.BranchId = BR.BranchId
		where (CONVERT(VARCHAR,IH.Date,101) >= @dFromDate and CONVERT(VARCHAR,IH.Date,101) <= @dToDate) and IH.InvoiceStatusID <> 5
		group by IT.ItemId,I.ItemCode,I.ItemDescription,C.CategoryType,B.BrandName,T.TypeName,U.FirstName,BR.BranchCode,IT.Price,IH.InvoiceNo,IH.Date order by IT.ItemId
	end
	else
	begin
		select 
			IT.ItemId,
			I.ItemCode,
			I.ItemDescription,
			C.CategoryType,
			B.BrandName,
			T.TypeName,
			U.FirstName,
			BR.BranchCode,
			--BR.InvPrefix,
			sum(isnull(IT.Quantity,0)) as SoldQuantity,
			sum(isnull(IT.TotalPrice,0)) as TotalSales,
			IT.Price,
			sum(isnull(IT.ItemCost,0) * isnull(IT.Quantity,0)) as ItemCost,
			(sum(isnull(IT.TotalPrice,0)) - (sum(isnull(IT.ItemCost,0) * isnull(IT.Quantity,0)))) as Profit,
			IH.InvoiceNo,
			IH.Date
		from dbo.tblInvoiceDetails IT inner join dbo.tblInvoiceHeader IH 
			on IT.InvoiceId = IH.InvoiceId
			inner join dbo.tblItems I on IT.ItemId = I.ItemId
			left outer join dbo.tblItemCategory C on I.CategoryId = C.CategoryId
			inner join dbo.tblBrands B on I.BrandId = B.BrandId
			left outer join dbo.tblTypes T on I.TypeId = T.TypeId
			LEFT OUTER JOIN dbo.tblUsers U ON IH.CreatedUser = U.UserId
			LEFT OUTER JOIN dbo.tblBranches BR ON IH.BranchId = BR.BranchId
		where IH.InvoiceStatusID <> 5
		group by IT.ItemId,I.ItemCode,I.ItemDescription,C.CategoryType,B.BrandName,T.TypeName,U.FirstName,BR.BranchCode,IT.Price,IH.InvoiceNo,IH.Date order by IT.ItemId
	end
		
END









