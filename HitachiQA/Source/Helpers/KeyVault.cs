using System;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace HitachiQA.Helpers
{
    public class KeyVault
    {
        private readonly String KEY_VAULT_URI;

        public KeyVault(String KEY_VAULT_URI)
        {
            this.KEY_VAULT_URI = KEY_VAULT_URI;
        }


        KeyVaultSecret secret;
        public String GetSecret(string secretName, bool optional)
        {
            if(String.IsNullOrWhiteSpace(KEY_VAULT_URI))
            {
                Functions.handleFailure(new ArgumentNullException("Helpers.KeyVault - KEY_VAULT_URI was not set properly"));
            }

            var client = new SecretClient(new Uri(Environment.GetEnvironmentVariable("APP_KEYVAULT_URI")), new DefaultAzureCredential());

            try
            {
                secret = client.GetSecretAsync(KEY_VAULT_URI, secretName).Result;
            }
            catch (AuthenticationFailedException e)
            {
                Console.WriteLine($"Authentication Failed. {e.Message}");
            }

            return secret.ToString() ?? throw new Exception("Keyvault.GetSecret returned null");
        }


        public String GetSecret(string secretName)
        {
            return GetSecret(secretName, false);
        }
        
    }
}
