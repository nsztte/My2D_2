using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My2D
{
    //������ �浹ü ����
    public class DetectionZone : MonoBehaviour
    {
        #region Variables
        //������ �ݶ��̴� ����Ʈ 
        public List<Collider2D> detectedColliders = new List<Collider2D>();
        #endregion

        private void OnTriggerEnter2D(Collider2D collision)
        {
            //�浹ü�� �����Ǹ� ����Ʈ�� �߰��Ѵ�
            detectedColliders.Add(collision);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            //�浹ü�� ������ ����Ʈ���� �����Ѵ�
            detectedColliders.Remove(collision);
        }
    }
}
