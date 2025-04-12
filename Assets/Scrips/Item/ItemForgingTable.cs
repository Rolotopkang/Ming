using Autohand;
using Tools;
using UnityEngine;

public class ItemForgingTable : Singleton<ItemForgingTable>
{
    public PlacePoint slot;
    public GameObject OutComeBallPrefab;
    public GameObject outcomePoint;
    public Transform HintPoint;

    public void Upgrade()
    {
        if (slot.placedObject!=null)
        {
            ItemBase itemBase = slot.placedObject.GetComponent<ItemBase>();
            if (!itemBase.ItemData.isOnly)
            {
                if (Player.GetInstance().TryBuy(5f))
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
                    ShowWrongHint(0);
                }
                
            }
            else
            {
                ShowWrongHint(1);
            }
        }
        else
        {
            ShowWrongHint(2);
        }
    }
    
    public void ShowWrongHint(int index)
    {
        switch (index)
        {
            case 0:
                HintManager.GetInstance().ShowHint("Need enough gold coins to upgrade! (5)", 4, HintPoint);
                break;
            case 1:
                HintManager.GetInstance().ShowHint("Please do not put in green quality items", 4, HintPoint);
                break;
            case 2:
                HintManager.GetInstance().ShowHint("Please put in the items that need to be recast", 4, HintPoint);
                break;
        }
    }
    
    public void updateNewPosition(Transform _transform)
    {
        transform.position = _transform.position;
    }
}
