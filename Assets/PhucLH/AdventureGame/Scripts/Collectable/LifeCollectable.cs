using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhucLH.AdventureGame
{
    public class LifeCollectable : Collectable
    {
        protected override void TriggerHandle()
        {
            GameManager.Ins.CurrentLive += bonus;
            GameData.Ins.live = GameManager.Ins.CurrentLive;
            GameData.Ins.SaveData();
            GUIManager.Ins.UpdateLive(GameData.Ins.live);
        }
    }

}
