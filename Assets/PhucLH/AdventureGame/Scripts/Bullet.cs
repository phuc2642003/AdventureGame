using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

namespace PhucLH.AdventureGame
{
    public class Bullet : MonoBehaviour
    {
        public float speed;
        public LayerMask targetLayer;
        public float timeToInactive;
        public Vector3 offSet;
        private Vector3 startPosition;
        private float activeTime;
       

        [HideInInspector]
        public Actor owner;
        private void Awake()
        {
            activeTime = timeToInactive;
            startPosition = transform.position;   
        }

        private void Update()
        {
            transform.Translate(transform.right * speed * Time.deltaTime, Space.World);
        }
        private void FixedUpdate()
        {
            InactiveDelay();
            
            Vector2 bulletDirection = (Vector2)(transform.position - startPosition);
            float distance = bulletDirection.magnitude;
            startPosition = transform.position;
            RaycastHit2D hit = Physics2D.Raycast(startPosition, bulletDirection, distance, targetLayer);
            if (hit && hit.collider)
            {
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                if (enemy)
                {
                    enemy.TakeDamage(owner.stat.Damage, owner);
                }
                InactiveObject();
            }
        }
        private void InactiveDelay()
        {
            if (gameObject.activeInHierarchy)
            {
                activeTime -= Time.deltaTime;
                if (activeTime <= 0)
                {
                    InactiveObject();
                }    
            }     
        }

        private void InactiveObject()
        {
            gameObject.SetActive(false);
            activeTime = timeToInactive;
        }
    }
}

