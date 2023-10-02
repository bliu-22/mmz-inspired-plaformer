using UnityEngine;

[CreateAssetMenu]
public class PlayerDataStorage : ScriptableObject
{
  
    public Vector2 initialPos;
    public float initialHealth;
    public int initialFacingDirection;

    public void ResetToDefault() 
    {
        initialPos = new Vector2(2.4f, 0);
        initialHealth = 10;
        initialFacingDirection = 1;
    }
}
