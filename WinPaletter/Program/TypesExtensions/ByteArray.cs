namespace WinPaletter.TypesExtensions
{
    public static class ByteArrayExtensions
    {
        // Compare two byte arrays for equality
        public static bool Equals_Method2(this byte[] array1, byte[] array2)
        {
            if (array1.Length != array2.Length)
            {
                return false;
            }

            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
