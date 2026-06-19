using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TestTask.NonEditable;

namespace TestTask.Editable
{
    public class ClientColors : MonoBehaviour
    {
        public static ClientColors Instance { get; private set; }

        [SerializeField]
        private ClientColor _clientColorPrefab;

        [SerializeField]
        private RectTransform _colorsContainer;

        private readonly List<ClientColor> _clientColorRuntime = new List<ClientColor>();

        public Button RequestColorButton;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        private void OnEnable()
        {
            RequestColorButton.onClick.AddListener(RequestColors);
        }

        public void RequestColors()
        {
            ClientPacketsHandler.SendColorListRequest();
        }

        public void SpawnColors(List<Color> colors)
        {
            ResetData();

            foreach(var color in colors)
            {
                var clientColor = Instantiate(_clientColorPrefab, _colorsContainer, false);

                if(clientColor.TryGetComponent(out ClientColor cc))
                {
                    cc.Initialize(color);

                    _clientColorRuntime.Add(cc);
                }
            }
        }

        private void ResetData()
        {
            for (int i = 0; i < _clientColorRuntime.Count; i++)
            {
                if (_clientColorRuntime[i] != null)
                {
                    Destroy(_clientColorRuntime[i].gameObject);
                }
            }

            _clientColorRuntime.Clear();
        }

        private void OnDisable()
        {
            RequestColorButton.onClick.RemoveAllListeners();
        }
    }
}
