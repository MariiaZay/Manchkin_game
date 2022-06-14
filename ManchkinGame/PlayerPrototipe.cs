using System.Collections.Generic;
using System.Linq;
using ManchkinCore.Interfaces;

namespace ManchkinGame;

public abstract class PlayerPrototipe
{
    public string Name { get; protected set; }
    public IManchkin Manchkin { get; protected set; }

    public List<string> CurrentFeatures { get; protected set; }

    public void AddFeatures(List<string> features)
    {
        foreach (var feature in features.Where(feature => !CurrentFeatures.Contains(feature)))
        {
            CurrentFeatures.Add(feature);
        }
    }
    

    public void RemoveFeatures(List<string> features)
    {
        foreach (var feature in features.Where(feature => CurrentFeatures.Contains(feature)))
        {
            CurrentFeatures.Remove(feature);
        }
    }
}