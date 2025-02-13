using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Game/Enemy")]
public class EnemyData : ScriptableObject
{
    public string EnemyName;
    public float MaxHealth;
    
}
