using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;

namespace ISOGES_PM_API.Models.Utilities
{
    public class PasswordHasher
    {
        private const int SaltSize = 16; // Tamaño del salt en bytes
        private const int HashSize = 32; // Tamaño del hash en bytes
        private const int Iterations = 10000; // Número de iteraciones para el algoritmo de hash

        public static string HashPassword(string password)
        {
            // Generar un salt aleatorio
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[SaltSize]);

            // Crear el derivador de claves PBKDF2 con el salt y número de iteraciones
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations))
            {
                byte[] hash = pbkdf2.GetBytes(HashSize);

                // Combinar el salt y hash en un solo array
                byte[] hashBytes = new byte[SaltSize + HashSize];
                Array.Copy(salt, 0, hashBytes, 0, SaltSize);
                Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

                // Convertir el array a su representación en base64
                string hashedPassword = Convert.ToBase64String(hashBytes);
                return hashedPassword;
            }
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            // Obtener el salt y el hash de la contraseña almacenada
            byte[] hashBytes = Convert.FromBase64String(hashedPassword);
            byte[] salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            // Crear el derivador de claves PBKDF2 con el salt y número de iteraciones
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations))
            {
                byte[] hash = pbkdf2.GetBytes(HashSize);

                // Comparar el hash calculado con el hash almacenado
                for (int i = 0; i < HashSize; i++)
                {
                    if (hashBytes[i + SaltSize] != hash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }

}