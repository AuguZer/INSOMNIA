using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vent : MonoBehaviour
{
    public bool ventOpen;
    Animator animator;

    public int screwDriverNumber = 0;

    [SerializeField] float timer;
    [SerializeField] float Ypos;
    Vector3 targetPosition;

    AudioSource audioSource;
    [SerializeField] AudioClip openClip;
    [SerializeField] AudioClip lockedClip;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        ventOpen = false;
        animator = GetComponent<Animator>();
        targetPosition = new Vector3(transform.localPosition.x, Ypos, transform.localPosition.z);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void VentOpen()
    {
        animator.SetTrigger("Open");
        audioSource.PlayOneShot(openClip);
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;
    }
    public void VentLocked()
    {
        animator.SetTrigger("Locked");
        audioSource.PlayOneShot(lockedClip);
    }

    IEnumerator OpenCoroutine(Vector3 targetPosition)
    {
        audioSource.Play();
        float duration = 0f;
      
        while(duration < timer)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, duration/timer);
            duration += Time.deltaTime;

            yield return null;
        }

        transform.position = targetPosition;
        
      
    }
}
