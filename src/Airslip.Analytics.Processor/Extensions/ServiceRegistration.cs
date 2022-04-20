using Airslip.Analytics.Core.Data;
using Airslip.Analytics.Core.Entities;
using Airslip.Analytics.Core.Interfaces;
using Airslip.Analytics.Core.Models;
using Airslip.Analytics.Core.Models.Raw.Api2Cart;
using Airslip.Analytics.Processor.Mappers;
using Airslip.Common.Services.Handoff.Data;
using Airslip.Common.Services.Handoff.Extensions;
using Airslip.Common.Types.Enums;
using Airslip.Integrations.Banking.Types.Models;
using Airslip.MerchantIntegrations.Types.Models;
using AutoMapper;

namespace Airslip.Analytics.Processor.Extensions;

public static class ServiceRegistration
{
    public static void RegisterHandoff(MessageHandoffOptions handoff)
    {
        handoff.Register<IRegisterDataService<Bank, BankModel, BankingBankModel>>
            (Integrations.Banking.Types.Data.Constants.EVENT_HUB_BANKING_BANKS);
        handoff.Register<IRegisterDataService<Integration, IntegrationModel, BankingAccountModel>>
            (Integrations.Banking.Types.Data.Constants.EVENT_HUB_BANKING_ACCOUNTS);
        handoff.Register<IRegisterDataService<BankTransaction, BankTransactionModel, BankingTransactionModel>>
            (Integrations.Banking.Types.Data.Constants.EVENT_HUB_BANKING_TRANSACTIONS);
        handoff.Register<IRegisterDataService<BankAccountBalance, BankAccountBalanceModel, BankingBalanceModel>>
            (Integrations.Banking.Types.Data.Constants.EVENT_HUB_BANKING_BALANCES);
        handoff.Register<IRegisterDataService<BankSyncRequest, BankSyncRequestModel, BankingSyncRequestModel>>
            (Integrations.Banking.Types.Data.Constants.EVENT_HUB_BANKING_SYNC_REQUESTS);

        handoff.Register<IRegisterDataService<MerchantTransaction, MerchantTransactionModel, TransactionEnvelope>>(Constants.EVENT_QUEUE_MERCHANT_TRANSACTIONS);
        handoff.Register<IRegisterDataService<Integration, IntegrationModel, RawApi2CartAccountModel>>(Constants.EVENT_QUEUE_COMMERCE_ACCOUNTS);
        
        handoff.Register<IRelationshipService>(Constants.EVENT_QUEUE_PARTNER_RELATIONSHIPS);
        handoff.Register<IBusinessService>(Constants.EVENT_QUEUE_BUSINESS);

        handoff.Register<IAnalysisHandlingService<BalanceAnalysisModel>>(Constants.MESSAGE_QUEUE_BANK_ACCOUNT_BALANCE_ENTITY);
        handoff.Register<IAnalysisHandlingService<BankTransactionModel>>(Constants.MESSAGE_QUEUE_BANK_TRANSACTION);
        handoff.Register<IAnalysisHandlingService<MerchantTransactionModel>>(Constants.MESSAGE_QUEUE_MERCHANT_TRANSACTION);
        handoff.Register<IAnalysisHandlingService<BankAccountBalanceModel>>(Constants.MESSAGE_QUEUE_BANK_ACCOUNT_BALANCE);
    }

    public static void RegisterMappings(IMapperConfigurationExpression cfg)
    {
        cfg
            .AddBankingData()
            .AddRawApi2CartData()
            .AddRawTransaction()
            .AddEntityModelMappings();
    }
}