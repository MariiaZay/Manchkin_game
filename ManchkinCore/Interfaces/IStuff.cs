using ManchkinCore.Enums;
using ManchkinCore.Enums.Accessory;
using ManchkinCore.Implementation;

namespace ManchkinCore.Interfaces;

public interface IStuff
{
    public int Price { get;}
    public int Damage { get;}
    public bool ActiveCheat { get;}
    public IMercenary? Mercenary { get; }
    public Bulkiness Weight { get;}
    public Arms Fullness { get;}

    
    public bool CheckRace(IRaceAndClass race);
    public bool CheckClass(IRaceAndClass _class);
    public bool CheckGender(Genders gender);

    public void SetMercenary(IMercenary? mercenary);
    public void RemoveMercenary();
    public void SetCheat();
    public void RemoveCheat();
}