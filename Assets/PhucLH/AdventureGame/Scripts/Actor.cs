using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhucLH.AdventureGame
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Actor : MonoBehaviour
    {
        [Header("Common:")]
        public ActorStat stat;
        [Header("Layer:")]
        [LayerList]
        public int normalLayer;
        [LayerList]
        public int invincibleLayer;
        [LayerList]
        public int deadLayer;

        [Header("Reference:")]
        [SerializeField]
        protected Animator m_anim;
        protected Rigidbody2D m_rb;

        [Header("Vfx:")]
        public FlashVfx flashVfx;
        public GameObject deadVfxPb;

        protected Actor m_whoHit;

        protected int m_curHp;
        protected bool m_isKnockBack;
        protected bool m_isInvincible;
        protected float m_startingGrav;
        protected bool m_isFacingLeft;
        protected float m_curSpeed;
        protected int m_hozDir, m_vertDir;

        public int CurHp { get => m_curHp;
            set
            {
                m_curHp = value;
                OnPlayerHPChange();
            }  
        }
        public float CurSpeed { get => m_curSpeed; }

        public bool IsFacingLeft
        {
            get => m_isFacingLeft;
        }

        protected virtual void Awake()
        {
            m_rb = GetComponent<Rigidbody2D>();
            if (m_rb)
                m_startingGrav = m_rb.gravityScale;

            if (stat == null) return;

            m_curHp = stat.hp;
            m_curSpeed = stat.moveSpeed;
        }

        public virtual void Start()
        {
            Init();
        }

        protected virtual void Init()
        {

        }

        public virtual void TakeDamage(int dmg, Actor whoHit = null)
        {
            if (m_isInvincible || m_isKnockBack) return;

            if (CurHp > 0)
            {
                m_whoHit = whoHit;
                CurHp -= dmg;

                if (CurHp <= 0)
                {
                    CurHp = 0;
                    Dead();
                }
                KnockBack();
            }
        }

        protected void KnockBack()
        {
            if (m_isInvincible || m_isKnockBack || !gameObject.activeInHierarchy) return;

            m_isKnockBack = true;

            StartCoroutine(StopKnockBack());

            if (flashVfx)
            {
                flashVfx.Flash(stat.invincibleTime);
            }
        }

        protected IEnumerator StopKnockBack()
        {
            yield return new WaitForSeconds(stat.knockBackTime);

            m_isKnockBack = false;
            m_isInvincible = true;
            gameObject.layer = invincibleLayer;
            StartCoroutine(StopInvincible(stat.invincibleTime));
        }

        protected IEnumerator StopInvincible(float time)
        {
            yield return new WaitForSeconds(time);

            m_isInvincible = false;
            gameObject.layer = normalLayer;
        }

        protected void KnockBackMove(float yRate)
        {
            if(m_whoHit == null)
            {
                m_vertDir = m_vertDir == 0 ? 1 : m_vertDir;
                m_rb.velocity = new Vector2(m_hozDir * stat.knockBackForce, 0.55f * m_vertDir * stat.knockBackForce);
            }
            else
            {
                Vector2 hiterToActor = m_whoHit.transform.position - transform.position;
                hiterToActor.Normalize();
                if(hiterToActor.x>0)
                {
                    m_rb.velocity = new Vector2(-stat.knockBackForce, yRate * stat.knockBackForce);
                }
                else if(hiterToActor.x<0)
                {
                    m_rb.velocity = new Vector2(stat.knockBackForce, yRate * stat.knockBackForce);
                }
            }
        }

        protected void Flip(Direction moveDir)
        {
            switch (moveDir)
            {
                case Direction.Left:
                    if (transform.localScale.x > 0)
                    {
                        transform.localScale = new Vector3(
                            transform.localScale.x * -1,
                            transform.localScale.y,
                            transform.localScale.z
                            );
                        m_isFacingLeft = true;
                    }
                    break;
                case Direction.Right:
                    if (transform.localScale.x < 0)
                    {
                        transform.localScale = new Vector3(
                            transform.localScale.x * -1,
                            transform.localScale.y,
                            transform.localScale.z
                            );
                        m_isFacingLeft = false;
                    }
                    break;
                case Direction.Up:
                    if (transform.localScale.y < 0)
                    {
                        transform.localScale = new Vector3
                        (
                            transform.localScale.x,
                            transform.localScale.y * -1,
                            transform.localScale.z
                        );
                    }
                    break;
                case Direction.Down:
                    if (transform.localScale.y > 0)
                    {
                        transform.localScale = new Vector3
                        (
                            transform.localScale.x,
                            transform.localScale.y * -1,
                            transform.localScale.z
                        );
                    }
                    break;
            }
        }

        protected virtual void Dead()
        {
            gameObject.layer = deadLayer;

            if (m_rb)
                m_rb.velocity = Vector2.zero;
        }

        protected void ReduceActionRate(ref bool isActed, ref float curTime, float startingTime)
        {
            if (isActed)
            {
                curTime -= Time.deltaTime;
                if (curTime <= 0)
                {
                    isActed = false;
                    curTime = startingTime;
                }
            }
        }

        public virtual void OnPlayerHPChange()
        {
            
        }
    }
}

