using ManchkinCore.Enums.Accessory;
using ManchkinCore.GameLogic.Implementation;
using ManchkinCore.Implementation;
using ManchkinCore.Interfaces;
using NUnit.Framework;

namespace Tests.ManchkinTests;

public class ClassTests
{
    private IManchkin _manchkin = null!;

    [SetUp]
    public void SetUp()
    {
        _manchkin = new Manchkin(Genders.MALE);
    }

    [Test]
    public void ChangeClassWhenOriginClassIsNobody_UpdateClassAndDescriptions()
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
    public void ChangeClassWhenOriginClassIsNotNobody_UpdateClassAndDescriptions()
    {
        var thief = new Thief();

        _manchkin.Class = new Cleric();
        _manchkin.Class = thief;

        Assert.Multiple(() =>
        {
            Assert.That(_manchkin.Class, Is.InstanceOf<Thief>());
            Assert.That(_manchkin.Descriptions, Is.EqualTo(thief.Descriptions));
        });
    }

    [Test]
    public void ChangeClassWhenOriginClassIsNotNobodyAndManchkinIsSuperManchkin_UpdateClassAndSpecialParameters()
    {
        var thief = new Thief();

        _manchkin.Class = new Cleric();
        _manchkin.BecameSuperManchkin();
        _manchkin.Class = thief;

        Assert.Multiple(() =>
        {
            Assert.That(_manchkin.Class, Is.InstanceOf<Thief>());
            Assert.That(_manchkin.Descriptions, Is.EqualTo(thief.Descriptions));
            Assert.That(_manchkin.IsSuperManchkin, Is.False);
        });
    }
}