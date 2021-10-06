using UnityEngine;

namespace Solitaire
{
    public class PlayingDeck : MonoBehaviour, IClickable
    {
        public Deck Deck;
        public int drawCount = 3;

        [SerializeField] PlayingCard cardPrefab;
        DrawStack drawStack;
        SpriteRenderer spriteRenderer;

        void Awake()
        {
            Deck = new Deck();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void Start()
        {
            drawStack = FindObjectOfType<DrawStack>();
        }

        public Card DrawCard()
        {
            spriteRenderer.enabled = Deck.CardsRemaining() > 1;
            return Deck.DrawCard();
        }
        public void Click()
        {
            int drawAmount = drawCount;
            if (Deck.CardsRemaining() < drawCount) drawAmount = Deck.CardsRemaining();

            if (drawAmount == 0)
            {
                RestackDeck();
                return;
            }

            PlayingCard[] cards = new PlayingCard[drawAmount];
            for (int i = 0; i < drawAmount; i++)
            {
                cards[i] = Instantiate(cardPrefab);
                cards[i].SetCard(DrawCard());
                cards[i].TurnFaceUp();
            }

            drawStack.RevealCards(cards);
        }

        void RestackDeck()
        {
            Deck = new Deck(drawStack.GetResetStack());
            spriteRenderer.enabled = Deck.CardsRemaining() > 0;
        }
    }
}