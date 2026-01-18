using System;
using CICD.Utils;
using CICD.Models;
using Newtonsoft.Json;
using RestSharp;
using CICD.Helpers;
using System.Reflection;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace CICD.Helpers
{
    public class APIHelper
    {
        public Client RestClient;
        public APIHelper()
        {
            this.RestClient = new Client();
        }
        public string GetToken()
        {
            try
            {
                Models.Token TokenJson = new Models.Token()
                {
                    UserId = "-1",
                    UserRole = "1",
                    TenantId = "1",
                    LocationId = "1"
                };
                string tokenJson = JsonConvert.SerializeObject(TokenJson);
                string uri = GetRequestUri(Constants.AUTHENTICATION_BASE_URL).ToString();
                ApiResult response = this.RestClient.GetToken(uri, tokenJson).Result;
                if (response.Data != null)
                {
                    return response.Data;
                }
                else
                    throw new KeyNotFoundException("token is not generated!");
            }
            catch (Exception ex)
            {
                throw new KeyNotFoundException(string.Format("token is not generated ({0}): {1}", ex.Message, ex.InnerException));
            }
        }
        public AuthenticationResponseData Authentication(Login login, string token)
        {
            AuthenticationResponseData authenticationResponseData = new AuthenticationResponseData();
            try
            {
                Models.Login paylod = new Models.Login()
                {
                    UserName = login.UserName,
                    Password = EnDecrypt(login.Password)
                };

                string paylodJson = JsonConvert.SerializeObject(paylod);
                string uri = GetRequestUri(Constants.CICD_BASE_URL + Constants.AUTHENTICATION_URL).ToString();
                ApiResult response = this.RestClient.Post(uri, paylodJson, token).Result;
                if (response.Data != null)
                {
                    dynamic data = JsonConvert.DeserializeObject(response.Data);
                    AuthenticationResponse authenticationResponse = new AuthenticationResponse();
                    authenticationResponse.IsAuthenticated = data.data.isAuthenticated;
                    authenticationResponse.Message = data.data.message;
                    authenticationResponse.RoleName = data.data.roleName;
                    authenticationResponseData.Data = authenticationResponse;
                }
                else
                {
                    authenticationResponseData.Exception = response.Exception;
                }
            }
            catch (Exception ex)
            {
            }
            return authenticationResponseData;
        }

        public bool IsValid(string token)
        {
            JwtSecurityToken jwtSecurityToken;
            try
            {
                jwtSecurityToken = new JwtSecurityToken(token);
            }
            catch (Exception ex)
            {
                return false;
            }

            return jwtSecurityToken.ValidTo > DateTime.UtcNow;
        }

        private Uri GetRequestUri(string uri)
        {
            string pattern = "/+";
            string replacement = "/";
            Regex rgx = new Regex(pattern);
            string result = rgx.Replace(uri, replacement);

            result = result.Replace(":/", "://");

            return new Uri(result);
        }

        /// <summary>
        /// Encrypt and decrypt
        /// </summary>
        /// <param name="input"></param>
        /// <param name="decrypt"></param>
        /// <returns></returns>
        public static string EnDecrypt(string input, bool decrypt = false)
        {
            string _alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ984023";

            if (decrypt)
            {
                Dictionary<string, uint> _index = null;
                Dictionary<string, Dictionary<string, uint>> _indexes = new Dictionary<string, Dictionary<string, uint>>(2, StringComparer.InvariantCulture);

                if (_index == null)
                {
                    Dictionary<string, uint> cidx;

                    string indexKey = "I" + _alphabet;

                    if (!_indexes.TryGetValue(indexKey, out cidx))
                    {
                        lock (_indexes)
                        {
                            if (!_indexes.TryGetValue(indexKey, out cidx))
                            {
                                cidx = new Dictionary<string, uint>(_alphabet.Length, StringComparer.InvariantCulture);
                                for (int i = 0; i < _alphabet.Length; i++)
                                {
                                    cidx[_alphabet.Substring(i, 1)] = (uint)i;
                                }
                                _indexes.Add(indexKey, cidx);
                            }
                        }
                    }

                    _index = cidx;
                }

                MemoryStream ms = new MemoryStream(Math.Max((int)Math.Ceiling(input.Length * 5 / 8.0), 1));

                for (int i = 0; i < input.Length; i += 8)
                {
                    int chars = Math.Min(input.Length - i, 8);

                    ulong val = 0;

                    int bytes = (int)Math.Floor(chars * (5 / 8.0));

                    for (int charOffset = 0; charOffset < chars; charOffset++)
                    {
                        uint cbyte;
                        if (!_index.TryGetValue(input.Substring(i + charOffset, 1), out cbyte))
                        {
                            throw new ArgumentException(string.Format("Invalid character {0} valid characters are: {1}", input.Substring(i + charOffset, 1), _alphabet));
                        }

                        val |= (((ulong)cbyte) << ((((bytes + 1) * 8) - (charOffset * 5)) - 5));
                    }

                    byte[] buff = BitConverter.GetBytes(val);
                    Array.Reverse(buff);
                    ms.Write(buff, buff.Length - (bytes + 1), bytes);
                }

                return System.Text.ASCIIEncoding.ASCII.GetString(ms.ToArray());
            }
            else
            {

                byte[] data = System.Text.ASCIIEncoding.ASCII.GetBytes(input);

                StringBuilder result = new StringBuilder(Math.Max((int)Math.Ceiling(data.Length * 8 / 5.0), 1));

                byte[] emptyBuff = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };
                byte[] buff = new byte[8];

                for (int i = 0; i < data.Length; i += 5)
                {
                    int bytes = Math.Min(data.Length - i, 5);

                    Array.Copy(emptyBuff, buff, emptyBuff.Length);
                    Array.Copy(data, i, buff, buff.Length - (bytes + 1), bytes);
                    Array.Reverse(buff);
                    ulong val = BitConverter.ToUInt64(buff, 0);

                    for (int bitOffset = ((bytes + 1) * 8) - 5; bitOffset > 3; bitOffset -= 5)
                    {
                        result.Append(_alphabet[(int)((val >> bitOffset) & 0x1f)]);
                    }
                }


                return result.ToString();
            }
        }
    }
}

