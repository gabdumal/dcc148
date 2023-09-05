using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float speed = 2f;
    private const float MAX_X = 0.75f;
    private const float MAX_Y = 0.75f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Capture current position
        Vector3 position = this.transform.position;

        // Capture movement input
        float vx = Input.GetAxis("Horizontal") * speed;
        float vy = Input.GetAxis("Vertical") * speed;

        // Calculate next position
        Vector3 v = new Vector3(vx, vy);
        position += v * Time.deltaTime;

        // Limit by boundaries
        if (position.y > MAX_Y)
            position.y = MAX_Y;
        else if (position.y < -MAX_Y)
            position.y = -MAX_Y;
        else if (position.x > MAX_X)
            position.x = MAX_Y;
        else if (position.x < -MAX_X)
            position.x = -MAX_Y;

        // Update position
        this.transform.position = position;

        // Capture selection
        bool spacePressed = Input.GetKeyDown(KeyCode.Space);
        if (spacePressed)
        {
            if (position.x > 0 && position.y > 0)
            {
                GameObject NEButton = GameObject.Find("Cube_NE");
                BotaoController controller = NEButton.GetComponent<BotaoController>();
                controller.PressionaBotao();
            }
            else if (position.x > 0 && position.y < 0)
            {
                GameObject NEButton = GameObject.Find("Cube_SE");
                BotaoController controller = NEButton.GetComponent<BotaoController>();
                controller.PressionaBotao();
            }
            else if (position.x < 0 && position.y > 0)
            {
                GameObject NEButton = GameObject.Find("Cube_NW");
                BotaoController controller = NEButton.GetComponent<BotaoController>();
                controller.PressionaBotao();
            }
            else if (position.x < 0 && position.y < 0)
            {
                GameObject NEButton = GameObject.Find("Cube_SW");
                BotaoController controller = NEButton.GetComponent<BotaoController>();
                controller.PressionaBotao();
            }
        }
    }
}
