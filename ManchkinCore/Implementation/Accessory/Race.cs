using ManchkinCore.Interfaces;

namespace ManchkinCore.Implementation;

public abstract class Race: IRaceAndClass
{
    public abstract void FirstOption();
    public abstract void SecondOption();
}

public class Elf : Race{
    public override void FirstOption()
    {
        throw new NotImplementedException();
    }

    public override void SecondOption()
    {
        throw new NotImplementedException();
    }
}

public class Human : Race
{
    public override void FirstOption()
    {
        throw new NotImplementedException();
    }

    public override void SecondOption()
    {
        throw new NotImplementedException();
    }
}

public class Dwarf : Race{
    public override void FirstOption()
    {
        throw new NotImplementedException();
    }

    public override void SecondOption()
    {
        throw new NotImplementedException();
    }
}
public class Halfling : Race{
    public override void FirstOption()
    {
        throw new NotImplementedException();
    }

    public override void SecondOption()
    {
        throw new NotImplementedException();
    }
}

//TODO: прописать особенности рас