CREATE or alter proc dbo.CreateMerchantMetricSnapshot(
    @Id as nvarchar(50)
)
AS
BEGIN

    Declare
        @Year as int,
        @Month as int,
        @Day as int,
        @CurrencyCode as nvarchar(3),
        @EntityId as nvarchar(50),
        @AirslipUserType as int;

    select @Year = Year,
           @Month = Month,
           @Day = Day,
           @CurrencyCode = CurrencyCode,
           @EntityId = EntityId,
           @AirslipUserType = AirslipUserType
    from MerchantTransactions
    where Id = @Id;

    if not exists(select *
                  from MerchantMetricSnapshots
                  where EntityId = @EntityId
                    and AirslipUserType = @AirslipUserType
                    and Day = @Day
                    and Month = @Month
                    and Year = @Year
                    and CurrencyCode = @CurrencyCode)
        insert into MerchantMetricSnapshots
        (EntityId, AirslipUserType, MetricDate, Year, Month, Day, CurrencyCode, OrderCount, RefundCount,
         SaleCount, TotalRefunds, TotalSales)
        VALUES (@EntityId, @AirslipUserType, datefromparts(@Year, @Month, @Day), @Year,
                @Month, @Day, @CurrencyCode, 0, 0, 0, 0, 0);

    update mams
    set mams.OrderCount   = y.OrderCount,
        mams.TotalSales   = y.TotalSales,
        mams.TotalRefunds = y.TotalRefunds,
        mams.SaleCount    = y.SaleCount,
        mams.RefundCount  = y.RefundCount
    from MerchantMetricSnapshots as mams
             join (select mt.EntityId,
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
                                             group by mp.MerchantTransactionId) as mp
                                            on mp.MerchantTransactionId = mt.Id
                            left outer join (select mr.MerchantTransactionId,
                                                    sum(isnull(mri.Refund, 0)) as RefundTotal,
                                                    sum(isnull(mri.Qty, 0))    as RefundCount
                                             from MerchantRefunds as mr
                                                      left outer join MerchantRefundItems as mri on mri.MerchantRefundId = mr.Id
                                             group by mr.MerchantTransactionId) as mr
                                            on mr.MerchantTransactionId = mt.Id
                   where mt.Day = @Day
                     and mt.Month = @Month
                     and mt.Year = @Year
                     and mt.EntityId = @EntityId
                     and mt.AirslipUserType = @AirslipUserType
                     and mt.CurrencyCode = @CurrencyCode
                   group by mt.IntegrationId, mt.EntityId, mt.AirslipUserType, mt.Year, mt.Month, mt.Day,
                            mt.CurrencyCode) y
                  on mams.CurrencyCode = y.CurrencyCode
    where mams.EntityId = y.EntityId
      and mams.AirslipUserType = y.AirslipUserType
      and mams.Day = y.Day
      and mams.Month = y.Month
      and mams.Year = y.Year
      and mams.CurrencyCode = y.CurrencyCode;

END