using System;
using Autohand;
using Tools;
using UnityEngine;

public class ItemUpgradeTabel : Singleton<ItemUpgradeTabel>
{
    public PlacePoint child1;
    public PlacePoint child2;
    public Transform OutComePos;
    public Animator animator; // 绑定你的 Animator
    [Range(0, 1)] public float progress = 0f; // 0~1 的进度值
    public PhysicsGadgetLever PhysicsGadgetLever;

    private void Update()
    {
        progress = Mathf.Clamp(Mathf.Abs(PhysicsGadgetLever.GetValue()), 0f, 1f);
        animator.Play("Up", 0, progress == 1 ? 0.99f : progress);
    }

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
                tmp_D.DoDestroy();
                tmp_g.transform.position = OutComePos.transform.position;
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
        transform.rotation = _transform.rotation;   
    }
}
