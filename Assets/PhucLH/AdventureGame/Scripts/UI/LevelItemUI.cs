using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PhucLH.AdventureGame
{
    public class LevelItemUI : MonoBehaviour
    {
        public Image preview;
        public GameObject lockedArea;
        public GameObject checkMask;
        public Text priceTxt;
        public Button buttonComponent;

        public void UpdateUI(LevelItem item, int levelIndex)
        {
            if (item == null) return;

            if(preview)
            {
                preview.sprite = item.previewSprite;
            }    

            if(priceTxt)
            {
                priceTxt.text = item.price.ToString();
            }
            bool isUnlocked = GameData.Ins.IsLevelUnlocked(levelIndex);
            if (lockedArea)
            {
                lockedArea.SetActive(!isUnlocked);
            }    
            if(isUnlocked)
            {
                if(checkMask)
                {
                    checkMask.SetActive(GameData.Ins.currentLevelId == levelIndex);
                }    
            }    
            else
            {
                if (checkMask)
                {
                    checkMask.SetActive(false);
                }
            }    
        }    

    }

}
