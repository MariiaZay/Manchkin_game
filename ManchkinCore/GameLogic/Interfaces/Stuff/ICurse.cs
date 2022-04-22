
using ManchkinCore.Enums.Aspects;

namespace ManchkinCore.Interfaces;

public interface ICurse
{
    public int TimeOfAction { get; }
    public ActionTime ActionTime { get; }
    public void SendCurse(IManchkin hero);
}