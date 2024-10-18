using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

namespace My2D
{
    public class Projectile : MonoBehaviour
    {
        #region Variables
        private Rigidbody2D rb2D;
        //이동
        [SerializeField] private Vector2 moveSpeed = new Vector2(5f, 0f);

        //데미지
        [SerializeField] private float attackDamage = 10f;
        [SerializeField] private Vector2 knockback = new Vector2(0f, 0f);

        //데미지 이펙트
        public GameObject impactEffectPrefab;

        #endregion

        private void Awake()
        {
            //참조
            rb2D = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            rb2D.velocity = new Vector2(moveSpeed.x * transform.localScale.x, moveSpeed.y);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Damageable damageable = collision.GetComponent<Damageable>();

            if(damageable != null)
            {
                Vector2 deliveredKnockback = (transform.localScale.x > 0) ? knockback : new Vector2(-knockback.x, knockback.y);  //x < 0 이면 반대 방향
                damageable.TakeDamage(attackDamage, deliveredKnockback);

                //데미지 이펙트
                GameObject effectGo = Instantiate(impactEffectPrefab, collision.transform.position, Quaternion.identity);
                Destroy(effectGo, 0.5f);

                //화살 킬
                Destroy(gameObject);
            }
        }
    }
}