using System.Text.Json.Serialization;

namespace backend.Models.Auth
{
    public class LoginRequest
    {

        public string usuario { get; set; }


        public string contraseña { get; set; }

         }
}
