using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float xSpeed;
    public float playerXLimit;
    public GameObject enemy;
    [SerializeField] private float playerHalfWidth;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Bounds spriteBounds;
    private SpriteRenderer enemySpriteRenderer;
    [SerializeField] private Bounds enemySpriteBounds;
    [SerializeField] private bool thereIsCollision;

    // Start is called before the first frame update
    void Start()
    {
        this.playerHalfWidth = this.transform.localScale.x / 2;
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
        this.enemySpriteRenderer = enemy.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float inputXOffset = Input.GetAxis("Horizontal");
        float realXOffset = inputXOffset * Time.fixedDeltaTime * this.xSpeed;
        float newXPosition = this.transform.position.x + realXOffset;
        if ((newXPosition + playerHalfWidth) < playerXLimit && (newXPosition - playerHalfWidth) > -playerXLimit)
        {
            Vector3 movement = new Vector3(realXOffset, 0, 0);
            this.transform.Translate(movement);
        }

        // Collision
        this.spriteBounds = spriteRenderer.bounds;
        this.enemySpriteBounds = enemySpriteRenderer.bounds;

        Vector2 centerDelta = (this.spriteBounds.center - enemySpriteBounds.center);
        float centerDeltaSquared = centerDelta.magnitude * centerDelta.magnitude;

        float thisRadius = this.spriteBounds.extents.x;
        float enemyRadius = enemySpriteBounds.extents.x;
        float radiusSum = (thisRadius + enemyRadius);
        float radiusSumSquared = radiusSum * radiusSum;

        this.thereIsCollision = centerDeltaSquared <= radiusSumSquared;

    }
}
