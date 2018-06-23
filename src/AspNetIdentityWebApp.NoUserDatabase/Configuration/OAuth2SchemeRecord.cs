using System.Collections.Generic;

namespace AspNetIdentityWebApp.Configuration
{
    /*
     Managed Secrets
        {
          "oauth2:0:clientId": "<blah>.apps.googleusercontent.com",
          "oauth2:0:clientSecret": "<blah>"
        }

    "oauth2": [
        {
            "scheme": "Google",
            "clientId": "<put in manage user secrets>",
            "clientSecret": "<put in manage user secrets>",
            "authority": "https://accounts.google.com",
            "callbackPath": "/signin-google",
            "additionalEndpointBaseAddresses": []
        }
    ]
    */

    public class OAuth2SchemeRecord
    {
        public string Scheme { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Authority { get; set; }
        public string CallbackPath { get; set; }
        public List<string> AdditionalEndpointBaseAddresses { get; set; }
    }
}
