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

        [ JsonProperty( "customer" ) ] public CustomerData Data;
    }
}
