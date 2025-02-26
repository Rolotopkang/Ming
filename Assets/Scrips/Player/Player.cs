using Tools;
using UnityEngine;

public class Player : Singleton<Player>
{
    public Transform GetRoot()
    {
        return transform.GetChild(0);
    }
}
