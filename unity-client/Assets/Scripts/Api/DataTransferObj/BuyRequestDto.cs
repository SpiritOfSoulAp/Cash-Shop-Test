namespace Api.DataTransferObj
{
    [System.Serializable]
    public class BuyRequestDto
    {
        public long user_id;
        public long item_id;
        public string duration_key;
        public string request_id;
    }
}
