using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Solitaire
{
    public class GameBoard : MonoBehaviour
    {
        public static GameBoard Instance;
        public Vector2 StockLocation => deckLocation.position;
        [SerializeField] Transform deckLocation;
        public List<TableauStack> Tableaux => tableaux;
        [SerializeField] List<TableauStack> tableaux;
        public List<FoundationStack> Foundations => foundations;
        [SerializeField] List<FoundationStack> foundations;
        public WasteStack WasteStack => wasteStack;
        [SerializeField] WasteStack wasteStack;

        public List<Stack> AllStacks
        {
            get
            {
                List<Stack> stacks = new List<Stack>();
                stacks.AddRange(tableaux);
                stacks.AddRange(foundations);
                stacks.Add(wasteStack);
                return stacks;
            }
        }

        void Awake() => Instance = this;
        
    }
}