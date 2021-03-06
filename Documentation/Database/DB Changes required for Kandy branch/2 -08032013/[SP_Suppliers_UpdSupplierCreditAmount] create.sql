/****** Object:  StoredProcedure [dbo].[SP_Suppliers_UpdSuppliers]    Script Date: 05/10/2013 01:21:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Nirshan Ibrahim
-- Create date: 10/05/2013
-- Description:	update Suppliers
-- =============================================

create PROCEDURE [dbo].[SP_Suppliers_UpdSupplierCreditAmount]

@iSupId				int,
@mAmount			money

AS
BEGIN
	if exists (select 1 from dbo.tblSuppliers where SupId = @iSupId)
	begin
		update dbo.tblSuppliers
		set
			CreditAmmount = (isnull(CreditAmmount,0)+@mAmount),
			ModifiedDate = getdate()
		where SupId = @iSupId
	end
	
END





