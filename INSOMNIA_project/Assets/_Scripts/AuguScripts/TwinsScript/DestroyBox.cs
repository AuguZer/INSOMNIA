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
        }
    }

    public bool DetectTwins()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, twinsMask);

        foreach (Collider collider in colliders)
        {
            collider.gameObject.SetActive(false);
            return true;
        }

        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
