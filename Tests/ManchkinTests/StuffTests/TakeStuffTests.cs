using ManchkinCore.Enums.Accessory;
using ManchkinCore.GameLogic.Implementation.Factories;
using ManchkinCore.Implementation;
using ManchkinCore.Implementation.Gears;
using ManchkinCore.Interfaces;
using NUnit.Framework;

namespace Tests.ManchkinTests.StuffTests;

public class TakeStuffTests
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
    public void TakeStuff_Hat()
    {
        var hat = new HelmetOfCourage();

        _manchkin.TakeStuff(hat);

        Assert.That(_manchkin.WornHat, Is.EqualTo(hat));
    }

    [Test]
    public void TakeStuff_Armor()
    {
        var armor = new MucousMembrane();

        _manchkin.TakeStuff(armor);

        Assert.That(_manchkin.WornArmor, Is.EqualTo(armor));
    }

    [Test]
    public void TakeStuff_Shoes()
    {
        var shoes = new SandalsOfProtection();

        _manchkin.TakeStuff(shoes);

        Assert.That(_manchkin.WornShoes, Is.EqualTo(shoes));
    }

    [Test]
    public void TakeStuff_Weapon()
    {
        var weapon = new BastardSword();

        _manchkin.TakeStuff(weapon);

        Assert.That(_manchkin.Hands.LeftHand, Is.EqualTo(weapon));
    }

    [Test]
    public void TakeStuff_DefaultCase()
    {
        var singingSword = new SingingSword { Cheat = true };
        var stepladder = new Stepladder { Cheat = true };

        _manchkin.TakeStuff(singingSword);
        _manchkin.TakeStuff(stepladder);

        Assert.That(_manchkin.HugeStuffs, Is.EqualTo(new List<IStuff?> { stepladder }));
        Assert.That(_manchkin.SmallStuffs, Is.EqualTo(new List<IStuff?> { singingSword }));
    }

    [Test]
    public void TakeSingleWeaponRightHand_WithEmptyHand()
    {
        var weapon = new Rapier();

        _manchkin.TakeSingleWeaponRightHand(weapon);

        Assert.That(_manchkin.Hands.RightHand, Is.EqualTo(weapon));
    }

    [Test]
    public void TakeSingleWeaponRightHand_WithNonEmptyHand()
    {
        var weapon = new Rapier();
        _manchkin.TakeSingleWeaponRightHand(new Buckler());

        _manchkin.TakeSingleWeaponRightHand(weapon);

        Assert.That(_manchkin.Hands.RightHand, Is.EqualTo(weapon));
    }

    [Test]
    public void TakeSingleWeaponLeftHand_WithEmptyHand()
    {
        var weapon = new Rapier();

        _manchkin.TakeSingleWeaponLeftHand(weapon);

        Assert.That(_manchkin.Hands.LeftHand, Is.EqualTo(weapon));
    }

    [Test]
    public void TakeSingleWeaponLeftHand_WithNonEmptyHand()
    {
        var weapon = new Rapier();
        _manchkin.TakeSingleWeaponLeftHand(new Buckler());

        _manchkin.TakeSingleWeaponLeftHand(weapon);

        Assert.That(_manchkin.Hands.LeftHand, Is.EqualTo(weapon));
    }
}