using ManchkinCore.CardEnums.Accessory;
using ManchkinCore.GameLogic.Implementation.Accessory.Classes;
using ManchkinCore.GameLogic.Implementation.Accessory.Races;
using ManchkinCore.GameLogic.Implementation.Factories;
using ManchkinCore.GameLogic.Implementation.Gears.Stuffs.ConcreteStuffs;
using ManchkinCore.GameLogic.Implementation.MainOutfit.Armor.ConcreteArmor;
using ManchkinCore.GameLogic.Implementation.MainOutfit.Hats.ConcreteHats;
using ManchkinCore.GameLogic.Implementation.MainOutfit.Shoes.ConcreteShoes;
using ManchkinCore.GameLogic.Implementation.MainOutfit.Weapons.ConcreteWeapons;
using ManchkinCore.GameLogic.Implementation.Manchkin;
using ManchkinCore.GameLogic.Interfaces.Manchkin;
using ManchkinCore.GameLogic.Interfaces.Stuff;
using NUnit.Framework;

namespace Tests.ManchkinTests.StuffTests;

public class LostStuffTests
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
    public void LostStuff_Hat()
    {
        var hat = new HelmetOfCourage { Cheat = true };
        _manchkin.TakeStuff(hat);

        _manchkin.LostStuff(hat);

        Assert.That(_manchkin.WornHat, Is.Null);
    }

    [Test]
    public void LostStuff_Armor()
    {
        var armor = new DwarfArmor { Cheat = true };
        _manchkin.TakeStuff(armor);

        _manchkin.LostStuff(armor);

        Assert.That(_manchkin.WornArmor, Is.Null);
    }

    [Test]
    public void LostStuff_Shoes()
    {
        var shoes = new SandalsOfProtection { Cheat = true };
        _manchkin.TakeStuff(shoes);

        _manchkin.LostStuff(shoes);

        Assert.That(_manchkin.WornShoes, Is.Null);
    }

    [Test]
    public void LostStuff_BothWeapon()
    {
        var weapon = new HugeRock { Cheat = true };
        _manchkin.TakeStuff(weapon);

        _manchkin.LostStuff(weapon);

        Assert.That(_manchkin.Hands.LeftHand, Is.Null);
        Assert.That(_manchkin.Hands.RightHand, Is.Null);
    }

    [Test]
    public void LostStuff_SingleWeapon()
    {
        var weapon = new Dagger { Cheat = true };
        _manchkin.TakeStuff(weapon);

        _manchkin.LostStuff(weapon);

        Assert.That(_manchkin.Hands.LeftHand, Is.Null);
    }

    [Test]
    public void LostStuff_OtherStuffs()
    {
        var hugeStuff = new Stepladder { Cheat = true };
        var smallStuff = new Cloack { Cheat = true };

        _manchkin.TakeStuff(hugeStuff);
        _manchkin.TakeStuff(smallStuff);

        _manchkin.LostStuff(hugeStuff);
        _manchkin.LostStuff(smallStuff);

        Assert.That(_manchkin.HugeStuffs, Is.Empty);
        Assert.That(_manchkin.SmallStuffs, Is.Empty);
    }

    [Test]
    public void LostMostPowerfulStuff()
    {
        var singingSword = new SingingSword { Cheat = true };
        var stepladder = new Stepladder { Cheat = true };
        _manchkin.TakeStuff(new Cloack { Cheat = true });
        _manchkin.TakeStuff(singingSword);
        _manchkin.TakeStuff(stepladder);

        _manchkin.LostMostPowerfulStuff();

        Assert.That(_manchkin.HugeStuffs, Is.EqualTo(new List<IStuff?> { stepladder }));
        Assert.That(_manchkin.SmallStuffs, Is.EqualTo(new List<IStuff?> { singingSword }));
    }
}