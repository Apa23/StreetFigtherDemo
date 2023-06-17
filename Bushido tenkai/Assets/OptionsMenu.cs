using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio; //para poder usar audio

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer; //serializar con metodo de audio que ya creamos
    public void PantallaCompleta (bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;
    }

    public void CambiarVolumen(float volumen) 
    {
        audioMixer.SetFloat("Volumen", volumen);
    }

    public void CambiarCalidad(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }
}
