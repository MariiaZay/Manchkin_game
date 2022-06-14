using ManchkinCore.Enums.Accessory;
using ManchkinCore.Implementation;
using ManchkinCore.Interfaces;

namespace ManchkinCore.GameLogic.Implementation.Factories;

public class ManchkinFactory
{
    private IClass _initialClass;
    private IRace _initialRace;
    private IHands _hands;
    private MercenaryFactory _mercenaryFactory;
    private HalfbloodFactory _halfbloodFactory;
    private SuperManchkinFactory _superManchkinFactory;

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

    public Manchkin Build() => new Manchkin(_initialClass, _initialRace, _hands,
        _mercenaryFactory, _halfbloodFactory, _superManchkinFactory, _gender);
}