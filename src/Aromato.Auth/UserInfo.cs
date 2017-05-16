using Newtonsoft.Json;

namespace Aromato.Auth
{
    public class UserInfo
    {
        [JsonProperty(PropertyName = "sub")]
        public string Subject { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "given_name")]
        public string GivenName { get; set; }
        [JsonProperty(PropertyName = "family_name")]
        public string FamilyName { get; set; }
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }
        [JsonProperty(PropertyName = "phone_number")]
        public string PhoneNumber { get; set; }
        [JsonProperty(PropertyName = "gender")]
        public string Gender { get; set; }
    }
}