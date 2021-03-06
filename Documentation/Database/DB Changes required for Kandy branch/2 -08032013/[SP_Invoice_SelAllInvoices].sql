/****** Object:  StoredProcedure [dbo].[SP_Invoice_SelAllInvoices]    Script Date: 04/28/2013 09:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Dinindu Fernando
-- Create date: 21/05/2011
-- Description:	get all Invoices 
-- =============================================

ALTER PROCEDURE [dbo].[SP_Invoice_SelAllInvoices]

AS
BEGIN
--declare @date datetime
--set @date='2011.10.13'
	select

		InvoiceId,
		InvoiceNo,
		Date,
		GrandTotal,
		I.BranchId,
		B.BranchCode,
		I.CustomerID,
		CustomerCode,
		Cus_Name,
		Cus_Tel,
		PaymentType,
		DueAmount,
		IsPaid,
		I_in,
		CreatedUser,
		U.UserName,
		CreatedDate,
		ModifiedUser,
		ModifiedDate,
		TransferNote,
		ChequeNumber,
		ChequeDate,
		Remarks,
		CardType,
		Banked_Ammount,
		BankComision,
		CardComisionRate

	from dbo.tblInvoiceHeader I inner join dbo.tblBranches B
	on I.BranchId = B.BranchId
	left outer join dbo.tblCustomers C on I.CustomerID = C.CustomerID
	inner join dbo.tblUsers U on I.CreatedUser = U.UserId --and CONVERT(VARCHAR,Date,101) <> CONVERT(VARCHAR,@date,101)
		
END















