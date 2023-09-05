using System;
using System.Collections;
using UnityEngine;

public class BotaoController : MonoBehaviour
{

    private AudioSource audioClip;
    private Light luzPontual;
    private float tempoDecorrido = 0.0f;
    private bool botaoPressionado = false;
    private bool isCoroutineExecuting = false;
    private const float LIGHT_Z = 2.25f;

    // Start is called before the first frame update
    void Start()
    {
        audioClip = GetComponent<AudioSource>();

        GameObject luzObj = GameObject.Find("LuzPontual");
        luzPontual = luzObj.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (botaoPressionado)
        {
            StartCoroutine(ExecuteAfterTime(1f, () =>
               {
                   luzPontual.enabled = false;
               }));
            botaoPressionado = false;
        }
    }

    IEnumerator ExecuteAfterTime(float time, Action task)
    {
        if (isCoroutineExecuting)
            yield break;

        isCoroutineExecuting = true;

        yield return new WaitForSeconds(time);

        task();

        isCoroutineExecuting = false;
    }

    public void PressionaBotao()
    {
        audioClip.Play();

        Vector3 buttonPosition = this.transform.position;
        Vector3 lightPosition = new Vector3(buttonPosition.x, buttonPosition.y, LIGHT_Z);
        luzPontual.transform.position = lightPosition;

        luzPontual.enabled = true;
        botaoPressionado = true;
    }

}
