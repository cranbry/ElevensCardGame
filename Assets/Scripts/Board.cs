using System;
using System.Collections.Generic;

public class Board
{
    private const int NUM_CARDS_IN_PLAY = 9;
    private const int PAIR_SUM = 11;

    private List<Card> cardsInPlay = new List<Card>();
    private Deck deck;

    // Constructor - Initialize the board with a new shuffled deck
    public Board()
    {
        deck = new Deck();
        deck.Shuffle();
        DealCards();
    }

    // Property to access cards in play
    public List<Card> CardsInPlay
    {
        get { return cardsInPlay; }
    }

    // Property to get remaining cards in deck
    public int RemainingCards
    {
        get { return deck.RemainingCards; }
    }

    // Deal initial cards to the board
    public void DealCards()
    {
        // Clear any existing cards
        cardsInPlay.Clear();

        // Deal cards until we have NUM_CARDS_IN_PLAY or the deck is empty
        while (cardsInPlay.Count < NUM_CARDS_IN_PLAY && deck.RemainingCards > 0)
        {
            Card card = deck.TakeTopCard();
            if (card != null)
            {
                card.FlipOver(); // Make cards face up when on the board
                cardsInPlay.Add(card);
            }
        }
    }

    // Deal a replacement card to a specific position
    public bool ReplaceCard(int position)
    {
        if (position < 0 || position >= cardsInPlay.Count)
            return false;

        if (deck.RemainingCards == 0)
            return false;

        Card newCard = deck.TakeTopCard();
        if (newCard == null)
            return false;

        newCard.FlipOver(); // Make the card face up
        cardsInPlay[position] = newCard;
        return true;
    }

    // Check if the selected positions form a pair that sums to 11
    public bool IsPairSumEleven(List<int> selectedPositions)
    {
        // Need exactly 2 cards for a sum pair
        if (selectedPositions == null || selectedPositions.Count != 2)
            return false;

        int firstPosition = selectedPositions[0];
        int secondPosition = selectedPositions[1];

        // Validate positions are in range
        if (firstPosition < 0 || firstPosition >= cardsInPlay.Count ||
            secondPosition < 0 || secondPosition >= cardsInPlay.Count)
            return false;

        // Get the cards and check their sum
        Card firstCard = cardsInPlay[firstPosition];
        Card secondCard = cardsInPlay[secondPosition];

        return firstCard.GetValue() + secondCard.GetValue() == PAIR_SUM;
    }

    // Check if the selected positions form a J-Q-K set
    public bool IsJQKSet(List<int> selectedPositions)
    {
        // Need exactly 3 cards for a J-Q-K set
        if (selectedPositions == null || selectedPositions.Count != 3)
            return false;

        bool hasJack = false;
        bool hasQueen = false;
        bool hasKing = false;

        // Check each selected position
        foreach (int position in selectedPositions)
        {
            // Validate position is in range
            if (position < 0 || position >= cardsInPlay.Count)
                return false;

            // Check card rank
            Card card = cardsInPlay[position];

            if (card.Rank == Rank.Jack) hasJack = true;
            if (card.Rank == Rank.Queen) hasQueen = true;
            if (card.Rank == Rank.King) hasKing = true;
        }

        // Must have all three face cards
        return hasJack && hasQueen && hasKing;
    }

    // Check if the selected cards form a valid set (either pair sum of 11 or J,Q,K set)
    public bool IsValidSelection(List<int> selectedPositions)
    {
        // For 2 cards, check for sum of 11
        if (selectedPositions != null && selectedPositions.Count == 2)
        {
            return IsPairSumEleven(selectedPositions);
        }
        // For 3 cards, check for J,Q,K
        else if (selectedPositions != null && selectedPositions.Count == 3)
        {
            return IsJQKSet(selectedPositions);
        }

        return false;
    }

    // Remove cards at the selected positions and replace with new cards from the deck
    public bool RemoveAndReplace(List<int> positions)
    {
        if (!IsValidSelection(positions))
            return false;

        // Sort positions in descending order to avoid index shifting when removing
        positions.Sort();
        positions.Reverse();

        foreach (int position in positions)
        {
            ReplaceCard(position);
        }

        return true;
    }

    // Check if the game is over (no valid moves possible)
    public bool IsGameOver()
    {
        // First check if there are any valid moves on the current board
        List<List<int>> possibleSelections = new List<List<int>>();

        // Generate all possible pairs
        for (int i = 0; i < cardsInPlay.Count; i++)
        {
            for (int j = i + 1; j < cardsInPlay.Count; j++)
            {
                possibleSelections.Add(new List<int> { i, j });
            }
        }

        // Generate all possible triplets
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

        // Check each possible selection
        foreach (List<int> selection in possibleSelections)
        {
            if (IsValidSelection(selection))
            {
                return false; // Found a valid move, game not over
            }
        }

        // No valid moves on the board, game is over
        return true;
    }
}