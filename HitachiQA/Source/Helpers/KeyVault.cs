using System;
using System.Collections.Generic;
using System.Text;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Core;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Azure.KeyVault;
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
        public String GetSecret(string secretName, bool optional)
        {
            if(String.IsNullOrWhiteSpace(KEY_VAULT_URI))
            {
                Functions.handleFailure(new ArgumentNullException("Helpers.KeyVault - KEY_VAULT_URI was not set properly"));
            }
            AzureServiceTokenProvider azureServiceTokenProvider = new AzureServiceTokenProvider();

            String secret = null;


            using (var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback)))
            {
                try
                {
                    var secretBundle = keyVaultClient.GetSecretAsync(this.KEY_VAULT_URI, secretName).Result;
                    secret = secretBundle.Value;
                }
                catch(Exception ex)
                {
                    if (optional)
                    {
                        return null;
                    }
                    else
                    {
                        Functions.handleFailure("Error while retrieving secrets from azure KeyVault", ex);
                    } 
                }
            }

            return secret ?? throw new Exception("Keyvault.GetSecret returned null");
        }
        public String GetSecret(string secretName)
        {
            return GetSecret(secretName, false);
        }
        
    }
}
