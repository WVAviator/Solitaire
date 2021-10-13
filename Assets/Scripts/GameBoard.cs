using System.Collections.Generic;
using UnityEngine;

namespace Solitaire
{
    public class GameBoard : MonoBehaviour
    {
        public static GameBoard Instance;

        public Transform Deck;

        public List<Tableau> MainStacks = new List<Tableau>();
        public Transform[] UpperStack;

        [HideInInspector] public Vector2 DeckLocation;
        [HideInInspector] public List<Vector2> MainStackLocations = new List<Vector2>();
        [HideInInspector] public List<Vector2> UpperStackLocations = new List<Vector2>();

        void Awake()
        {
            Instance = this;

            DeckLocation = Deck.position;

            foreach (Tableau t in MainStacks) MainStackLocations.Add(t.transform.position);
            foreach (Transform t in UpperStack) UpperStackLocations.Add(t.position);
        }
    }
}