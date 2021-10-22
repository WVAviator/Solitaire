using UnityEngine;

namespace Solitaire
{
    public class StockSounds : MonoBehaviour
    {
        [SerializeField] Sound dealSound;
        void Awake() => dealSound.Source = gameObject.AddComponent<AudioSource>();
        public void PlayDealSound() => dealSound.Play();
    }
}