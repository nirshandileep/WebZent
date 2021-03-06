/****** Object:  UserDefinedFunction [dbo].[getGRNIDsCommaDelByCustomerID]    Script Date: 07/16/2012 01:25:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Nirshan Ibrahim
-- Create date: 06/07/201
-- Description:	Get all grn id's to be paid for customer
-- =============================================

CREATE function [dbo].[getGRNIDsCommaDelByCustomerID](@iCustomerID int)
returns varchar(1000)
as
begin
	declare @sGRNIDs varchar(1000)
	
	declare @CardId int
	select top 1 @CardId = CustomerID from tblCustomers where Cus_Name='CARD'

	SELECT @sGRNIDs = COALESCE(@sGRNIDs+',' ,'') + CONVERT(VARCHAR(20),GR.GRNId)
	FROM dbo.tblGoodsReceived GR
	INNER JOIN dbo.tblInvoiceHeader IH	ON GR.InvoiceId = IH.InvoiceId
	INNER JOIN dbo.tblCustomers C ON C.CustomerID = IH.CustomerID
	where C.CustomerID <> 1 AND C.CustomerID <> @CardId AND C.CustomerID = @iCustomerID
		AND (GR.ReceivedTotal-ISNULL(GR.SupplierPaidAmmount,0))>0

return Replace(@sGRNIDs,' ','')
end

