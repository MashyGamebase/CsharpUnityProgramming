using System.Collections;
using System.Collections.Generic;
using TestTask.NonEditable;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace TestTask.Editable
{
    public class ClientMonster : MonoBehaviour
    {
        public MonsterData MonsterData { get; private set; }

        [SerializeField]
        private List<MonsterIDKey> _monsterIdKey;


        [SerializeField] private Image _monsterImageContainer;
        [SerializeField] private Image _monsterHealthContainer;
        [SerializeField] private TMP_Text _monsterNameContainer;


        public void Initialize(MonsterData data)
        {
            MonsterData = data;

            _monsterNameContainer.text = data.MonsterName;

            _monsterImageContainer.sprite = _monsterIdKey.FirstOrDefault(t => t.MonsterID == data.MonsterName)?.MonsterSprite;

            _monsterHealthContainer.fillAmount = (data.MonsterCurrentHealth / data.MonsterMaxHealth);

            data.MonsterDamaged += HandleMonsterDamaged;
        }

        public void ApplyDamage(float damage)
        {
            ClientPacketsHandler.SendMonsterDamageRequest(damage);
        }


        void HandleMonsterDamaged(float damage)
        {
            _monsterHealthContainer.fillAmount = damage;
        }
    }

    [System.Serializable]
    public class MonsterIDKey
    {
        public string MonsterID;
        public Sprite MonsterSprite;
    }
}