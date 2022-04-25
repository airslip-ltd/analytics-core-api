CREATE or alter proc dbo.CreateMerchantMetricSnapshot(
    @EntityId as nvarchar(50),
    @AirslipUserType as int,
    @Id as nvarchar(50)
)
AS
Declare @Year as int, @Month as int, @Day as int, @CurrencyCode as nvarchar(3)

select @Year = Year, @Month = Month, @Day = Day, @CurrencyCode = CurrencyCode
from MerchantTransactions
where Id = @Id
    merge into MerchantMetricSnapshots with (HOLDLOCK) as mms
    using
        (select mt.EntityId,
                mt.AirslipUserType,
                mt.Year,
                mt.Month,
                mt.Day,
                mt.CurrencyCode,
                count(*)                       as OrderCount,
                sum(isnull(mp.SalesTotal, 0))  as TotalSales,
                sum(isnull(mr.RefundTotal, 0)) as TotalRefunds,
                sum(isnull(mp.SaleCount, 0))   as SaleCount,
                sum(isnull(mr.RefundCount, 0)) as RefundCount
         from MerchantTransactions as mt
                  left outer join (select mp.MerchantTransactionId,
                                          sum(isnull(mp.TotalPrice, 0)) as SalesTotal,
                                          sum(isnull(mp.Quantity, 0))   as SaleCount
                                   from MerchantProducts as mp
                                   group by mp.MerchantTransactionId) as mp on mp.MerchantTransactionId = mt.Id
                  left outer join (select mr.MerchantTransactionId,
                                          sum(isnull(mri.Refund, 0)) as RefundTotal,
                                          sum(isnull(mri.Qty, 0))    as RefundCount
                                   from MerchantRefunds as mr
                                            left outer join MerchantRefundItems as mri on mri.MerchantRefundId = mr.Id
                                   group by mr.MerchantTransactionId) as mr on mr.MerchantTransactionId = mt.Id
         where mt.Day = @Day
           and mt.Month = @Month
           and mt.Year = @Year
           and mt.EntityId = @EntityId
           and mt.AirslipUserType = @AirslipUserType
           and mt.CurrencyCode = @CurrencyCode
         group by mt.EntityId, mt.AirslipUserType, mt.Year, mt.Month, mt.Day, mt.CurrencyCode) y
    on mms.EntityId = y.EntityId
        and mms.AirslipUserType = y.AirslipUserType
        and mms.Day = y.Day
        and mms.Month = y.Month
        and mms.Year = y.Year
        and mms.CurrencyCode = y.CurrencyCode
    when matched then
        update
        set mms.OrderCount   = y.OrderCount,
            mms.TotalSales   = y.TotalSales,
            mms.TotalRefunds = y.TotalRefunds,
            mms.SaleCount    = y.SaleCount,
            mms.RefundCount  = y.RefundCount
    when not matched then
        insert (EntityId, AirslipUserType, MetricDate, Year, Month, Day, TotalSales, SaleCount, TotalRefunds,
                RefundCount, OrderCount, CurrencyCode)
        VALUES (@EntityId, @AirslipUserType, datefromparts(@Year, @Month, @Day), @Year, @Month, @Day, y.TotalSales,
                y.SaleCount, y.TotalRefunds,
                y.RefundCount, y.OrderCount, y.CurrencyCode);

