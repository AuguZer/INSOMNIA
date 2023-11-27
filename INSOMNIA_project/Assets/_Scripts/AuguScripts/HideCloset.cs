using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideCloset : MonoBehaviour
{
    [SerializeField] Transform hidPos;
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
            CharacterController characterController = other.gameObject.GetComponent<CharacterController>();
            characterController.enabled = false;
            //other.transform.localRotation = Quaternion.identity;
            StartCoroutine(LerpPosition(other.gameObject, other.transform.position, new Vector3(hidPos.position.x, other.transform.position.y, hidPos.position.z), 2f));
           
        }
    }

    public IEnumerator LerpPosition(GameObject player, Vector3 startPos, Vector3 endPos, float duration)
    {
        float t = 0f;

        while (t < duration)
        {
            player.transform.position = Vector3.Lerp(startPos, endPos, t / duration);
            t += Time.deltaTime;

            yield return null;
        }

        player.transform.position = endPos;
    }
}
