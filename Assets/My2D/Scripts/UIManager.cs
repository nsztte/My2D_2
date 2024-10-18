using UnityEngine;
using TMPro;

namespace My2D
{

    public class UIManager : MonoBehaviour
    {
        #region Variables
        public GameObject damageTextPrefab;
        public GameObject healTextPrefab;

        private Canvas canvas;
        [SerializeField] private Vector3 healthTextOffset = Vector3.zero;
        #endregion

        private void Awake()
        {
            canvas = FindObjectOfType<Canvas>();    //���̶�Űâ�� �ִ� Ȱ��ȭ�� ������Ʈ ã�� �Լ�
        }

        private void OnEnable()     //Prefab �����ɶ�
        {
            //ĳ���� ���� �̺�Ʈ �Լ� ���
            CharacterEvents.characterDamaged += CharacterTakeDamage;
            CharacterEvents.characterHealed += CharacterHealed;
        }

        private void OnDestroy()    //Prefab ���ŵɶ�
        {
            //ĳ���� ���� �̺�Ʈ �Լ� ����
            CharacterEvents.characterDamaged -= CharacterTakeDamage;
            CharacterEvents.characterHealed -= CharacterHealed;
        }

        public void CharacterTakeDamage(GameObject character, float damage)
        {
            //damageTextPrefab ����
            Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);   //���� �������� ��ũ�� �����ǿ� ����

            GameObject textGo = Instantiate(damageTextPrefab, spawnPosition + healthTextOffset, Quaternion.identity, canvas.transform);
            TextMeshProUGUI damageText = textGo.GetComponent<TextMeshProUGUI>();
            damageText.text = damage.ToString();
        }

        public void CharacterHealed(GameObject character, float restore)    //����
        {
            //healTextPrefab ����
            Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);   //���� �������� ��ũ�� �����ǿ� ����

            GameObject textGo = Instantiate(healTextPrefab, spawnPosition + healthTextOffset, Quaternion.identity, canvas.transform);
            TextMeshProUGUI healText = textGo.GetComponent<TextMeshProUGUI>();
            healText.text = restore.ToString();
        }
    }
}