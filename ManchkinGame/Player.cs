using System;
using System.Collections.Generic;
using ManchkinCore.Enums.Accessory;
using ManchkinCore.GameLogic.Implementation;
using ManchkinCore.Interfaces;

namespace ManchkinGame;

public class Player : PlayerPrototipe
{
    public Player(string name, Genders gender)
    {
        Name = name;
        Manchkin = new Manchkin(gender);
        CurrentFeatures = PlayerPossibilities.Always;
        CurrentFeatures.AddRange(PlayerPossibilities.AlwaysButNotInFight);
    }
}