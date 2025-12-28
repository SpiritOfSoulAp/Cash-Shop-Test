using System.Collections.Generic;

namespace Api.DataTransferObj
{
    public class UserDto
    {
        public long id;
        public int cash;
        public List<InventoryItemDto> InventoryItems;
    }
}