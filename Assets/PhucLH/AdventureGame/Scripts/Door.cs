using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhucLH.AdventureGame
{
    public class Door : MonoBehaviour
    {
        public int keyRequired;
        public Sprite openSprite;
        public Sprite closedSprite;

        private SpriteRenderer sprite;
        public bool IsOpen { get; private set; }

        private void Awake()
        {
            sprite = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            DoorChecking();
        }

        private void DoorChecking()
        {
            IsOpen = GameData.Ins.IsLevelUnlocked(LevelManager.Ins.CurrentLevelId + 1);
            sprite.sprite = IsOpen ? openSprite : closedSprite;
        }

        public void OpenDoor()
        {
            if (IsOpen)
            {
                GameManager.Ins.CurrentKey = 0;
                GameManager.Ins.LevelClear();
                GUIManager.Ins.UpdateKey(GameManager.Ins.CurrentKey);
                return;
            }

            if (GameManager.Ins.CurrentKey >= keyRequired)
            {
                GameManager.Ins.CurrentKey = 0;
                GameData.Ins.key = 0;
                GameData.Ins.UpdateLevelUnlocked(LevelManager.Ins.CurrentLevelId+1,true);
                GameData.Ins.UpdateLevelPassed(LevelManager.Ins.CurrentLevelId, true);
                GameData.Ins.SaveData();
                GameManager.Ins.LevelClear();
                DoorChecking();

                GUIManager.Ins.UpdateKey(GameManager.Ins.CurrentKey);

            }
        }
    }
}

