# ElevensGame
## Test & Project Overview:
   Elevens is a card-based logic game where the player is dealt 9 cards. The goal is to find valid combinations that either:

   -  Add up to 11 (using two cards), or

   - Include a Jack, Queen, and King (as a set of three).

Link: https://youtu.be/X6Hc_NICcmQ

### The game ends when there are no more valid combinations on the board, and the deck is empty. The player wins by clearing all cards from both the board and the deck.

## ✅ What is included in this complete implementation?
   Full class design for: Suit, Rank, Card, Deck, Board, CardController, GameManager and ElevensGame.

### Core gameplay logic:

- Card shuffling and dealing.
- Move validation (checkForSum, checkForJQK, etc.).
- Valid selection and move execution.
- Detection of game over and win conditions.
- Full game loop with support for restarting games.
- Polished Unity UI implementation:
- Dynamic card rendering and layout in a grid.
- Interactive selection/deselection with visual feedback.
- Score and remaining deck count display.
- Game-over and win messages.
- Button controls for submitting selections and starting a new game.

## 👥 Key Stakeholders:

- End-user: Anyone looking for a fun, casual card game that offers light math and pattern-recognition challenges.

- Developer: Game developers and testers building logic-based card games in Unity.

- Operators: Same as end-users — this game is self-contained and user-operated.

## 🧑‍💻 User Functional Requirements:

- View 8-card game board and current deck count.
- Select and deselect cards to form valid combinations.
- Automatically replace cleared cards with new ones from the deck.
- Get feedback for invalid selections.
- Restart the game at any time.
- Win or lose messages displayed appropriately.

## ⚙️ System Functional Requirements:

- Deal 9 random cards to the board at game start.
- Replace removed cards with new ones if deck is not empty.
  
- ### Validate card selections:
      - Two cards summing to 11.
      - Three cards forming J-Q-K set.
      - Detect and handle game over and win conditions.
      - Display error messages for invalid selections.
      - Track and display score, remaining deck count, and selections.
      - Display winning message when board and deck are cleared.

## 📐 UML Diagram (Made with Figma):

<img width="710" alt="Screenshot 2025-03-22 at 10 22 54 AM" src="https://github.com/user-attachments/assets/64ec1035-5e37-4b4d-b28a-c3d7c2e11bb8" />
