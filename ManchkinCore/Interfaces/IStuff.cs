using ManchkinCore.Enums;
using ManchkinCore.Enums.Accessory;

namespace ManchkinCore.Interfaces;

public interface IStuff
{
    public int Price { get; set; }
    public int Damage { get; set; }
    public bool ActiveCheat { get; set; }
    public Bulkiness Weight { get; set; }
    public Arms Fullness { get; set; }


    public bool CheckRace(IRaceAndClass race);
    public bool CheckClass(IRaceAndClass race);
    public bool CheckGender(Genders gender);

    public void Drop();
    public void Change();
    public void Present();
    public void Lost(); //TODO: здесь нужно прописать, что мы теряем
    public void Sell();

}