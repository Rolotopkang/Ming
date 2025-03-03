using Tools;
using UnityEngine;

public class ItemTable : Singleton<ItemTable>
{
    public void updateNewPosition(Transform _transform)
    {
        transform.position = _transform.position;
    }
}
