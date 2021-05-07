using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoopScrollMenu : MonoBehaviour
{
    public enum Direction
    {
        Horizontal,
        Vertical
    }

    public int m_ItemCount;

    public RectTransform ItemPrefab;

    public Direction direction;

    public float m_fSpace;

    private bool m_IsSetuped = false;

    private LinkedList<RectTransform> Items = new LinkedList<RectTransform>();

    private RectTransform m_RectTrans;

    private float anchoredPosition
    {
        get
        {
            return direction == Direction.Vertical ? -m_RectTrans.anchoredPosition.y : m_RectTrans.anchoredPosition.y;
        }
    }

    private float diffPreFramePosition = 0;

    private int currentItemNO = 0;

    private float ItemScale
    {
        get
        {
            float value = 0;
            if(ItemPrefab != null)
            {
                value = direction == Direction.Vertical ? ItemPrefab.sizeDelta.y : ItemPrefab.sizeDelta.x;
            }
            return value;
        }
    }

    private void LoopMenuSetup()
    {
        GetComponentInParent<ScrollRect>().movementType = ScrollRect.MovementType.Unrestricted;
        m_IsSetuped = true;
    }

    private void ItemSetup(int itemIndex, GameObject obj)
    {
        if (m_IsSetuped)
        {
            return;
        }
        obj.GetComponentInChildren<Item>().SetItem(itemIndex);
    }

    void Start()
    {
        m_RectTrans = GetComponent<RectTransform>();
        ScrollRect rect = GetComponentInParent<ScrollRect>();

        rect.horizontal = direction == Direction.Horizontal;
        rect.vertical = direction == Direction.Vertical;
        rect.content = m_RectTrans;

        ItemPrefab.gameObject.SetActive(false);

        for(int i = 0; i < m_ItemCount; i++)
        {
            RectTransform item = Instantiate(ItemPrefab) as RectTransform;
            item.SetParent(this.transform, false);
            item.name = i.ToString("item00");
            item.anchoredPosition = (direction == Direction.Vertical) ? new Vector2(0, (-ItemScale - m_fSpace) * i) : new Vector2((ItemScale + m_fSpace) * i, 0);
            Items.AddLast(item);
            item.gameObject.SetActive(true);
            ItemSetup(i, item.gameObject);
        }
        LoopMenuSetup();
    }

    void Update()
    {
        while(anchoredPosition - diffPreFramePosition < -(ItemScale + m_fSpace) * 2)
        {
            diffPreFramePosition -= (ItemScale + m_fSpace);

            RectTransform item = Items.First.Value;
            Items.RemoveFirst();
            Items.AddLast(item);

            var pos = (ItemScale + m_fSpace) * m_ItemCount + (ItemScale + m_fSpace) * currentItemNO;
            item.anchoredPosition = (direction == Direction.Vertical) ? new Vector2(0, -pos) : new Vector2(pos, 0);

            currentItemNO++;
        }

        while(anchoredPosition - diffPreFramePosition > 0)
        {
            diffPreFramePosition += (ItemScale + m_fSpace);

            RectTransform item = Items.Last.Value;
            Items.RemoveLast();
            Items.AddFirst(item);

            currentItemNO++;

            var pos = (ItemScale + m_fSpace) * currentItemNO;

            item.anchoredPosition = (direction == Direction.Vertical) ? new Vector2(0, -pos) : new Vector2(pos, 0);
        }


    }
}


