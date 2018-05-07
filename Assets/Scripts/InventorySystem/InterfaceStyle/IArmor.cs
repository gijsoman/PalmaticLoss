public interface IArmor : IInventoryItem, IPickupable
{
    int ArmorRate { get; }
    int SocketAmount { get; }
}
