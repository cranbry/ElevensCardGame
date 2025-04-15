using System;

public class Card
{
    //Fields, example: Rank rank;
    private Rank rank;
    private Suit suit;
    private bool isFaceUp;

    //Card Constructor
    public Card(Suit suit, Rank rank)
    {
        this.suit = suit;
        this.rank = rank;
        this.isFaceUp = false; // making cards start face down by default
    }

    // properties for all above fields
    public Suit Suit
    {
        get
        {
            return suit;
        }
    }
    public Rank Rank
    {
        get
        {
            return rank;
        }
    }
    public bool IsFaceUp
    {
        get
        {
            return isFaceUp;
        }
    }

    // get the value of the card
    public int GetValue()
    {
        if (rank >= Rank.Jack) // for Jack, Queen, and King
        {
            return 10;
        }
        else
        {
            return rank + 1; // ace = 1, two = 2...
        }
    }

    public void FlipOver()
    {
        isFaceUp = !isFaceUp;
    }

}