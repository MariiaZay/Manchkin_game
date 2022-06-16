using ManchkinCore.Enums;
using ManchkinCore.Enums.Accessory;
using ManchkinCore.GameLogic.Implementation;
using ManchkinCore.GameLogic.Implementation.Factories;
using ManchkinCore.Implementation;
using ManchkinCore.Interfaces;
using NUnit.Framework;

namespace Tests.ManchkinTests;

public class SuperManchkinTests
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
    public void BecomeSuperManchkin_WithoutSecondClass_BecameAndUpdateSpecialParameters()
    {
        _manchkin.BecameSuperManchkin();

        Assert.Multiple(() =>
        {
            Assert.That(_manchkin.IsSuperManchkin, Is.True);
            Assert.That(_manchkin.SuperManchkin.HalfType, Is.EqualTo(HalfTypes.SINGLE_CLEAN));
            Assert.That(_manchkin.SuperManchkin.SecondClass, Is.Null);
        });
    }

    [Test]
    public void BecomeSuperManchkin_WithSecondClass_BecameAndUpdateSpecialParameters()
    {
        var desc = _manchkin.Descriptions;
        var thief = new Thief();
        desc.AddRange(thief.Descriptions);

        _manchkin.BecameSuperManchkin(thief);

        Assert.Multiple(() =>
        {
            Assert.That(_manchkin.IsSuperManchkin, Is.True);
            Assert.That(_manchkin.SuperManchkin.HalfType, Is.EqualTo(HalfTypes.BOTH));
            Assert.That(_manchkin.SuperManchkin.SecondClass, Is.InstanceOf<Thief>());
            Assert.That(_manchkin.Descriptions, Is.EqualTo(desc));
        });
    }

    [Test]
    public void RefuseSuperManchkin_WithoutSecondClass_Refuse()
    {
        _manchkin.BecameSuperManchkin();
        _manchkin.RefuseSuperManchkin();

        Assert.That(_manchkin.IsSuperManchkin, Is.False);
    }

    [Test]
    public void RefuseSuperManchkin_WithSecondClass_RefuseAndUpdateSpecialParameters()
    {
        var manchkinDesc = _manchkin.Descriptions.ToArray();
        var thief = new Thief();

        _manchkin.BecameSuperManchkin(thief);
        _manchkin.RefuseSuperManchkin();

        Assert.Multiple(() =>
        {
            Assert.That(_manchkin.IsSuperManchkin, Is.False);
            Assert.That(_manchkin.Descriptions, Is.EqualTo(manchkinDesc.ToList()));
        });
    }

    [Test]
    public void RefuseSuperManchkin_ManchkinIsNotSuperManchkin_NothingHappens()
    {
        Assert.That(_manchkin.IsSuperManchkin, Is.False);

        _manchkin.RefuseSuperManchkin();

        Assert.That(_manchkin.IsSuperManchkin, Is.False);
    }
}