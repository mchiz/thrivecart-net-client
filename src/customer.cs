using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThriveCart {
    public struct Customer {
        public struct CustomerData {
            public struct AddressData {
                [ JsonProperty( "country" ) ] public string Country;
            }

            [ JsonProperty( "name" ) ]       public string Name;
            [ JsonProperty( "email" ) ]      public string Email;
            [ JsonProperty( "ip_address" ) ] public string IpAddress;
            [ JsonProperty( "address" ) ]    public AddressData Address;
        }

        public struct Subscription {
            [ JsonProperty( "status" ) ] public string Status;
            [ JsonProperty( "order_id" ) ] public int OrderId;
            [ JsonProperty( "invoice_id" ) ] public int InvoiceId;
            [ JsonProperty( "subscription_id" ) ] public int SubscriptionId;
            [ JsonProperty( "subscription_reference" ) ] public string SubscriptionReference;
            [ JsonProperty( "currency" ) ] public string Currency;
            [ JsonProperty( "frequency" ) ] public string Frequency;
            [ JsonProperty( "amount" ) ] public int Amount;
            [ JsonProperty( "payments" ) ] public int Payments;
            [ JsonProperty( "total_paid" ) ] public int TotalPaid;
            [ JsonProperty( "last_payment" ) ] public DateTime? LastPayment;
            [ JsonProperty( "date_started" ) ] public DateTime? DateStarted;
            [ JsonProperty( "processor" ) ] public string Processor;
            [ JsonProperty( "item_name" ) ] public string ItemName;
            [ JsonProperty( "item_type" ) ] public string ItemType;
            [ JsonProperty( "item_id" ) ] public int ItemId;
            [ JsonProperty( "next_payment" ) ] public DateTime? NextPayment;

        }

        [ JsonProperty( "customer" ) ] public CustomerData Data;
        [ JsonProperty( "subscriptions" ) ] public Subscription [ ]Subscriptions;
    }
}
