using ManchkinCore.Implementation;
using ManchkinCore.Implementation.Gears;
using ManchkinCore.Interfaces;

namespace ManchkinCore.GameLogic;

public static class CardsBase
{
    public static List<IDescriptable> Races = new List<IDescriptable>() { new Dwarf(), new Halfling(), new Elf() };

    public static List<IDescriptable> Classes = new List<IDescriptable>()
    {
        new Cleric(), new Thief(),
        new Warrior(), new Wizard()
    };

    //TODO: сделать DIY - контейнер
    public static List<IDescriptable> SmallStuffs = new List<IDescriptable>()
    {
        new GreatTitle(), new SpikedKnees(),
        new SingingSword(), new Sandwich(), new Cloack(),
        new Pantyhose(), new KneepadsOfAllure()
    };

    public static List<IDescriptable> HugeStuffs = new List<IDescriptable>()
    {
        new Stepladder()
    };

    public static List<IDescriptable> Armors = new List<IDescriptable>()
    {
        new MithrilArmor(),
        new LeatherArmor(),
        new DwarfArmor(),
        new MucousMembrane(),
        new FlamingArmor()
    };

    public static List<IDescriptable> Hats = new List<IDescriptable>()
    {
        new HornedHelmet(), new BandanaOfBastartism(),
        new HatOfPower(), new HelmetOfCourage()
    };

    public static List<IDescriptable> Shoeses = new List<IDescriptable>()
    {
        new MightyShoes(), new SandalsOfProtection(), new ReallyFastBoots()
    };

    public static List<IDescriptable> Weapons = new List<IDescriptable>()
    {
        new Buckler(), new Hammer(), new ProgressiveSword(),
        new BastardSword(), new Chainsaw(), new CheeseGrater(),
        new NapalmStuff(), new Rapier(), new Dagger(), new Mace(),
        new Shild(), new Club(), new Polearm(), new HugeRock(),
        new Pole(), new Bow(), new RatOnStick(), new TubeOfCharm()
    };
}