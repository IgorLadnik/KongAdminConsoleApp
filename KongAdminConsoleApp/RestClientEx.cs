using System;
using System.Net.Security;
using RestSharp;

namespace KongAdminConsoleApp
{
    public class RestClientEx : RestClient
    {
        public RestClientEx(RemoteCertificateValidationCallback remoteCertificateValidationCallback = null) =>
            SetRemoteCertificateValidation(remoteCertificateValidationCallback);

        public RestClientEx(Uri baseUrl, RemoteCertificateValidationCallback remoteCertificateValidationCallback = null) : base(baseUrl) =>
            SetRemoteCertificateValidation(remoteCertificateValidationCallback);

        public RestClientEx(string baseUrl, RemoteCertificateValidationCallback remoteCertificateValidationCallback = null) : base(baseUrl) =>
            SetRemoteCertificateValidation(remoteCertificateValidationCallback);

        private void SetRemoteCertificateValidation(RemoteCertificateValidationCallback remoteCertificateValidationCallback) =>
            RemoteCertificateValidationCallback += remoteCertificateValidationCallback ?? ((sender, certificate, chain, sslPolicyErrors) => true);
    }
}
