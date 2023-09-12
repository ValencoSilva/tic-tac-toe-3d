using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    private bool isRotating = false;
    private Vector3 mouseReference;
    private Vector3 mouseOffset;
    private Vector3 rotation;
    public float sensitivity = 0.4f;

    void Update()
    {
        if (isRotating)
        {
            // Captura o deslocamento do mouse em relação ao ponto inicial
            mouseOffset = (Input.mousePosition - mouseReference);

            // Aplica a rotação no cubo
            rotation.y = -(mouseOffset.x + mouseOffset.y) * sensitivity;
            rotation.x = (mouseOffset.y - mouseOffset.x) * sensitivity;
            transform.Rotate(rotation);

            // Atualiza a referência do mouse para a posição atual do mouse
            mouseReference = Input.mousePosition;
        }
    }

    void OnMouseDown()
    {
        // Ativa a rotação e define o ponto de referência do mouse
        isRotating = true;
        mouseReference = Input.mousePosition;
    }

    void OnMouseUp()
    {
        // Desativa a rotação
        isRotating = false;
    }
}
