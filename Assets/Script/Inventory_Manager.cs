using UnityEngine;

public class Inventory_Manager : MonoBehaviour
{
    private static Inventory_Manager instance;
    public SlotData slotPrefab;
    public Inventory myBag;
    public GameObject slotGrid;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;

        Inventory_Manager.Init();
    }

    public static void Init()
    {
        instance.myBag.itemList.Clear();
        instance.myBag.itemHold.Clear();
    }
    public static void Refresh()
    {
        for (int i = 0; i < instance.slotGrid.transform.childCount; i++)
        {
            if (instance.slotGrid.transform.childCount == 0)
                break;
            Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
        }
        for(int i = 0;i< instance.myBag.itemList.Count; i++)
        {
            SlotData NewSlot = Instantiate(instance.slotPrefab, instance.slotGrid.transform.position,
                Quaternion.identity, instance.slotGrid.transform);

            NewSlot.slotImage.sprite = instance.myBag.itemList[i].itemImage;
            NewSlot.slotNum.text = instance.myBag.itemHold[i].ToString();
        }
    }
}


