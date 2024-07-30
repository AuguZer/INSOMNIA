using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(AudioSource))]
public class PickUpObject : MonoBehaviour
{
    [SerializeField] public bool isHeld;

    Rigidbody rb;
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        rb.angularDrag = 1f;

        audioSource.spatialBlend = 1;
        audioSource.playOnAwake = false;

        StartCoroutine(SoundCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent == null) 
        {
            isHeld = false;
        }
        else
        {
            isHeld= true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        audioSource.Play();
    }

    IEnumerator SoundCoroutine()
    {
        yield return new WaitForSeconds(3f);
        audioSource.volume = .5f;
    }
}
