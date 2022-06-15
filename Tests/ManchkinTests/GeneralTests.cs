using ManchkinCore.Enums.Accessory;
using ManchkinCore.GameLogic.Implementation;
using ManchkinCore.Implementation;
using ManchkinCore.Interfaces;
using NUnit.Framework;

namespace Tests.ManchkinTests;

public class GeneralTests
{
    private IManchkin _manchkin = null!;

    [SetUp]
    public void SetUp()
    {
        _manchkin = new Manchkin(Genders.MALE);
    }

    [Test]
    public void Manchkin_Create_PropertiesValuesAreAdequate()
    {
        Assert.Multiple(() =>
        {
            Assert.That(_manchkin.Gender, Is.EqualTo(Genders.MALE));
            Assert.That(_manchkin.Level, Is.EqualTo(1));
        });
        Assert.Multiple(() =>
        {
            Assert.That(_manchkin.Level, Is.EqualTo(_manchkin.Damage));
            Assert.That(_manchkin.IsDead, Is.EqualTo(false));
            Assert.That(_manchkin.IsHalfBlood, Is.EqualTo(false));
            Assert.That(_manchkin.IsSuperManchkin, Is.EqualTo(false));
            Assert.That(_manchkin.Race, Is.InstanceOf<Human>());
        });
        Assert.Multiple(() =>
        {
            Assert.That(_manchkin.Mercenaries, Is.Empty);
            Assert.That(_manchkin.CardsCount, Is.EqualTo(5));
            Assert.That(_manchkin.WornHat, Is.Null);
            Assert.That(_manchkin.WornArmor, Is.Null);
            Assert.That(_manchkin.WornShoes, Is.Null);
            Assert.That(_manchkin.Hands.LeftHand, Is.Null);
            Assert.That(_manchkin.Hands.RightHand, Is.Null);
        });
        Assert.Multiple(() =>
        {
            Assert.That(_manchkin.Descriptions, Is.Empty);
            Assert.That(_manchkin.HasMercenary, Is.EqualTo(false));
        });
    }

    [Test]
    public void Manchkin_ChangeGender_Works()
    {
        var manchkinMale = new Manchkin(Genders.MALE);
        var manchkinFemale = new Manchkin(Genders.FEMALE);

        manchkinMale.ChangeGender();
        manchkinFemale.ChangeGender();

        Assert.Multiple(() =>
        {
            Assert.That(manchkinMale.Gender, Is.EqualTo(Genders.FEMALE));
            Assert.That(manchkinFemale.Gender, Is.EqualTo(Genders.MALE));
        });
    }

    [Test]
    public void Manchkin_ChangeRace_Works()
    {
        var elf = new Elf();
        var human = new Human();

        Assert.Multiple(() =>
        {
            Assert.That(_manchkin.Race, Is.InstanceOf<Human>());
            Assert.That(_manchkin.Descriptions, Is.EqualTo(human.Descriptions));
        });

        _manchkin.Race = elf;

        Assert.Multiple(() =>
        {
            Assert.That(_manchkin.Race, Is.InstanceOf<Elf>());
            Assert.That(_manchkin.Descriptions, Is.EqualTo(elf.Descriptions));
        });
    }

    [Test]
    public void Manchkin_ChangeClass_Works()
    {
        var thief = new Thief();
        var nobody = new Nobody();

        Assert.Multiple(() =>
        {
            Assert.That(_manchkin.Class, Is.InstanceOf<Nobody>());
            Assert.That(_manchkin.Descriptions, Is.EqualTo(nobody.Descriptions));
        });

        _manchkin.Class = thief;

        Assert.Multiple(() =>
        {
            Assert.That(_manchkin.Class, Is.InstanceOf<Thief>());
            Assert.That(_manchkin.Descriptions, Is.EqualTo(thief.Descriptions));
        });
    }

    [Test]
    public void Manchkin_ToDie_HeIsDead()
    {
        Assert.That(_manchkin.IsDead, Is.False);

        _manchkin.ToDie();

        Assert.That(_manchkin.IsDead, Is.True);
    }
}