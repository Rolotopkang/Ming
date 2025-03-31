using Autohand;
using Tools;
using UnityEngine;

public class ItemForgingTable : Singleton<ItemForgingTable>
{
    public PlacePoint slot;
    public GameObject OutComeBallPrefab;
    public GameObject outcomePoint;

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
                OutComeBall tmp_pp = Instantiate(OutComeBallPrefab, outcomePoint.transform.position, Quaternion.identity).GetComponent<OutComeBall>();
                tmp_pp.Init(tmp_go);
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
    
    public void updateNewPosition(Transform _transform)
    {
        transform.position = _transform.position;
    }
}
