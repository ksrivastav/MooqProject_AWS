using Amazon;
using Amazon.Runtime;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace IdentityServerCognito
{
    public  class IdentityCognitoConfigretriever
    {
        //IMemoryCache memCache;
        //public IdentityCognitoConfigretriever(IMemoryCache _memCache)
        //{
        //    memCache = _memCache;
        //}

       
        public async Task<GetSecretValueResponse> GetConfig()
        {
             GetSecretValueResponse response;
             string secretName = "IdentityServerConfig";
             string region = "eu-west-2";
             var i = RegionEndpoint.GetBySystemName(region);

            
            
           
            IAmazonSecretsManager client = new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName(region));


            GetSecretValueRequest request = new GetSecretValueRequest
            {
                SecretId = secretName,
                VersionStage = "AWSCURRENT", // VersionStage defaults to AWSCURRENT if unspecified.
            };

           

            try
            {
                response = await client.GetSecretValueAsync(request);
            }
            catch (Exception e)
            {
                // For a list of the exceptions thrown, see
                // https://docs.aws.amazon.com/secretsmanager/latest/apireference/API_GetSecretValue.html
                throw e;
                
            }

            return response;
        }
    }

}