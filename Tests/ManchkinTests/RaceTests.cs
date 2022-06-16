using ManchkinCore.CardEnums.Accessory;
using ManchkinCore.GameLogic.Implementation.Accessory.Classes;
using ManchkinCore.GameLogic.Implementation.Accessory.Races;
using ManchkinCore.GameLogic.Implementation.Factories;
using ManchkinCore.GameLogic.Implementation.Manchkin;
using ManchkinCore.GameLogic.Interfaces.Manchkin;
using NUnit.Framework;

namespace Tests.ManchkinTests;

public class RaceTests
{
    private IManchkin _manchkin = null!;

    [SetUp]
    public void SetUp()
    {
        var manchkinFactory = new ManchkinFactory(
                new Nobody(),
                new Human(),
                new Hands(),
                new MercenaryFactory(),
                new HalfbloodFactory(),
                new SuperManchkinFactory()
            )
            .SetGender(Genders.MALE);
        _manchkin = manchkinFactory.Build();
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

    [Test]
    public void ChangeRace_ManchkinIsHalfBlood_UpdatingRelevantParametersAndRefuseHalfblood()
    {
        var elf = new Elf();

        _manchkin.BecameHalfBlood();
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