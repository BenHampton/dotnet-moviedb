// using System.Security.Cryptography.X509Certificates;
// using System.Text;
//
// namespace movie_api.Authorization.Google;
//
// public class GoogleKey
// {
//     public required string alg { get; set; }
//     public required string crv { get; set; }
//     public required string kid { get; set; }
//     public required string kty { get; set; }
//     public required string use { get; set; }
//     public required string x { get; set; }
//     public required string y { get; set; }
//
//     public X509Certificate2 Cert
//     {
//         get
//         {
//             return new X509Certificate2(Encoding.UTF8.GetBytes(x));
//         }
//     }
// }
//
// public class GoogleCertificate
// {
//     public required List<GoogleKey> keys { get; set; }
// }
