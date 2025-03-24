using System;
using Autohand;
using Cysharp.Threading.Tasks;
using TMPro;
using Tools;
using UnityEngine;

public class HealingBottle : Singleton<HealingBottle>
{
    public Grabbable CandyPrefab;
    public TextMeshProUGUI text;

    private void Start()
    { 
        reGen(GetComponent<PlacePoint>());
        Player.GetInstance().CurrentHealthBottleCount -= 1;
    }
    
    private void Update()
    {
        text.text = GetComponent<PlacePoint>().placedObject != null ? (Player.GetInstance().CurrentHealthBottleCount+1).ToString() : Player.GetInstance().CurrentHealthBottleCount.ToString();
    }

    public void OnCandyTaken()
    {
        if (Player.GetInstance().CurrentHealthBottleCount>0)
        {
            reGen(GetComponent<PlacePoint>());
            Player.GetInstance().CurrentHealthBottleCount -= 1;
        }
    }
    
    private async void reGen(PlacePoint placePoint)
    {
        await UniTask.WaitForSeconds(1f);
        Grabbable clone = Instantiate(CandyPrefab,Vector3.zero, Quaternion.identity).GetComponent<Grabbable>();
        placePoint.Place(clone);
    }
}
