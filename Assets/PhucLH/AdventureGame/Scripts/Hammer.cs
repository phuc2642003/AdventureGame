using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace PhucLH.AdventureGame
{
    public class Hammer : MonoBehaviour
    {
        public LayerMask enemyLayer;
        public float attackRadius;
        public Vector3 offset;
        
        [SerializeField]
        private Player player;
        
        public void Attack()
        {
            if (player == null) return;

            Collider2D col = Physics2D.OverlapCircle(transform.position + offset, attackRadius, enemyLayer);
            if(col)
            {
                Enemy enemy = col.GetComponent<Enemy>();
                if(enemy)
                {
                    enemy.TakeDamage(player.stat.Damage, player);
                }
            }
        }
        private void Update()
        {
            if (player == null)
                return;
            
            if(player.transform.localScale.x>0)
            {
                if(offset.x<0)
                {
                    offset = new Vector3(-offset.x, offset.y, offset.z);
                }
            }
            else
            {
                if(offset.x>0)
                {
                    offset = new Vector3(-offset.x, offset.y, offset.z);
                }
            }
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Helper.ChangAlpha(Color.green,0.5f);
            Gizmos.DrawSphere(transform.position + offset, attackRadius);
        }
    }
}

