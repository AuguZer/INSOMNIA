using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class HideCloset : MonoBehaviour
{
    [SerializeField] public Transform hidePos;
    [SerializeField] public Transform outPos;

    [SerializeField] GameObject door;

    float targetAngle = -120f; // Angle cible défini dans l'inspector
    float speed = 20f;        // Vitesse de lerp définie dans l'inspector



    public bool playerInRange;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator OpenCloseCoroutine()
    {
        if (door != null)
        {
            float currentAngle = 0f; // Angle actuel de l'objet
            bool isReturning = false; // Indicateur si on retourne à 0

            // Aller de 0 à l'angle cible
            while (Mathf.Abs(currentAngle - targetAngle) > 0.1f)
            {
                currentAngle = Mathf.Lerp(currentAngle, targetAngle, Time.deltaTime * speed); // Lerp de l'angle actuel vers l'angle cible
                door.transform.localRotation = Quaternion.Euler(0f, currentAngle, 0f); // Appliquer la rotation à l'objet
                yield return null; // Attendre la frame suivante
            }

            // S'assurer que la rotation atteint exactement l'angle cible
            currentAngle = targetAngle;
            door.transform.localRotation = Quaternion.Euler(0f, currentAngle, 0f);

            // Attendre un petit moment une fois l'angle cible atteint
            yield return new WaitForSeconds(0.25f);

            // Retourner de l'angle cible à 0
            while (Mathf.Abs(currentAngle - 0f) > 0.1f)
            {
                currentAngle = Mathf.Lerp(currentAngle, 0f, Time.deltaTime * speed); // Lerp de l'angle actuel vers 0
                door.transform.localRotation = Quaternion.Euler(0f, currentAngle, 0f); // Appliquer la rotation à l'objet
                yield return null; // Attendre la frame suivante
            }

            // S'assurer que la rotation revient exactement à 0
            currentAngle = 0f;
            door.transform.localRotation = Quaternion.Euler(0f, currentAngle, 0f);
        }
    }
}

