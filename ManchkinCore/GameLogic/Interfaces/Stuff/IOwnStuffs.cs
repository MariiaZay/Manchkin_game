namespace ManchkinCore.Interfaces;

public interface IOwnStuffs
{
    #region Stuffs
    
    public List<IStuff> SmallStuffs { get; }
    public List<IStuff> HugeStuffs { get; }
    public  List<IMercenary> Mercenaries { get; }
    public IStuff? WornArmor { get; }
    public IStuff? WornShoes { get; }
    public IStuff? WornHat { get; }
    public IHands Hands { get; }
    
    public bool Cheat { get; }

    #endregion
    
    public bool HasHugeStuff { get; }
    public bool HasMercenary { get; }
    public void ChangeCheat();

    public void GetMercenary();
    public void GetMercenary(IStuff stuff);
    public void GiveToMercenary(IStuff stuff);
    public void KillMercenary();
    
    public bool CanTakeStuff(IStuff stuff);

    public string TakeStuff(IStuff stuff);
    //public void ChangeEquipment(List<IStuff> equipment);

    #region Lost Stuff

    public void LostMostPowerfulStuff();
    public void LostStuff(IStuff stuff);
    public void LostAllStuffs();

    #endregion
    public void SellStuffs(List<IStuff> stuffs);
    public void SellByDoublePrice(IStuff stuff);
}