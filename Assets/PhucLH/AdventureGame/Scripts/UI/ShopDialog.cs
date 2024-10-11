using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.UI;

namespace PhucLH.AdventureGame
{
    public class ShopDialog : Dialog
    {
        public Transform gridRoot;
        public ShopItemUI shopItemPrefab;
        public Text coinCountingText;

        public override void Show(bool isShow)
        {
            base.Show(isShow);
            UpdateUI();
        }

        private void UpdateUI()
        {
            if(coinCountingText)
            {
                coinCountingText.text = GameData.Ins.coin.ToString();
            }

            var shopItems = ShopManager.Ins.shopItems;
            if (shopItems.Length <= 0 || shopItems == null) return;

            Helper.ClearChilds(gridRoot);

            for(int i=0; i<shopItems.Length; i++)
            {
                int itemIndex = i;
                var shopItem = shopItems[i];
                if (shopItem != null)
                {
                    var itemUIClone = Instantiate(shopItemPrefab, Vector3.zero, Quaternion.identity);
                    itemUIClone.transform.SetParent(gridRoot);
                    itemUIClone.transform.localScale = Vector3.one;
                    itemUIClone.transform.localPosition = Vector3.zero;
                    itemUIClone.UpdateUI(shopItem, itemIndex);

                    if(itemUIClone.buttonComp)
                    {
                        itemUIClone.buttonComp.onClick.RemoveAllListeners();
                        itemUIClone.buttonComp.onClick.AddListener(() => ItemEvent(shopItem, itemIndex));
                    }    
                }    
            }    
        }

        private void ItemEvent(ShopItem shopItem, int itemIndex)
        {
            if (shopItem == null) return;

            if(shopItem.price<= GameData.Ins.coin)
            {
                if(shopItem.type == CollectableType.Hp)
                {
                    GameData.Ins.hp++;
                }   
                else if(shopItem.type == CollectableType.Live)
                {
                    GameData.Ins.live++;
                }
                else if (shopItem.type == CollectableType.Bullet)
                {
                    GameData.Ins.bullet++;
                }
                else if (shopItem.type == CollectableType.Key)
                {
                    GameData.Ins.key++;
                }
                GameData.Ins.coin -= shopItem.price;
                GameData.Ins.SaveData();
                UpdateUI();
                
                AudioController.ins.PlaySound(AudioController.ins.buy);
            }    
            else
            {
                Debug.Log("You don't have enough coins !!!");
            }    
        }
    }

}
