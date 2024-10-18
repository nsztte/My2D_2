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

        public Vector2 knockback = Vector2.zero;
        #endregion


        //충돌 체크해서 공격력만큼 데미지 준다
        private void OnTriggerEnter2D(Collider2D collision)
        {
            //데미지 입는 객체 찾기
            Damageable damagable = collision.GetComponent<Damageable>();

            if(damagable != null)
            {
                //knockback 방향 설정
                Vector2 deliveredKnockback = (transform.parent.localScale.x > 0) ? knockback : new Vector2(-knockback.x, knockback.y);  //x < 0 이면 반대 방향
                //Debug.Log($"{collision.name} 데미지를 입었다");
                damagable.TakeDamage(attackDamage, deliveredKnockback);
            }
        }

    }
}
