namespace ZaHash
{
    public class ZaHasher
    {
        /// <summary>
        /// Generate the hash from a specific value.
        /// </summary>
        /// <param name="data">Input byte array to hash.</param>
        /// <returns>A string containing the full hash.</returns>
        public static string HashData(byte[] data)
        {
            string hash = GetNormalHash(data);
            ProtoRandom.ProtoRandom protoRandom = new ProtoRandom.ProtoRandom(100);

            string chars = "1234567";
            string generated = protoRandom.GetRandomString(chars, 2);
            int firstChar = int.Parse(generated[0].ToString()), secondChar = int.Parse(generated[1].ToString());
            string newHash = "";

            foreach (char c in hash)
            {
                int theChar = c;
                theChar += firstChar;
                newHash += (char)theChar;
            }

            newHash = firstChar.ToString() + secondChar.ToString() + newHash;

            string totalNewHash = "";

            foreach (char c in newHash)
            {
                int theChar = c;
                theChar += 6;
                totalNewHash += (char)theChar;
            }

            return totalNewHash;
        }

        /// <summary>
        /// Check if a specific hash is valid and corresponds to given data.
        /// </summary>
        /// <param name="hash">Hash string value to analyze.</param>
        /// <param name="data">Input bytes data to compare.</param>
        /// <returns>A boolean that indicates if the hash corresponds to the given input data.</returns>
        public static bool IsHashValid(string hash, byte[] data)
        {
            string newHash = "";

            foreach (char c in hash)
            {
                int theChar = c;
                theChar -= 6;
                newHash += (char)theChar;
            }

            string theChars = hash.Substring(0, 2);
            string newString = hash.Substring(2);

            string theNewHash = "";

            foreach (char c in newString)
            {
                int theChar = c;
                theChar -= int.Parse(theChars[0].ToString());
                theNewHash += (char)theChar;
            }

            if (GetNormalHash(data) == theNewHash)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Get the normal non-secure hash from a byte array.
        /// </summary>
        /// <param name="data">Input bytes data.</param>
        /// <returns>A string containing the normal non-secure hash.</returns>
        private static string GetNormalHash(byte[] data)
        {
            decimal total = 0, total1 = 0, total2 = 0, total3 = 0;

            for (int i = 0; i < data.Length; i++)
            {
                total += ((decimal)data[i]) * (i + 1);
                total1 += ((decimal)data[i]) * (i + 1);
                decimal thing = decimal.Parse(data[i].ToString() + "," + (i + 1).ToString());
                total += thing;
                total2 += thing;
                decimal toAdd = 0;

                if ((((int)data[i]) % 2 == 0))
                {
                    if (i % 2 == 0)
                    {
                        toAdd = 0.1M + i;
                    }
                    else
                    {
                        toAdd = 0.2M + i;
                    }
                }
                else
                {
                    if (i % 2 == 0)
                    {
                        toAdd = 0.3M + i;
                    }
                    else
                    {
                        toAdd = 0.4M + i;
                    }
                }

                total += toAdd;
                total3 += toAdd;
            }

            string hash = total.ToString() + "-" + total1.ToString() + "-" + total2.ToString() + "-" + total3.ToString();

            hash = hash
                .Replace("1", "A")
                .Replace("2", "B")
                .Replace("3", "C")
                .Replace("4", "D")
                .Replace("5", "E")
                .Replace("6", "F")
                .Replace("7", "G")
                .Replace("8", "H")
                .Replace("9", "I")
                .Replace("0", "J")
                .Replace(",", "K")
                .Replace("-", "L");

            return hash;
        }
    }
}