using System.Reflection.PortableExecutable;
using ManchkinCore.Enums.Accessory;
using ManchkinCore.Implementation;
using ManchkinCore.Implementation.Gears;
using ManchkinCore.Interfaces;

namespace ManchkinCore.GameLogic.Implementation;

public class Manchkin : IManchkin
{
    #region Main fields
        public int Level { get; private set; }
        //TODO: придумать, как добавлять и удалять текстовые св-ва
        //TODO: придумать, как обрабатывать смену/потерю рассы
        public IRace Race
        {
            get => Race;
            set
            {
                switch (value)
                {
                    case Elf:
                        FlushingBonus += 1;
                        break;
                    case Dwarf:
                        CardsCount = 6;
                        break;
                    case Halfling:
                        DoublePrice = true;
                        break;
                    case Human:
                        FlushingBonus = 0;
                        CardsCount = 5;
                        DoublePrice = false;
                        break;
                }
            }
        }

        public IClass Class { get; }
        public Genders Gender { get; private set; }
        public int Damage { get; private set; }
        
        public bool IsDead { get; }
    #endregion

    #region Additional fields
        public int CardsCount { get; private set; }
        public int FlushingBonus { get; private set; }
        public bool DoublePrice { get; set; }
        public int DebuffOnDiceRolls { get; }
        #endregion
    
        
    public Manchkin(Genders gender)
    {
        Level = 1;
        Race = new Human();
        Class = new Nobody();
        Gender = gender;
        Damage = Level;
        
        CardsCount = 5;
        IsDead = false;
        FlushingBonus = 0;
        DebuffOnDiceRolls = 0;
        DoublePrice = false;
        
        Hands = new Hands();
        SmallStuffs = new List<IStuff>();
        HugeStuffs = new List<IStuff>();
        Mercenaries = new List<IMercenary>();
        Cheat = false;
    }


    #region Main methods

        public void ChangeGender()
            => Gender = Gender == Genders.FEMALE ? Genders.MALE : Genders.FEMALE;


        public void RecalculateDamage()
        {
            Damage = Damage = Level + SmallStuffs.Sum(stuff => stuff.Damage) + HugeStuffs.Sum(stuff => stuff.Damage);
            foreach (var mer in Mercenaries)
            {
                Damage += 1;
                if (mer.Item != null)
                    Damage += mer.Item.Damage;
            }

        }


        public void ToDie() => LostAllStuffs();
        

    #endregion

    #region Own stuff

        public List<IStuff> SmallStuffs { get; }
        public List<IStuff> HugeStuffs { get; }
        
        public List<IMercenary> Mercenaries { get; }
        public IStuff? WornArmor { get; private set; }
        public IStuff? WornShoes { get; private set; }
        public IStuff? WornHat { get; private set; }
        public IHands Hands { get; }
        
        public bool Cheat { get; private set; }

    #endregion

    #region OwnStuff methods

        #region Mercenary methods

            public bool HasMercenary => Mercenaries.Count != 0;
            public void GetMercenary()
            {
                Mercenaries.Add(new Mercenary());
                Damage++;
            }

            public void GetMercenary(IStuff stuff)
            {
                Mercenaries.Add(new Mercenary(stuff));
                Damage++;
            }

            public void GiveToMercenary(IStuff stuff)
            {
                foreach (var mer in Mercenaries.Where(mer => mer.Item == null))
                {
                    mer.ChangeEquipment(stuff);
                    break;
                }
            }

            public void KillMercenary()
            {
                throw new NotImplementedException();
            }
            //TODO: придумать, как убиать наемничков

            

        #endregion

        #region Stuffs methods

            public bool HasHugeStuff=> HugeStuffs.Count != 0 ;
        
            public void ChangeCheat() => Cheat = !Cheat;
            
            public bool CanTakeStuff(IStuff stuff)
            {
                if (!stuff.CanBeUsed(Class) || !stuff.CanBeUsed(Race) || !stuff.CanBeUsed(Gender))
                    return false;
                if (Cheat)
                    return true;
                return Race is not Dwarf && !HasHugeStuff || Race is Dwarf;
            }

            public string TakeStuff(IStuff stuff)
            {
                if (!CanTakeStuff(stuff)) return "невозможно взять шмотку";
                switch (stuff)
                {
                    case Hat:
                        WornHat = stuff;
                        break;
                    case Armor:
                        WornArmor = stuff;
                        break;
                    case Shoes:
                        WornShoes = stuff;
                        break;
                    case Weapon:
                        if(stuff.Fullness == Arms.BOTH)
                            Hands.TakeInBothHands(stuff);
                        else
                        {
                            if (Hands.RightHand == null)
                                Hands.TakeInRightHand(stuff);
                            else
                                Hands.TakeInLeftHand(stuff);
                        }
                        break;
                    default:
                    {
                        if(stuff.Weight == Bulkiness.HUGE)
                            HugeStuffs.Add(stuff);
                        else
                            SmallStuffs.Add(stuff);
                        break;
                    }
                }
                return "шмотка успешно добавлена";
            }

            private bool IsNull(IStuff? stuff) => stuff == null;

            private List<IStuff> GetAllWornStuffs()
            {
                var wornStuffs = new List<IStuff>();
                if(!IsNull(WornHat))
                    wornStuffs.Add(WornHat);
                if(!IsNull(WornArmor))
                    wornStuffs.Add(WornArmor);
                if(!IsNull(WornShoes))
                    wornStuffs.Add(WornShoes);
                
                if(Hands.LeftHand == Hands.RightHand)
                    wornStuffs.Add(Hands.RightHand);
                else
                {
                    if(!IsNull(Hands.LeftHand))
                        wornStuffs.Add(Hands.LeftHand);
                    if(!IsNull(Hands.RightHand))
                        wornStuffs.Add(Hands.RightHand);
                }

                wornStuffs.AddRange(SmallStuffs.Where(stuff => !IsNull(stuff)));
                wornStuffs.AddRange(HugeStuffs.Where(stuff => !IsNull(stuff)));
                return wornStuffs;
            }

            public void LostMostPowerfulStuff()
            {
                var wornStuffs = GetAllWornStuffs();
                var maxPowerStuff = wornStuffs.MaxBy(stuff => stuff.Damage);
                if(!IsNull(maxPowerStuff))
                    LostStuff(maxPowerStuff);
            }

            public void LostStuff(IStuff stuff)
            {
                switch (stuff)
                {
                    case Hat:
                        WornHat = null;
                        break;
                    case Armor:
                        WornArmor = null;
                        break;
                    case Shoes:
                        WornShoes = null;
                        //TODO: доработать ботинк с текстовыми полями
                        break;
                    case Weapon:
                        switch (stuff.Fullness)
                        {
                            case Arms.BOTH:
                                Hands.TakeInBothHands(null);
                                break;

                            case Arms.SINGLE:
                                if (Hands.LeftHand == stuff)
                                    Hands.TakeInLeftHand(null);
                                else if (Hands.RightHand == stuff)
                                    Hands.TakeInRightHand(stuff);
                                break;
                            case Arms.NO:
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                        break;
                    default:
                        if (stuff.Weight == Bulkiness.HUGE)
                            HugeStuffs.Remove(stuff);
                        else
                            SmallStuffs.Remove(stuff);
                        break;
                }
            }

            public void LostAllStuffs()
            {
                SmallStuffs.Clear();
                HugeStuffs.Clear();
                
                WornArmor = null;
                WornHat = null;
                WornShoes = null;
                Hands.TakeInBothHands(null);
            }

            public void SellStuffs(List<IStuff> stuffs)
            {
                var price = 0;
                foreach (var stuff  in stuffs)
                {
                    price += stuff.Price;
                    LostStuff(stuff);
                }

                Level += price / 1000;
            }

            public void SellByDoublePrice(IStuff stuff)
            {
                Level += stuff.Price * 2 / 1000;
                LostStuff(stuff);
            }

        #endregion
        
    #endregion
    

    //TODO:дописать
}