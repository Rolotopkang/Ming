using Autohand;
using UnityEngine;

public class ItemForgingTable : MonoBehaviour
{
    public PlacePoint slot;

    public void Upgrade()
    {
        if (slot.placedObject!=null)
        {
            ItemBase itemBase = slot.placedObject.GetComponent<ItemBase>();
            if (!itemBase.ItemData.isOnly)
            {
                int tmp_count = itemBase.ItemCount;
                slot.Remove();
                Grabbable grabbable = itemBase.transform.GetComponent<Grabbable>();
                grabbable.DoDestroy();
                Grabbable tmp_go = Instantiate(ItemDatabaseManager.GetInstance().GetRandomNoneIsOnlyItem(), transform).GetComponent<Grabbable>();
                tmp_go.transform.GetComponent<ItemBase>().ItemCount = tmp_count;
                slot.Place(tmp_go);
            }
            else
            {
                ShowWrongHint();
            }
        }
        else
        {
            ShowWrongHint();
        }
    }
    
    public void ShowWrongHint()
    {
        Debug.Log("不能升级！");
    }
}
