using ManchkinCore.CardEnums.Accessory;
using ManchkinCore.CardEnums.Aspects;
using ManchkinCore.GameLogic.Implementation.Accessory.Races;
using ManchkinCore.GameLogic.Interfaces.Accessory;

namespace ManchkinCore.GameLogic.Implementation.MainOutfit.Hats.ConcreteHats;

public class BandanaOfBastartism : Hat
{
    public BandanaOfBastartism()
    {
        Price = 400;
        Damage = 3;
        Weight = Bulkiness.SMALL;
        Fullness = Arms.NO;
        Descriptions = new List<string>();
        FlushingBonus = 0;
        TextRepresentation = "Бандана сволочизма";
    }

    public override bool CanBeUsed(IRace? race) => race is Human || Cheat;

    public override bool CanBeUsed(IClass? _class) => true;

    public override bool CanBeUsed(Genders gender) => true;
}