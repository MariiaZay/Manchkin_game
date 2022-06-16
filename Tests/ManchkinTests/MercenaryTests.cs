using ManchkinCore.Enums.Accessory;
using ManchkinCore.GameLogic.Implementation;
using ManchkinCore.GameLogic.Implementation.Factories;
using ManchkinCore.Implementation;
using ManchkinCore.Interfaces;
using NUnit.Framework;

namespace Tests.ManchkinTests;

[TestFixture]
public class MercenaryTests
{
    private IManchkin _manchkin = null!;

    [SetUp]
    public void SetUp()
    {
        _manchkin = new Manchkin(
            new Nobody(),
            new Human(),
            new Hands(),
            new MercenaryFactory(),
            new HalfbloodFactory(),
            new SuperManchkinFactory(),
            Genders.MALE
        );
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
}