using System;
using System.Collections.Generic;

public class Board
{
    private const int NUM_CARDS_IN_PLAY = 9;
    private const int PAIR_SUM = 11;

    private List<Card> cardsInPlay = new List<Card>();
    private Deck deck;

    // Constructor to initialize the board with a new shuffled deck
    public Board()
    {
        deck = new Deck();
        deck.Shuffle();
        DealCards();
    }

    // property to access cards in play
    public List<Card> CardsInPlay
    {
        get { return cardsInPlay; }
    }

    private void DealCards()
    {

    }

    public bool ReplaceCard(int position)
    {

    }

    
}
