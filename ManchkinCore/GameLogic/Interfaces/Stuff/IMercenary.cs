using ManchkinCore.GameLogic.Interfaces.Manchkin;

namespace ManchkinCore.GameLogic.Interfaces.Stuff;

public interface IMercenary : IDescriptable
{
    public IStuff? Item { get; }
    public void ChangeEquipment(IStuff? stuff);
}