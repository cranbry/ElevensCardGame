using System;

public class Card
{
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
        int rankValue = (int)rank;

        if (rankValue >= (int)global::Rank.Jack) // for Jack, Queen, and King // had to add explicit cast to int for comparison
            return 10;

        return rankValue + 1; // ace = 1, two = 2...
    }

    public void FlipOver()
    {
        isFaceUp = !isFaceUp;
    }

    // overriding ToString to debug why is NOT WORKINGGGGGGG Im slowly losing my mind
    public override string ToString()
    {
        return $"{rank} of {suit}";
    }
}