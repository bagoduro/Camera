using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float moveHorizontal = 0f;
        float moveVertical = 0f;

        // Verifica as teclas pressionadas usando o novo Input System
        if (Keyboard.current.aKey.isPressed) moveHorizontal = -1f;
        if (Keyboard.current.dKey.isPressed) moveHorizontal = 1f;

        if (Keyboard.current.sKey.isPressed) moveVertical = -1f;
        if (Keyboard.current.wKey.isPressed) moveVertical = 1f;

        // Cria o vetor de movimento (X, Y, Z)
        // Mantemos o Y como 0.0f para o player não "voar" ao andar
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        /*
         * ALTERAÇÃO REALIZADA:
         * O método de movimentação foi modificado de transform.Translate para Rigidbody.MovePosition.
         *
         * JUSTIFICATIVA:
         * O uso de transform.Translate move o objeto diretamente através de sua transformação,
         * sem considerar o sistema de física da Unity. Isso pode causar problemas como o player
         * atravessar paredes, ignorar colisões ou apresentar comportamentos inconsistentes
         * quando interage com outros objetos que possuem Collider.
         *
         * Ao utilizar o Rigidbody.MovePosition, o movimento passa a ser tratado pelo sistema
         * de física da engine, garantindo que colisões sejam respeitadas corretamente.
         * Dessa forma, o player não atravessa objetos e o comportamento se torna mais realista
         * e adequado às boas práticas de desenvolvimento na Unity.
         *
         * Essa alteração também melhora a estabilidade do movimento em diferentes taxas de
         * frames (FPS), tornando o jogo mais consistente independentemente do desempenho da máquina.
         */
        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
    }
}