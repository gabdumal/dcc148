using UnityEngine;

public class WalkerObject : MonoBehaviour
{
    private Vector2 position;
    private Vector2 direction;
    private float chanceToChangeDirection;
    // public float chanceToAddRoom;

    public WalkerObject(Vector2 position, Vector2 direction, float chanceToChangeDirection)
    {
        this.position = position;
        this.direction = direction;
        this.chanceToChangeDirection = chanceToChangeDirection;
    }

    public Vector2 GetPosition()
    {
        return this.position;
    }

    public float GetChanceToChangeDirection()
    {
        return this.chanceToChangeDirection;
    }

    public void SetDirection(Vector2 direction)
    {
        this.direction = direction;
    }

    public void UpdatePosition(int gridWidth, int gridHeight)
    {
        this.position += this.direction;
        this.position.x = Mathf.Clamp(this.position.x, 1, gridWidth - 2);
        this.position.y = Mathf.Clamp(this.position.y, 1, gridHeight - 2);
    }
}
