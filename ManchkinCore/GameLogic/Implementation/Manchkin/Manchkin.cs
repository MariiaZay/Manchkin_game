using ManchkinCore.Enums;
using ManchkinCore.Enums.Accessory;
using ManchkinCore.GameLogic.Implementation.Factories;
using ManchkinCore.Implementation;
using ManchkinCore.Interfaces;

namespace ManchkinCore.GameLogic.Implementation;

public class Manchkin : IManchkin
{
        #region Main fields

    public int Level { get; private set; }

    private IRace? _race;

    public IRace? Race
    {
        get => _race;
        set
        {
            _race = ChangeRace(value);
            RecalculateParameters();
        }
    }

    private IClass? _class;

    public IClass? Class
    {
        get => _class;
        set
        {
            _class = ChangeClass(value);
            RecalculateParameters();
        }
    }

    private Genders _gender;

    public Genders Gender
    {
        get => _gender;
        private set
        {
            _gender = value;
            RecalculateParameters();
        }
    }

    public int Damage { get; private set; }
    public List<string> Descriptions { get; }
    public string TextRepresentation { get; }
    public bool IsDead { get; set; }

    #endregion

    #region Additional fields

    public int CardsCount { get; private set; }


    public int FlushingBonus { get; private set; }

    public bool DoublePrice { get; set; } //TODO: придумать,как обновлять перед каждым ходом

    public IHulfblood? HalfBlood { get; private set; }
    public ISuperManchkin? SuperManchkin { get; private set; }

    #endregion
    
    #region Factories

    private MercenaryFactory _mercenaryFactory;
    private HalfbloodFactory _halfbloodFactory;
    private SuperManchkinFactory _superManchkinFactory;

    #endregion

    public Manchkin(
        IClass initialClass, 
        IRace initialRace,
        IHands hands,
        MercenaryFactory mercenaryFactory,
        HalfbloodFactory halfbloodFactory,
        SuperManchkinFactory superManchkinFactory,
        Genders gender)
    {
        _mercenaryFactory = mercenaryFactory;
        _halfbloodFactory = halfbloodFactory;
        _superManchkinFactory = superManchkinFactory;
        
        Level = 1;

        CardsCount = 5;
        IsDead = false;
        FlushingBonus = 0;
        DoublePrice = false;

        Hands = hands;
        WornArmor = null;
        WornShoes = null;
        WornHat = null;

        SmallStuffs = new List<IStuff?>();
        HugeStuffs = new List<IStuff?>();
        Mercenaries = new List<IMercenary>();

        Descriptions = new List<string>();

        HalfBlood = null;
        SuperManchkin = null;

        Race = initialRace;
        Class = initialClass;
        Gender = gender;
        Damage = Level;
        TextRepresentation = "Манчкин";
    }


    #region Main methods

    public void ChangeGender()
    {
        Gender = Gender == Genders.FEMALE ? Genders.MALE : Genders.FEMALE;
        RecalculateParameters();
    }

    public IClass? ChangeClass(IClass? manClass)
    {
        if (IsNull(_class)) return manClass;

        if(IsSuperManchkin)
            RefuseSuperManchkin();
        LostDescriptions(Class.Descriptions);
        PurchaseDescriptions(manClass.Descriptions);

        return manClass;
    }

    public void RecalculateParameters()
    {
        RemoveUnsuitableStuff();
        RecalculateDamage();
        RecalculateFlushingBonus();
    }

    public IRace? ChangeRace(IRace? race)
    {
        FlushingBonus = race.FlushingBonus;
        CardsCount = race.CardCount;
        DoublePrice = race.CellingByDoublePrice;

        if (IsNull(Race)) return race;
        
        if(IsHalfBlood) RefuseHalfblood();
        LostDescriptions(Race.Descriptions);
        PurchaseDescriptions(race.Descriptions);

        return race;
    }


    public void RecalculateDamage()
    {
        Damage = 0;
        Damage = Level + SmallStuffs.Sum(stuff => stuff.Damage) + HugeStuffs.Sum(stuff => stuff.Damage);

        foreach (var mer in Mercenaries)
        {
            Damage += 1;
            if (mer.Item != null)
                Damage += mer.Item.Damage;
        }
    }

    public void RecalculateFlushingBonus()
    {
        FlushingBonus = 0;
        if (Race is Elf)
            FlushingBonus = 1;

        FlushingBonus += SmallStuffs.Sum(stuff => stuff.FlushingBonus) +
                         HugeStuffs.Sum(stuff => stuff.FlushingBonus);

        foreach (var mer in Mercenaries.Where(mer => mer.Item != null))
            FlushingBonus += mer.Item.FlushingBonus;
    }

    public void ToDie()
    {
        LostAllStuffs();
        RecalculateParameters();
    }

    public void GetLevel()
    {
        if (Level < 10) Level++;
        RecalculateDamage();
    }


    public void LostLevel()
    {
        if (Level > 1)
            Level--;
        RecalculateDamage();
    }

    #endregion

    #region Own stuff

    public List<IStuff?> SmallStuffs { get; }
    public List<IStuff?> HugeStuffs { get; }

    public List<IMercenary> Mercenaries { get; }
    public IStuff? WornArmor { get; private set; }
    public IStuff? WornShoes { get; private set; }
    public IStuff? WornHat { get; private set; }
    public IHands Hands { get; }

    #endregion

    #region OwnStuff methods

    #region Mercenary methods

    public bool HasMercenary => Mercenaries.Count != 0;

    public void GetMercenary()
    {
        var mercenary = _mercenaryFactory.ResetStuff().Build();
        Mercenaries.Add(mercenary);
        RecalculateParameters();
    }

    public void GetMercenary(IStuff stuff)
    {
        var mercenary = _mercenaryFactory.SetStuff(stuff).Build();
        Mercenaries.Add(mercenary);
        RecalculateParameters();
    }

    public void GiveToMercenary(IStuff stuff)
    {
        foreach (var mer in Mercenaries.Where(mer => mer.Item == null))
        {
            mer.ChangeEquipment(stuff);
            break;
        }

        RecalculateParameters();
    }

    public void KillMercenary(IMercenary mercenary)
    {
        LostMercenary(mercenary);
        Level++;
    }

    public void LostMercenary(IMercenary mercenary)
    {
        Mercenaries.Remove(mercenary);
        RecalculateParameters();
    }

    #endregion

    #region Stuffs methods

    public bool HasHugeStuff => HugeStuffs.Count != 0;

    public void UseCheat(IStuff? stuff) => stuff.Cheat = true;
    public void CancelCheat(IStuff? stuff) => stuff.Cheat = false;

    public bool CanTakeStuff(IStuff? stuff)
    {
        
        var can = CanHaveStuff(stuff);
        if (!can) return can;
        if (stuff.Weight == Bulkiness.HUGE)
            return Race is not Dwarf && !HasHugeStuff || Race is Dwarf
                                                      || stuff.Cheat
                                                      || IsHalfBlood && HalfBlood.HalfType == HalfTypes.BOTH
                                                                     && HalfBlood.SecondRace is Dwarf;
        return can;
    }

    public bool CanHaveStuff(IStuff? stuff)
    {
        bool mainRight;
        bool additionalRaceRight;
        var additionalClassRight = false;


        if (IsNull(stuff))
            mainRight = additionalRaceRight = additionalClassRight = true;
        else
        {
            if (stuff.Cheat)
                mainRight = true;
            else
                mainRight = stuff.CanBeUsed(Class) && stuff.CanBeUsed(Race) && stuff.CanBeUsed(Gender);

            if (IsHalfBlood)
            {
                if (HalfBlood.HalfType == HalfTypes.BOTH)
                    additionalRaceRight = stuff.CanBeUsed(Class) && stuff.CanBeUsed(HalfBlood.SecondRace)
                                                                 && stuff.CanBeUsed(Gender);
                else
                    additionalRaceRight = stuff.CanBeUsed(Class) && stuff.CanBeUsed(Gender);
            }
            else
                additionalRaceRight = false;

            if (IsSuperManchkin)
            {
                if (SuperManchkin.HalfType == HalfTypes.BOTH)
                    additionalRaceRight = stuff.CanBeUsed(SuperManchkin.SecondClass) && stuff.CanBeUsed(Race)
                        && stuff.CanBeUsed(Gender);
                else
                    additionalRaceRight = stuff.CanBeUsed(Race) && stuff.CanBeUsed(Gender);
                //TODO: переделать это место
            }
            else
                additionalClassRight = false;
        }

        return mainRight || additionalRaceRight || additionalClassRight;
    }

    //TODO:возможно, тут тоже нуно будет переписать
    public bool CheckStuffBeforeChanging(IDescriptable? descriptable)
    {
        var stuff = GetAllWornStuffs();
        return descriptable is IClass
            ? stuff.All(s => CanHaveStuff(s, descriptable as IClass, Race, Gender))
            : stuff.All(s => CanHaveStuff(s, Class, descriptable as IRace, Gender));
    }

    public bool CheckStuffBeforeChangingHalfblood()
    {
        var last = HalfBlood.SecondRace;
        RefuseHalfblood();
        var stuff = GetAllWornStuffs();
        var ok = stuff.All(s => CanHaveStuff(s, Class, Race, Gender));
        if(last == null)
            BecameHalfBlood();
        else
            BecameHalfBlood(last);

        return ok;
    }

    public bool CheckStuffBeforeChangingSuperManchkin()
    {
        var last = SuperManchkin.SecondClass;
        RefuseSuperManchkin();
        var stuff = GetAllWornStuffs();
        var ok = stuff.All(s => CanHaveStuff(s, Class, Race, Gender));
        if(last == null)
            BecameSuperManchkin();
        else
            BecameSuperManchkin(last);
        return ok;
    }
    
    public bool CheckStuffBeforeChanging(Genders gender)
    {
        var stuff = GetAllWornStuffs();
        return stuff.All(s => CanHaveStuff(s, Class, Race, gender));
    }

    private bool CanHaveStuff(IStuff? stuff, IClass? cl, IRace? race, Genders gender)
    {
        bool mainRight;
        bool additionalRaceRight;
        var additionalClassRight = false;


        if (IsNull(stuff))
            mainRight = additionalRaceRight = additionalClassRight = true;
        else
        {
            if (stuff.Cheat)
                mainRight = true;
            else
                mainRight = stuff.CanBeUsed(cl) && stuff.CanBeUsed(race) && stuff.CanBeUsed(gender);

            if (IsHalfBlood)
            {
                if (HalfBlood.HalfType == HalfTypes.BOTH)
                    additionalRaceRight = stuff.CanBeUsed(Class) && stuff.CanBeUsed(HalfBlood.SecondRace)
                                                                 && stuff.CanBeUsed(Gender);
                else
                    additionalRaceRight = stuff.CanBeUsed(cl) && stuff.CanBeUsed(gender);
            }
            else
                additionalRaceRight = false;

            if (IsSuperManchkin)
            {
                if (SuperManchkin.HalfType == HalfTypes.BOTH)
                    additionalRaceRight = stuff.CanBeUsed(SuperManchkin.SecondClass) && stuff.CanBeUsed(race)
                        && stuff.CanBeUsed(gender);
                else
                    additionalRaceRight = stuff.CanBeUsed(Race) && stuff.CanBeUsed(gender);
            }
            else
                additionalClassRight = false;
        }

        return mainRight || additionalRaceRight || additionalClassRight;
    }

    public void RemoveUnsuitableStuff()
    {
        //TODO: возможно, придетя переписать
        var stuff = GetAllWornStuffs();
        while(stuff.Count() != 0)
        {
            var s = stuff.Last();
            if (!CanHaveStuff(s))
            {
                LostStuff(s);
            }
            stuff.Remove(s);
        }
    }

    private bool IsNull(object? ob) => ob == null;


    //TODO: придумать, как вынести повторяющиеся куски в отдельный метод
    //TODO: проверить, что это стабильно и правильно работает

    private void ReturnStuff(IStuff? stuff)
    {
        if (IsNull(stuff)) return;
        PurchaseDescriptions(stuff.Descriptions);
        if (stuff.Weight == Bulkiness.HUGE)
            HugeStuffs.Add(stuff);
        else
            SmallStuffs.Add(stuff);
    }

    private void AddStuff(IStuff? stuff)
    {
        if(IsNull(stuff)) return;
        PurchaseDescriptions(stuff.Descriptions);
        if (stuff.Weight == Bulkiness.HUGE)
            HugeStuffs.Add(stuff);
        else
            SmallStuffs.Add(stuff);
    }


    public bool TakeStuff(IStuff? stuff) //это говнокод, но оно работает
    {
        IStuff? st;
        var ok = false;
        switch (stuff)
        {
            case Hat:

                st = WornHat; //TODO: вынести в отдельный метод
                LostStuff(WornHat);

                if (!CanTakeStuff(stuff))
                {
                    WornHat = st;
                    ReturnStuff(st);
                    ok = false;
                }
                else
                {
                    WornHat = stuff;
                    AddStuff(stuff);
                    ok = true;
                }

                break;


            case Armor:

                st = WornArmor;
                LostStuff(WornArmor);


                if (!CanTakeStuff(stuff))
                {
                    WornArmor = st;
                    ReturnStuff(st);
                    ok = false;
                }
                else
                {
                    WornArmor = stuff;
                    AddStuff(stuff);
                    ok = true;
                }

                break;


            case Shoes:

                st = WornShoes;
                LostStuff(WornShoes);


                if (!CanTakeStuff(stuff))
                {
                    WornShoes = st;
                    ReturnStuff(st);
                    ok = false;
                }
                else
                {
                    WornShoes = stuff;
                    AddStuff(stuff);
                    ok = true;
                }

                break;

            case Weapon: //TODO: придумать, как переписать эту хуйню
                IStuff? right = null;
                if (!IsNull(Hands.LeftHand) && Hands.LeftHand.Fullness == Arms.BOTH)
                {
                    st = Hands.LeftHand;
                    LostStuff(Hands.LeftHand);
                }
                else
                {
                    if (!IsNull(Hands.LeftHand))
                    {
                        st = Hands.LeftHand;
                        LostStuff(Hands.LeftHand);
                    }
                    else
                        st = null;

                    if (!IsNull(Hands.RightHand))
                    {
                        right = Hands.RightHand;
                        LostStuff(Hands.RightHand);
                    }
                    else
                        right = null;
                }

                if (!CanTakeStuff(stuff))
                {
                    if (!IsNull(st) && st.Fullness == Arms.BOTH)
                    {
                        Hands.TakeInBothHands(st);
                        ReturnStuff(st);
                    }


                    else
                    {
                        Hands.TakeInLeftHand(st);
                        if (!IsNull(st))
                            ReturnStuff(st);
                        Hands.TakeInRightHand(right);
                        if (!IsNull(right))
                            ReturnStuff(right);
                    }
                    ok = false;
                }
                else
                {
                    Hands.TakeInBothHands(stuff);
                    AddStuff(stuff);
                    ok = true;
                }

                break;

            default:
                if (CanTakeStuff(stuff))
                {
                    PurchaseDescriptions(stuff.Descriptions);
                    if (stuff.Weight == Bulkiness.HUGE)
                        HugeStuffs.Add(stuff);
                    else
                        SmallStuffs.Add(stuff);
                    ok = true;
                }
                else
                    ok = false;

                break;
        }

        RecalculateDamage();
        RecalculateFlushingBonus();
        return ok;
    }
    
    public bool TakeSingleWeaponRightHand(IStuff? stuff)
    {
        IStuff? last;
        bool ok;
        
        last = Hands.RightHand;
        LostStuff(Hands.RightHand);
        
        if (!CanTakeStuff(stuff))
        {
            Hands.TakeInRightHand(last);
            ReturnStuff(last);
            ok = false;
        }
        else
        {
            Hands.TakeInRightHand(stuff);
            AddStuff(stuff);
            ok = true;
            RecalculateDamage();
            RecalculateFlushingBonus();
        }
        
        return ok;
    }
    
    public bool TakeSingleWeaponLeftHand(IStuff? stuff)
    {
        IStuff? last;
        bool ok;
        
        last = Hands.LeftHand;
        LostStuff(Hands.LeftHand);
        
        if (!CanTakeStuff(stuff))
        {
            Hands.TakeInLeftHand(last);
            ReturnStuff(last);
            ok = false;
        }
        else
        {
            Hands.TakeInLeftHand(stuff);
            AddStuff(stuff);
            ok = true;
        }
        
        RecalculateDamage();
        RecalculateFlushingBonus();
        return ok;
    }

    private bool IsNull(IStuff? stuff) => stuff == null;

    private List<IStuff> GetAllWornStuffs()
    {
        var wornStuffs = SmallStuffs.ToList();
        wornStuffs.AddRange(HugeStuffs);
        return wornStuffs;
    }

    public void LostMostPowerfulStuff()
    {
        var wornStuffs = GetAllWornStuffs();
        var maxPowerStuff = wornStuffs.MaxBy(stuff => stuff.Damage);
        if (!IsNull(maxPowerStuff))
            LostStuff(maxPowerStuff);
    }

    public void LostStuff(IStuff? stuff)
    {
        if (IsNull(stuff)) return;
        CancelCheat(stuff);
        LostDescriptions(stuff.Descriptions);
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
                            Hands.TakeInRightHand(null);
                        break;
                }

                break;
        }

        if (stuff.Weight == Bulkiness.HUGE)
            HugeStuffs.Remove(stuff);
        else
            SmallStuffs.Remove(stuff);
        RecalculateDamage();
        RecalculateFlushingBonus();
    }

    public void LostAllStuffs()
    {
        while (SmallStuffs.Count != 0)
        {
            var stuff = SmallStuffs.Last();
            LostStuff(stuff);
        }

        while (HugeStuffs.Count != 0)
        {
            var stuff = HugeStuffs.Last();
            LostStuff(stuff);
        }
    }

    public void SellStuffs(List<IStuff?> stuffs)
    {
        var price = 0;
        foreach (var stuff in stuffs)
        {
            price += stuff.Price;
            LostStuff(stuff);
        }

        Level += price / 1000;
    }

    public void SellByDoublePrice(IStuff? stuff)
    {
        Level += stuff.Price * 2 / 1000;
        LostStuff(stuff);
    }

    #endregion

    #endregion

    #region Description Methods

    private void LostDescriptions(List<string> descriptions)
    {
        foreach (var desc in descriptions)
            Descriptions.Remove(desc);
    }

    private void PurchaseDescriptions(List<string> descriptions)
    {
        foreach (var desc in descriptions)
            Descriptions.Add(desc);
    }

    #endregion

    #region Halfblood region

    public bool IsHalfBlood => HalfBlood != null;

    public void BecameHalfBlood(IRace second)
    {
        HalfBlood = _halfbloodFactory
            .SetSecondRace(second)
            .Build();
        PurchaseDescriptions(second.Descriptions);
    }

    public void BecameHalfBlood() => HalfBlood = _halfbloodFactory
        .ResetSecondRace()
        .Build();


    public void RefuseHalfblood()
    {
        if (HalfBlood == null) return;
        if (HalfBlood.SecondRace != null)
            LostDescriptions(HalfBlood.SecondRace.Descriptions);
        RecalculateParameters();
        HalfBlood = null;
    }

    #endregion

    #region SuperManchkin region

    public bool IsSuperManchkin => SuperManchkin != null;

    public void BecameSuperManchkin(IClass second)
    {
        SuperManchkin = _superManchkinFactory
            .SetSecondClass(second)
            .Build();
        PurchaseDescriptions(second.Descriptions);
    }

    public void BecameSuperManchkin() => SuperManchkin = _superManchkinFactory
        .ResetSecondClass()
        .Build();

    public void RefuseSuperManchkin()
    {
        if (SuperManchkin == null) return;
        if (SuperManchkin.SecondClass != null)
            LostDescriptions(SuperManchkin.SecondClass.Descriptions);
        RecalculateParameters();
        SuperManchkin = null;
    }

    #endregion
}