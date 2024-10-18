using UnityEngine;

namespace My2D
{
    //발사체(화살) 발사
    public class ProjectileLauncher : MonoBehaviour
    {
        #region Variables
        public GameObject projectilePrefab;

        public Transform firePoint;
        #endregion

        //발사체(화살) 발사
        public void FireProjectile()
        {
            Debug.Log("화살 발사");
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, projectilePrefab.transform.rotation);
            Destroy(projectile, 5f);

            //화살의 방향 결정
            Vector3 originScale = projectile.transform.localScale;
            projectile.transform.localScale = new Vector3(
                originScale.x * transform.localScale.x > 0 ? 1 : -1,    //플레이어의 x 스케일 따라감
                originScale.y,
                originScale.z);
        }
    }
}