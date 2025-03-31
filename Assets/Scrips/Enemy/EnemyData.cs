using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Game/Enemy")]
public class EnemyData : ScriptableObject
{
    public string EnemyName;
    public bool isBoss = false;
    public AnimationCurve MaxHPCurve;
    public AnimationCurve SpeedCurve;
    public AnimationCurve BaseAtkCurve;
    

}
