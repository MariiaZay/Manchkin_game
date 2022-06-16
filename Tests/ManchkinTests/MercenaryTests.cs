using ManchkinCore.CardEnums.Accessory;
using ManchkinCore.GameLogic.Implementation.Accessory.Classes;
using ManchkinCore.GameLogic.Implementation.Accessory.Races;
using ManchkinCore.GameLogic.Implementation.Factories;
using ManchkinCore.GameLogic.Implementation.MainOutfit.Weapons.ConcreteWeapons;
using ManchkinCore.GameLogic.Implementation.Manchkin;
using ManchkinCore.GameLogic.Interfaces.Manchkin;
using NUnit.Framework;

namespace Tests.ManchkinTests;

[TestFixture]
public class MercenaryTests
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

    [TestCase(true)]
    [TestCase(false)]
    public void GetMercenary_HasMercenaryIsTrue(bool withStuff)
    {
        if (withStuff)
            _manchkin.GetMercenary(new Bow());
        else
            _manchkin.GetMercenary();

        Assert.That(_manchkin.HasMercenary, Is.True);
    }

    [Test]
    public void GiveToMercenary_EquipmentChanges()
    {
        _manchkin.GetMercenary();
        var mercenary = _manchkin.Mercenaries.First();

        Assert.That(mercenary.Item, Is.Null);

        _manchkin.GiveToMercenary(new Bow());

        Assert.That(mercenary.Item, Is.Not.EqualTo(null));
    }

    [Test]
    public void KillMercenaries_ListOfMercenariesIsEmptyAndLevelUp()
    {
        _manchkin.GetMercenary();
        _manchkin.GetMercenary();

        Assert.That(_manchkin.Mercenaries, Has.Count.EqualTo(2));

        _manchkin.KillMercenaries();
        
        Assert.Multiple(() =>
        {
            Assert.That(_manchkin.Mercenaries, Has.Count.EqualTo(0));
            Assert.That(_manchkin.Level, Is.EqualTo(3));
        });
    }
}