using UnityEngine;

namespace Solitaire
{
    public class StockSounds : MonoBehaviour
    {
        [SerializeField] Sound dealSound;

        void Awake()
        {
            dealSound.Source = gameObject.AddComponent<AudioSource>();
            Stock.OnNewGameDeal += PlayDealSound;
        }

        void PlayDealSound() => dealSound.Play();
    }
}