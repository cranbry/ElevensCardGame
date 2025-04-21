using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game References")]
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Transform cardsContainer;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text deckCountText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Button submitButton;
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button restartButton;

    // Card layout parameters
    [Header("Card Layout")]
    [SerializeField] private float cardWidth = 120f;
    [SerializeField] private float cardHeight = 180f;
    [SerializeField] private float cardSpacing = 10f;
    [SerializeField] private int gridColumns = 4;

    private ElevensGame elevensGame;
    private List<CardController> cardControllers = new List<CardController>();
    private List<int> selectedPositions = new List<int>();

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializeGame();

        // Button event listeners
        submitButton.onClick.AddListener(OnSubmitSelection);
        newGameButton.onClick.AddListener(StartNewGame);

        if (restartButton != null)
            restartButton.onClick.AddListener(StartNewGame);

        // Initially hide the game over panel
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    // Initialize the game
    private void InitializeGame()
    {
        elevensGame = new ElevensGame();
        ClearBoard();
        CreateCardObjects();
        UpdateUI();
    }

    // Clear the game board
    private void ClearBoard()
    {
        selectedPositions.Clear();

        foreach (CardController card in cardControllers)
        {
            Destroy(card.gameObject);
        }

        cardControllers.Clear();
    }

    // Create card objects based on the game board
    private void CreateCardObjects()
    {
        List<Card> cardsInPlay = elevensGame.GameBoard.CardsInPlay;

        Debug.Log($"Creating {cardsInPlay.Count} cards");

        for (int i = 0; i < cardsInPlay.Count; i++)
        {
            Card currentCard = cardsInPlay[i];
            Debug.Log($"Card {i}: {currentCard.Rank} of {currentCard.Suit} (Value: {currentCard.GetValue()})");

            GameObject cardObject = Instantiate(cardPrefab, cardsContainer);
            CardController cardController = cardObject.GetComponent<CardController>();

            cardController.InitializeCard(currentCard, i);
            cardControllers.Add(cardController);

            // Position the card in a grid layout
            PositionCardInGrid(cardObject.GetComponent<RectTransform>(), i);
        }
    }

    // Position a card in the grid layout with centered grid
    private void PositionCardInGrid(RectTransform cardRect, int index)
    {
        // Get list of cards in play for total count
        List<Card> cardsInPlay = elevensGame.GameBoard.CardsInPlay;

        int row = index / gridColumns;
        int col = index % gridColumns;

        // Calculate the total grid width and height
        float totalGridWidth = gridColumns * (cardWidth + cardSpacing) - cardSpacing;
        int rowCount = (cardsInPlay.Count + gridColumns - 1) / gridColumns; // Ceiling division
        float totalGridHeight = rowCount * (cardHeight + cardSpacing) - cardSpacing;

        // Calculate starting position (top-left of grid)
        float startX = -totalGridWidth / 2 + cardWidth / 2;
        float startY = totalGridHeight / 2 - cardHeight / 2;

        // Calculate position for this specific card
        float xPos = startX + col * (cardWidth + cardSpacing);
        float yPos = startY - row * (cardHeight + cardSpacing);

        cardRect.anchoredPosition = new Vector2(xPos, yPos);
        cardRect.sizeDelta = new Vector2(cardWidth, cardHeight);
    }

    // Update the UI elements
    private void UpdateUI()
    {
        // Update score
        scoreText.text = "Score: " + elevensGame.Score.ToString();

        // Update deck count
        int remainingCards = elevensGame.GetRemainingCards();
        deckCountText.text = "Cards: " + remainingCards.ToString();

        // Show game over panel if needed
        if (gameOverPanel != null)
            gameOverPanel.SetActive(elevensGame.IsGameOver);

        // Enable/disable submit button based on selections
        submitButton.interactable = selectedPositions.Count == 2 || selectedPositions.Count == 3;
    }

    // Handle card selection
    public void OnCardSelected(CardController cardController)
    {
        int position = cardController.GetBoardPosition();

        if (selectedPositions.Contains(position))
        {
            // Deselect the card
            selectedPositions.Remove(position);
            cardController.ToggleSelection();
        }
        else
        {
            // If we already have 3 cards selected, deselect all first
            if (selectedPositions.Count >= 3)
            {
                DeselectAllCards();
            }

            // Select the card
            selectedPositions.Add(position);
            cardController.ToggleSelection();
        }

        UpdateUI();
    }

    // Deselect all cards
    private void DeselectAllCards()
    {
        selectedPositions.Clear();

        foreach (CardController card in cardControllers)
        {
            card.Deselect();
        }
    }

    // Handle submit button click
    public void OnSubmitSelection()
    {
        Debug.Log("Submit button clicked");

        if (selectedPositions.Count == 2)
        {
            Card card1 = elevensGame.GameBoard.CardsInPlay[selectedPositions[0]];
            Card card2 = elevensGame.GameBoard.CardsInPlay[selectedPositions[1]];
            Debug.Log($"Trying to match: {card1.Rank} ({card1.GetValue()}) + {card2.Rank} ({card2.GetValue()}) = {card1.GetValue() + card2.GetValue()}");
            Debug.Log($"Is this a valid selection? {elevensGame.GameBoard.IsValidSelection(selectedPositions)}");
        }

        if (elevensGame.MakeMove(selectedPositions))
        {
            Debug.Log("Move successful, replacing cards");
            RefreshBoard();
        }
        else
        {
            Debug.Log("Move failed, not a valid selection");
            DeselectAllCards();
            UpdateUI();
        }
    }

    // Refresh the board after a move
    private void RefreshBoard()
    {
        ClearBoard();
        CreateCardObjects();
        UpdateUI();
    }

    // Start a new game
    public void StartNewGame()
    {
        elevensGame.NewGame();
        RefreshBoard();

        // Hide game over panel
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }
}