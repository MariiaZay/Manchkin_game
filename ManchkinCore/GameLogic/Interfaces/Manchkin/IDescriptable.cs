using ManchkinCore.CardEnums.Aspects;

namespace ManchkinCore.Interfaces;

public interface IDescriptable
{
    public List<IDescriptor> Descriptor { get; }
    public void AddDescriptions(List<DescriptorFlags> args); 
    public void RemoveDescriptions(List<DescriptorFlags> args);
}