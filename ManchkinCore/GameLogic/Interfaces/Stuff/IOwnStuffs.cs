namespace ManchkinCore.Interfaces;

public interface IOwnStuffs
{
    #region Stuffs
    
    public List<IStuff> SmallStuffs { get; }
    public List<IStuff> HugeStuffs { get; }
    public  List<IMercenary> Mercenaries { get; }
    public IStuff Armor { get; }
    public IStuff Shoes { get; }
    public IStuff Hat { get; }
    public IHands Hands { get; }

    #endregion
    
    public bool HasHugeStuff();
    public bool HasMercenary();
    
    public bool CanTakeStuff(IStuff stuff);
    //public void ChangeEquipment(List<IStuff> equipment);

    #region Lost Stuff

    public void LostMostPowerfulStuff();
    public void LostStuff(IStuff stuff);
    public void LostAllStuffs();

    #endregion
    public int SellStuffs(List<IStuff> stuffs);
}