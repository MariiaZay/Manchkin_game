using ManchkinCore.Enums.Accessory;
using ManchkinCore.Implementation;
using NUnit.Framework;

namespace ManchkinCore.GameLogic.Implementation;

[TestFixture]
public class ManchkinTest
{
    [Test]
    public void Manchkin_Create_PropertiesValuesAreAdequate()
    {
        var manchkin = new Manchkin(Genders.MALE);

        Assert.AreEqual(Genders.MALE, manchkin.Gender);
        Assert.AreEqual(1, manchkin.Level);
        Assert.AreEqual(manchkin.Damage, manchkin.Level);
        Assert.IsInstanceOf<Human>(manchkin.Race);
        Assert.AreEqual(false, manchkin.IsDead);
        Assert.AreEqual(false, manchkin.IsHalfBlood);
        Assert.AreEqual(false, manchkin.IsSuperManchkin);
        Assert.IsEmpty(manchkin.Mercenaries);
        Assert.AreEqual(5, manchkin.CardsCount);
        Assert.IsNull(manchkin.WornArmor);
        Assert.IsNull(manchkin.Hands.LeftHand);
        Assert.IsNull(manchkin.Hands.RightHand);
        Assert.IsEmpty(manchkin.Descriptions);
        Assert.AreEqual(false,manchkin.HasMercenary);
        
    }
}