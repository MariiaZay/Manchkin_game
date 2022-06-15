using ManchkinCore.Enums.Accessory;
using ManchkinCore.GameLogic.Implementation;
using ManchkinCore.Implementation;
using ManchkinCore.Interfaces;
using NUnit.Framework;

namespace Tests.ManchkinTests;

public class RaceTests
{
    private IManchkin _manchkin = null!;

    [SetUp]
    public void SetUp()
    {
        _manchkin = new Manchkin(Genders.MALE);
    }

    [Test]
    public void ChangeRace_UpdateRaceAndSpecialParameters()
    {
        var elf = new Elf();
        var human = new Human();

        Assert.Multiple(() =>
        {
            Assert.That(_manchkin.Race, Is.InstanceOf<Human>());
            Assert.That(_manchkin.Descriptions, Is.EqualTo(human.Descriptions));
            Assert.That(_manchkin.FlushingBonus, Is.EqualTo(human.FlushingBonus));
            Assert.That(_manchkin.CardsCount, Is.EqualTo(human.CardCount));
            Assert.That(_manchkin.DoublePrice, Is.EqualTo(human.CellingByDoublePrice));
        });

        _manchkin.Race = elf;

        Assert.Multiple(() =>
        {
            Assert.That(_manchkin.Race, Is.InstanceOf<Elf>());
            Assert.That(_manchkin.Descriptions, Is.EqualTo(elf.Descriptions));
            Assert.That(_manchkin.FlushingBonus, Is.EqualTo(elf.FlushingBonus));
            Assert.That(_manchkin.CardsCount, Is.EqualTo(elf.CardCount));
            Assert.That(_manchkin.DoublePrice, Is.EqualTo(elf.CellingByDoublePrice));
        });
    }

    //TODO исправить код
    // [Test]
    // public void ChangeRaceWhenNewRaceIsNull_WithoutAnyChanges()
    // {
    //     var human = new Human();
    //
    //     _manchkin.Race = null;
    //
    //     Assert.Multiple(() =>
    //     {
    //         Assert.That(_manchkin.Race, Is.InstanceOf<Human>());
    //         Assert.That(_manchkin.Descriptions, Is.EqualTo(human.Descriptions));
    //         Assert.That(_manchkin.FlushingBonus, Is.EqualTo(human.FlushingBonus));
    //         Assert.That(_manchkin.CardsCount, Is.EqualTo(human.CardCount));
    //         Assert.That(_manchkin.DoublePrice, Is.EqualTo(human.CellingByDoublePrice));
    //     });
    // }

    [Test]
    public void ChangeRaceWhenManchkinIsHalfBlood_UpdatingRelevantParametersAndRefuseHalfblood()
    {
        var elf = new Elf();

        _manchkin.BecameHalfBlood(elf);
        _manchkin.Race = elf;

        Assert.Multiple(() =>
        {
            Assert.That(_manchkin.Race, Is.InstanceOf<Elf>());
            Assert.That(_manchkin.Descriptions, Is.EqualTo(elf.Descriptions));
            Assert.That(_manchkin.FlushingBonus, Is.EqualTo(elf.FlushingBonus));
            Assert.That(_manchkin.CardsCount, Is.EqualTo(elf.CardCount));
            Assert.That(_manchkin.DoublePrice, Is.EqualTo(elf.CellingByDoublePrice));
            Assert.That(_manchkin.IsHalfBlood, Is.False);
        });
    }
}