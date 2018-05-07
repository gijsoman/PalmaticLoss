public interface IWeapon : IInventoryItem, IPickupable
{
    int Damage { set; get; }
    int SocketAmount { set; get; }
}
