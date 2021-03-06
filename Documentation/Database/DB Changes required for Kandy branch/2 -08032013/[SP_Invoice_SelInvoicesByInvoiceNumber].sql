/****** Object:  StoredProcedure [dbo].[SP_Invoice_SelInvoicesByInvoiceNumber]    Script Date: 05/04/2013 19:50:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		Dinindu Fernando
-- Create date: 21/05/2011
-- Description:	get Invoice by invoice number
-- Nirshan	08/10/2011	increased length to 15 
-- =============================================

ALTER PROCEDURE [dbo].[SP_Invoice_SelInvoicesByInvoiceNumber]

	@sInvoiceNo varchar(15)

AS
BEGIN

	select

		I.InvoiceId,
		InvoiceNo,
		Date,
		GrandTotal,
		BranchId,
		CustomerID,
		PaymentType,
		DueAmount,
		IsPaid,
		I_in,
		CreatedUser,
		CreatedDate,
		ModifiedUser,
		ModifiedDate,
		TransferNote,
		ChequeNumber,
		ChequeDate,
		Remarks,
		CustomerDebitUsed,
		CardType,
		Banked_Ammount,
		BankComision,
		CardComisionRate,
		isnull(InvoiceStatusID,0) as InvoiceStatusID,
		
		(select TOP 1 1 from dbo.tblPaymentDetails where InvoiceId=I.InvoiceId) AS IsVoucherPaymentMade,
		
		case when exists(select GPId from dbo.tblGatePass where InvoiceId=I.InvoiceId)
			then '1' else '0' end as IsIssued

	from dbo.tblInvoiceHeader I
	where I.InvoiceNo = @sInvoiceNo
END




















