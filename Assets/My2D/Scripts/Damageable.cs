using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My2D
{
    public class Damageable : MonoBehaviour
    {
        #region Variables
        private Animator animator;
        //체력
        [SerializeField] private float maxHealth = 100f;
        public float MaxHealth
        {
            get { return maxHealth; }
            private set { maxHealth = value; }
        }

        private float currentHealth;
        public float CurrentHealth
        {
            get { return currentHealth; }
            private set
            {
                currentHealth = value;

                //죽음
                if(currentHealth <= 0)
                {
                    IsDeath = true;
                }
            }
        }

        private bool isDeath = false;
        public bool IsDeath
        {
            get { return isDeath; }
            private set
            {
                isDeath = value;
                //애니메이션
                animator.SetBool(AnimationString.IsDeath, value);
            }
        }

        //무적모드
        private bool isInvicible = false;
        [SerializeField] private float invicibleTimer = 3f;
        private float countdown = 0f;
        #endregion


        private void Awake()
        {
            //참조
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            //초기화
            CurrentHealth = MaxHealth;
            countdown = invicibleTimer;
        }

        private void Update()
        {
            //무적상태이면 무적 타이머를 돌린다
            if (isInvicible)
            {
                if(countdown <= 0f)
                {
                    isInvicible = false;

                    //타이머 초기화
                    countdown = invicibleTimer;
                }
                countdown -= Time.deltaTime;
            }
        }

        public void TakeDamage(float damage)
        {
            if (!IsDeath && !isInvicible)
            {
                //무적모드 초기화
                isInvicible = true;

                CurrentHealth -= damage;
                Debug.Log($"{transform.name}의 현재 체력은 {CurrentHealth}");

                //애니메이션
                animator.SetTrigger(AnimationString.HitTrigger);
            }
        }
    }
}