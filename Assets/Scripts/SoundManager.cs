using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Solitaire
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] Sound dealSound;
        [SerializeField] Sound winSound;

        void Awake()
        {
            dealSound.Source = gameObject.AddComponent<AudioSource>();
            winSound.Source = gameObject.AddComponent<AudioSource>();
        }
        void OnEnable()
        {
            GameManager.OnNewGameDeal += PlayDealSound;
            GameManager.OnGameWin += PlayWinSound;
        }
        void PlayDealSound() => dealSound.Play();
        void PlayWinSound() => winSound.Play();
        
    }
}
