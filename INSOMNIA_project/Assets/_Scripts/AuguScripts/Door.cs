using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] bool open = false;
    [SerializeField] float openAngle = -90f;
    [SerializeField] float closeAngle = 0f;
    [SerializeField] float smooth = 2f;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void ChangeDoorState()
    {
        open = !open;
    }
    // Update is called once per frame
    void Update()
    {
        if (open)
        {
            Quaternion targetRotation = Quaternion.Euler(0, openAngle, 0);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
        }
        else
        {
            Quaternion targetRotation1 = Quaternion.Euler(0, closeAngle, 0);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation1, smooth * Time.deltaTime);
        }
    }
}
