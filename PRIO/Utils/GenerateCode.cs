namespace PRIO.Utils
{
    public static class GenerateCode
    {
        public static string Generate(string name)
        {
            return string.Concat(name.ToUpper().AsSpan(0, 3), Guid.NewGuid().ToString().AsSpan(0, 5));
        }
    }
}
