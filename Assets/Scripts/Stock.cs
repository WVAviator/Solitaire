using UnityEngine;

namespace Solitaire
{
    public class Stock : MonoBehaviour, IClickable
    {
        public Deck Deck;
        public int drawCount = 3;

        [SerializeField] PlayingCard cardPrefab;
        Waste waste;
        SpriteRenderer spriteRenderer;

        void Awake()
        {
            Deck = new Deck();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void Start()
        {
            waste = FindObjectOfType<Waste>();
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

            waste.RevealCards(cards);
        }

        void RestackDeck()
        {
            Deck = new Deck(waste.GetResetStack());
            spriteRenderer.enabled = Deck.CardsRemaining() > 0;
        }
    }
}