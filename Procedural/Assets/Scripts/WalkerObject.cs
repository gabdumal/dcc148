using UnityEngine;

public class WalkerObject : MonoBehaviour
{
    public Vector2 position;
    public Vector2 direction;
    public float chanceToChangeDirection;
    // public float chanceToAddRoom;

    public WalkerObject(Vector2 position, Vector2 direction, float chanceToChangeDirection){
        this.position = position;
        this.direction = direction;
        this.chanceToChangeDirection = chanceToChangeDirection;
    }

}
