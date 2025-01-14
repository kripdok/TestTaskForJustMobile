using R3;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TestObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public ScrollRect scrollRect; // ������ �� ScrollRect
    private ReactiveProperty<bool> isPointerOverScroll = new(); // ���� ��� ������������ ��������� �������

    private void Awake()
    {
        isPointerOverScroll.Subscribe(e => HandleScroll(e));
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerOverScroll.Value = true; // ������������� ����, ����� ������ ��� ��������
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerOverScroll.Value = false; // ���������� ����, ����� ������ �������� ������
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
