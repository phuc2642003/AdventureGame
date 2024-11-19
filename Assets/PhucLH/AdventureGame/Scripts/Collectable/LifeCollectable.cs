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
        }
    }

}
