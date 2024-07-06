using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBox : MonoBehaviour
{

    [SerializeField] float radius;
    [SerializeField] LayerMask twinsMask;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (DetectTwins())
        {
            this.enabled = false;
            Debug.Log("End of : " + "EventTwins" + gameObject.name);
        }
    }

    public bool DetectTwins()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, twinsMask);

        foreach (Collider collider in colliders)
        {
            StartCoroutine(DisableTwins(collider));
            return true;
        }

        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    IEnumerator DisableTwins(Collider collider)
    {
        collider.transform.GetChild(0).gameObject.SetActive(false);
        collider.gameObject.GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(7f);
        collider.gameObject.SetActive(false);

    }
}
