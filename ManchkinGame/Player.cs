using System;
using System.Collections.Generic;
using ManchkinCore.GameLogic.Implementation;
using ManchkinCore.GameLogic.Interfaces.Manchkin;
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