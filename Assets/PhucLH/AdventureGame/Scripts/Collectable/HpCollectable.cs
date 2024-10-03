using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhucLH.AdventureGame
{
    public class HpCollectable : Collectable
    {
        protected override void TriggerHandle()
        {
            base.TriggerHandle();
            player.CurHp += bonus;
            GameData.Ins.hp = player.CurHp;
            GameData.Ins.SaveData();
            //UpdateGameGUI
            GUIManager.Ins.UpdateHp(GameData.Ins.hp);
        }
    }

}
