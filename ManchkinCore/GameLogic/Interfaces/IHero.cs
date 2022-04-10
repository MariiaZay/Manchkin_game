using ManchkinCore.Enums.Accessory;
using ManchkinCore.Implementation;

namespace ManchkinCore.Interfaces;

public interface IHero
{
    public int Level { get; }
    public IRace Race { get; }
    public IClass Class { get; }
    public Genders Gender { get; set; }

    public int FlushingBonus { get; }
    public int DebuffOnDiceRolls { get; }
    
    public int Damage { get; }
    public IStuff Armor { get; }
    public IStuff Shoes { get; }
    public IStuff Hat { get; }
    
    public void ChangeRace(IRace race);
    public void ChangeClass(IClass _class);
    public void ChangeGender();
    public void ChangeEquipment(List<IStuff> equipment);
    
    public void LostLevel();
    public void GetLevel();
    
    public void LostRace();
    public void LostClass();

    public void LostHugeStuff(IStuff stuff);
    public void LostSmallStuff(IStuff stuff);
    public void LostMostPowerfulStuff();
    public void LostStuff(IStuff stuff);
    public void LostAllStuffs();
    
    
    public void LostArmor();
    public void LostShoes();
    public void LostHat();
    
    public int SellStuffs(List<IStuff> stuffs);
    

    //TODO: придумать,как сделать пересчет урона
    //TODO: придумать, как надевать шмотки
    //TODO: добавить долгосрочные проклятия
    //TODO: добавить состояние в игре
    //TODO: придумать,как написать руки с оружием
    //TODO: придумать, как лучше хранить шмотки, которыми владеет манчкин (не экипировку!)
    //TODO: придумать, как хранить наемничков
    //TODO: придумать, как хранить "текстовые св-ва манчкина"
}
