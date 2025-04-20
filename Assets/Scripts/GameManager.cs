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

    // Card layout parameters
    [Header("Card Layout")]
    [SerializeField] private float cardWidth;
    [SerializeField] private float cardHeight;
    [SerializeField] private float cardSpacing;
    [SerializeField] private int gridColumns;

    private ElevensGame elevensGame;
    private List<CardController> cardControllers = new List<CardController>();
    private List<int> selectedPositions = new List<int>();

    private void Awake()
    {
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

        // button event listeners
        submitButton.onClick.AddListener(OnSubmitSelection);
        newGameButton.onClick.AddListener(StartNewGame);

        // start with submit button disabled 
        submitButton.interactable = false;
    }

    // initializing the game
    private void InitializeGame()
    {
        elevensGame = new ElevensGame(); // it was error here
        ClearBoard();
        CreateCardObjects();
        UpdateUI();
    }

    // clearing the game board
    private void ClearBoard()
    {
        selectedPositions.Clear();

        foreach (CardController card in cardControllers)
        {
            Destroy(card.gameObject);
        }

        cardControllers.Clear();
    }

    // creating card objects based on the game board
    private void CreateCardObjects()
    {
        List<Card> cardsInPlay = elevensGame.GameBoard.CardsInPlay;

        for (int i = 0; i < cardsInPlay.Count; i++)
        {
            GameObject cardObject = Instantiate(cardPrefab, cardsContainer);
            CardController cardController = cardObject.GetComponent<CardController>();

            cardController.InitializeCard(cardsInPlay[i], i);
            cardControllers.Add(cardController);

            // position the card in a grid layout
            PositionCardInGrid(cardObject.GetComponent<RectTransform>(), i, cardsInPlay.Count);

        }
    }

    // position a card in the grid layout
    private void PositionCardInGrid(RectTransform cardRect, int index, int totalCards)
    {
        int row = index / gridColumns;
        int col = index % gridColumns;

        // Calculate the total grid width and height
        float totalGridWidth = gridColumns * (cardWidth + cardSpacing) - cardSpacing;
        float totalGridHeight = ((totalCards - 1) / gridColumns + 1) * (cardHeight + cardSpacing) - cardSpacing;

        // Calculate starting position (top-left of grid)
        // This centers the entire grid within the container
        float startX = -totalGridWidth / 2 + cardWidth / 2;
        float startY = totalGridHeight / 2 - cardHeight / 2;

        // Calculate position for this specific card
        float xPos = startX + col * (cardWidth + cardSpacing);
        float yPos = startY - row * (cardHeight + cardSpacing);

        cardRect.anchoredPosition = new Vector2(xPos, yPos);
        cardRect.sizeDelta = new Vector2(cardWidth, cardHeight);
    }

    // updating the UI stuff
    private void UpdateUI()
    {
        // updating score
        scoreText.text = "Score: " + elevensGame.Score.ToString();

        // updating deck count
        int remainingCards = elevensGame.GetRemainingCards();
        deckCountText.text = "Cards: " + remainingCards.ToString();

        // shwoing game over if needed
        gameOverPanel.SetActive(elevensGame.IsGameOver);

        // enable or disable submit button if selected
        submitButton.interactable = selectedPositions.Count == 2 || selectedPositions.Count == 3;
    }

    // handling card selection
    public void OnCardSelected(CardController cardController)
    {
        int position = cardController.GetBoardPosition();

        if (selectedPositions.Contains(position))
        {
            // deslecting the card
            selectedPositions.Remove(position);
            cardController.ToggleSelection();
        }
        else
        {
            // if we already have 3 cards selected then deselect all first
            if (selectedPositions.Count >= 3)
            {
                DeselectAllCards();
            }

            // selecting the card
            selectedPositions.Add(position);
            cardController.ToggleSelection();
        }

        UpdateUI();
    }

    // deselecting all cards
    private void DeselectAllCards()
    {
        selectedPositions.Clear();

        foreach (CardController card in cardControllers)
        {
            card.Deselect();
        }
    }

    // handling submit button click
    private void OnSubmitSelection()
    {
        Debug.Log($"Submit pressed with {selectedPositions.Count} cards selected");

        // printing the selected cards for debugging
        foreach (int pos in selectedPositions)
        {
            Card card = elevensGame.GameBoard.CardsInPlay[pos];
            Debug.Log($"Selected card: {card.Rank} of {card.Suit} with value {card.GetValue()}");
        }

        if (elevensGame.MakeMove(selectedPositions))
        {
            Debug.Log("Valid move!");
            RefreshBoard();
        }
        else
        {
            Debug.Log("Invalid move!");
            DeselectAllCards();
            UpdateUI();
        }
    }

    // refreshig the board after a move
    private void RefreshBoard()
    {
        ClearBoard();
        CreateCardObjects();
        UpdateUI();
    }

    // starting a new game
    private void StartNewGame()
    {
        elevensGame.NewGame();
        RefreshBoard();
    }
}