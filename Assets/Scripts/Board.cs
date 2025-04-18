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

    // get remaining cards in deck
    public int RemainingCards
    {
        get { return deck.RemainingCards; }
    }

    // deal first cards to the board
    public void DealCards()
    {
        // Clear any existing cards
        cardsInPlay.Clear();

        // deal cards until we have NUM_CARDS_IN_PLAY or the deck is empty
        while (cardsInPlay.Count < NUM_CARDS_IN_PLAY && deck.RemainingCards > 0)
        {
            Card card = deck.TakeTopCard();
            if (card != null)
            {
                card.FlipOver(); // makes cards face up when on the board
                cardsInPlay.Add(card);
            }
        }
    }

    // dealing replacement cards to specific positions
    public bool ReplaceCard(int position)
    {
        if (position < 0 || position >= cardsInPlay.Count)
            return false;

        if (deck.RemainingCards == 0)
            return false;

        Card newCard = deck.TakeTopCard();
        if (newCard == null)
            return false;

        newCard.FlipOver(); // makes the card face up
        cardsInPlay[position] = newCard;
        return true;
    }

    // checking if selected cards add up to the appropriate sum 11
    public bool IsSumOfEleven(List<int> selectedPositions)
    {
        int firstSelected = selectedPositions[0];
        int secondSelected = selectedPositions[1];
        int selectedSum = 0;

        // checking for empty or invalid selection
        if (selectedPositions == null || selectedPositions.Count != 2)
        {
            return false;
        }
        // checking if positions in range
        if ((firstSelected < 0 || firstSelected >= cardsInPlay.Count) ||
        (secondSelected < 0 || secondSelected >= cardsInPlay.Count))
        {

            return false;
        }

        Card firstCard = cardsInPlay[firstSelected];
        Card secondCard = cardsInPlay[secondSelected];

        selectedSum = firstCard.GetValue() + secondCard.GetValue();

        // returns true if valid sum of 11
        return PAIR_SUM == selectedSum;
    }

    // checking for J,K,Q
    public bool IsJQKSet(List<int> selectedPositions)
    {
        bool hasJack = false;
        bool hasQueen = false;
        bool hasKing = false;

        // checking for empty or invalid selection
        if (selectedPositions == null || selectedPositions.Count != 3)
        {
            return false;
        }

        // checking each selected position
        foreach (int position position in selectedPositions) {

            // checking if positions in range
            if (position < 0 || position >= cardsInPlay)
            {
                return false;
            }

            // checking card rank
            Card card = cardsInPlay[position];

            if (card.Rank == Rank.Jack)
            {
                hasJack = true;
            }
            else if (card.Rank == Rank.Queen)
            {
                hasQueen = true;
            }
            else if (card.Rank == Rank.King)
            {
                hasKing = true;
            }
        }

        // all have to be true
        return hasJack && hasQueen && hasKing;
    }

    // checking if selected cards are valid pairs can be sum of 11 or JQK
    public bool isValidSelection(List<int> selectedPositions)
    {
        // for 2 cards we check for sum of 11
        if (selectedPositions != null && selectedPositions.Count == 2)
        {
            return IsSumOfEleven(selectedPositions);
        }
        // for 3 cards we check for JQK set
        else if (selectedPositions != null && selectedPositions.Count == 3)
        {
            return IsJQKSet(selectedPositions);
        }

        return false;
    }
}

