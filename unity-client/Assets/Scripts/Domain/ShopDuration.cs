namespace Domain
{
    public class ShopDuration
    {
        public string Key { get; }
        public int Seconds { get; }
        public int Price { get; }

        public ShopDuration(string key, int seconds, int price)
        {
            Key = key;
            Seconds = seconds;
            Price = price;
        }

        public string DisplayLabel
        {
            get
            {
                if (Seconds < 3600)
                    return $"{Seconds / 60}m";

                if (Seconds < 86400)
                    return $"{Seconds / 3600}h";

                return $"{Seconds / 86400}d";
            }
        }
    }
}
