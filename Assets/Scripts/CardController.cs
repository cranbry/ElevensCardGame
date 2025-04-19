using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardController : MonoBehaviour
{
    [SerializeField] private Image cardImage;
    [SerializeField] private Sprite cardBackSprite;
    [SerializeField] private Sprite[] cardSprites; // using array for all card sprites from the sheet

    private Card cardData;
    private int boardPosition;
    private bool isSelected = false;

    // initializing the card with its data and position
    public void InitializeCard(Card card, int position)
    {
        cardData = card;
        boardPosition = position;
        UpdateCardVisuals();
    }

    // getting board position
    public int GetBoardPosition()
    {
        get {
            return boardPosition;
        }
    }

    // getting card data
    public Card GetCardData()
    {
        get {
            return cardData;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
