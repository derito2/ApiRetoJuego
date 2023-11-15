namespace Api;

public class purse
{
    public int purse_id { get; set; }
    public int user_id { get; set; }
    public int? match_id { get; set; } // Nullable to handle DEFAULT NULL
    public int coins { get; set; }
    public int transaction_id { get; set; }
    public decimal cost { get; set; }

    public purse()
    {
    }
}


