using System;
using System.Collections.Generic;
using ManchkinCore.Enums.Accessory;
using ManchkinCore.GameLogic.Implementation;
using ManchkinCore.Interfaces;
using Ninject;

namespace ManchkinGame;

public class Player : PlayerPrototipe
{
    public string Name { get; }

    public IManchkin Manchkin { get; }

    public Player(string name, IManchkin manchkin)
    {
        Name = name;
        Manchkin = manchkin;
        CurrentFeatures = PlayerPossibilities.Always;
        CurrentFeatures.AddRange(PlayerPossibilities.AlwaysButNotInFight);
    }

}