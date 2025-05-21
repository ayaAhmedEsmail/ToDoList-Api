namespace ToDoList_Api.Data
{
    public class JwtOptions
    {
            public string Issure { get; set; }
            public string Audience { get; set; }
            public int lifetime { get; set; }
            public string SigningKey { get; set; }

    }
}
