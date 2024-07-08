using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

namespace PhucLH.AdventureGame
{
    public class ShopItemUI : MonoBehaviour
    {
        public Text priceTxt;
        public Text amountTxt;
        public Image previewImg;
        public Button buttonComp;

        public void UpdateUI(ShopItem shopItem, int itemIndex)
        {
            if (shopItem == null) return;

            if(previewImg)
            {
                previewImg.sprite = shopItem.preview;
            }    
                
            if(shopItem.type == CollectableType.Hp)
            {
                UpdateAmountText(GameData.Ins.hp);
            }  
            else if(shopItem.type == CollectableType.Bullet)
            {
                UpdateAmountText(GameData.Ins.bullet);
            }
            else if (shopItem.type == CollectableType.Live)
            {
                UpdateAmountText(GameData.Ins.live);
            }
            else if (shopItem.type == CollectableType.Key)
            {
                UpdateAmountText(GameData.Ins.key);
            }

            if(priceTxt)
            {
                priceTxt.text = shopItem.price.ToString();
            }    
            
        }

        private void UpdateAmountText(int amount)
        {
            if(amountTxt)
            {
                amountTxt.text = amount.ToString();
            }    
        }
    }


}
