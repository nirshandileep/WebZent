/****** Object:  StoredProcedure [dbo].[SP_Invoice_SelInvoicesByInvoiceID]    Script Date: 05/11/2013 11:14:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		Dinindu Fernando
-- Create date: 21/05/2011
-- Description:	get Invoice by invoice ID
-- =============================================

ALTER PROCEDURE [dbo].[SP_Invoice_SelInvoicesByInvoiceID]

	@iInvoiceId int

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
		GRNId,
		CardType,Banked_Ammount,BankComision,CardComisionRate,
		
		(select TOP 1 CAST(1 AS BIT) from dbo.tblPaymentDetails where InvoiceId=I.InvoiceId) AS IsVoucherPaymentMade,
		
		isnull(InvoiceStatusID,0) as InvoiceStatusID,
		case when exists(select GPId from dbo.tblGatePass where InvoiceId=@iInvoiceId)
			then '1' else '0' end as IsIssued

	from dbo.tblInvoiceHeader I
	where InvoiceId = @iInvoiceId
END





















