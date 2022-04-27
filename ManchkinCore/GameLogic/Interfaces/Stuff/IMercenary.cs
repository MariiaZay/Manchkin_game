namespace ManchkinCore.Interfaces;

public interface IMercenary
{
    public IStuff? Item { get; }
    public void ChangeEquipment(IStuff? stuff);
    
    //TODO: придумать, как реализовать остальной функционал наемничка
}