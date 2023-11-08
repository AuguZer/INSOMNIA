using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] float smooth = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "PickUpObj")
        {
            if (collision.gameObject.GetComponent<PickUpObject>().isHeld == false)
            {
                Debug.Log("Descendre button");
                Vector3 targetPosition = new Vector3(transform.position.x, .85f, transform.position.z);
                transform.position = Vector3.Lerp(transform.position, targetPosition, smooth * Time.deltaTime);
            }

        }
    }
}
