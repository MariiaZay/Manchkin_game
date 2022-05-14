using ManchkinCore.Enums.Accessory;
using ManchkinCore.GameLogic.Implementation;
using ManchkinCore.Interfaces;

namespace ManchkinGame;

public class Player
{
    private string _name;
    public string Name
    {
        get => _name;
    }
    private IManchkin _manchkin;

    public IManchkin Manchkin
    {
        get => _manchkin;
    }

    public Player(string name, Genders gender)
    {
        _name = name;
        _manchkin = new Manchkin(gender);
    }
}