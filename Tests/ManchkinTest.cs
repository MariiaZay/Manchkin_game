using ManchkinCore.Enums;
using ManchkinCore.Enums.Accessory;
using ManchkinCore.GameLogic.Implementation;
using ManchkinCore.Implementation;
using NUnit.Framework;

namespace Tests;

[TestFixture]
public class ManchkinTest
{
    [Test]
    public void Manchkin_Create_PropertiesValuesAreAdequate()
    {
        var manchkin = new Manchkin(Genders.MALE);

        Assert.Multiple(() =>
        {
            Assert.That(manchkin.Gender, Is.EqualTo(Genders.MALE));
            Assert.That(manchkin.Level, Is.EqualTo(1));
        });
        Assert.Multiple(() =>
        {
            Assert.That(manchkin.Level, Is.EqualTo(manchkin.Damage));
            Assert.That(manchkin.IsDead, Is.EqualTo(false));
            Assert.That(manchkin.IsHalfBlood, Is.EqualTo(false));
            Assert.That(manchkin.IsSuperManchkin, Is.EqualTo(false));
            Assert.That(manchkin.Race, Is.InstanceOf<Human>());
        });
        Assert.Multiple(() =>
        {
            Assert.That(manchkin.Mercenaries, Is.Empty);
            Assert.That(manchkin.CardsCount, Is.EqualTo(5));
            Assert.That(manchkin.WornHat, Is.Null);
            Assert.That(manchkin.WornArmor, Is.Null);
            Assert.That(manchkin.WornShoes, Is.Null);
            Assert.That(manchkin.Hands.LeftHand, Is.Null);
            Assert.That(manchkin.Hands.RightHand, Is.Null);
        });
        Assert.Multiple(() =>
        {
            Assert.That(manchkin.Descriptions, Is.Empty);
            Assert.That(manchkin.HasMercenary, Is.EqualTo(false));
        });
    }

    [Test]
    public void Manchkin_GetLevel_IncreasesLevel()
    {
        var manchkin = new Manchkin(Genders.FEMALE);
        var originLevel = manchkin.Level;

        manchkin.GetLevel();

        Assert.That(manchkin.Level, Is.GreaterThan(originLevel));
    }

    [Test]
    public void Manchkin_GetLevel_IncreasesDamage()
    {
        var manchkin = new Manchkin(Genders.FEMALE);
        var originDamage = manchkin.Damage;

        manchkin.GetLevel();

        Assert.That(manchkin.Damage, Is.GreaterThan(originDamage));
    }

    [Test]
    public void Manchkin_GetLevel_HasUpperBound10()
    {
        var manchkin = new Manchkin(Genders.MALE);
        for (var _ = 0; _ < 10; _++)
            manchkin.GetLevel();
        var originLevel = manchkin.Level;

        manchkin.GetLevel();

        Assert.That(manchkin.Level, Is.EqualTo(originLevel));
    }

    [Test]
    public void Manchkin_BecomeSuperManchkin_Became1()
    {
        var manchkin = new Manchkin(Genders.FEMALE);

        manchkin.BecameSuperManchkin();

        Assert.Multiple(() =>
        {
            Assert.That(manchkin.IsSuperManchkin, Is.True);
            Assert.That(manchkin.SuperManchkin!.HalfType, Is.EqualTo(HalfTypes.SINGLE_CLEAN));
            Assert.That(manchkin.SuperManchkin.SecondClass, Is.Null);
        });
    }

    [Test]
    public void Manchkin_BecomeSuperManchkin_Became2()
    {
        var manchkin = new Manchkin(Genders.FEMALE);
        var desc = manchkin.Descriptions;
        var thief = new Thief();
        desc.AddRange(thief.Descriptions);

        manchkin.BecameSuperManchkin(thief);

        Assert.Multiple(() =>
        {
            Assert.That(manchkin.IsSuperManchkin, Is.True);
            Assert.That(manchkin.SuperManchkin!.HalfType, Is.EqualTo(HalfTypes.BOTH));
            Assert.That(manchkin.SuperManchkin.SecondClass, Is.InstanceOf<Thief>());
            Assert.That(manchkin.Descriptions, Is.EqualTo(desc));
        });
    }

    [Test]
    public void Manchkin_RefuseSuperManchkin_Refuse1()
    {
        var manchkin = new Manchkin(Genders.FEMALE);

        manchkin.BecameSuperManchkin();
        manchkin.RefuseSuperManchkin();

        Assert.That(manchkin.IsSuperManchkin, Is.False);
    }

    [Test]
    public void Manchkin_RefuseSuperManchkin_Refuse2()
    {
        var manchkin = new Manchkin(Genders.FEMALE);
        var manchkinDesc = manchkin.Descriptions.ToArray();
        var thief = new Thief();

        manchkin.BecameSuperManchkin(thief);
        manchkin.RefuseSuperManchkin();

        Assert.Multiple(() =>
        {
            Assert.That(manchkin.IsSuperManchkin, Is.False);
            Assert.That(manchkin.Descriptions, Is.EqualTo(manchkinDesc.ToList()));
        });
    }

    [Test]
    public void Manchkin_RefuseSuperManchkin_Refuse3()
    {
        var manchkin = new Manchkin(Genders.FEMALE);

        Assert.That(manchkin.IsSuperManchkin, Is.False);

        manchkin.RefuseSuperManchkin();

        Assert.That(manchkin.IsSuperManchkin, Is.False);
    }

    [Test]
    public void Manchkin_BecomeHalfblood_Became()
    {
        var manchkin = new Manchkin(Genders.FEMALE);

        Assert.That(manchkin.IsHalfBlood, Is.False);

        manchkin.BecameHalfBlood();

        Assert.Multiple(() =>
        {
            Assert.That(manchkin.IsHalfBlood, Is.True);
            Assert.That(manchkin.HalfBlood?.HalfType, Is.EqualTo(HalfTypes.SINGLE_CLEAN));
            Assert.That(manchkin.HalfBlood?.SecondRace, Is.Null);
        });
    }

    [Test]
    public void Manchkin_BecomeHalfblood_Became2()
    {
        var manchkin = new Manchkin(Genders.FEMALE);
        var secondRace = new Dwarf();

        Assert.That(manchkin.IsHalfBlood, Is.False);

        manchkin.BecameHalfBlood(secondRace);

        Assert.Multiple(() =>
        {
            Assert.That(manchkin.IsHalfBlood, Is.True);
            Assert.That(manchkin.HalfBlood?.HalfType, Is.EqualTo(HalfTypes.BOTH));
            Assert.That(manchkin.HalfBlood?.SecondRace, Is.EqualTo(secondRace));
        });
    }

    [Test]
    public void Manchkin_RefuseHalfblood_Works1()
    {
        var manchkin = new Manchkin(Genders.MALE);

        Assert.That(manchkin.IsHalfBlood, Is.False);

        manchkin.RefuseHalfblood();

        Assert.That(manchkin.IsHalfBlood, Is.False);
    }

    [Test]
    public void Manchkin_RefuseHalfblood_Works2()
    {
        var manchkin = new Manchkin(Genders.MALE);

        manchkin.BecameHalfBlood();
        manchkin.RefuseHalfblood();

        Assert.That(manchkin.IsHalfBlood, Is.False);
    }

    [Test]
    public void Manchkin_RefuseHalfblood_LostDescriptions()
    {
        var manchkin = new Manchkin(Genders.MALE);
        var manchkinDesc = manchkin.Descriptions;
        var secondRace = new Dwarf();

        manchkin.BecameHalfBlood(secondRace);
        manchkin.RefuseHalfblood();

        Assert.That(manchkin.Descriptions, Is.EqualTo(manchkinDesc));
    }

    [Test]
    public void Manchkin_GetMercenary_GetMercenaryTrue()
    {
        var manchkin = new Manchkin(Genders.FEMALE);

        manchkin.GetMercenary();

        Assert.That(manchkin.HasMercenary, Is.True);
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
        var manchkin = new Manchkin(Genders.MALE);

        Assert.Multiple(() =>
        {
            Assert.That(manchkin.Race, Is.InstanceOf<Human>());
            Assert.That(manchkin.Descriptions, Is.EqualTo(human.Descriptions));
        });

        manchkin.Race = elf;

        Assert.Multiple(() =>
        {
            Assert.That(manchkin.Race, Is.InstanceOf<Elf>());
            Assert.That(manchkin.Descriptions, Is.EqualTo(elf.Descriptions));
        });
    }

    [Test]
    public void Manchkin_ChangeClass_Works()
    {
        var thief = new Thief();
        var nobody = new Nobody();
        var manchkin = new Manchkin(Genders.FEMALE);

        Assert.Multiple(() =>
        {
            Assert.That(manchkin.Class, Is.InstanceOf<Nobody>());
            Assert.That(manchkin.Descriptions, Is.EqualTo(nobody.Descriptions));
        });

        manchkin.Class = thief;

        Assert.Multiple(() =>
        {
            Assert.That(manchkin.Class, Is.InstanceOf<Thief>());
            Assert.That(manchkin.Descriptions, Is.EqualTo(thief.Descriptions));
        });
    }

    [Test]
    public void Manchkin_ToDie_LosesAllStuff()
    {
        var manchkin = new Manchkin(Genders.MALE);

        for (var i = 0; i < 9; i++)
            manchkin.GetLevel();
        manchkin.TakeStuff(new HelmetOfCourage());
        manchkin.TakeStuff(new LeatherArmor());
        manchkin.TakeStuff(new MightyShoes());

        Assert.Multiple(() =>
        {
            Assert.That(manchkin.WornHat, Is.Not.Null);
            Assert.That(manchkin.WornArmor, Is.Not.Null);
            Assert.That(manchkin.WornShoes, Is.Not.Null);
        });

        manchkin.ToDie();

        Assert.Multiple(() =>
        {
            Assert.That(manchkin.SmallStuffs, Is.Empty);
            Assert.That(manchkin.HugeStuffs, Is.Empty);
            Assert.That(manchkin.WornHat, Is.Null);
            Assert.That(manchkin.WornArmor, Is.Null);
            Assert.That(manchkin.WornShoes, Is.Null);
        });
    }
}