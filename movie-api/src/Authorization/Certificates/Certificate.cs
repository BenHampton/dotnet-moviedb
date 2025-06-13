// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace movie_api.Authorization.Certificates;

public class Key
{
    public required string alg { get; set; }
    public required string crv { get; set; }
    public required string kid { get; set; }
    public required string kty { get; set; }
    public required string use { get; set; }
    public required string x { get; set; }
    public required string y { get; set; }

    public X509Certificate2 Cert
    {
        get
        {
            return new X509Certificate2(Encoding.UTF8.GetBytes(x));
        }
    }
}
public class Certificate
{
    public required List<Key> keys { get; set; }

}
