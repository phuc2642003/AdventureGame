using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

namespace PhucLH.AdventureGame
{
    public class PlayerDetect : MonoBehaviour
    {
        public bool disable;
        public DetectMethod detectMethod;
        public LayerMask targetLayer;
        public float detectDistance;

        private Player target;
        private Vector2 directionToTarget;
        private bool isDetected;

        public Player Target { get => target; }
        public Vector2 DirectionToTarget { get => directionToTarget; }
        public bool IsDetected { get => isDetected; }

        private void Start()
        {
            target = GameManager.Ins.player;
        }
        private void FixedUpdate()
        {
            if (!target || disable) return;

            if(detectMethod == DetectMethod.RayCast)
            {
                directionToTarget = target.transform.position - transform.position;
                directionToTarget.Normalize();

                RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(directionToTarget.x,0), detectDistance, targetLayer);
                isDetected = hit.collider != null;
            }
            else
            {
                Collider2D col = Physics2D.OverlapCircle(transform.position, detectDistance, targetLayer);
                isDetected = col != null;
            }
        }

        private void OnDrawGizmos()
        {
            if (detectMethod == DetectMethod.RayCast)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, new Vector3(transform.position.x+detectDistance
                                                                , transform.position.y, transform.position.z));
            }
            else 
            {
                Gizmos.color = Helper.ChangAlpha(Color.blue, 0.3f);
                Gizmos.DrawSphere(transform.position, detectDistance);
            }

        }
    }
}

