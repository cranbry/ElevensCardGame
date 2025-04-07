using UnityEngine;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class ElevensGame
{
    private Deck deck;
    private Board board;
    private int loss_count;
    private int win_count;

    // constructor to initialize wins and losses
    public ElevensGame()
    {
        win_count = 0;
        loss_count = 0;
    }
    public void StarNewGame() {
        // creating new deck and shuffling it
        deck = new Deck;
        deck.Shuffle();

        // creating new board
        board = new Board();

        // dealing first board of cards
        for (int i =0; i < 9; i++) {
            Card card = deck.DealCard();

            if (card != null) {
                // turn card face up
                card.FlipOver();

                // adding it to the board
                board.replaceSelectedCard(i, card);
            }
        }
    }
    public void selectedCard() {
        Card selectedCard = board.Select(position);

        // check if selected cards are valid combination
        if (board.GetSelectedCards().Count >= 2) {
            SelectinoProcess();
        }
    }

}

