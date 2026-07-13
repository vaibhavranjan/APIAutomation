using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicAPITests.Base
{
    public class RestLibrary
    {
        public RestClient restClient { get; }

        public RestLibrary()
        {
            var restClientOptions = new RestClientOptions
            {
                BaseUrl = new Uri("https://localhost:5001/"),
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true

            };

            restClient = new RestClient(restClientOptions);
        }
    }
}
