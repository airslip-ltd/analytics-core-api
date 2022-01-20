using Airslip.Analytics.Core.Constants;
using Airslip.Analytics.Core.Entities;
using Airslip.Analytics.Core.Interfaces;
using Airslip.Analytics.Core.Models;
using Airslip.Analytics.Core.Models.Raw;
using Airslip.Analytics.Core.Models.Raw.Yapily;
using Airslip.Analytics.Processor.Mappers;
using Airslip.Common.Services.Handoff.Data;
using Airslip.Common.Services.Handoff.Extensions;
using Airslip.Common.Types.Transaction;
using AutoMapper;

namespace Airslip.Analytics.Processor.Extensions;

public static class ServiceRegistration
{
    public static void RegisterHandoff(MessageHandoffOptions handoff)
    {
        handoff.Register<IRegisterDataService<Bank, BankModel, RawYapilyBankModel>>(Constants.EVENT_QUEUE_YAPILY_BANKS);
        handoff.Register<IRegisterDataService<BankAccount, BankAccountModel, RawYapilyAccountModel>>(Constants.EVENT_QUEUE_YAPILY_ACCOUNTS);
        handoff.Register<IRegisterDataService<BankTransaction, BankTransactionModel, RawYapilyTransactionModel>>(Constants.EVENT_QUEUE_YAPILY_TRANSACTIONS);
        handoff.Register<IRegisterDataService<BankAccountBalance, BankAccountBalanceModel, RawYapilyBalanceModel>>(Constants.EVENT_QUEUE_YAPILY_BALANCES);
        handoff.Register<IRegisterDataService<BankSyncRequest, BankSyncRequestModel, RawYapilySyncRequestModel>>(Constants.EVENT_QUEUE_YAPILY_SYNC_REQUESTS);
        handoff.Register<IRegisterDataService<MerchantTransaction, MerchantTransactionModel, TransactionEnvelope>>(Constants.EVENT_QUEUE_MERCHANT_TRANSACTIONS);
    }

    public static void RegisterMappings(IMapperConfigurationExpression cfg)
    {
        cfg
            .AddRawYapilyData()
            .AddRawTransaction()
            .AddEntityModelMappings();
    }
}