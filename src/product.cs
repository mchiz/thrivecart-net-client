using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThriveCart {
    public struct Product {
        [ JsonProperty( "product_id" ) ] public int ProductId;
        [ JsonProperty( "name" ) ] public string Name;
        [ JsonProperty( "label" ) ] public string Label;
        [ JsonProperty( "url" ) ] public string Url;
        [ JsonProperty( "embed_type" ) ] public string EmbedType;
        [ JsonProperty( "status" ) ] public int Status;
        [ JsonProperty( "statusString" ) ] public string StatusString;
        [ JsonProperty( "type" ) ] public int Type;
        [ JsonProperty( "typeString" ) ] public string TypeString;
    }
}
