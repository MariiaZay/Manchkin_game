namespace ManchkinCore.Interfaces;

public interface IMercenary : IDescriptable
{
    public IStuff? Item { get; }
    public void ChangeEquipment(IStuff? stuff);
}