using ManchkinCore.Enums.Accessory;

namespace ManchkinCore.Interfaces;

public interface IOwnStuffs
{
    #region Stuffs
    
    public List<IStuff?> SmallStuffs { get; }
    public List<IStuff?> HugeStuffs { get; }
    public  List<IMercenary> Mercenaries { get; }
    public IStuff? WornArmor { get; }
    public IStuff? WornShoes { get; }
    public IStuff? WornHat { get; }
    public IHands Hands { get; }

    #endregion
    
    public bool HasHugeStuff { get; }
    public bool HasMercenary { get; }
    public void UseCheat(IStuff? stuff);
    public void CancelCheat(IStuff? stuff);

    public void GetMercenary();
    public void GetMercenary(IStuff stuff);
    public void GiveToMercenary(IStuff stuff);
    public void KillMercenary(IMercenary mercenary);
    public void LostMercenary(IMercenary mercenary);
    
    public bool CanTakeStuff(IStuff? stuff);
    public bool CanHaveStuff(IStuff stuff);
    public void CheckStuffsForСompatibility();
    public void TakeStuff(IStuff? stuff);

    #region Lost Stuff

    public void LostMostPowerfulStuff();
    public void LostStuff(IStuff? stuff);
    public void LostAllStuffs();

    #endregion
    public void SellStuffs(List<IStuff?> stuffs);
    public void SellByDoublePrice(IStuff? stuff);
}