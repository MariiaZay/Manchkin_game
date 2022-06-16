using ManchkinCore.Enums.Accessory;
using ManchkinCore.GameLogic.Implementation;
using ManchkinCore.GameLogic.Implementation.Factories;
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
    public void ChangeClass_OriginClassIsNobody_UpdateClassAndDescriptions()
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
    public void ChangeClass_OriginClassIsNotNobody_UpdateClassAndDescriptions()
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
    public void ChangeClass_OriginClassIsNotNobodyAndManchkinIsSuperManchkin_UpdateClassAndSpecialParameters()
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