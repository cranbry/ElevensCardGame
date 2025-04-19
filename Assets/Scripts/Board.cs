using System;
using System.Collections.Generic;

public class Board
{
    private const int NUM_CARDS_IN_PLAY = 9;
    private const int PAIR_SUM = 11;

    private List<Card> cardsInPlay = new List<Card>();
    private Deck deck;

    // constructor lil bro initializes the board with a new shuffled deck
    public Board()
    {
        deck = new Deck();
        deck.Shuffle();
        DealCards();
    }

    // prop to access cards in play
    public List<Card> CardsInPlay
    {
        get { return cardsInPlay; }
    }

    // prop to get remaining cards in deck
    public int RemainingCards
    {
        get { return deck.RemainingCards; } // accessing cards property ->> was issue DONT FORGET TO CHANGE
    }

    // dealing initial cards to the board
    public void DealCards()
    {
        // clearing existing cards
        cardsInPlay.Clear();

        // dealing cards until we have this under or the deck is empty
        while (cardsInPlay.Count < NUM_CARDS_IN_PLAY && deck.RemainingCards > 0)
        {
            Card card = deck.TakeTopCard();
            if (card != null)
            {
                card.FlipOver(); // making cards face up when on the board
                cardsInPlay.Add(card);
            }
        }
    }

    // dealing replacement card to pos
    public bool ReplaceCard(int position)
    {
        if (position < 0 || position >= cardsInPlay.Count)
            return false;

        if (deck.RemainingCards == 0)
            return false;

        Card newCard = deck.TakeTopCard();
        if (newCard == null)
            return false;

        newCard.FlipOver(); // making the card face up
        cardsInPlay[position] = newCard;
        return true;
    }

    // checking if the selected positions are sum 11
    public bool IsPairSumEleven(List<int> selectedPositions)
    {
        // needs  2 cards for a sum 
        if (selectedPositions == null || selectedPositions.Count != 2)
            return false;

        int firstPosition = selectedPositions[0];
        int secondPosition = selectedPositions[1];

        // validatng positions are in range
        if (firstPosition < 0 || firstPosition >= cardsInPlay.Count ||
            secondPosition < 0 || secondPosition >= cardsInPlay.Count)
            return false;

        // getting cards and check and sum
        Card firstCard = cardsInPlay[firstPosition];
        Card secondCard = cardsInPlay[secondPosition];

        return firstCard.GetValue() + secondCard.GetValue() == PAIR_SUM;
    }

    // checking if the selected positions are JQK 
    public bool IsJQKSet(List<int> selectedPositions)
    {
        // needs  3 cards for a JQK
        if (selectedPositions == null || selectedPositions.Count != 3)
            return false;

        bool hasJack = false;
        bool hasQueen = false;
        bool hasKing = false;

        // checking each selected position
        foreach (int position in selectedPositions)
        {
            // validating position is in range
            if (position < 0 || position >= cardsInPlay.Count)
                return false;

            // checking card rank
            Card card = cardsInPlay[position];

            if (card.Rank == Rank.Jack) hasJack = true;
            if (card.Rank == Rank.Queen) hasQueen = true;
            if (card.Rank == Rank.King) hasKing = true;
        }

        // needs to be all three face cards
        return hasJack && hasQueen && hasKing;
    }

    // checking if the selected cards form a valid set 11 or JQK
    public bool IsValidSelection(List<int> selectedPositions)
    {
        // 2 cards check for sum of 11
        if (selectedPositions != null && selectedPositions.Count == 2)
        {
            return IsPairSumEleven(selectedPositions);
        }
        // 3 cards check for JQK
        else if (selectedPositions != null && selectedPositions.Count == 3)
        {
            return IsJQKSet(selectedPositions);
        }

        return false;
    }

    // remvoing cards at the selected positions and replace with new cards from the deck
    public bool RemoveAndReplace(List<int> positions)
    {
        if (!IsValidSelection(positions))
            return false;

        // sortging positions in desc order to avoid index shifting when removing
        positions.Sort();
        positions.Reverse();

        foreach (int position in positions)
        {
            ReplaceCard(position);
        }

        return true;
    }

    // checking if the game is over (no valid moves possible)
    public bool IsGameOver()
    {
        // we check if there are any valid moves on the current board
        List<List<int>> possibleSelections = new List<List<int>>();

        // then generate all possible pairs
        for (int i = 0; i < cardsInPlay.Count; i++)
        {
            for (int j = i + 1; j < cardsInPlay.Count; j++)
            {
                possibleSelections.Add(new List<int> { i, j });
            }
        }

        // generate all possible triplets
        for (int i = 0; i < cardsInPlay.Count; i++)
        {
            for (int j = i + 1; j < cardsInPlay.Count; j++)
            {
                for (int k = j + 1; k < cardsInPlay.Count; k++)
                {
                    possibleSelections.Add(new List<int> { i, j, k });
                }
            }
        }

        // checking each possible selection
        foreach (List<int> selection in possibleSelections)
        {
            if (IsValidSelection(selection))
            {
                return false; // there is valid move dont end
            }
        }

        // valid moves on the board are cooked game is over
        return true;
    }
}