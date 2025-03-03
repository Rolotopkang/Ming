using System;
using Autohand;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ItemPlaceWall : MonoBehaviour
{
    private PlacePoint[] _placePoints;

    private void Awake()
    {
        _placePoints = GetComponentsInChildren<PlacePoint>();
    }

    private void Start()
    {
        foreach (PlacePoint placePoint in _placePoints)
        {
            placePoint.OnRemove.AddListener((PlacePoint placePoint, Grabbable grabbable) =>
            {
                
                reGen(placePoint,grabbable);
            });
        }

        int i = 0;
        foreach (GameObject gameObject in ItemDatabaseManager.GetInstance().GetItemDictionary().Values)
        {
            if (i >= _placePoints.Length)
            {
                Debug.LogWarning("place wall not enough!!!!!!!!!");
                return;
            }
            _placePoints[i++].Place(Instantiate(gameObject).GetComponent<Grabbable>());
        }
    }

    private async void reGen(PlacePoint placePoint, Grabbable grabbable)
    {
        await UniTask.WaitForSeconds(1f);
        Grabbable clone = Instantiate(ItemDatabaseManager.GetInstance().GetItemGOByName(grabbable.GetComponent<ItemBase>().ItemData.itemName),Vector3.zero, Quaternion.identity).GetComponent<Grabbable>();
        placePoint.Place(clone);
        // placePoint.StopHighlight(clone);
    }
}
