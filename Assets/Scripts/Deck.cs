using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;
using System.Threading.Tasks;


public class Deck
{
    List<Card> cards = new List<Card>();
    //Deck Constructor
    public Deck()
    {
        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
        {
            foreach (Rank rank in Enum.GetValues(typeof(Rank)))
            {
                // create new card and add to back
                Card newCard = new Card(suit, rank);
                cards.Add(newCard);
            }
        }
    }

    //Implement a property to get Cards
    public List<Card> Cards
    {
        get
        {
            return cards;
        }
    }

    //Takes top card from deck (if deck is not empty, otherwise return null)
    public Card TakeTopCard()
    {
        if (cards.Count == 0)
        {
            return null;
        }
        else
        {
            // take top card
            Card topCard = cards[0];
            // remove it from deck
            cards.RemoveAt(0);
            return topCard;
        }
    }

    public Card DealCard()
    {
        return TakeTopCard();
    }

    //Shuffle Method
    public void Shuffle()
    {
        // random number generator:
        Random rndmCard = new Random();

        // list to hold shuffeled deck:
        List<Card> suffeledDeck = new List<Card>();

        while (cards.Count > 0)
        {
            // taking random card from the original deck
            int rndmIndex = rndmCard.Next(cards.Count);

            // taking the random card and adding it to the shuffeled deck
            Card selectedCard = cards[rndmIndex];
            suffeledDeck.Add(selectedCard);

            // removing that random card from the original deck
            cards.RemoveAt(rndmIndex);

        }

    }

    //Cut method
    public void Cut(int index)
    {
        // checking for invalid index
        if (index <= 0 || index >= cards.Count)
        {
            return;
        }
        // storing the cut portion in topPortion.
        List<Card> topPortion = cards.GetRange(0, index);
        // deleting the portion
        cards.RemoveRange(0, index);
        // adding the portion back to the back
        cards.AddRange(topPortion);
    }

    public int getRemainingCards()
    {
        return cards.Count;
    }
    public bool isEmpty()
    {
        return cards.Count == 0;
    }
}

