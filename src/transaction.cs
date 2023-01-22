using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThriveCart {
    public struct Transaction {
        public struct CustomerData {
            [ JsonProperty( "name" ) ] public string Name;
            [ JsonProperty( "email" ) ] public string Email;

        }

        [ JsonProperty( "event_id" ) ] public ulong EventId;
        [ JsonProperty( "base_product" ) ] public int BaseProduct;
        [ JsonProperty( "date" ) ] public DateTime Date;
        [ JsonProperty( "time" ) ] public DateTime Time;
        [ JsonProperty( "timestamp" ) ] public long TimeStamp;
        [ JsonProperty( "transaction_type" ) ] public string TransactionType;
        [ JsonProperty( "transaction_info" ) ] public string TransactionInfo;
        [ JsonProperty( "item_type" ) ] public string ItemType;
        [ JsonProperty( "item_id" ) ] public int ItemId;
        [ JsonProperty( "item_name" ) ] public string ItemName;
        [ JsonProperty( "item_pricing_option_name" ) ] public string ItemPricingOptionName;
        [ JsonProperty( "item_pricing_option_id" ) ] public int ItemPricingOptionId;
        [ JsonProperty( "subscription_id" ) ] public string SubscriptionId;
        [ JsonProperty( "internal_subscription_id" ) ] public string InternalSubscriptionId;
        [ JsonProperty( "transaction_id" ) ] public string TransactionId;
        [ JsonProperty( "amount" ) ] public int Amount;
        [ JsonProperty( "order_id" ) ] public long OrderId;
        [ JsonProperty( "invoice_id" ) ] public int InvoiceId;
        [ JsonProperty( "currency" ) ] public string Currency;
        [ JsonProperty( "processor" ) ] public string Processor;
        [ JsonProperty( "customer" ) ] public CustomerData Customer;
        [ JsonProperty( "coupon" ) ] public string Coupon;
        [ JsonProperty( "quantity" ) ] public int Quantity;
        [ JsonProperty( "reference" ) ] public string Reference;
    }

}
