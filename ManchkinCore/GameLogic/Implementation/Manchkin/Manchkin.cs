using System.Reflection.PortableExecutable;
using ManchkinCore.Enums;
using ManchkinCore.Enums.Accessory;
using ManchkinCore.Implementation;
using ManchkinCore.Interfaces;

namespace ManchkinCore.GameLogic.Implementation;

public class Manchkin : IManchkin
{
    #region Main fields

    public int Level { get; private set; }

    private IRace _race;

    public IRace Race
    {
        get => _race;
        private set
        {
            _race = ChangeRace(value);
            RecalculateParameters();
        }
    }

    private IClass _class;

    public IClass Class
    {
        get => _class;
        private set
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
    public bool IsDead { get; }

    #endregion

    #region Additional fields

    public int CardsCount { get; private set; }


    public int FlushingBonus { get; private set; }

    public bool DoublePrice { get; set; } //TODO: придумать,как обновлять перед каждым ходом

    public IHulfblood? HalfBlood { get; private set; }
    public ISuperManchkin? SuperManchkin { get; private set; }

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
        DoublePrice = false;

        Hands = new Hands();
        SmallStuffs = new List<IStuff>();
        HugeStuffs = new List<IStuff>();
        Mercenaries = new List<IMercenary>();
        Descriptions = new List<string>();

        HalfBlood = null;
        SuperManchkin = null;
    }


    #region Main methods

    public void ChangeGender()
    {
        Gender = Gender == Genders.FEMALE ? Genders.MALE : Genders.FEMALE;
        RecalculateParameters();
    }

    public IClass ChangeClass(IClass manClass)
    {
        LostDescriptions(Class.Descriptions);
        PurchaseDescriptions(manClass.Descriptions);
        return manClass;
    }

    private void RecalculateParameters()
    {
        CheckStuffsForСompatibility();
        RecalculateDamage();
        RecalculateFlushingBonus();
    }

    public IRace ChangeRace(IRace race)
    {
        FlushingBonus = race.FlushingBonus;
        CardsCount = race.CardCount;
        DoublePrice = race.CellingByDoublePrice;
        LostDescriptions(Race.Descriptions);
        PurchaseDescriptions(race.Descriptions);
        return race;
    }

    private int GetEquipmentDamage()
    {
        var equipment = new List<IStuff>() {WornHat, WornArmor, WornShoes};
        var damage = equipment.Where(eq => !IsNull(eq)).Sum(eq => eq.Damage);
        if (!IsNull(Hands.LeftHand))
        {
            damage += Hands.LeftHand.Damage;
            if (!IsNull(Hands.RightHand) && Hands.LeftHand != Hands.RightHand)
                damage += Hands.RightHand.Damage;
        }
        else
        {
            if (!IsNull(Hands.RightHand))
                damage += Hands.RightHand.Damage;
        }

        return damage;
    }

    private int GetEquipmentFlushingBonus()
    {
        var equipment = new List<IStuff>() {WornHat, WornArmor, WornShoes};
        var flushingBonus = equipment.Where(eq => !IsNull(eq)).Sum(eq => eq.FlushingBonus);
        ;

        if (!IsNull(Hands.LeftHand))
        {
            flushingBonus += Hands.LeftHand.FlushingBonus;
            if (!IsNull(Hands.RightHand) && Hands.LeftHand != Hands.RightHand)
                flushingBonus += Hands.RightHand.FlushingBonus;
        }
        else if (!IsNull(Hands.RightHand))
            flushingBonus += Hands.RightHand.FlushingBonus;

        return flushingBonus;
    }

    public void RecalculateDamage()
    {
        Damage = 0;
        Damage = Level + SmallStuffs.Sum(stuff => stuff.Damage) + HugeStuffs.Sum(stuff => stuff.Damage)
                 + GetEquipmentDamage();

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
                         HugeStuffs.Sum(stuff => stuff.FlushingBonus) +
                         GetEquipmentFlushingBonus();

        foreach (var mer in Mercenaries.Where(mer => mer.Item != null))
            FlushingBonus += mer.Item.FlushingBonus;
    }

    public void ToDie() => LostAllStuffs();
    public void GetLevel() => Level++;

    public void LostLevel()
    {
        if (Level > 1)
            Level--;
    }

    #endregion

    #region Own stuff

    public List<IStuff> SmallStuffs { get; }
    public List<IStuff> HugeStuffs { get; }

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
        Mercenaries.Add(new Mercenary());
        RecalculateParameters();
    }

    public void GetMercenary(IStuff stuff)
    {
        Mercenaries.Add(new Mercenary(stuff));
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

    public void UseCheat(IStuff stuff) => stuff.Cheat = true;
    public void CancelCheat(IStuff stuff) => stuff.Cheat = false;

    public bool CanTakeStuff(IStuff stuff)
    {
        CanHaveStuff(stuff);
        return Race is not Dwarf && !HasHugeStuff || Race is Dwarf
                                                  || IsHalfBlood && HalfBlood.HalfType == HalfTypes.BOTH
                                                                 && HalfBlood.SecondRace is Dwarf;
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
            }
            else
                additionalClassRight = false;
        }

        return mainRight || additionalRaceRight || additionalClassRight;
    }

    public void CheckStuffsForСompatibility()
    {
        if (!CanHaveStuff(WornHat))
            LostStuff(WornHat);

        if (!CanHaveStuff(WornArmor))
            LostStuff(WornArmor);

        if (!CanHaveStuff(WornShoes))
            LostStuff(WornShoes);

        if (Hands.LeftHand == Hands.RightHand)
            LostStuff(Hands.LeftHand);

        else
        {
            if (!CanHaveStuff(Hands.LeftHand))
                LostStuff(Hands.LeftHand);

            if (!CanHaveStuff(Hands.RightHand))
                LostStuff(Hands.RightHand);
        }

        foreach (var stuff in SmallStuffs.Where(stuff => !CanHaveStuff(stuff)))
            LostStuff(stuff);

        foreach (var stuff in HugeStuffs.Where(stuff => !CanHaveStuff(stuff)))
            LostStuff(stuff);
    }

    public string TakeStuff(IStuff stuff)
    {
        if (!CanTakeStuff(stuff)) return "невозможно взять шмотку";
        PurchaseDescriptions(stuff.Descriptions);
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
                if (stuff.Fullness == Arms.BOTH)
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
                if (stuff.Weight == Bulkiness.HUGE)
                    HugeStuffs.Add(stuff);
                else
                    SmallStuffs.Add(stuff);
                break;
            }
        }

        RecalculateParameters();
        return "шмотка успешно добавлена";
    }

    private bool IsNull(IStuff? stuff) => stuff == null;

    private IEnumerable<IStuff> GetAllWornStuffs()
    {
        var wornStuffs = new List<IStuff>();
        if (!IsNull(WornHat))
            wornStuffs.Add(WornHat);
        if (!IsNull(WornArmor))
            wornStuffs.Add(WornArmor);
        if (!IsNull(WornShoes))
            wornStuffs.Add(WornShoes);

        if (!IsNull(Hands.RightHand) && Hands.LeftHand.Fullness == Arms.BOTH)
            wornStuffs.Add(Hands.RightHand);
        else
        {
            if (!IsNull(Hands.LeftHand))
                wornStuffs.Add(Hands.LeftHand);
            if (!IsNull(Hands.RightHand))
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
        if (!IsNull(maxPowerStuff))
            LostStuff(maxPowerStuff);
    }

    public void LostStuff(IStuff stuff)
    {
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
                            Hands.TakeInRightHand(stuff);
                        break;
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
        foreach (var stuff in SmallStuffs)
            LostStuff(stuff);

        foreach (var stuff in HugeStuffs)
            LostStuff(stuff);

        LostStuff(WornArmor);
        LostStuff(WornHat);
        LostStuff(WornShoes);
        LostStuff(Hands.LeftHand);
        LostStuff(Hands.RightHand);
    }

    public void SellStuffs(List<IStuff> stuffs)
    {
        var price = 0;
        foreach (var stuff in stuffs)
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
        HalfBlood = new Halfblood(HalfTypes.BOTH, second);
        PurchaseDescriptions(second.Descriptions);
    }

    public void BecameHalfBlood()
        => HalfBlood = new Halfblood(HalfTypes.SINGLE_CLEAN);


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
        SuperManchkin = new SuperManchkin(HalfTypes.BOTH, second);
        PurchaseDescriptions(second.Descriptions);
    }

    public void BecameSuperManchkin()
        => SuperManchkin = new SuperManchkin(HalfTypes.SINGLE_CLEAN);

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