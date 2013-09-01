/****** Object:  StoredProcedure [dbo].[SP_Customers_UpdCustomers]    Script Date: 05/10/2013 01:28:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Nirshan Ibrahim
-- Create date: 10/05/2013
-- Description:	update Cust
-- =============================================

create PROCEDURE [dbo].[SP_Customers_UpdCustomerCreditAmount]

@iCustomerID	int,
@mAmount		money

AS
BEGIN
	if exists (select 1 from dbo.tblCustomers where CustomerID = @iCustomerID )
	begin
		update dbo.tblCustomers 
		set
			Cus_CreditTotal = (ISNULL(Cus_CreditTotal,0)+@mAmount)
		where CustomerID = @iCustomerID
		
	end
				
END










