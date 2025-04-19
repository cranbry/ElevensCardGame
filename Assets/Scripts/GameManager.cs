using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehavior
{
    public static GameManager Instance { get; private set; }

    [Header("Game References")]
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Transform cardsContainer;
    [SerializeField] private TextMeshProGUI scoreText;
    [SerializeField] private TextMeshProGUI deckCountText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Button submitButton;
    [SerializeField] private Button newGameButton;

    // Card layout parameters
    [Header("Card Layout")]
    [SerializeField] private float cardWidth = 120f;
    [SerializeField] private float cardHeight = 180f;
    [SerializeField] private float cardSpacing = 10f;
    [SerializeField] private int gridColumns = 3;

    private ElevensGame game;

    // list to keep track of all car objects on the screen:
    private List<CardController> cardControllers = new List<CardController>();

    // list of cards currently selected
    private List<int> selectedPositions = new List<int>();

    // initializing awake and start:
    private void Awake()
    {
        // making sure only 1 gameManager exists
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
        // calls to intialize game
        InitializeGame();

        // button event listener
        submitButton.onClick.AddListener(OnSubmitSelection);
        newGameButton.onClick.AddListener(StartNewGame);

        // disabling the submit button
        submitButton.interactable = false;
    }

    // creating new game using elevensgame class:
    private void InitializeGame()
    {
        game = new ElevensGame();
        // should:
        ClearBoard();
        // update UI
        // create the card object
    }

    // removing all existing cards from the screen and clears selection
    private void ClearBoard()
    {

        selectedPositions.Clear();

        foreach (CardController card in cardControllers)
        {
            Destroy(card.GameObject);
        }

        cardControllers.Clear();
    }

    private void CreateCardObjects()
    {
        List<Card> cardsInPlay = elevensGame.GameBoard.CardsInPlay;

        for (int i = 0; i < cardsInPlay.Count; i++)
        {
            GameObject cardObject = Instantiate(cardPrefab, cardsContainer);
            CardController cardController = cardObject.GetComponent<CardController>();

            cardController.InitializeCard(cardsInPlay[i], i);
            cardControllers.Add(cardController);

            // positioning the card in a grid layout
            PositionCardInGrid(cardObject.GetComponent<RectTransform>(), i);
        }
    }

    // for card position, made it a grid layout
    private void PositionCardInGrid(RectTransform cardRect, int index)
    {
        int row = index / gridColumns;
        int col = index % gridColumns;

        float xPos = col * (cardWidth + cardSpacing);
        float yPos = -row * (cardHeight + cardSpacing);

        cardRect.anchoredPosition = new Vector2(xPos, yPos);
        cardRect.sizeDelta = new Vector2(cardWidth, cardHeight);
    }

    // updating all the UI elements
    private void UpdateUI()
    {
        // uptdating score
        scoreText.text = "Score: " + elevensGame.Score.ToString();

        // updating deck count
        int remainingCards = elevensGame.GetRemainingCards();
        deckCountText.text = "Cards: " + remainingCards.ToString();

        // shwoing game over when needed
        gameOverPanel.SetActive(elevensGame.IsGameOver);

        // to enable or desable submit button based on what was selected
        submitButton.interactable = selectedPositions.Count == 2 || selectedPositions.Count == 3;
    }

    // handling card selection
    public void OnCardSelected(CardController cardController)
    {
        int position = cardController.GetBoardPosition();

        if (selectedPositions.Contains(position))
        {
            // deselcting the card
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

    // handling the submit button
    private void OnSubmitSelection()
    {
        if (elevensGame.MakeMove(selectedPositions))
        {
            // successful move and updating board
            RefreshBoard();
        }
        else
        {
            // invalid move and making the cards deselected
            DeselectAllCards();
            UpdateUI();
        }
    }

    // refreshing the board after a move
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