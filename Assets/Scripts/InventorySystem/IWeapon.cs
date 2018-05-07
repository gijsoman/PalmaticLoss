public interface IWeapon : IInventoryItem
{
    int Damage { set; get; }
    int SocketAmount { set; get; }
}
