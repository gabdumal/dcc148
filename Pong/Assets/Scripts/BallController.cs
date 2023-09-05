using UnityEngine;
using TMPro;

public class BallController : MonoBehaviour
{

    public GameObject player;
    public GameObject computer;
    public GameObject playerScoreTxt;
    public GameObject computerScoreTxt;
    [SerializeField] private float ballSpeed;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float computerGoingUp = 1;
    [SerializeField] private int playerScore = 0;
    [SerializeField] private int computerScore = 0;
    private Vector2 direction;
    private int playerDidLastPoint = -1;
    private float maxX = 8.75f;
    private float maxY = 4.75f;
    private float barRemainingLength = 0.5f;

    void ResetBall()
    {
        this.transform.position = new Vector2(0, 0);
        float dice = Random.Range(-1, 1);
        if (dice < 0)
            dice = -1;
        else dice = 1;
        direction = new Vector2(playerDidLastPoint * 0.5f, dice * Random.Range(-0.5f, 0.5f));
        direction.Normalize();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Inicia o vetor de direção da bola, saindo para a esquerda na diagonal
        this.ResetBall();
    }

    void FixedUpdate()
    {
        // Move a bola
        Vector3 dv = direction * ballSpeed * Time.fixedDeltaTime;
        this.transform.Translate(dv);

        // Reflete a direção da bola caso bata no teto ou no piso
        if (this.transform.position.y >= maxY)
            direction = Vector2.Reflect(direction, Vector2.down);
        else if (this.transform.position.y <= -maxY)
            direction = Vector2.Reflect(direction, Vector2.up);

        // Move o jogador
        float inputDeltaY = Input.GetAxis("Horizontal");
        float playerDeltaY = -inputDeltaY * playerSpeed * Time.fixedDeltaTime;
        if (player.transform.position.y + playerDeltaY + barRemainingLength < maxY
            && player.transform.position.y + playerDeltaY - barRemainingLength > -maxY)
            player.transform.Translate(0, playerDeltaY, 0, Space.World);

        // Move o computador
        float computerDeltaY = computerGoingUp * playerSpeed * Time.fixedDeltaTime;
        if (computer.transform.position.y + computerDeltaY + barRemainingLength < maxY
            && computer.transform.position.y + computerDeltaY - barRemainingLength > -maxY)
            computer.transform.Translate(0, computerDeltaY, 0, Space.World);
        else
            computerGoingUp *= -1;

        // Computa os pontos
        if (this.transform.position.x >= maxX)
        {
            playerScore++;
            playerDidLastPoint = 1;
            playerScoreTxt.GetComponent<TMP_Text>().SetText(playerScore.ToString());
            this.ResetBall();
        }
        else if (this.transform.position.x <= -maxX)
        {
            computerScore++;
            playerDidLastPoint = -1;
            computerScoreTxt.GetComponent<TMP_Text>().SetText(computerScore.ToString());
            this.ResetBall();
        }
    }

    void OnCollisionEnter2D(Collision2D ball)
    {
        // Obtém o primeiro ponto de contato e reflete a bola em função da normal ao ponto
        ContactPoint2D contact = ball.GetContact(0);
        direction = Vector2.Reflect(direction, contact.normal);
    }

}
