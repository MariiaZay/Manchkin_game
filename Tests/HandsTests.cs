using ManchkinCore.GameLogic.Implementation.MainOutfit.Weapons.ConcreteWeapons;
using ManchkinCore.GameLogic.Implementation.Manchkin;
using NUnit.Framework;

namespace Tests;

public class HandsTests
{
    [Test]
    public void TakeInHands_Works()
    {
        var hands = new Hands();
        var weapon1 = new BastardSword();
        var weapon2 = new Buckler();

        hands.TakeInRightHand(weapon1);
        hands.TakeInLeftHand(weapon2);

        Assert.Multiple(() =>
        {
            Assert.That(hands.RightHand, Is.EqualTo(weapon1));
            Assert.That(hands.LeftHand, Is.EqualTo(weapon2));
        });
    }

    [Test]
    public void TakeInBothHands_Works()
    {
        var hands = new Hands();
        var weapon = new BastardSword();

        hands.TakeInBothHands(weapon);

        Assert.Multiple(() =>
        {
            Assert.That(hands.RightHand, Is.EqualTo(weapon));
            Assert.That(hands.LeftHand, Is.EqualTo(weapon));
        });
    }

    [Test]
    public void DropStuffsFromHands_Works()
    {
        var hands = new Hands();
        var weapon = new BastardSword();

        hands.TakeInRightHand(weapon);
        hands.TakeInLeftHand(weapon);

        hands.DropFromLeftHand();
        Assert.That(hands.LeftHand, Is.Null);

        hands.DropFromRightHand();
        Assert.That(hands.RightHand, Is.Null);

        hands.TakeInBothHands(weapon);
        hands.DropFromBothHands();
        Assert.Multiple(() =>
        {
            Assert.That(hands.LeftHand, Is.Null);
            Assert.That(hands.RightHand, Is.Null);
        });
    }
}