using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TestTask.Editable
{
    public class ClientColor : MonoBehaviour
    {
        [SerializeField]
        private Image _colorImage;

        public Color Color { get; private set; }

        public void Initialize(Color color)
        {
            Color = color;

            if (_colorImage == null)
                _colorImage = GetComponent<Image>();

            _colorImage.color = Color;
        }
    }
}