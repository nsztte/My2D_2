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
            canvas = FindObjectOfType<Canvas>();    //하이라키창에 있는 활성화된 오브젝트 찾는 함수
        }

        private void OnEnable()     //Prefab 생성될때
        {
            //캐릭터 관련 이벤트 함수 등록
            CharacterEvents.characterDamaged += CharacterTakeDamage;
            CharacterEvents.characterHealed += CharacterHealed;
        }

        private void OnDestroy()    //Prefab 제거될때
        {
            //캐릭터 관련 이벤트 함수 제거
            CharacterEvents.characterDamaged -= CharacterTakeDamage;
            CharacterEvents.characterHealed -= CharacterHealed;
        }

        public void CharacterTakeDamage(GameObject character, float damage)
        {
            //damageTextPrefab 스폰
            Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);   //월드 포지션을 스크린 포지션에 적용

            GameObject textGo = Instantiate(damageTextPrefab, spawnPosition + healthTextOffset, Quaternion.identity, canvas.transform);
            TextMeshProUGUI damageText = textGo.GetComponent<TextMeshProUGUI>();
            damageText.text = damage.ToString();
        }

        public void CharacterHealed(GameObject character, float restore)    //구현
        {
            //healTextPrefab 스폰
            Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);   //월드 포지션을 스크린 포지션에 적용

            GameObject textGo = Instantiate(healTextPrefab, spawnPosition + healthTextOffset, Quaternion.identity, canvas.transform);
            TextMeshProUGUI healText = textGo.GetComponent<TextMeshProUGUI>();
            healText.text = restore.ToString();
        }
    }
}