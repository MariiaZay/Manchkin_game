using ManchkinCore.CardEnums.Aspects;
using ManchkinCore.Enums.Accessory;
using ManchkinCore.Implementation;

namespace ManchkinCore.Interfaces;

public interface IManchkin
{
    #region Main properties

        public int Level { get; }
        public IRace Race { get; }
        public IClass Class { get; }
        public Genders Gender { get; set; }
        public int Damage { get; }
        public bool IsDead { get; }

    #endregion
    
    public void ChangeGender();
    public void RecalculateDamage();
    public void ToDie();
        

    #region Halfbood and SuperManchkin
    
        public IHulfblood Hulfblood { get; }
        public ISuperManchkin SuperManchkin { get; }
    
    #endregion

    #region Methods

        #region SuperManchkin

            public bool IsSuperManchkin();
            public void BecameSuperManchkin(IClass second);
            public void BecameSuperManchkin();

        #endregion

        #region HalfBlood

            public bool IsHalfBlood();
            public void BecameHalfBlood(IRace second);
            public void BecameHalfBlood();

        #endregion
        

    #endregion
    
    #region Additional properties

        public int FlushingBonus { get; }
        public int DebuffOnDiceRolls { get; }
        public List<IDescriptor> Descriptor { get; }

    #endregion

    #region Methods

        public void AddDescriptions(List<DescriptorFlags> args); 
        public void RemoveDescriptions(List<DescriptorFlags> args);

    #endregion
    
    public List<ICurse> ActiveCurses { get; }
    public void ApplyCurses();

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
