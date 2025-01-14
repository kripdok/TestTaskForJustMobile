using R3;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TestObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public ScrollRect scrollRect; // Ссылка на ScrollRect
    private ReactiveProperty<bool> isPointerOverScroll = new(); // Флаг для отслеживания состояния курсора

    private void Awake()
    {
        isPointerOverScroll.Subscribe(e => HandleScroll(e));
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerOverScroll.Value = true; // Устанавливаем флаг, когда курсор над скроллом
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerOverScroll.Value = false; // Сбрасываем флаг, когда курсор покидает скролл
    }

    private void HandleScroll(bool isFlag)
    {

        if (isFlag)
        {
            scrollRect.horizontal = true;
        }
        else
        {
            scrollRect.horizontal = false;
        }
    }

}
