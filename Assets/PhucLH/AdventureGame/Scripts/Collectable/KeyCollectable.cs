using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhucLH.AdventureGame
{
    public class KeyCollectable : Collectable
    {
        protected override void TriggerHandle()
        {
            GameManager.Ins.CurrentKey += bonus;
        }
    }

}
