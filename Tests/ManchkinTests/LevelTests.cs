using ManchkinCore.Enums.Accessory;
using ManchkinCore.GameLogic.Implementation;
using ManchkinCore.Interfaces;
using NUnit.Framework;

namespace Tests.ManchkinTests;

public class LevelTests
{
    private IManchkin _manchkin = null!;

    [SetUp]
    public void SetUp()
    {
        _manchkin = new Manchkin(Genders.MALE);
    }

    [Test]
    public void Manchkin_GetLevel_IncreasesLevel()
    {
        var originLevel = _manchkin.Level;

        _manchkin.GetLevel();

        Assert.That(_manchkin.Level, Is.GreaterThan(originLevel));
    }

    [Test]
    public void Manchkin_GetLevel_IncreasesDamage()
    {
        var originDamage = _manchkin.Damage;

        _manchkin.GetLevel();

        Assert.That(_manchkin.Damage, Is.GreaterThan(originDamage));
    }

    [Test]
    public void Manchkin_GetLevel_HasUpperBound10()
    {
        for (var _ = 0; _ < 10; _++)
            _manchkin.GetLevel();
        var originLevel = _manchkin.Level;

        _manchkin.GetLevel();

        Assert.That(_manchkin.Level, Is.EqualTo(originLevel));
    }

    [Test]
    public void Manchkin_LostLevel_DecreasesLevel()
    {
        _manchkin.GetLevel();
        var originLevel = _manchkin.Level;
        _manchkin.LostLevel();

        Assert.That(_manchkin.Level, Is.LessThan(originLevel));
    }

    [Test]
    public void Manchkin_LostLevel_DecreasesDamage()
    {
        _manchkin.GetLevel();
        var originDamage = _manchkin.Damage;
        _manchkin.LostLevel();

        Assert.That(_manchkin.Damage, Is.LessThan(originDamage));
    }

    [Test]
    public void Manchkin_LostLevel_HasLowerBound1()
    {
        Assert.That(_manchkin.Level, Is.EqualTo(1));

        _manchkin.LostLevel();

        Assert.That(_manchkin.Level, Is.EqualTo(1));
    }
}