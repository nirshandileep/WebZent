/****** Object:  StoredProcedure [dbo].[SP_Customers_SelByID]    Script Date: 05/09/2013 00:40:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Dinindu Fernando
-- Create date: 12/04/2011
-- Description:	get customers by id
-- =============================================

ALTER PROCEDURE [dbo].[SP_Customers_SelByID]
@iCustomerID int 
AS
BEGIN
	select 
			CustomerID,
			CustomerCode,
			Cus_Name,
			Cus_Address,
			Cus_Tel,
			Cus_Contact,
			IsActive,
			IsCreditCustomer,
			Cus_CreditTotal,
			(dbo.getGRNIDsCommaDelByCustomerID(CustomerID)) as GRNIds
	from dbo.tblCustomers
	where CustomerID = @iCustomerID
END











