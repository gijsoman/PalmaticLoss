public interface IInventoryItem : IPickupable
{
    string Name{ set; get; }
    string Rareness { set; get; }
}
