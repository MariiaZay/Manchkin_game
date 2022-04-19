using ManchkinCore.CardEnums.Aspects;

namespace ManchkinCore.Interfaces;

public class IDescriptor
{
    public string Text { get; }
    public DescriptorFlags Flag { get; }
}