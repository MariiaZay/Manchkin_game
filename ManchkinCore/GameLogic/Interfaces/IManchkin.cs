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

    #region Methods

        #region Race

            public void ChangeRace(IRace race);
            public void LostRace();

        #endregion

        #region Class
        
            public void ChangeClass(IClass _class);
            public void LostClass();

        #endregion

        #region Level
        
            public void LostLevel();
            public void GetLevel();
            
        #endregion
        public void ChangeGender();
        public void RecalculateDamage();
        public void ToDie();

    #endregion

    #region Hulfbood and SuperManchkin
    
        public IHulfblood Hulfblood { get; }
        public ISuperManchkin SuperManchkin { get; }
    
    #endregion

    #region Methods

        #region SuperManchkin

            public bool IsSuperManchkin();
            public void BecameSuperManchkin(IClass first, IClass second);
            public void BecameSuperManchkin(IClass first);

        #endregion

        #region HalfBlood

            public bool IsHalfBlood();
            public void BecameHalfBlood(IRace first, IRace second);
            public void BecameHalfBlood(IRace first);

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

    #region Methods

    public bool HasHugeStuff();
    public bool HasMercenary();
    
    public bool CanTakeStuff(IStuff stuff);
    public void TakeStuff(IStuff stuff);
    public void ChangeEquipment(List<IStuff> equipment);

    #region Lost Stuff

        public void LostHugeStuff(IStuff stuff);
        public void LostSmallStuff(IStuff stuff);
        public void LostMostPowerfulStuff();
        public void LostStuff(IStuff stuff);
        public void LostAllStuffs();

    #endregion

    #region Lost Equipment

        public void LostArmor();
        public void LostShoes();
        public void LostHat();

    #endregion
    
    public int SellStuffs(List<IStuff> stuffs);

    #endregion
}
