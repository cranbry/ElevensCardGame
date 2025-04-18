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
        get { return gameBoard; }
    }

    // the current score
    public int Score
    {
        get { return score; }
    }

    // the game is over
    public bool IsGameOver
    {
        get { return isGameOver || gameBoard.IsGameOver(); }
    }

}

