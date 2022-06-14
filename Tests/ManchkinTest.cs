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
    public void Manchkin_BecomeSuperManchkin_Became()
    {
        var manchkin = new Manchkin(Genders.FEMALE);

        manchkin.BecameSuperManchkin();

        Assert.That(manchkin.IsSuperManchkin, Is.True);
    }

    [Test]
    public void Manchkin_BecomeHalfblood_Became()
    {
        var manchkin = new Manchkin(Genders.FEMALE);

        manchkin.BecameHalfBlood();

        Assert.That(manchkin.IsHalfBlood, Is.True);
    }

    [Test]
    public void Manchkin_RefuseHalfblood_Works()
    {
        var manchkin = new Manchkin(Genders.MALE);
        manchkin.BecameHalfBlood();

        manchkin.RefuseHalfblood();

        Assert.That(manchkin.IsHalfBlood, Is.False);
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
        var manchkin = new Manchkin(Genders.MALE);

        manchkin.ChangeGender();

        Assert.That(manchkin.Gender, Is.EqualTo(Genders.FEMALE));
    }

    [Test]
    public void Manchkin_ChangeRace_Works()
    {
        var manchkin = new Manchkin(Genders.MALE)
        {
            Race = new Elf()
        };

        Assert.That(manchkin.Race, Is.InstanceOf<Elf>());
    }

    [Test]
    public void Manchkin_ChangeClass_Works()
    {
        var manchkin = new Manchkin(Genders.FEMALE)
        {
            Class = new Thief()
        };

        Assert.That(manchkin.Class, Is.InstanceOf<Thief>());
    }

    [Test]
    public void Manchkin_ToDie_LosesAllStuff()
    {
        var manchkin = new Manchkin(Genders.MALE);
        for (var i = 0; i < 9; i++)
            manchkin.GetLevel();
        manchkin.TakeStuff(new HornedHelmet());
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