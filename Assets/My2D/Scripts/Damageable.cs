using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace My2D
{
    public class Damageable : MonoBehaviour
    {
        #region Variables
        private Animator animator;

        //데미지 입을때 등록된 함수 호출
        public UnityAction<float, Vector2> hitAction;   //딜리게이트 함수

        //체력
        [SerializeField] private float maxHealth = 100f;
        public float MaxHealth
        {
            get { return maxHealth; }
            private set { maxHealth = value; }
        }

        [SerializeField] private float currentHealth;
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


        //넉백동안 움직임 제한
        public bool LockVelocity
        {
            get
            {
               return animator.GetBool(AnimationString.LockVelocity);
            }
            private set
            {
                animator.SetBool(AnimationString.LockVelocity, value);
            }
        }
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

        public void TakeDamage(float damage, Vector2 knockback)
        {
            if (!IsDeath && !isInvicible)
            {
                //무적모드 초기화
                isInvicible = true;

                //데미지 받기 전의 hp
                float beforeHealth = CurrentHealth;

                CurrentHealth -= damage;
                CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
                Debug.Log($"{transform.name}의 현재 체력은 {CurrentHealth}");

                //실제 데미지
                float realDamage = beforeHealth - CurrentHealth;

                LockVelocity = true;
                animator.SetTrigger(AnimationString.HitTrigger);    //애니메이션

                //이벤트 함수 호출
                hitAction?.Invoke(damage, knockback);    //?: if(hitAction != null)
                CharacterEvents.characterDamaged?.Invoke(gameObject, realDamage);
            }
        }

        //회복
        public bool Heal(float amount)
        {
            if (CurrentHealth >= MaxHealth)
            {
                return false;
            }

            //힐 전의 hp
            float beforeHealth = CurrentHealth;

            CurrentHealth += amount;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

            //실제 힐 hp값
            float realHealth = CurrentHealth - beforeHealth;

            CharacterEvents.characterHealed?.Invoke(gameObject, realHealth);    //실행

            return true;
        }
    }
}