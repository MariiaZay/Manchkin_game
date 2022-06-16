using ManchkinCore.CardEnums.Accessory;
using ManchkinCore.GameLogic.Implementation.Accessory.Classes;
using ManchkinCore.GameLogic.Implementation.Accessory.Races;
using ManchkinCore.GameLogic.Implementation.Factories;
using ManchkinCore.GameLogic.Implementation.MainOutfit.Weapons.ConcreteWeapons;
using ManchkinCore.GameLogic.Implementation.Manchkin;
using ManchkinCore.GameLogic.Interfaces.Manchkin;
using NUnit.Framework;

namespace Tests.ManchkinTests.StuffTests;

public class CanHaveStuffTests
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
    public void CanHaveStuff_NullStuff()
    {
        Assert.That(_manchkin.CanHaveStuff(null!), Is.True);
    }

    [Test]
    public void CanHaveStuff_HalfBlood()
    {
        _manchkin.BecameHalfBlood();

        Assert.That(_manchkin.CanHaveStuff(new Chainsaw { Cheat = true }), Is.True);
    }

    [Test]
    public void CanHaveStuff_SuperManchkin()
    {
        _manchkin.BecameSuperManchkin();

        Assert.That(_manchkin.CanHaveStuff(new Chainsaw { Cheat = true }), Is.True);
    }
}