using ManchkinCore.CardEnums.Accessory;
using ManchkinCore.GameLogic.Interfaces.Manchkin;

namespace ManchkinCore.GameLogic.Interfaces.Stuff;

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
    public void KillMercenaries();
    
    public bool CanTakeStuff(IStuff? stuff);
    public bool CanHaveStuff(IStuff stuff);
    public void RemoveUnsuitableStuff();
    public bool CheckStuffBeforeChangingSuperManchkin();
    public bool CheckStuffBeforeChangingHalfblood();
    public bool CheckStuffBeforeChanging(IDescriptable descriptable);
    public bool CheckStuffBeforeChanging(Genders gender);
    public bool TakeStuff(IStuff? stuff);
    public bool TakeSingleWeaponRightHand(IStuff? stuff);
    public bool TakeSingleWeaponLeftHand(IStuff? stuff);
    

    #region Lost Stuff

    public void LostMostPowerfulStuff();
    public void LostStuff(IStuff? stuff);
    public void LostAllStuffs();

    #endregion
    public int SellStuffs(List<IStuff?> stuffs);
    public int SellByDoublePrice(IStuff? stuff);
}