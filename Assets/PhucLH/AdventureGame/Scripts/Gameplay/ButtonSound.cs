using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PhucLH.AdventureGame
{
    public class ButtonSound : MonoBehaviour
    {
        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
        }

        // Start is called before the first frame update
        void Start()
        {
            if (button != null)
            {
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(()=> PlayButtonSound());
            }
        }

        private void PlayButtonSound()
        {
            AudioController.ins.PlaySound(AudioController.ins.btnClick);
        }
    }

}
