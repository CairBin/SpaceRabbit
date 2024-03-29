﻿using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Commons
{
    public static class HashHelper
    {
        private static string ToHashString(byte[] bytes)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i <bytes.Length;i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }

            return builder.ToString();
        }

        public static string ComputeSha256(Stream stream)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(stream);
                return ToHashString(bytes);
            }
        }

        public static string ComputeSha256(string input)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                return ToHashString(bytes);
            }
        }

        public static string ComputeMd5(Stream stream)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] bytes = md5Hash.ComputeHash(stream);
                return ToHashString(bytes);
            }
        }

        public static string ComputeMd5(string input)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] bytes = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                return ToHashString(bytes);
            }
        }

        public static string GenerateSalt()
        {
            Random random = new Random();
            return random.Next(1000, 20000).ToString();
        }

    }
}
