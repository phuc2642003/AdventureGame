using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PhucLH.AdventureGame
{
    public class LevelSelectDialog : Dialog
    {
        public Transform gridRoot;
        public LevelItemUI levelItemUIPrefab;
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

            var levels = LevelManager.Ins.levels;

            if (levels == null || gridRoot == null || levelItemUIPrefab == null)
                return;
            Helper.ClearChilds(gridRoot);
            for(int i=0; i<levels.Length;i++)
            {
                int index = i;
                var level = levels[index];

                if (level == null) continue;
                var itemUIClone = Instantiate(levelItemUIPrefab, Vector3.zero, Quaternion.identity);
                itemUIClone.transform.SetParent(gridRoot);
                itemUIClone.transform.localScale = Vector3.one;          
                itemUIClone.transform.localPosition = Vector3.zero;
                itemUIClone.UpdateUI(level, index);
                if(itemUIClone.buttonComponent)
                {
                    itemUIClone.buttonComponent.onClick.RemoveAllListeners();
                    itemUIClone.buttonComponent.onClick.AddListener(() => LevelItemEvent(level, index));
                }    
            }    
        }

        private void LevelItemEvent(LevelItem levelItem, int index)
        {
            if (levelItem == null) return;

            bool isUnlocked = GameData.Ins.IsLevelUnlocked(index);

            if(isUnlocked)
            {
                GameData.Ins.currentLevelId = index;
                GameData.Ins.SaveData();

                LevelManager.Ins.CurrentLevelId = index;
                SceneController.Ins.LoadLevelScene(index);
            }    
            else
            {
                if(GameData.Ins.coin >= levelItem.price)
                {
                    GameData.Ins.coin -= levelItem.price;
                    GameData.Ins.currentLevelId = index;
                    GameData.Ins.UpdateLevelUnlocked(index,true);
                    GameData.Ins.SaveData();

                    LevelManager.Ins.CurrentLevelId = index;

                    UpdateUI();

                    SceneController.Ins.LoadLevelScene(index);
                }    
                else
                {
                    Debug.Log("You don't have enough coins !!!");
                }    
            }    
        }
    }

}
