using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My2D
{
    public class Damageable : MonoBehaviour
    {
        #region Variables
        private Animator animator;
        //ü��
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

                //����
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
                //�ִϸ��̼�
                animator.SetBool(AnimationString.IsDeath, value);
            }
        }

        //�������
        private bool isInvicible = false;
        [SerializeField] private float invicibleTimer = 3f;
        private float countdown = 0f;
        #endregion


        private void Awake()
        {
            //����
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            //�ʱ�ȭ
            CurrentHealth = MaxHealth;
            countdown = invicibleTimer;
        }

        private void Update()
        {
            //���������̸� ���� Ÿ�̸Ӹ� ������
            if (isInvicible)
            {
                if(countdown <= 0f)
                {
                    isInvicible = false;

                    //Ÿ�̸� �ʱ�ȭ
                    countdown = invicibleTimer;
                }
                countdown -= Time.deltaTime;
            }
        }

        public void TakeDamage(float damage)
        {
            if (!IsDeath && !isInvicible)
            {
                //������� �ʱ�ȭ
                isInvicible = true;

                CurrentHealth -= damage;
                Debug.Log($"{transform.name}�� ���� ü���� {CurrentHealth}");

                //�ִϸ��̼�
                animator.SetTrigger(AnimationString.HitTrigger);
            }
        }
    }
}