using System.Collections.Generic;

namespace Api.DataTransferObj
{
    public class ShopItemDto
    {
        public long id;
        public string code;
        public string name;
        public string type;
        public int base_price;
        public List<ShopDurationDto> durations;
    }
}