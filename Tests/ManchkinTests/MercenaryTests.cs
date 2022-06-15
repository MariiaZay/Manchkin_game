using ManchkinCore.Enums.Accessory;
using ManchkinCore.GameLogic.Implementation;
using ManchkinCore.Interfaces;
using NUnit.Framework;

namespace Tests.ManchkinTests;

[TestFixture]
public class MercenaryTests
{
    private IManchkin _manchkin = null!;

    [SetUp]
    public void SetUp()
    {
        _manchkin = new Manchkin(Genders.MALE);
    }

    [Test]
    public void Manchkin_GetMercenary_GetMercenaryTrue()
    {
        _manchkin.GetMercenary();

        Assert.That(_manchkin.HasMercenary, Is.True);
    }
}