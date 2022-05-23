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

    [Test]
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
    public void Manchin_BecomeSuperManchkin_Became()
    {
        var manchkin = new Manchkin(Genders.FEMALE);
        
        manchkin.BecameSuperManchkin();
        
        Assert.True(manchkin.IsSuperManchkin);
    }
    
    public void Manchin_BecomeHalfblood_Became()
    {
        var manchkin = new Manchkin(Genders.FEMALE);
        
        manchkin.BecameHalfBlood();
        
        Assert.True(manchkin.IsHalfBlood);
    }

    [Test]
    public void Manchkin_RefuseHalfblood_Works()
    {
        var manchkin = new Manchkin(Genders.MALE);
        manchkin.BecameHalfBlood();

        manchkin.RefuseHalfblood();
        
        Assert.False(manchkin.IsHalfBlood);
    }
    
    [Test]
    public void Manchin_GetMercenary_GetMercenaryTrue()
    {
        var manchkin = new Manchkin(Genders.FEMALE);
        
        manchkin.GetMercenary();
        
        Assert.True(manchkin.HasMercenary);
    }
    
    [Test]
    public void Manchkin_ChangeGender_Works()
    {
        var trapikFromRtf = new Manchkin(Genders.MALE);
        
        trapikFromRtf.ChangeGender();
        
        Assert.AreEqual(Genders.FEMALE, trapikFromRtf.Gender);
    }

    [Test]
    public void Manchkin_ChangeRace_Works()
    {
        var manchkin = new Manchkin(Genders.MALE);

        manchkin.Race = new Elf();
        
        Assert.IsInstanceOf<Elf>(manchkin.Race);
    }

    [Test]
    public void Manchkin_ChangeClass_Works()
    {
        var manchkin = new Manchkin(Genders.FEMALE);
        
        manchkin.Class = new Thief();
        
        Assert.IsInstanceOf<Thief>(manchkin.Class);
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