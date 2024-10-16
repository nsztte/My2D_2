using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My2D
{
    public class Attack : MonoBehaviour
    {
        #region
        //공격력
        [SerializeField] private float attackDamage = 10f;
        #endregion


        //충돌 체크해서 공격력만큼 데미지 준다
        private void OnTriggerEnter2D(Collider2D collision)
        {
            //데미지 입는 객체 찾기
            Damageable damagable = collision.GetComponent<Damageable>();

            if(damagable != null)
            {
                //Debug.Log($"{collision.name} 데미지를 입었다");
                damagable.TakeDamage(attackDamage);
            }
        }

    }
}
