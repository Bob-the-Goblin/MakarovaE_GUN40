using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Project
{
    public class Cell : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
    {
        [SerializeField]
        private MeshRenderer _focus;
        [SerializeField]
        private MeshRenderer _select;
        [SerializeField]
        public Unit unit { get; set; }

        public event Action<Cell> OnPointerClickEvent;
        public void SetSelect(Material material)
        {
            (_select.enabled, _select.material) = (true, material);
        }

        public void ResetSelect()
        {
            _select.enabled = false;
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            OnPointerClickEvent.Invoke(this);
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            _focus.enabled = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _focus.enabled = false;
        }


    }
}