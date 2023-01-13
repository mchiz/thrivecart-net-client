using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThriveCart {
    public struct PricingOption {
        [ JsonProperty( "id" ) ]                     public int Id;
        [ JsonProperty( "name" ) ]                   public string Name;
        [ JsonProperty( "preferred" ) ]              public bool Preferred;
        [ JsonProperty( "type" ) ]                   public string Type;
        [ JsonProperty( "price" ) ]                  public int Price;
        [ JsonProperty( "price_str" ) ]              public string PriceStr;
        [ JsonProperty( "subscription_price" ) ]     public int SubscriptionPrice;
        [ JsonProperty( "subscription_price_str" ) ] public string SubscriptionPriceStr;
        [ JsonProperty( "trial_period" ) ]           public int TrialPeriod;
        [ JsonProperty( "frequency" ) ]              public string Frequency;
        [ JsonProperty( "payments" ) ]               public int Payments;
    }
}
