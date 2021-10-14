using UnityEngine;

namespace Solitaire
{
    public class Stock : MonoBehaviour, IClickable
    {
        public Deck Deck { get; private set; }

        public int DrawCount => drawCount;
        [SerializeField] int drawCount = 3;

        [SerializeField] PlayingCard cardPrefab;
        
        WasteStack wasteStack;
        SpriteRenderer spriteRenderer;

        void Awake()
        {
            Deck = new Deck();
            spriteRenderer = GetComponent<SpriteRenderer>();
            wasteStack = FindObjectOfType<WasteStack>();
        }

        public CardData DrawCard()
        {
            CardData nextCard = Deck.DrawCard();
            UpdateStockSprite();
            return nextCard;
        }

        void UpdateStockSprite() => spriteRenderer.enabled = Deck.CardsRemaining() != 0;
        

        public void Click()
        {
            int numberOfCardsToDraw = GetNumberOfCardsToDraw();

            if (numberOfCardsToDraw == 0)
            {
                RestackDeck();
                return;
            }

            PlayingCard[] cards = DrawCardsFromDeck(numberOfCardsToDraw);
            wasteStack.RevealCards(cards);
        }

        PlayingCard[] DrawCardsFromDeck(int cardsToDraw)
        {
            PlayingCard[] cards = new PlayingCard[cardsToDraw];
            for (int i = 0; i < cardsToDraw; i++)
            {
                cards[i] = Instantiate(cardPrefab);
                cards[i].SetCard(DrawCard());
                cards[i].TurnFaceUp();
            }
            return cards;
        }

        int GetNumberOfCardsToDraw()
        {
            int drawAmount = drawCount;
            if (Deck.CardsRemaining() < drawCount) drawAmount = Deck.CardsRemaining();
            return drawAmount;
        }

        void RestackDeck()
        {
            Deck = new Deck(wasteStack.GetResetStack());
            UpdateStockSprite();
        }
    }
}