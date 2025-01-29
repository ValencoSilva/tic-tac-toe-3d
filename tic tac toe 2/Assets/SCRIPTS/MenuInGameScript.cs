using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInGameScript : MonoBehaviour
{
    [SerializeField] private GameObject painelOpcoes;
    [SerializeField] private GameObject painelCreditos;
    [SerializeField] private GameObject painelLanguage;
    [SerializeField] private GameObject painelAudio;
    [SerializeField] private GameObject painelComoJogar;

    public void OnOpcoes()
    {
        painelOpcoes.SetActive(true);
    }

    public void VoltarOpcoes()
    {
        painelOpcoes.SetActive(false);
    }

    public void OnLanguage()
    {
        painelLanguage.SetActive(true);
    }

    public void OnAudio()
    {
        painelAudio.SetActive(true);
        painelOpcoes.SetActive(false);
    }

    public void OnComoJogar()
    {
        painelComoJogar.SetActive(true);
    }

    public void VoltarLanguage()
    {
        painelLanguage.SetActive(false);
        painelOpcoes.SetActive(true);
    }
    public void VoltarAudio()
    {
        painelAudio.SetActive(false);
        painelOpcoes.SetActive(true);
    }

    public void VoltarCreditos()
    {
        painelCreditos.SetActive(false);
    }

    public void VoltarComoJogar()
    {
        painelComoJogar.SetActive(false);
    }

}
