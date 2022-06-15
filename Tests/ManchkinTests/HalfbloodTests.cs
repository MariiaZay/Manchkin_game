﻿using ManchkinCore.Enums;
using ManchkinCore.Enums.Accessory;
using ManchkinCore.GameLogic.Implementation;
using ManchkinCore.Implementation;
using ManchkinCore.Interfaces;
using NUnit.Framework;

namespace Tests.ManchkinTests;

public class HalfbloodTests
{
    private IManchkin _manchkin = null!;

    [SetUp]
    public void SetUp()
    {
        _manchkin = new Manchkin(Genders.MALE);
    }

    [Test]
    public void Manchkin_BecomeHalfblood_Became()
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
    public void Manchkin_BecomeHalfblood_Became2()
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
    public void Manchkin_RefuseHalfblood_Works1()
    {
        Assert.That(_manchkin.IsHalfBlood, Is.False);

        _manchkin.RefuseHalfblood();

        Assert.That(_manchkin.IsHalfBlood, Is.False);
    }

    [Test]
    public void Manchkin_RefuseHalfblood_Works2()
    {
        _manchkin.BecameHalfBlood();
        _manchkin.RefuseHalfblood();

        Assert.That(_manchkin.IsHalfBlood, Is.False);
    }

    [Test]
    public void Manchkin_RefuseHalfblood_LostDescriptions()
    {
        var manchkinDesc = _manchkin.Descriptions;
        var secondRace = new Dwarf();

        _manchkin.BecameHalfBlood(secondRace);
        _manchkin.RefuseHalfblood();

        Assert.That(_manchkin.Descriptions, Is.EqualTo(manchkinDesc));
    }
}