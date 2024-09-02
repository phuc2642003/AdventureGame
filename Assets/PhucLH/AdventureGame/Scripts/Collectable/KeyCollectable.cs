using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhucLH.AdventureGame
{
    public class KeyCollectable : Collectable
    {
        protected override void TriggerHandle()
        {
            base.TriggerHandle();
            GameManager.Ins.CurrentKey += bonus;
            GameData.Ins.key = GameManager.Ins.CurrentKey;
            GameData.Ins.SaveData();
        }
    }

}
