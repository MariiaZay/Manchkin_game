using ManchkinCore.Enums.Accessory;
using ManchkinCore.GameLogic.Implementation;
using ManchkinCore.Implementation;
using ManchkinCore.Interfaces;
using NUnit.Framework;

namespace Tests.ManchkinTests;

public class StuffsTests
{
    private IManchkin _manchkin = null!;

    [SetUp]
    public void SetUp()
    {
        _manchkin = new Manchkin(Genders.MALE);
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
    public void SellStuffs_Works()
    {
        var stuffs = new List<IStuff?> { new HelmetOfCourage(), new LeatherArmor(), new MightyShoes() };
        var expectedLevel = _manchkin.Level + stuffs.Select(st => st!.Price).Sum() / 1000;
        foreach (var stuff in stuffs)
            _manchkin.TakeStuff(stuff);

        _manchkin.SellStuffs(stuffs);

        Assert.That(_manchkin.Level, Is.EqualTo(expectedLevel));
    }

    [Test]
    public void SellByDoublePriceStuffs_Works()
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