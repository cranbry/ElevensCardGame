# ElevensGame
 # Project Overview:
  Elevens is a card game where the user is dealt 9 cards randomly, the point of the game is to find pairs of cards that when added, their value results in 11. The game ends when the user     runs out of available pairs of 11 on the board. The user wins when there are no more cards on the deck and the board.

## What is included in this partial implementation?
  -> Class design for Suit, Rank, Card, Deck, and Board.  
  -> Functions for shuffle, dealCard, isEmpty, checkForSum, checkForJQK, getCardValues.  
  -> Missing functions: selectedCard, gameOver, gameWon, validMoves, getRemainingCard... and GUI implementation.  
  
## Key Stakeholders:
  **End-user:** People looking for a casual card game for mental stimulation and for some easy quick math.  
  **Developer:** Game developer / tester.  
  **Operators:** Same as the end user.  
  
## User related Functional Needs:
  -> Users must be able to see the board with the cards they are dealt.  
  -> Users must be able to see the amount of remaining cards in the deck.  
  -> Users must be able to select and deselect cards on the board.  
  -> Users must be able to see new cards appearing when old cards have been replaced.  
  -> Users must be displayed a message if pairs selected do not amount to 11 in value.  
  -> Users must be able to start a new game when they want.  
   
## System related Functional Needs:
  -> System must deal 9 random cards to the board at the start of the game.  
  -> System must replace removed cards with new ones from the deck when available.  
  -> System must detect no more valid pairs on the board.  
  -> System must save and display game statistics (amount of wins/losses per game played).  
  -> System must detect if no more cards are available.  
  -> System must detect valid selection (2 cards amounting to 11 in value, 3 cards if J,K,Q).  
  -> System must display an error message if pairs selected do not amount to 11 in value.  
  -> System must display a winning message when all cards have been used.  

## UML made Using Figma:

<img width="710" alt="Screenshot 2025-03-22 at 10 22 54 AM" src="https://github.com/user-attachments/assets/64ec1035-5e37-4b4d-b28a-c3d7c2e11bb8" />
