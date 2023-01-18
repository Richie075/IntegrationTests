using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;


namespace ASPI.Model.Measurements
{
    public class Authentication
    {
        //private static BearerToken _bearerToken = null;
        private static DateTime _expirationTime = DateTime.UtcNow;
        private static HttpClient _httpClient;

        //public static AuthenticationHeaderValue GetAuthenticationHeaderValue(HttpClient httpClient)
        //{

        //    if (httpClient == null)
        //        throw new NullReferenceException("httpClient");

        //    _httpClient = httpClient;

        //    if (_bearerToken == null)
        //    {
        //        try
        //        {
        //            var loginTask = Login();
        //            loginTask.Wait();
        //            _bearerToken = loginTask.Result;
        //        }
        //        catch (Exception e)
        //        {
        //            throw;
        //        }
        //    }
        //    else
        //    {
        //        //do nothing
        //    }

        //    if (_bearerToken == null)
        //        throw new NullReferenceException("_bearerToken");

        //    return new AuthenticationHeaderValue("Bearer", _bearerToken.AccessToken);
        //}

        //private static async Task<BearerToken> Login()
        //{
        //    var token = new Token("password", "laetusadmin", "Laetus01");

        //    JObject payload;

        //    Add Authorization header
        //   _httpClient.DefaultRequestHeaders.Authorization =
        //        new AuthenticationHeaderValue("Basic",
        //            Convert.ToBase64String(
        //                Encoding.ASCII.GetBytes(
        //                    string.Format("{0}:{1}", "Laetus.EC", "EC01C6C6-8842-478E-A551-A7B296D94C67"))));

        //    Create request body:
        //    var tokenRequest = new HttpRequestMessage
        //    {
        //        Method = HttpMethod.Post,
        //        RequestUri = new Uri(_httpClient.BaseAddress.AbsoluteUri + "token"),//new Uri("http://192.168.228.25:55555/token".), //PIDALSSVMANE01.tts.loc
        //        Content = new FormUrlEncodedContent(new Dictionary<string, string> {
        //                { "grant_type", token.GrantType },
        //                { "username", token.Username },
        //                { "password", token.Password },
        //                { "client_id", "Laetus.EC" },
        //                { "client_secret", "EC01C6C6-8842-478E-A551-A7B296D94C67" }
        //            })
        //    };

        //    var response = await _httpClient.SendAsync(tokenRequest).ConfigureAwait(false);

        //    if (!response.IsSuccessStatusCode)
        //        throw new Exception(response.ReasonPhrase);

        //    payload = JObject.Parse(await response.Content.ReadAsStringAsync());

        //    _bearerToken = new BearerToken
        //    {
        //        TokenType = payload.Value<string>("token_type"),
        //        AccessToken = payload.Value<string>("access_token"),
        //        RefreshToken = payload.Value<string>("refresh_token"),
        //        ExpiresIn = payload.Value<int>("expires_in")
        //    };

        //    _expirationTime = DateTime.UtcNow.AddMinutes(300);

        //    return _bearerToken;
        //}
    }
}
