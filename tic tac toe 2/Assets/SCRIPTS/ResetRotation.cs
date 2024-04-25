using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResetRotation : MonoBehaviour
{
    Vector3 originalPos;
    Vector3 originalRot;
    Quaternion startRotation;
    float time;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalPos = gameObject.transform.position;
        startRotation = transform.rotation;
    }

    public void OnDisable()
    {
        transform.position = originalPos;
        transform.rotation = Quaternion.Slerp(startRotation, Quaternion.identity, time);
        time += Time.deltaTime;
        rb.angularVelocity = Vector3.forward * 0f;
        Debug.Log("Posicao do Cubo Resetada");
    }
    void Teste()
    {

    }
}
