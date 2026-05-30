using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardExtension {

    public static Card FindCard(this Card[] cards, string name)
    {
        for (int i = 0; i < cards.Length; i++)
        {
            if (cards[i] != null && cards[i].Name == name)
                return cards[i];
        }

        return null;
    }

    public static Card FindCard(this Card[] cards, Card.Properties properties)
    {
        for (int i = 0; i < cards.Length; i++)
        {
            if (cards[i] != null && cards[i].Property == properties)
                return cards[i];
        }

        return null;
    }

    public static Card FindCard(this Card[] cards, CardManager.CardNames name)
    {
        for (int i = 0; i < cards.Length; i++)
        {
            if (cards[i] != null && cards[i].Name == name.ToString())
                return cards[i];
        }

        return null;
    }

    public static T GetCard<T>(this Card card) where T : Card
    {
        return card as T;
    } 
}
