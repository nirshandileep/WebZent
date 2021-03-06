USE [CeramicHomes_Kandy]
GO
/****** Object:  StoredProcedure [dbo].[uspTruncateAllTransactionTables]    Script Date: 06/10/2012 21:54:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[uspTruncateAllTransactionTables]
AS
BEGIN
truncate table dbo.Reports$
/*
go
truncate table dbo.tblCheques
go
truncate table dbo.tblCreditNote
go
truncate table dbo.tblDamagedItems
go
truncate table dbo.tblGetPassDetails
go
truncate table dbo.tblGroupItems
--truncate table dbo.tblIGroups
go
truncate table dbo.tblItemTransferInvoice
--truncate table dbo.tblItemTransfer
go
truncate table dbo.tblLogMessages
go
--truncate table dbo.tblPayments
truncate table dbo.tblPaymentDetails
go
truncate table dbo.tblPOItems
--truncate table dbo.tblPurchaseOrders
go
truncate table dbo.tblPurchaseReturnDetails
--truncate table dbo.tblPurshaseReturns
go
truncate table dbo.tblSalesReturns
go
truncate table dbo.tblVoucherDetails
go

-----------------------------------------------
delete dbo.tblVoucherHeader
DBCC CHECKIDENT (tblVoucherHeader, RESEED, 0)

delete dbo.tblAccountTypes
DBCC CHECKIDENT (tblAccountTypes, RESEED, 0)

delete dbo.tblBranchStocks
--DBCC CHECKIDENT (tblBranchStocks, RESEED, 0)

delete dbo.tblChequeBooks
DBCC CHECKIDENT (tblChequeBooks, RESEED, 0)

delete dbo.tblCheques
DBCC CHECKIDENT (tblCheques, RESEED, 0)
--
delete dbo.tblDamagedItems
DBCC CHECKIDENT (tblDamagedItems, RESEED, 0)

delete dbo.tblGatePass
DBCC CHECKIDENT (tblGatePass, RESEED, 0)

delete dbo.tblGRNDetails
DBCC CHECKIDENT (tblGRNDetails, RESEED, 0)

delete dbo.tblIGroups
DBCC CHECKIDENT (tblIGroups, RESEED, 0)

delete dbo.tblInvoiceDetails
DBCC CHECKIDENT (tblInvoiceDetails, RESEED, 0)

delete dbo.tblInvoiceHeader
DBCC CHECKIDENT (tblInvoiceHeader, RESEED, 0)

delete dbo.tblItemTransferInvoice
DBCC CHECKIDENT (tblItemTransferInvoice, RESEED, 0)

delete dbo.tblItemTransfer
DBCC CHECKIDENT (tblItemTransfer, RESEED, 0)

delete dbo.tblPaymentDetails
DBCC CHECKIDENT (tblPaymentDetails, RESEED, 0)

delete dbo.tblPayments
DBCC CHECKIDENT (tblPayments, RESEED, 0)

delete dbo.tblPOItems
DBCC CHECKIDENT (tblPOItems, RESEED, 0)

delete dbo.tblGRNDetails
DBCC CHECKIDENT (tblGRNDetails, RESEED, 0)

delete dbo.tblGoodsReceived
DBCC CHECKIDENT (tblGoodsReceived, RESEED, 0)

delete dbo.tblPurchaseOrders
DBCC CHECKIDENT (tblPurchaseOrders, RESEED, 0)

delete dbo.tblPurchaseReturnDetails
DBCC CHECKIDENT (tblPurchaseReturnDetails, RESEED, 0)

delete dbo.tblPurshaseReturns
DBCC CHECKIDENT (tblPurshaseReturns, RESEED, 0)

go
delete from dbo.tblCustomers where CustomerID <> 1
DBCC CHECKIDENT (tblCustomers, RESEED, 1)
INSERT INTO [CeramicHomes_Kandy].[dbo].[tblCustomers]
([CustomerCode] ,[Cus_Name] ,[Cus_Address] ,[Cus_Tel] ,[Cus_Contact] ,[IsActive] ,[IsCreditCustomer] ,[Cus_CreditTotal])
VALUES ('CARD','CARD','123','123','123',1,1,0)
go

update dbo.tblItems 
set QuantityInHand=0
	,FreezedQty=null
	,InvoicedQty=0
go

--

delete from dbo.tblUsers where UserId > 2
DBCC CHECKIDENT (tblUsers, RESEED, 2)
go

DELETE FROM tblBranches where BranchId > 2
DBCC CHECKIDENT (tblBranches, RESEED, 2)
go

*/

END