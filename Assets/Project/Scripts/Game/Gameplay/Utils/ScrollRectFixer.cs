using R3;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Project.Scripts.Game.Gameplay.Utils
{
    public class ScrollRectFixer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private ScrollRect scrollRect;

        private ReactiveProperty<bool> isPointerOverScroll = new();

        private void Awake()
        {
            isPointerOverScroll.Subscribe(e => HandleScroll(e));
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            isPointerOverScroll.Value = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isPointerOverScroll.Value = false;
        }

        private void HandleScroll(bool isFlag)
        {
            scrollRect.horizontal = isFlag;
        }
    }
}

