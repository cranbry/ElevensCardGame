using System;
using System.Collections.Generic;

public class Deck
{
    private List<Card> cards = new List<Card>();

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

    public int RemainingCards
    {
        get { return cards.Count; }
    }

    // property to get access Cards
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

    //Shuffle Method
    public void Shuffle()
    {
        // random number generator:
        Random rngCard = new Random();

        // list to hold shuffled deck:
        List<Card> shuffledDeck = new List<Card>();

        while (cards.Count > 0)
        {
            // taking random card from the original deck
            int rngIndex = rngCard.Next(cards.Count);

            // taking the random card and adding it to the shuffled deck
            Card selectedCard = cards[rngIndex];
            shuffledDeck.Add(selectedCard);

            // removing that random card from the original deck
            cards.RemoveAt(rngIndex);
        }
        // updating original deck with shuffled deck
        cards = shuffledDeck;
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
}

