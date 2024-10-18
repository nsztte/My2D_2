using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My2D
{
    public class PickupHealth : MonoBehaviour
    {
        #region Variables
        //힐
        [SerializeField] private float restoreHealth = 20f;
        [SerializeField] private Vector3 rotateSpeed = new Vector3(0f, 180f, 0f);
        #endregion

        private void Update()
        {
            //회전
            transform.eulerAngles += rotateSpeed * Time.deltaTime;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            //충돌한 오브젝트 damagable 여부 검사하여 힐
            Damageable damageable = collision.GetComponent<Damageable>();
            if (damageable != null)
            {
                bool isHeal = damageable.Heal(restoreHealth);

                if (isHeal)
                {
                    //아이템 킬
                    Destroy(gameObject);
                }
            }
        }
    }
}