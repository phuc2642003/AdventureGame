using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhucLH.AdventureGame
{
    public class Bullet : MonoBehaviour
    {
        public float speed;
        public LayerMask enemyLayer;
        private Vector3 startPosition;

        [HideInInspector]
        public Actor owner;
        private void Awake()
        {
            startPosition = transform.position;   
        }

        private void Update()
        {
            transform.Translate(transform.right * speed * Time.deltaTime, Space.World);
        }
        private void FixedUpdate()
        {
            Vector2 bulletDirection = (Vector2)(transform.position - startPosition);
            float distance = bulletDirection.magnitude;
            RaycastHit2D hit = Physics2D.Raycast(startPosition, bulletDirection, distance, enemyLayer);
            if (hit && hit.collider)
            {
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                if (enemy)
                {
                    enemy.TakeDamage(owner.stat.Damage, owner);
                }
                gameObject.SetActive(false);
            }

            startPosition = transform.position;
        }

    }
}

