CREATE or alter proc dbo.CreateMerchantMetricSnapshot(
    @EntityId as varchar(33),
    @AirslipUserType as int,
    @Id as varchar(33)
)
AS
Declare @Year as int, @Month as int, @Day as int

select @Year = Year, @Month = Month, @Day = Day
from MerchantTransactions
where Id = @Id
    
    merge into MerchantMetricSnapshots as mms
    using
        (
            select mt.EntityId,
                   mt.AirslipUserType,
                   mt.Year,
                   mt.Month,
                   mt.Day,
                   count(*)                       as OrderCount,
                   sum(isnull(mp.SalesTotal, 0))  as TotalSales,
                   sum(isnull(mr.RefundTotal, 0)) as TotalRefunds,
                   sum(isnull(mp.SaleCount, 0))   as SaleCount,
                   sum(isnull(mr.RefundCount, 0)) as RefundCount
            from MerchantTransactions as mt
                     left outer join (
                select mp.MerchantTransactionId,
                       sum(isnull(mp.TotalPrice, 0)) as SalesTotal,
                       sum(isnull(mp.Quantity, 0))   as SaleCount
                from MerchantProducts as mp
                group by mp.MerchantTransactionId
            ) as mp on mp.MerchantTransactionId = mt.Id
                     left outer join (
                select mr.MerchantTransactionId,
                       sum(isnull(mri.Refund, 0)) as RefundTotal,
                       sum(isnull(mri.Qty, 0))    as RefundCount
                from MerchantRefunds as mr
                         left outer join MerchantRefundItems as mri on mri.MerchantRefundId = mr.Id
                group by mr.MerchantTransactionId
            ) as mr on mr.MerchantTransactionId = mt.Id
            where mt.Day = @Day
              and mt.Month = @Month
              and mt.Year = @Year
              and mt.EntityId = @EntityId
              and mt.AirslipUserType = @AirslipUserType
            group by mt.EntityId, mt.AirslipUserType, mt.Year, mt.Month, mt.Day) y
    on mms.EntityId = y.EntityId
        and mms.AirslipUserType = y.AirslipUserType
        and mms.Day = y.Day
        and mms.Month = y.Month
        and mms.Year = y.Year
    when matched then
        update
        set mms.OrderCount   = y.OrderCount,
            mms.TotalSales   = y.TotalSales,
            mms.TotalRefunds = y.TotalRefunds,
            mms.SaleCount    = y.SaleCount,
            mms.RefundCount  = y.RefundCount
    when not matched then
        insert (EntityId, AirslipUserType, MetricDate, Year, Month, Day, TotalSales, SaleCount, TotalRefunds,
                RefundCount, OrderCount)
        VALUES (@EntityId, @AirslipUserType, datefromparts(@Year, @Month, @Day), @Year, @Month, @Day, y.TotalSales,
                y.SaleCount, y.TotalRefunds,
                y.RefundCount, y.OrderCount);
