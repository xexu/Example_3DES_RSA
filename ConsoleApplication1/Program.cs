using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string message = "hello cryptoworld";
            
            #region 3des
            string pass3des = "3des";
            System.Console.WriteLine("+3DES");
            System.Console.WriteLine("Encrypt: " + message + " using pass: " + pass3des);
            string enc3des = Cryptography.EncryptTripleDES(message, pass3des);
            System.Console.WriteLine("Result: " + enc3des);
            System.Console.WriteLine("Decrypt: " + enc3des + " using pass: " + pass3des);
            string dec3des = Cryptography.DecryptTripleDES(enc3des, pass3des);
            System.Console.WriteLine("Result: " + dec3des);
            System.Console.WriteLine((dec3des == message) ? "OK" : "ERROR");
            System.Console.WriteLine("-3DES");
            #endregion
            System.Console.Write("Press enter..."); System.Console.ReadLine();
            #region rsa
            String pub = @"-----BEGIN PUBLIC KEY-----
MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDiDQQq+dXdxKIJpVNbj7Irn3Cs
q9ADg4btsT5i9n+ECgYGfMI1WeuzoH5jkoFpvCvQRTyWqrilkbHE4aw72kG2lt/s
ZqKdQSTcmmyrGpdfJnEqSLEinlo3imO4ZvA+jUhY7IeUa7dzeJ/96zznMGgtwNHL
tGadMOdC9IisoU0zywIDAQAB
-----END PUBLIC KEY-----";
            String priv = @"-----BEGIN RSA PRIVATE KEY-----
MIICXAIBAAKBgQDiDQQq+dXdxKIJpVNbj7Irn3Csq9ADg4btsT5i9n+ECgYGfMI1
WeuzoH5jkoFpvCvQRTyWqrilkbHE4aw72kG2lt/sZqKdQSTcmmyrGpdfJnEqSLEi
nlo3imO4ZvA+jUhY7IeUa7dzeJ/96zznMGgtwNHLtGadMOdC9IisoU0zywIDAQAB
AoGAby6LwD+Fi0AtmqTwLPauYXpYRvVyqGfGs54b2zz2VxE/CW3Pb8I8hesIfx0N
/d2JhzSmqkxp6TWRxA5jUkT1UFac9ignE6YqUjaseGGjE285b8mtn2hOwTNHujse
EK+tK+A6aJpnNwn2DdXKYc4ueZB2YybWYCBBnm4820MZ/VECQQDxNvFmHZ+CX2dD
ligxR4dGJ0Z/hTxEx/SZjWnmqE/9CBLBm2lkKc5bu7KyBVqIN4Tm5PV5Y/PkrZHa
V5mTo7R3AkEA7+gg6G4GVBh68DT66bqrZiKJlqZO2axc0LZugBaI4IuJCx7tH6qb
DI3zaze2JCEosWPTaMqZfQG2L7AuU4Z0TQJAPPzBSCpRPCtW9pWuj9cf8rLXdkJ/
nHxZ8cD5d6Iypy01YNIkcXjIfhUU90G3RB2VcrONBSYqcjUYXXYslFGdvQJAJ2cs
4IARsgZDSuiovXLXa/MIPiIamU3iALW1+Hu7B4Zjf9wYfjb7OFioPlfsJor7sAcB
VhjQlOOPFM4PDdDrkQJBAJucAfsH+Ex7dnd7hTmKRNdDdENqjYr4eWSRJ36wKuVu
hUgsrgQNQwG8LSpWpLpFrDma+YHzrUlHdxKgMuGZEq8=
-----END RSA PRIVATE KEY-----";
            System.Console.WriteLine("+RSA");
            System.Console.WriteLine("Encrypt: " + message + " using public key");
            byte[] encrsa_bytes = Cryptography.EncryptRSA(pub, message);
            System.Console.WriteLine("Decrypt: byte[] using private key");
            string decrsa = Cryptography.DecryptRSA(priv, encrsa_bytes);
            System.Console.WriteLine("Result: " + decrsa);
            System.Console.WriteLine((decrsa == message) ? "OK" : "ERROR");
            System.Console.WriteLine("-RSA");
            #endregion
            System.Console.Write("Press any key..."); System.Console.ReadKey();
        }
    }
}
