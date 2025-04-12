using Tools;
using UnityEngine;

public class HintManager : Singleton<HintManager>
{
    public GameObject HintPrefab;

    public Hint ShowHint(string content, float lifetime, Transform target)
    {
        Hint tmp_UI = Instantiate(HintPrefab, target.position, Quaternion.identity).GetComponent<Hint>();
        tmp_UI.Init(content,lifetime);
        return tmp_UI;
    }
    
}
