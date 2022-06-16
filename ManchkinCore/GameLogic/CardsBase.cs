using ManchkinCore.GameLogic.Implementation.Gears.Stuffs;
using ManchkinCore.Implementation;
using ManchkinCore.Implementation.Gears;
using ManchkinCore.Interfaces;


namespace ManchkinCore.GameLogic;

public class CardsBase
{
    public List<IDescriptable> Races => _cards
        .Where(x => x is IRace)
        .ToList();
    
    public List<IDescriptable> Classes => _cards
        .Where(x => x is IClass)
        .ToList();
    
    public List<IDescriptable> SmallStuffs => _cards
        .Where(x => x is SmallStuff)
        .ToList();
    
    public List<IDescriptable> HugeStuffs => _cards
        .Where(x => x is HugeStuff)
        .ToList();
    
    public List<IDescriptable> Armors => _cards
        .Where(x => x is Armor)
        .ToList();
    
    public List<IDescriptable> Hats => _cards
        .Where(x => x is Hat)
        .ToList();
    
    public List<IDescriptable> Shoeses => _cards
        .Where(x => x is Shoes)
        .ToList();
    
    public List<IDescriptable> SingleHandWeapons => _cards
        .Where(x => x is SingleHandWeapon)
        .ToList();
    
    public List<IDescriptable> BothHandWeapons => _cards
        .Where(x => x is BothHandWeapon)
        .ToList();

    private List<IDescriptable> _cards;
    public CardsBase(List<IDescriptable> cards) => _cards = cards;
}