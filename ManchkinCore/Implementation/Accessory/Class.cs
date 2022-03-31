using ManchkinCore.Interfaces;

namespace ManchkinCore.Implementation;

public abstract class Class : IRaceAndClass
{
    public abstract void FirstOption();
    public abstract void SecondOption();
}

public class Cleric: Class
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

public class Warrior: Class
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

public class Thief: Class
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

public class Wizard: Class
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

public class Nobody : Class
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

//TODO: прописать особенности классов