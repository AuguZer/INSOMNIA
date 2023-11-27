using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideCloset : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.transform.SetParent(transform);
            StartCoroutine(HidePosition(other.transform.localRotation, Quaternion.identity, 2f, other.transform.position, transform.position));
        }
    }


    IEnumerator HidePosition(Quaternion startValue, Quaternion endValue, float duration, Vector3 startPos, Vector3 endPos)
    {
        float t = 0f;
        //Quaternion startValue = transform.localRotation;

        while (t < duration)
        {
            transform.localRotation = Quaternion.Lerp(startValue, endValue, t / duration);
            transform.localPosition = Vector3.Lerp(startPos, endPos, t / duration);
            t += Time.deltaTime;

            yield return null;
        }
        transform.localRotation = endValue;
        transform.localPosition = endPos;
    }
}
