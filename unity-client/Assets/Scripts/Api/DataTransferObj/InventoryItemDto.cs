using System;

namespace Api.DataTransferObj
{
    [Serializable]
    public class InventoryItemDto
    {
        public long inventory_id;
        public long item_id;
        public string item_name;
        public string expire_at;
    }
}


