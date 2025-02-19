using Autohand;
using UnityEngine;

public class ItemUpgradeTabel : MonoBehaviour
{
    public PlacePoint child1;
    public PlacePoint child2;
    public PlacePoint OutCome;
    public Transform OutComePos;

    public void Upgrade()
    {
        if (child1.placedObject != null && child2.placedObject != null)
        {
            ItemBase u1 = child1.placedObject.GetComponent<ItemBase>();
            ItemBase u2 = child2.placedObject.GetComponent<ItemBase>();

            if (u1.CheckCanUpgrade() && u2.CheckCanUpgrade() && u1.ItemData.itemName == u2.ItemData.itemName)
            {
                u1.UpgradeItem(u2);
                Grabbable tmp_g = child1.placedObject;
                Grabbable tmp_D = child2.placedObject;
                child1.Remove();
                child2.Remove();
                Destroy(tmp_D.gameObject);
                tmp_g.transform.position = OutCome.transform.position;
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
