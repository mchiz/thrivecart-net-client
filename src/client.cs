using Newtonsoft.Json;
using System.Net;
using System.Web;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ThriveCart {
    public class Client : IDisposable {
        public Client( string apiKey, ConnectionMode mode ) {
            _httpClient.DefaultRequestHeaders.Add( "Authorization", "Bearer " + apiKey );
            _httpClient.DefaultRequestHeaders.Add( "X-TC-Mode", mode == ConnectionMode.Live ? "live" : "test" );
            _httpClient.DefaultRequestHeaders.Add( "X-TC-Sdk", SdkVersion );
            _httpClient.DefaultRequestHeaders.Add( "X-TC-Version", ApiVersion );
        }

        public async Task< Customer > GetCustomerDataAsync( string emailAddress ) {
            string target = "https://thrivecart.com/api/external/customer";

            var content = new FormUrlEncodedContent( new[ ] {
                new KeyValuePair< string, string >( "email", emailAddress ),
            }  );

            using var result = await _httpClient.PostAsync( target, content );

            if( !result.IsSuccessStatusCode )
                throw new Exception( $"{result.ReasonPhrase} - {result.StatusCode}" );

            string jsonData = await result.Content.ReadAsStringAsync( );

            var data = Newtonsoft.Json.JsonConvert.DeserializeObject< Customer >( jsonData );

            return data;
        }

        public async Task< PricingOption[ ] > GetProductPricingOptionsAsync( int productId ) {
            string target = $"https://thrivecart.com/api/external/products/{productId}/pricing_options";
            
            using var result = await _httpClient.GetAsync( target );

            if( !result.IsSuccessStatusCode )
                throw new Exception( $"{result.ReasonPhrase} - {result.StatusCode}" );

            string jsonData = await result.Content.ReadAsStringAsync( );

            var data = Newtonsoft.Json.JsonConvert.DeserializeObject< PricingOption[ ] >( jsonData );

            return data;
        }

        public async Task< Transaction[ ] > GetTransactionsAsync( DateTime from, DateTime to, TransactionsFilter filter = default ) {
            if( to < from )
                throw new ArgumentException( "'from' has to be older than 'to'", "from" );

            const int transactionsPerPage = 100;
            int page = 1;

            var transactions = new List< Transaction >( );

            var settings = new JsonSerializerSettings {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            string tt = _transactionTypeFilters[ filter.TransactionType ];

            string ccf = "";

            if( !string.IsNullOrWhiteSpace( filter.CurrencyCode ) )
                ccf = $"&currency={filter.CurrencyCode}";

            do {
                string target = $"https://thrivecart.com/api/external/transactions?page={page}&perPage={transactionsPerPage}{tt}{ccf}";

                using var result = await _httpClient.GetAsync( target );

                if( !result.IsSuccessStatusCode )
                    throw new Exception( $"{result.ReasonPhrase} - {result.StatusCode}" );

                ++page;

                string jsonData = await result.Content.ReadAsStringAsync( );

                var trd = Newtonsoft.Json.JsonConvert.DeserializeObject< TransactionDataResponse >( jsonData, settings );

                bool addAll = false;

                if( trd.transactions.Length > 2 ) {
                    var first = trd.transactions[ 0 ];
                    var last = trd.transactions[ trd.transactions.Length - 1 ];

                    addAll = first.Time >= from && first.Time <= to &&
                             last.Time  >= from && last.Time  <= to;

                }

                bool finished = trd.transactions.Length < transactionsPerPage;

                if( addAll ) {
                    transactions.AddRange( trd.transactions );

                } else {
                    for( int i = 0; i < trd.transactions.Length; ++i ) {
                        var t = trd.transactions[ i ];

                        if( t.Time >= from && t.Time <= to ) {
                            transactions.Add( t );

                        } else if( t.Time < from ) {
                            finished = true;
                            break;

                        }
                    }
                }

                if( finished )
                    break;

            } while( true );

            return transactions.ToArray( );
        }

        public async Task < Product[ ] > GetAllProductsAsync( ) {
            string target = "https://thrivecart.com/api/external/products";
            
            using var result = await _httpClient.GetAsync( target );

            if( !result.IsSuccessStatusCode )
                throw new Exception( $"{result.ReasonPhrase} - {result.StatusCode}" );

            string jsonData = await result.Content.ReadAsStringAsync( );

            var settings = new JsonSerializerSettings {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            return Newtonsoft.Json.JsonConvert.DeserializeObject< Product[ ] >( jsonData, settings );

        }

        void IDisposable.Dispose( ) {
            _httpClient.Dispose( );
        }

        struct TransactionDataResponse {
            public Transaction [ ]transactions; 
        }


        HttpClient _httpClient = new HttpClient( );
        
        readonly string SdkVersion = "1.0.11";
        readonly string ApiVersion = "1.0.0";

        readonly static Dictionary< TransactionType, string > _transactionTypeFilters = new Dictionary< TransactionType, string >( ) {
            { TransactionType.Any,       "&transactionType=any" },
            { TransactionType.Charge, "&transactionType=charge" },
            { TransactionType.Rebill, "&transactionType=rebill" },
            { TransactionType.Refund, "&transactionType=refund" },
            { TransactionType.Cancel, "&transactionType=cancel" },
        };
    }
}