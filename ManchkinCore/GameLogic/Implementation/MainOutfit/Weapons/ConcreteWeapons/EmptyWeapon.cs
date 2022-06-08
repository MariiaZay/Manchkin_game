using ManchkinCore.Enums.Accessory;
using ManchkinCore.Interfaces;

namespace ManchkinCore.Implementation;

public class EmptyWeapon: Weapon
{
    public EmptyWeapon()
    {
        TextRepresentation = "";
    }
    public override bool CanBeUsed(IRace? race)
    {
        throw new NotImplementedException();
    }

    public override bool CanBeUsed(IClass? _class)
    {
        throw new NotImplementedException();
    }

    public override bool CanBeUsed(Genders gender)
    {
        throw new NotImplementedException();
    }
}