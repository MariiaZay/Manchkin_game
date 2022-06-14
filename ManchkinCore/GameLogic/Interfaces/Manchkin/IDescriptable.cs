﻿namespace ManchkinCore.Interfaces;

public interface IDescriptable
{
    public List<string> Descriptions { get; }
    public string TextRepresentation { get; }
}