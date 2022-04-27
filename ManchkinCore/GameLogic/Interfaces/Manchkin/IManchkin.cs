using ManchkinCore.CardEnums.Aspects;
using ManchkinCore.Enums.Accessory;
using ManchkinCore.Implementation;

namespace ManchkinCore.Interfaces;

public interface IManchkin : IOwnStuffs /*, IСurseable, IHalfBloodAble, ISuper, IDescriptable*/
{
    #region Main properties

        public int Level { get; }
        public IRace Race { get; }
        public IClass Class { get; }
        public Genders Gender { get;}
        public int Damage { get;}
        public int CardsCount { get; }
        public bool IsDead { get; }

    #endregion
    
    public void ChangeGender();
    public void RecalculateDamage();
    public void ToDie();
   
    
    #region Additional properties

        public int FlushingBonus { get; }
        public int DebuffOnDiceRolls { get; }

        public bool DoublePrice { get; }

        #endregion
}
