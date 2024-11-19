using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhucLH.AdventureGame
{
    public class HpCollectable : Collectable
    {
        protected override void TriggerHandle()
        {
            player.CurHp += bonus;
        }
    }

}
