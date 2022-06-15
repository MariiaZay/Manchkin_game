using ManchkinCore.Enums.Accessory;
using ManchkinCore.GameLogic.Implementation;
using ManchkinCore.Implementation;
using ManchkinCore.Implementation.Gears;
using ManchkinCore.Interfaces;
using NUnit.Framework;

namespace Tests.ManchkinTests;

public class GeneralTests
{
    private IManchkin _manchkin = null!;

    [SetUp]
    public void SetUp()
    {
        _manchkin = new Manchkin(Genders.MALE);
    }

    [Test]
    public void Create_PropertiesValuesAreAdequate()
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
    public void ChangeGender_Works()
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
    public void ToDie_HeIsDead()
    {
        Assert.That(_manchkin.IsDead, Is.False);

        _manchkin.ToDie();

        Assert.That(_manchkin.IsDead, Is.True);
    }

    [Test]
    public void RecalculateDamage_WithoutMercenaries_UpdateDamage()
    {
        var smallStuff = new GreatTitle();
        var hugeStuff = new Stepladder { Cheat = true };
        var expectedDamage = _manchkin.Level
                             + smallStuff.Damage
                             + hugeStuff.Damage;

        _manchkin.TakeStuff(smallStuff);
        _manchkin.TakeStuff(hugeStuff);

        Assert.That(_manchkin.Damage, Is.EqualTo(expectedDamage));
    }

    [Test]
    public void RecalculateDamage_WithMercenaries_UpdateDamage()
    {
        var smallStuff = new GreatTitle();
        var hugeStuff = new Stepladder { Cheat = true };
        var mercenaryStuff = new Pole();
        _manchkin.GetMercenary(mercenaryStuff);
        var expectedDamage = _manchkin.Level
                             + smallStuff.Damage
                             + hugeStuff.Damage
                             + _manchkin.Mercenaries
                                 .Select(m =>
                                 {
                                     if (m.Item != null)
                                         return 1 + m.Item.Damage;
                                     return 1;
                                 })
                                 .Sum();

        _manchkin.TakeStuff(smallStuff);
        _manchkin.TakeStuff(hugeStuff);

        Assert.That(_manchkin.Damage, Is.EqualTo(expectedDamage));
    }

    [TestCase(true)]
    [TestCase(false)]
    public void RecalculateFlushingBonus_WithoutMercenaries_UpdateFlushingBonus(bool isElf)
    {
        if (isElf)
            _manchkin.Race = new Elf();
        var smallStuff = new GreatTitle();
        var hugeStuff = new Stepladder { Cheat = true };
        var expectedFlushingBonus = isElf
            ? 1
            : 0
              + smallStuff.FlushingBonus
              + hugeStuff.FlushingBonus;

        _manchkin.TakeStuff(smallStuff);
        _manchkin.TakeStuff(hugeStuff);

        Assert.That(_manchkin.FlushingBonus, Is.EqualTo(expectedFlushingBonus));
    }

    [TestCase(true)]
    [TestCase(false)]
    public void RecalculateFlushingBonus_WithMercenaries_UpdateFlushingBonus(bool isElf)
    {
        if (isElf)
            _manchkin.Race = new Elf();
        var smallStuff = new GreatTitle();
        var hugeStuff = new Stepladder { Cheat = true };
        var mercenaryStuff = new Pole();
        _manchkin.GetMercenary(mercenaryStuff);
        var expectedFlushingBonus = isElf
            ? 1
            : 0
              + smallStuff.FlushingBonus
              + hugeStuff.FlushingBonus
              + _manchkin.Mercenaries
                  .Where(m => m.Item != null)
                  .Select(m => m.Item!.FlushingBonus)
                  .Sum();

        _manchkin.TakeStuff(smallStuff);
        _manchkin.TakeStuff(hugeStuff);

        Assert.That(_manchkin.FlushingBonus, Is.EqualTo(expectedFlushingBonus));
    }
    
    // [Test]
    // public void 
}