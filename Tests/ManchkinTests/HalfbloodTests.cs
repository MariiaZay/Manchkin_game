﻿using ManchkinCore.CardEnums;
using ManchkinCore.CardEnums.Accessory;
using ManchkinCore.GameLogic.Implementation.Accessory.Classes;
using ManchkinCore.GameLogic.Implementation.Accessory.Races;
using ManchkinCore.GameLogic.Implementation.Factories;
using ManchkinCore.GameLogic.Implementation.Manchkin;
using ManchkinCore.GameLogic.Interfaces.Manchkin;
using NUnit.Framework;

namespace Tests.ManchkinTests;

public class HalfbloodTests
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
    public void BecomeHalfblood_WithoutSecondRace_Became()
    {
        Assert.That(_manchkin.IsHalfBlood, Is.False);

        _manchkin.BecameHalfBlood();

        Assert.Multiple(() =>
        {
            Assert.That(_manchkin.IsHalfBlood, Is.True);
            Assert.That(_manchkin.HalfBlood?.HalfType, Is.EqualTo(HalfTypes.SINGLE_CLEAN));
            Assert.That(_manchkin.HalfBlood?.SecondRace, Is.Null);
        });
    }

    [Test]
    public void BecomeHalfblood_WithSecondRace_Became()
    {
        var secondRace = new Dwarf();

        Assert.That(_manchkin.IsHalfBlood, Is.False);

        _manchkin.BecameHalfBlood(secondRace);

        Assert.Multiple(() =>
        {
            Assert.That(_manchkin.IsHalfBlood, Is.True);
            Assert.That(_manchkin.HalfBlood?.HalfType, Is.EqualTo(HalfTypes.BOTH));
            Assert.That(_manchkin.HalfBlood?.SecondRace, Is.EqualTo(secondRace));
        });
    }

    [Test]
    public void RefuseHalfblood_BeforeBecoming_Works()
    {
        Assert.That(_manchkin.IsHalfBlood, Is.False);

        _manchkin.RefuseHalfblood();

        Assert.That(_manchkin.IsHalfBlood, Is.False);
    }

    [Test]
    public void RefuseHalfblood_AfterBecoming_Works()
    {
        _manchkin.BecameHalfBlood();
        _manchkin.RefuseHalfblood();

        Assert.That(_manchkin.IsHalfBlood, Is.False);
    }

    [Test]
    public void RefuseHalfblood_AfterBecoming_LostDescriptions()
    {
        var manchkinDesc = _manchkin.Descriptions;
        var secondRace = new Dwarf();

        _manchkin.BecameHalfBlood(secondRace);
        _manchkin.RefuseHalfblood();

        Assert.That(_manchkin.Descriptions, Is.EqualTo(manchkinDesc));
    }
}