using ManchkinCore.CardEnums.Accessory;
using ManchkinCore.GameLogic.Implementation.Accessory.Classes;
using ManchkinCore.GameLogic.Implementation.Accessory.Races;
using ManchkinCore.GameLogic.Implementation.Factories;
using ManchkinCore.GameLogic.Implementation.Gears.Stuffs.ConcreteStuffs;
using ManchkinCore.GameLogic.Implementation.MainOutfit.Armor.ConcreteArmor;
using ManchkinCore.GameLogic.Implementation.MainOutfit.Hats.ConcreteHats;
using ManchkinCore.GameLogic.Implementation.MainOutfit.Shoes.ConcreteShoes;
using ManchkinCore.GameLogic.Implementation.Manchkin;
using ManchkinCore.GameLogic.Interfaces.Manchkin;
using ManchkinCore.GameLogic.Interfaces.Stuff;
using NUnit.Framework;

namespace Tests.ManchkinTests.StuffTests;

public class StuffsTests
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
    public void UseCheat()
    {
        var stuff = new Cloack();

        Assert.That(stuff.Cheat, Is.False);

        _manchkin.UseCheat(stuff);

        Assert.That(stuff.Cheat, Is.True);
    }

    [Test]
    public void CancelCheat()
    {
        var stuff = new Cloack { Cheat = true };

        Assert.That(stuff.Cheat, Is.True);

        _manchkin.CancelCheat(stuff);

        Assert.That(stuff.Cheat, Is.False);
    }

    [Test]
    public void ToDie_LosesAllStuff()
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
    public void SellStuffs()
    {
        var stuffs = new List<IStuff?> { new HelmetOfCourage(), new LeatherArmor(), new MightyShoes() };
        var expectedLevel = _manchkin.Level + stuffs.Select(st => st!.Price).Sum() / 1000;
        foreach (var stuff in stuffs)
            _manchkin.TakeStuff(stuff);

        _manchkin.SellStuffs(stuffs);

        Assert.That(_manchkin.Level, Is.EqualTo(expectedLevel));
    }

    [Test]
    public void SellByDoublePriceStuffs()
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