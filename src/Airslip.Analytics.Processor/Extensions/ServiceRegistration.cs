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
            (Integrations.Banking.Types.Data.Constants.EVENT_HUB_BANKING_BANKS, DataSources.Yapily);
        handoff.Register<IRegisterDataService<Integration, IntegrationModel, BankingAccountModel>>
            (Integrations.Banking.Types.Data.Constants.EVENT_HUB_BANKING_ACCOUNTS, DataSources.Yapily);
        handoff.Register<IRegisterDataService<BankTransaction, BankTransactionModel, BankingTransactionModel>>
            (Integrations.Banking.Types.Data.Constants.EVENT_HUB_BANKING_TRANSACTIONS, DataSources.Yapily);
        handoff.Register<IRegisterDataService<BankAccountBalance, BankAccountBalanceModel, BankingBalanceModel>>
            (Integrations.Banking.Types.Data.Constants.EVENT_HUB_BANKING_BALANCES, DataSources.Yapily);
        handoff.Register<IRegisterDataService<BankSyncRequest, BankSyncRequestModel, BankingSyncRequestModel>>
            (Integrations.Banking.Types.Data.Constants.EVENT_HUB_BANKING_SYNC_REQUESTS, DataSources.Yapily);

        handoff.Register<IRegisterDataService<MerchantTransaction, MerchantTransactionModel, TransactionEnvelope>>(Constants.EVENT_QUEUE_MERCHANT_TRANSACTIONS, DataSources.Api2Cart);
        handoff.Register<IRegisterDataService<Integration, IntegrationModel, RawApi2CartAccountModel>>(Constants.EVENT_QUEUE_API_2_CART_ACCOUNTS, DataSources.Api2Cart);
        
        handoff.Register<IRelationshipService>(Constants.EVENT_QUEUE_PARTNER_RELATIONSHIPS, DataSources.CustomerPortal);
        handoff.Register<IBusinessService>(Constants.EVENT_QUEUE_BUSINESS, DataSources.CustomerPortal);
        
        handoff.Register<IAnalysisHandlingService<BankTransactionModel>>(Constants.MESSAGE_QUEUE_BANK_TRANSACTION, 
            DataSources.Analytics);
        handoff.Register<IAnalysisHandlingService<MerchantTransactionModel>>(Constants.MESSAGE_QUEUE_MERCHANT_TRANSACTION, 
            DataSources.Analytics);
        handoff.Register<IAnalysisHandlingService<BankAccountBalanceModel>>(Constants.MESSAGE_QUEUE_BANK_ACCOUNT_BALANCE, 
            DataSources.Analytics);
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