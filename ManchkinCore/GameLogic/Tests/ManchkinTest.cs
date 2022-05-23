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
        Assert.IsNull(manchkin.WornHat);
        Assert.IsNull(manchkin.WornArmor);
        Assert.IsNull(manchkin.WornShoes);
        Assert.IsNull(manchkin.Hands.LeftHand);
        Assert.IsNull(manchkin.Hands.RightHand);
        Assert.IsEmpty(manchkin.Descriptions);
        Assert.AreEqual(false,manchkin.HasMercenary);
    }

    [Test]
    public void Manchkin_GetLevel_IncreasesLevel()
    {
        var manchkin = new Manchkin(Genders.FEMALE);
        var originLevel = manchkin.Level;
        
        manchkin.GetLevel();
        
        Assert.Greater(manchkin.Level, originLevel);
    }

    [Test]
    public void Manchkin_GetLevel_IncreasesDamage()
    {
        var manchkin = new Manchkin(Genders.FEMALE);
        var originDamage = manchkin.Damage;
        
        manchkin.GetLevel();
        
        Assert.Greater(manchkin.Damage, originDamage);
    }

    public void Manchkin_GetLevel_HasUpperBound10()
    {
        var manchkin = new Manchkin(Genders.MALE);
        for (var i = 0; i < 10; i++)
            manchkin.GetLevel();
        var originLevel = manchkin.Level;
        
        manchkin.GetLevel();
        
        Assert.AreEqual(originLevel, manchkin.Level);
    }

    [Test]
    public void Manchkin_ToDie_LosesAllStuff()
    {
        var manchkin = new Manchkin(Genders.MALE);
        for (var i = 0; i < 9; i++)
            manchkin.GetLevel();
        manchkin.TakeStuff(new HornedHelmet());
        manchkin.TakeStuff(new LeatherArmor());
        manchkin.TakeStuff(new MightyShoes());
        
        Assert.NotNull(manchkin.WornHat);
        Assert.NotNull(manchkin.WornArmor);
        Assert.NotNull(manchkin.WornShoes);
        // а что он типа вообще брать может я не шарю
        
        manchkin.ToDie();
        
        Assert.IsEmpty(manchkin.SmallStuffs);
        Assert.IsEmpty(manchkin.HugeStuffs);
        Assert.IsNull(manchkin.WornHat);
        Assert.IsNull(manchkin.WornArmor);
        Assert.IsNull(manchkin.WornShoes);
    }
}