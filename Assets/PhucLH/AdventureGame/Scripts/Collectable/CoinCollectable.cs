using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhucLH.AdventureGame
{
    public class CoinCollectable : Collectable
    {
        protected override void TriggerHandle()
        {
            base.TriggerHandle();
            GameManager.Ins.AddCoins(bonus);
        }
    }

}
