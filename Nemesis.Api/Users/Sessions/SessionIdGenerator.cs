public static class SessionIdGenerator
{
    public static string GenerateId()
    {
        var guids = Enumerable.Repeat(1, 4).Select(_ => Guid.NewGuid().ToString());

        return string.Join(" ", guids).Replace("-", "");
    }
}