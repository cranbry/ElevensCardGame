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

    private List<CardController> cardControllers = new List<CardController>();
    private List<int> selectedPositions = new List<int>();

    private void Awake() { }

    private void Start() { }

    private void Update() { }






}
