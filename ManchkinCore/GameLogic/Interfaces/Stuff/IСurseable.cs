namespace ManchkinCore.Interfaces;

public interface IСurseable
{
    public List<ICurse> ActiveCurses { get; }
    public void ApplyCurses();
}