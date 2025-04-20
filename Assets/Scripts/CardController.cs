using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardController : MonoBehaviour
{
    [SerializeField] private Image cardFaceImage; // for the front face image
    [SerializeField] private GameObject cardFaceObject; // GameObject containing the front face
    [SerializeField] private GameObject cardBackObject; // GameObject containing the back face
    [SerializeField] private Sprite[] cardSprites; // array of all card sprites
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

    // getting the board position
    public int GetBoardPosition()
    {
        return boardPosition;
    }

    // getting the card data
    public Card GetCardData()
    {
        return cardData;
    }

    // updating visuals to match card data
    public void UpdateCardVisuals()
    {
        if (cardData == null)
        {
            gameObject.SetActive(false);
            return;
        }

        gameObject.SetActive(true);

        // setting face up/down state by activating/deactivating GameObjects
        cardFaceObject.SetActive(cardData.IsFaceUp);
        cardBackObject.SetActive(!cardData.IsFaceUp);

        if (cardData.IsFaceUp)
        {
            // calculating sprite index based on suit and rank
            int suitOffset = (int)cardData.Suit * 13;
            int rankIndex = (int)cardData.Rank;
            int spriteIndex = suitOffset + rankIndex;

            // making sure index is within bounds
            if (spriteIndex >= 0 && spriteIndex < cardSprites.Length)
            {
                cardFaceImage.sprite = cardSprites[spriteIndex];
            }
        }
    }

    // selection state
    public void ToggleSelection()
    {
        isSelected = !isSelected;

        // visual effect for selection
        if (isSelected)
        {
            transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        }
        else
        {
            transform.localScale = Vector3.one;
        }
    }

    // checking if the card is selected
    public bool IsSelected()
    {
        return isSelected;
    }

    // deselecting the card
    public void Deselect()
    {
        isSelected = false;
        transform.localScale = Vector3.one;
    }

    // handling click event
    public void OnCardClick()
    {
        GameManager.Instance.OnCardSelected(this);
    }
}