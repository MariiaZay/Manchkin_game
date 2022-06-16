using ManchkinCore.CardEnums.Accessory;
using ManchkinCore.GameLogic.Implementation.Accessory.Classes;
using ManchkinCore.GameLogic.Implementation.Accessory.Races;
using ManchkinCore.GameLogic.Implementation.Factories;
using ManchkinCore.GameLogic.Implementation.Manchkin;
using ManchkinCore.GameLogic.Interfaces.Manchkin;
using NUnit.Framework;

namespace Tests.ManchkinTests;

public class LevelTests
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
    public void GetLevel_IncreasesLevel()
    {
        var originLevel = _manchkin.Level;

        _manchkin.GetLevel();

        Assert.That(_manchkin.Level, Is.GreaterThan(originLevel));
    }

    [TestCase(0, 1)]
    [TestCase(1, 2)]
    [TestCase(5, 6)]
    [TestCase(15, 9)]
    public void GetLevel_WithIncreaseByValue_IncreasesLevel(int increaseByValue, int expectedLevel)
    {
        _manchkin.GetLevel(increaseByValue);

        Assert.That(_manchkin.Level, Is.EqualTo(expectedLevel));
    }

    [Test]
    public void GetLevel_IncreasesDamage()
    {
        var originDamage = _manchkin.Damage;

        _manchkin.GetLevel();

        Assert.That(_manchkin.Damage, Is.GreaterThan(originDamage));
    }

    [Test]
    public void GetLevel_HasUpperBoundTen()
    {
        for (var _ = 0; _ < 10; _++)
            _manchkin.GetLevel();
        var originLevel = _manchkin.Level;

        _manchkin.GetLevel();

        Assert.That(_manchkin.Level, Is.EqualTo(originLevel));
    }

    [Test]
    public void LostLevel_DecreasesLevel()
    {
        _manchkin.GetLevel();
        var originLevel = _manchkin.Level;
        _manchkin.LostLevel();

        Assert.That(_manchkin.Level, Is.LessThan(originLevel));
    }

    [Test]
    public void LostLevel_DecreasesDamage()
    {
        _manchkin.GetLevel();
        var originDamage = _manchkin.Damage;
        _manchkin.LostLevel();

        Assert.That(_manchkin.Damage, Is.LessThan(originDamage));
    }

    [Test]
    public void LostLevel_HasLowerBoundOne()
    {
        Assert.That(_manchkin.Level, Is.EqualTo(1));

        _manchkin.LostLevel();

        Assert.That(_manchkin.Level, Is.EqualTo(1));
    }
}