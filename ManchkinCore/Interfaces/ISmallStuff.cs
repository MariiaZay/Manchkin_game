using ManchkinCore.Enums.Gears;

namespace ManchkinCore.Interfaces;

public interface ISmallStuff
{
    public Stuffs Stuff { get; set; }
    public DisposableStuff DisposableStuff { get; set; }
}