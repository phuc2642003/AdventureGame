using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhucLH.AdventureGame
{
    public class BulletCollectable : Collectable
    {
        protected override void TriggerHandle()
        {
            base.TriggerHandle();
            GameManager.Ins.CurrentBullet += bonus;
            GameData.Ins.bullet = GameManager.Ins.CurrentBullet;
            GameData.Ins.SaveData();
        }
    }

}
