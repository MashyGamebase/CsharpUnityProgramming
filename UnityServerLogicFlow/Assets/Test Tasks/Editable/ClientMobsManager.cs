using TestTask.NonEditable;
using UnityEngine;
using UnityEngine.UI;

namespace TestTask.Editable
{
    public class ClientMobsManager : MonoBehaviour
    {
        public static ClientMobsManager Instance { get; private set; }

        private MonsterData _monsterData;

        public ClientMonster clientMonsterPrefab;
        private ClientMonster _clientMonsterRuntime;

        public RectTransform monsterSpawnAnchor;

        public Button ApplyDamageButton;
        public float Damage = 10;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        public void SpawnMonster(MonsterData monsterData)
        {
            // Clear stored data before assigning new incoming data
            ResetData();

            _monsterData = monsterData;

            var monster = Instantiate(clientMonsterPrefab, monsterSpawnAnchor, false);

            if(monster.TryGetComponent(out ClientMonster cm))
            {
                cm.Initialize(_monsterData);
                ApplyDamageButton.onClick.AddListener(() => cm.ApplyDamage(Damage));
                _clientMonsterRuntime = cm;
            }
        }


        // Helper method to reset data
        private void ResetData()
        {
            _monsterData = null;

            ApplyDamageButton.onClick.RemoveAllListeners();

            if (_clientMonsterRuntime != null)
            {
                Destroy(_clientMonsterRuntime.gameObject);
                _clientMonsterRuntime = null;
            }
        }
    }
}
