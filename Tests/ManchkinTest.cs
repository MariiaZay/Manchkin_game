using ManchkinCore.Enums;
using ManchkinCore.Enums.Accessory;
using ManchkinCore.GameLogic.Implementation;
using ManchkinCore.Implementation;
using ManchkinCore.Interfaces;
using NUnit.Framework;

namespace Tests;

[TestFixture]
public class ManchkinTest
{
    private IManchkin _manchkin = null!;

    [SetUp]
    public void SetUp()
    {
        _manchkin = new Manchkin(Genders.MALE);
    }

    [Test]
    public void Manchkin_Create_PropertiesValuesAreAdequate()
    {
        Assert.Multiple(() =>
        {
            Assert.That(_manchkin.Gender, Is.EqualTo(Genders.MALE));
            Assert.That(_manchkin.Level, Is.EqualTo(1));
        });
        Assert.Multiple(() =>
        {
            Assert.That(_manchkin.Level, Is.EqualTo(_manchkin.Damage));
            Assert.That(_manchkin.IsDead, Is.EqualTo(false));
            Assert.That(_manchkin.IsHalfBlood, Is.EqualTo(false));
            Assert.That(_manchkin.IsSuperManchkin, Is.EqualTo(false));
            Assert.That(_manchkin.Race, Is.InstanceOf<Human>());
        });
        Assert.Multiple(() =>
        {
            Assert.That(_manchkin.Mercenaries, Is.Empty);
            Assert.That(_manchkin.CardsCount, Is.EqualTo(5));
            Assert.That(_manchkin.WornHat, Is.Null);
            Assert.That(_manchkin.WornArmor, Is.Null);
            Assert.That(_manchkin.WornShoes, Is.Null);
            Assert.That(_manchkin.Hands.LeftHand, Is.Null);
            Assert.That(_manchkin.Hands.RightHand, Is.Null);
        });
        Assert.Multiple(() =>
        {
            Assert.That(_manchkin.Descriptions, Is.Empty);
            Assert.That(_manchkin.HasMercenary, Is.EqualTo(false));
        });
    }

    [Test]
    public void Manchkin_GetLevel_IncreasesLevel()
    {
        var originLevel = _manchkin.Level;

        _manchkin.GetLevel();

        Assert.That(_manchkin.Level, Is.GreaterThan(originLevel));
    }

    [Test]
    public void Manchkin_GetLevel_IncreasesDamage()
    {
        var originDamage = _manchkin.Damage;

        _manchkin.GetLevel();

        Assert.That(_manchkin.Damage, Is.GreaterThan(originDamage));
    }

    [Test]
    public void Manchkin_GetLevel_HasUpperBound10()
    {
        for (var _ = 0; _ < 10; _++)
            _manchkin.GetLevel();
        var originLevel = _manchkin.Level;

        _manchkin.GetLevel();

        Assert.That(_manchkin.Level, Is.EqualTo(originLevel));
    }

    [Test]
    public void Manchkin_LostLevel_DecreasesLevel()
    {
        _manchkin.GetLevel();
        var originLevel = _manchkin.Level;
        _manchkin.LostLevel();

        Assert.That(_manchkin.Level, Is.LessThan(originLevel));
    }

    [Test]
    public void Manchkin_GetLevel_DecreasesDamage()
    {
        _manchkin.GetLevel();
        var originDamage = _manchkin.Damage;
        _manchkin.LostLevel();

        Assert.That(_manchkin.Damage, Is.LessThan(originDamage));
    }

    [Test]
    public void Manchkin_GetLevel_HasLowerBound1()
    {
        Assert.That(_manchkin.Level, Is.EqualTo(1));

        _manchkin.LostLevel();

        Assert.That(_manchkin.Level, Is.EqualTo(1));
    }

    [Test]
    public void Manchkin_BecomeSuperManchkin_Became1()
    {
        _manchkin.BecameSuperManchkin();

        Assert.Multiple(() =>
        {
            Assert.That(_manchkin.IsSuperManchkin, Is.True);
            Assert.That(_manchkin.SuperManchkin.HalfType, Is.EqualTo(HalfTypes.SINGLE_CLEAN));
            Assert.That(_manchkin.SuperManchkin.SecondClass, Is.Null);
        });
    }

    [Test]
    public void Manchkin_BecomeSuperManchkin_Became2()
    {
        var desc = _manchkin.Descriptions;
        var thief = new Thief();
        desc.AddRange(thief.Descriptions);

        _manchkin.BecameSuperManchkin(thief);

        Assert.Multiple(() =>
        {
            Assert.That(_manchkin.IsSuperManchkin, Is.True);
            Assert.That(_manchkin.SuperManchkin.HalfType, Is.EqualTo(HalfTypes.BOTH));
            Assert.That(_manchkin.SuperManchkin.SecondClass, Is.InstanceOf<Thief>());
            Assert.That(_manchkin.Descriptions, Is.EqualTo(desc));
        });
    }

    [Test]
    public void Manchkin_RefuseSuperManchkin_Refuse1()
    {
        _manchkin.BecameSuperManchkin();
        _manchkin.RefuseSuperManchkin();

        Assert.That(_manchkin.IsSuperManchkin, Is.False);
    }

    [Test]
    public void Manchkin_RefuseSuperManchkin_Refuse2()
    {
        var manchkinDesc = _manchkin.Descriptions.ToArray();
        var thief = new Thief();

        _manchkin.BecameSuperManchkin(thief);
        _manchkin.RefuseSuperManchkin();

        Assert.Multiple(() =>
        {
            Assert.That(_manchkin.IsSuperManchkin, Is.False);
            Assert.That(_manchkin.Descriptions, Is.EqualTo(manchkinDesc.ToList()));
        });
    }

    [Test]
    public void Manchkin_RefuseSuperManchkin_Refuse3()
    {
        Assert.That(_manchkin.IsSuperManchkin, Is.False);

        _manchkin.RefuseSuperManchkin();

        Assert.That(_manchkin.IsSuperManchkin, Is.False);
    }

    [Test]
    public void Manchkin_BecomeHalfblood_Became()
    {
        Assert.That(_manchkin.IsHalfBlood, Is.False);

        _manchkin.BecameHalfBlood();

        Assert.Multiple(() =>
        {
            Assert.That(_manchkin.IsHalfBlood, Is.True);
            Assert.That(_manchkin.HalfBlood?.HalfType, Is.EqualTo(HalfTypes.SINGLE_CLEAN));
            Assert.That(_manchkin.HalfBlood?.SecondRace, Is.Null);
        });
    }

    [Test]
    public void Manchkin_BecomeHalfblood_Became2()
    {
        var secondRace = new Dwarf();

        Assert.That(_manchkin.IsHalfBlood, Is.False);

        _manchkin.BecameHalfBlood(secondRace);

        Assert.Multiple(() =>
        {
            Assert.That(_manchkin.IsHalfBlood, Is.True);
            Assert.That(_manchkin.HalfBlood?.HalfType, Is.EqualTo(HalfTypes.BOTH));
            Assert.That(_manchkin.HalfBlood?.SecondRace, Is.EqualTo(secondRace));
        });
    }

    [Test]
    public void Manchkin_RefuseHalfblood_Works1()
    {
        Assert.That(_manchkin.IsHalfBlood, Is.False);

        _manchkin.RefuseHalfblood();

        Assert.That(_manchkin.IsHalfBlood, Is.False);
    }

    [Test]
    public void Manchkin_RefuseHalfblood_Works2()
    {
        _manchkin.BecameHalfBlood();
        _manchkin.RefuseHalfblood();

        Assert.That(_manchkin.IsHalfBlood, Is.False);
    }

    [Test]
    public void Manchkin_RefuseHalfblood_LostDescriptions()
    {
        var manchkinDesc = _manchkin.Descriptions;
        var secondRace = new Dwarf();

        _manchkin.BecameHalfBlood(secondRace);
        _manchkin.RefuseHalfblood();

        Assert.That(_manchkin.Descriptions, Is.EqualTo(manchkinDesc));
    }

    [Test]
    public void Manchkin_GetMercenary_GetMercenaryTrue()
    {
        _manchkin.GetMercenary();

        Assert.That(_manchkin.HasMercenary, Is.True);
    }

    [Test]
    public void Manchkin_ChangeGender_Works()
    {
        var manchkinMale = new Manchkin(Genders.MALE);
        var manchkinFemale = new Manchkin(Genders.FEMALE);

        manchkinMale.ChangeGender();
        manchkinFemale.ChangeGender();

        Assert.Multiple(() =>
        {
            Assert.That(manchkinMale.Gender, Is.EqualTo(Genders.FEMALE));
            Assert.That(manchkinFemale.Gender, Is.EqualTo(Genders.MALE));
        });
    }

    [Test]
    public void Manchkin_ChangeRace_Works()
    {
        var elf = new Elf();
        var human = new Human();

        Assert.Multiple(() =>
        {
            Assert.That(_manchkin.Race, Is.InstanceOf<Human>());
            Assert.That(_manchkin.Descriptions, Is.EqualTo(human.Descriptions));
        });

        _manchkin.Race = elf;

        Assert.Multiple(() =>
        {
            Assert.That(_manchkin.Race, Is.InstanceOf<Elf>());
            Assert.That(_manchkin.Descriptions, Is.EqualTo(elf.Descriptions));
        });
    }

    [Test]
    public void Manchkin_ChangeClass_Works()
    {
        var thief = new Thief();
        var nobody = new Nobody();

        Assert.Multiple(() =>
        {
            Assert.That(_manchkin.Class, Is.InstanceOf<Nobody>());
            Assert.That(_manchkin.Descriptions, Is.EqualTo(nobody.Descriptions));
        });

        _manchkin.Class = thief;

        Assert.Multiple(() =>
        {
            Assert.That(_manchkin.Class, Is.InstanceOf<Thief>());
            Assert.That(_manchkin.Descriptions, Is.EqualTo(thief.Descriptions));
        });
    }

    [Test]
    public void Manchkin_ToDie_HeIsDead()
    {
        Assert.That(_manchkin.IsDead, Is.False);

        _manchkin.ToDie();

        Assert.That(_manchkin.IsDead, Is.True);
    }

    [Test]
    public void Manchkin_ToDie_LosesAllStuff()
    {
        for (var _ = 0; _ < 9; _++)
            _manchkin.GetLevel();
        _manchkin.TakeStuff(new HelmetOfCourage());
        _manchkin.TakeStuff(new LeatherArmor());
        _manchkin.TakeStuff(new MightyShoes());

        Assert.Multiple(() =>
        {
            Assert.That(_manchkin.WornHat, Is.Not.Null);
            Assert.That(_manchkin.WornArmor, Is.Not.Null);
            Assert.That(_manchkin.WornShoes, Is.Not.Null);
        });

        _manchkin.ToDie();

        CheckThatManchkinDoesntHaveAnyStuffs();
    }

    [Test]
    public void Manchkin_SellStuffs_Works()
    {
        var stuffs = new List<IStuff?> { new HelmetOfCourage(), new LeatherArmor(), new MightyShoes() };
        var expectedLevel = _manchkin.Level + stuffs.Select(st => st!.Price).Sum() / 1000;
        foreach (var stuff in stuffs)
            _manchkin.TakeStuff(stuff);

        _manchkin.SellStuffs(stuffs);

        Assert.That(_manchkin.Level, Is.EqualTo(expectedLevel));
    }

    [Test]
    public void Manchkin_SellByDoublePriceStuffs_Works()
    {
        var stuffs = new List<IStuff?> { new HelmetOfCourage(), new LeatherArmor(), new MightyShoes() };
        var expectedLevel = _manchkin.Level + stuffs.Select(st => st!.Price * 2 / 1000).Sum();
        foreach (var stuff in stuffs)
            _manchkin.TakeStuff(stuff);

        foreach (var stuff in stuffs)
            _manchkin.SellByDoublePrice(stuff);

        Assert.That(_manchkin.Level, Is.EqualTo(expectedLevel));
    }

    private void CheckThatManchkinDoesntHaveAnyStuffs()
    {
        Assert.Multiple(() =>
        {
            Assert.That(_manchkin.SmallStuffs, Is.Empty);
            Assert.That(_manchkin.HugeStuffs, Is.Empty);
            Assert.That(_manchkin.WornHat, Is.Null);
            Assert.That(_manchkin.WornArmor, Is.Null);
            Assert.That(_manchkin.WornShoes, Is.Null);
            Assert.That(_manchkin.Hands.RightHand, Is.Null);
            Assert.That(_manchkin.Hands.LeftHand, Is.Null);
        });
    }
}