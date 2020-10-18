using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace eQACoLTD.ClientMvc.Handlers
{
    public class BearerTokenHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public BearerTokenHandler(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var accessToken = await GetAccessTokenAsync();
            if (!string.IsNullOrWhiteSpace(accessToken))
                request.SetBearerToken(accessToken);
            _httpContextAccessor.HttpContext.Session.SetString("access_token", accessToken);
            return await base.SendAsync(request, cancellationToken);
        }

        private async Task<string> GetAccessTokenAsync()
        {
            try
            {
                var expiresAtToken = await _httpContextAccessor
                    .HttpContext.GetTokenAsync("expires_at");
                var expiresAtDateTimeOffset =
                    DateTimeOffset.Parse(expiresAtToken, CultureInfo.InvariantCulture);
                if ((expiresAtDateTimeOffset.AddSeconds(-60)).ToUniversalTime() > DateTime.UtcNow)
                    return await _httpContextAccessor
                        .HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
                var refreshResponse = await GetRefreshResponseFromIDP();
                var updatedTokens = GetUpdatedTokens(refreshResponse);
                var currentAuthenticateResult = await _httpContextAccessor
                    .HttpContext
                    .AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                currentAuthenticateResult.Properties.StoreTokens(updatedTokens);
                await _httpContextAccessor.HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    currentAuthenticateResult.Principal,
                    currentAuthenticateResult.Properties);
                return refreshResponse.AccessToken;
            }
            catch
            {
                return "";
            }
        }

        private async Task<TokenResponse> GetRefreshResponseFromIDP()
        {
            var idpClient = _httpClientFactory.CreateClient("IDPClient");
            var metaDataResponse = await idpClient.GetDiscoveryDocumentAsync();
            var refreshToken = await _httpContextAccessor
            .HttpContext
            .GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);
            var refreshResponse = await idpClient.RequestRefreshTokenAsync(
            new RefreshTokenRequest
            {
                Address = metaDataResponse.TokenEndpoint,
                ClientId = _configuration["ClientId"],
                ClientSecret = _configuration["ClientSecret"],
                RefreshToken = refreshToken
            });
            return refreshResponse;
        }

        private List<AuthenticationToken> GetUpdatedTokens(TokenResponse refreshResponse)
        {
            var updatedTokens = new List<AuthenticationToken>();
            updatedTokens.Add(new AuthenticationToken
            {
                Name = OpenIdConnectParameterNames.IdToken,
                Value = refreshResponse.IdentityToken
            });
            updatedTokens.Add(new AuthenticationToken
            {
                Name = OpenIdConnectParameterNames.AccessToken,
                Value = refreshResponse.AccessToken
            });
            updatedTokens.Add(new AuthenticationToken
            {
                Name = OpenIdConnectParameterNames.RefreshToken,
                Value = refreshResponse.RefreshToken
            });
            updatedTokens.Add(new AuthenticationToken
            {
                Name = "expires_at",
                Value = (DateTime.UtcNow + TimeSpan.FromSeconds(refreshResponse.ExpiresIn)).
            ToString("o", CultureInfo.InvariantCulture)
            });
            return updatedTokens;
        }
    }
}