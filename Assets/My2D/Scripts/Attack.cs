using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My2D
{
    public class Attack : MonoBehaviour
    {
        #region
        //���ݷ�
        [SerializeField] private float attackDamage = 10f;
        #endregion


        //�浹 üũ�ؼ� ���ݷ¸�ŭ ������ �ش�
        private void OnTriggerEnter2D(Collider2D collision)
        {
            //������ �Դ� ��ü ã��
            Damageable damagable = collision.GetComponent<Damageable>();

            if(damagable != null)
            {
                //Debug.Log($"{collision.name} �������� �Ծ���");
                damagable.TakeDamage(attackDamage);
            }
        }

    }
}
