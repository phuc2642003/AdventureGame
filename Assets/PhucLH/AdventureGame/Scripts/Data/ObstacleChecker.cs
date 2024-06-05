using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhucLH.AdventureGame
{
    public class ObstacleChecker : MonoBehaviour
    {
        public LayerMask groundLayer;
        public LayerMask waterLayer;
        public LayerMask ladderLayer;
        public float checkingRadius;
        public float deepWaterCheckingDistance;
        public Vector3 offSet;
        public Vector3 deepWaterOffSet;
        private bool isOnGround;
        private bool isOnWater;
        private bool isOnDeepWater;
        private bool isOnLadder;

        public bool IsOnGround { get => isOnGround;}
        public bool IsOnWater { get => isOnWater; }
        public bool IsOnLadder { get => isOnLadder; }
        public bool IsOnDeepWater { get => isOnDeepWater; }

        private void FixedUpdate()
        {
            isOnGround = OverlapChecking(groundLayer);
            isOnWater = OverlapChecking(waterLayer);
            isOnLadder = OverlapChecking(ladderLayer);

            RaycastHit2D waterHit = Physics2D.Raycast(transform.position + deepWaterOffSet, Vector2.up, deepWaterCheckingDistance, waterLayer);
            isOnDeepWater = waterHit;
        }

        private bool OverlapChecking(LayerMask checkingLayer)
        {
            Collider2D col = Physics2D.OverlapCircle(transform.position + offSet, checkingRadius, checkingLayer);

            return col != null;
        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(transform.position + offSet, checkingRadius);
        }
    }

}
