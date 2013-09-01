ALTER TABLE dbo.tblBranchStocks /*with nocheck*/
 ADD CONSTRAINT CHK_StockLessThanZero CHECK(QuantityInHand >= 0)
GO