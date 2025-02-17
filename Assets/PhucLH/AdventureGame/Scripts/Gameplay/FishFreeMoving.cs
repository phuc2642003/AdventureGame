using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhucLH.AdventureGame
{
    public class FishFreeMoving : EnemyFreeMoving
    {
        protected override void Update()
        {
            if (!GameManager.Ins.player.obstacleChecker.IsOnWater)
            {
                fsm.ChangeState(EnemyAnimState.Moving);
                return; 
            }
            base.Update();
        }
        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            if (!GameManager.Ins.player.obstacleChecker.IsOnWater)
            {
                fsm.ChangeState(EnemyAnimState.Moving);
            }
        }
        protected override void InWaterChecking(ref float x,ref float y)
        {
            while (!IsInWater(x, y))
            {
                x = Random.Range(x_leftPosition,x_rightPosition);
                y = Random.Range(y_downPosition, y_topPosition);
            }
        }

        private bool IsInWater(float x, float y)
        {
            Collider2D collider = Physics2D.OverlapPoint(new Vector2(x,y), LayerMask.GetMask("Water"));

            return collider != null;
        }
    }
}
