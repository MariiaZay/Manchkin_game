using ManchkinCore.CardEnums.Accessory;
using ManchkinCore.GameLogic.Interfaces.Accessory;
using ManchkinCore.GameLogic.Interfaces.Stuff;

namespace ManchkinCore.GameLogic.Interfaces.Manchkin;

public interface IManchkin : IOwnStuffs, IDescriptable, IHalfBloodAble, ISuper
{
    #region Main properties

        public int Level { get; }
        public IRace? Race { get; set; }
        public IClass? Class { get; set; }
        public Genders Gender { get;}
        public int Damage { get;}
        public int CardsCount { get; }
        public bool IsDead { get; set; }

        #endregion
    
    public void ChangeGender();
    public IRace? ChangeRace(IRace? race);
    public IClass? ChangeClass(IClass? manCLass);
    public void RecalculateDamage();
    public void RecalculateFlushingBonus();
    public void ToDie();
    public void GetLevel();
    public void GetLevel(int level);
    public void RecalculateParameters();
    public void LostLevel();
    
    #region Additional properties

        public int FlushingBonus { get; }

        public bool DoublePrice { get; set; }

        #endregion
}
