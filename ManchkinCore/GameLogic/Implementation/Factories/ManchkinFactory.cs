using ManchkinCore.CardEnums.Accessory;
using ManchkinCore.GameLogic.Interfaces.Accessory;
using ManchkinCore.GameLogic.Interfaces.Manchkin;

namespace ManchkinCore.GameLogic.Implementation.Factories;

public class ManchkinFactory
{
    private readonly IClass _initialClass;
    private readonly IRace _initialRace;
    private readonly IHands _hands;
    private readonly MercenaryFactory _mercenaryFactory;
    private readonly HalfbloodFactory _halfbloodFactory;
    private readonly SuperManchkinFactory _superManchkinFactory;

    private Genders _gender;
    
    public ManchkinFactory(IClass initialClass, IRace initialRace, IHands hands,
        MercenaryFactory mercenaryFactory, HalfbloodFactory halfbloodFactory, SuperManchkinFactory superManchkinFactory)
    {
        _initialClass = initialClass;
        _initialRace = initialRace;
        _hands = hands;

        _mercenaryFactory = mercenaryFactory;
        _halfbloodFactory = halfbloodFactory;
        _superManchkinFactory = superManchkinFactory;
    }

    public ManchkinFactory SetGender(Genders gender)
    {
        _gender = gender;
        return this;
    }

    public Manchkin.Manchkin Build() => new Manchkin.Manchkin(_initialClass, _initialRace, _hands,
        _mercenaryFactory, _halfbloodFactory, _superManchkinFactory, _gender);
}