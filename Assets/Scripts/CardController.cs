using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardController : MonoBehaviour
{
    [SerializeField] private Image cardImage;
    [SerializeField] private Sprite cardBackSprite;
    [SerializeField] private Sprite[] cardSprites; // Array of all card sprites from your sprite sheet

    private Card cardData;
    private int boardPosition;
    private bool isSelected = false;

    // Initialize the card with its data and position
    public void InitializeCard(Card card, int position)
    {
        cardData = card;
        boardPosition = position;
        UpdateCardVisuals();
    }

    // Get the board position
    public int GetBoardPosition()
    {
        return boardPosition;
    }

    // Get the card data
    public Card GetCardData()
    {
        return cardData;
    }

    // Update visuals to match card data
    public void UpdateCardVisuals()
    {
        if (cardData == null)
        {
            gameObject.SetActive(false);
            return;
        }

        gameObject.SetActive(true);

        if (cardData.IsFaceUp)
        {
            // Calculate sprite index based on suit and rank
            // This assumes your sprite sheet is organized as shown in your screenshot
            int suitOffset = (int)cardData.Suit * 13;
            int rankIndex = (int)cardData.Rank;
            int spriteIndex = suitOffset + rankIndex;

            // Make sure index is within bounds
            if (spriteIndex >= 0 && spriteIndex < cardSprites.Length)
            {
                cardImage.sprite = cardSprites[spriteIndex];
            }
        }
        else
        {
            // Show card back
            cardImage.sprite = cardBackSprite;
        }
    }

    // Toggle selection state
    public void ToggleSelection()
    {
        isSelected = !isSelected;

        // Visual effect for selection
        if (isSelected)
        {
            transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            cardImage.color = new Color(0.8f, 0.8f, 1f);
        }
        else
        {
            transform.localScale = Vector3.one;
            cardImage.color = Color.white;
        }
    }

    // Check if the card is selected
    public bool IsSelected()
    {
        return isSelected;
    }

    // Deselect the card
    public void Deselect()
    {
        isSelected = false;
        transform.localScale = Vector3.one;
        cardImage.color = Color.white;
    }

    // Handle click event
    public void OnCardClick()
    {
        GameManager.Instance.OnCardSelected(this);
    }
}