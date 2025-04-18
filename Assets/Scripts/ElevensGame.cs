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
        win_count = 0;
        loss_count = 0;
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
        if (gameBoard.isValidSelection(selectedPositions))
        {
            // when valid move then remove and replace the card and update score
            gameBoard.RemoveAndReplace(selectedPositions);

            // update score +1 point
            score++;

            // checking if game is over
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

