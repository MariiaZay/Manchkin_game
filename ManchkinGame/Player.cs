using ManchkinCore.Enums.Accessory;
using ManchkinCore.GameLogic.Implementation;
using ManchkinCore.Interfaces;
using Ninject;

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

    public Player(string name, IManchkin manchkin)
    {
        _name = name;
        _manchkin = manchkin;
    }
}