using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : SingletonGameObject<CardManager> {
    public enum CardNames
    {
        None,

        ReflectCard,
        RevolutionCard,

        PointAttackCard,
        SplitAttackCard,
        SectorFormAttackCard,
        AllDirectionsAttackCard,
        Circular_coneAttackCard,
        MotifAttackCard,
        SpectrumAttackCard,
        RhombusAttackCard,

        HandGun,
        AssaultRifle,
        ShotGun,
        LaserGun,

        FlameCard,
        FreezeCard,
        ThunderCard,
    }

    private Card[] cards;   //배열로 각각의 클래스 초기화

    public Card GetCard(int number)
    {
        for (int i = 0; i < pCards.Length; i++)
        {
            if (number == pCards[i].Number)
                return pCards[i];
        }

        return null;
    }

    public Card GetCard(string name)
    {
        for (int i = 0; i < pCards.Length; i++)
        {
            if (pCards[i] != null && pCards[i].Name == name)
                return pCards[i];
        }

        return null;
    }

    public Card GetCard(CardNames type)
    {
        for (int i = 0; i < pCards.Length; i++)
        {
            if (pCards[i] != null && pCards[i].Name == type.ToString())
                return pCards[i];
        }

        return null;
    }


    #region Property
    public Card[] Cards
    {
        get
        {
            return cards;
        }
    }

    private Card[] pCards
    {
        get
        {
            if (cards == null)
            {
                cards = new Card[]
                {
                 new ReflectCard(),
                 new RevolutionCard(),

                 new PointAttackCard(),
                 new SplitAttackCard(),
                 new SectorFormAttackCard(),
                 new AllDirectionsAttackCard(),
                 new Circular_coneAttackCard(),
                 new MotifAttackCard(),
                 new SpectrumAttackCard(),
                 new RhombusAttackCard(),

                 new HandGun(),
                 new AssaultRifle(),
                 new ShotGun(),
                 new LaserGun(),

                 new FlameCard(),
                 new FreezeCard(),
                 new ThunderCard(),
                };

                for (int i = 0; i < cards.Length; i++)
                {
                    cards[i].SetCardNumber();
                }
            }

            return cards;
        }
        set
        {
            cards = value;
        }
    }
#endregion
}
