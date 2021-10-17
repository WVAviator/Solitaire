using UnityEngine;

namespace Solitaire
{
    public class Stock : MonoBehaviour, IClickable
    {
        public Deck Deck { get; private set; }

        public int DrawCount => drawCount;
        [SerializeField] int drawCount = 3;

        [SerializeField] PlayingCard cardPrefab;
        
        WasteStack _wasteStack;
        SpriteRenderer _spriteRenderer;

        void Awake()
        {
            Deck = new Deck();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _wasteStack = FindObjectOfType<WasteStack>();
        }

        void OnEnable() => GameManager.OnNewGameDeal += DestroySelf;
        void OnDisable() => GameManager.OnNewGameDeal -= DestroySelf;

        void DestroySelf() => Destroy(gameObject);

        public CardData DrawCard()
        {
            CardData nextCard = Deck.DrawCard();
            UpdateStockSprite();
            return nextCard;
        }

        void UpdateStockSprite() => _spriteRenderer.enabled = Deck.CardsRemaining() != 0;
        

        public void Click()
        {
            int numberOfCardsToDraw = GetNumberOfCardsToDraw();

            if (numberOfCardsToDraw == 0)
            {
                RecycleWaste();
                return;
            }

            PlayingCard[] cards = DrawCardsFromDeck(numberOfCardsToDraw);
            _wasteStack.RevealCards(cards);
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

        void RecycleWaste()
        {
            Deck = new Deck(_wasteStack.GetRecycledStock());
            UpdateStockSprite();
        }
    }
}