using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Card
{
    //Fields, example: Rank rank;
    private Rank rank;
    private Suit suit;
    private bool faceUp;
    private int value;

    //Card Constructor
    public Card(Suit suit, Rank rank)
    {
        this.suit = suit;
        this.rank = rank;
        this.faceUp = false; // making cards start face down by default
        this.value = CalculateValue(rank);
    }

    //Define properties for all above fields
    //code example: public Suit Suit { get { return suit; } }
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
    public bool FaceUp
    {
        get
        {
            return faceUp;
        }
    }

    public void FlipOver()
    {
        // if face down then face up
        if (faceUp == false)
        {
            faceUp = true;
        }
        // if face up then down
        else
        {
            faceUp = false;
        }
    }

}