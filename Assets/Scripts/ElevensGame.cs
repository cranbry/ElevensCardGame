using System;
using System.Collections.Generic;

public class ElevensGame
{
    private Board gameBoard;
    private int score;
    private bool isGameOver;

    // constructor to initialize wins and losses
    public ElevensGame()
    {
        gameBoard = new Board();
        score = 0;
        isGameOver = false;
    }

    // accessing the game board
    public Board GameBoard
    {
        get
        {
            return gameBoard;
        }
    }

    // the current score
    public int Score
    {
        get
        {
            return score;
        }
    }

    // the game is over
    public bool IsGameOver
    {
        get
        {
            return isGameOver || gameBoard.IsGameOver();
        }
    }

    public bool MakeMove(List<int> selectedPositions)
    {
        if (isGameOver)
        {
            return false;
        }
        if (gameBoard.IsValidSelection(selectedPositions))
        {
            // checking if JQK set
            bool isJQKSet = false;
            if (selectedPositions.Count == 3)
            {
                isJQKSet = gameBoard.IsJQKSet(selectedPositions);
            }

            // removing and replacing cards
            gameBoard.RemoveAndReplace(selectedPositions);

            // update score only if not JQK set
            if (!isJQKSet)
            {
                score++;
            }

            // checking if game is over after move
            isGameOver = gameBoard.IsGameOver();

            return true;
        }

        return false;
    }

    // resetting and restarting a new game
    public void NewGame()
    {
        gameBoard = new Board();
        score = 0;
        isGameOver = false;
    }

    // getting number of cards remaining in the deck
    public int GetRemainingCards()
    {
        return gameBoard.RemainingCards;
    }
}

