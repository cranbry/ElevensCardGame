using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
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

    }

    // creating new game using elevensgame class:
    private void InitializeGame()
    {
        game = new ElevensGame();
        // should:
        // clear board
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

    private void CreateCardObject()
    {

    }
    private void UpdateUI()
    {
        // update score:
        scoreText.text = "Score: " + game.score;

        // update deck count
        deckCountText.text = "Cards: " + game.deckCount;

        // showing game over if needed:        
    }






}
