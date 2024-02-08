using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBlink : MonoBehaviour
{
    Animator animator;
    [SerializeField] int blinkNum;
    [SerializeField] bool isBlinking;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        blinkNum = 0;
        StartCoroutine(BlinkCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        //if (blinkNum > 0) isBlinking = true;
    }

    IEnumerator BlinkCoroutine()
    {

        while (blinkNum >= 0)
        {
            blinkNum = (int)Random.Range(0f, 2f);
            animator.SetInteger("BlinkNum", blinkNum);
            float randomTime = Random.Range(3f, 10f);
            yield return new WaitForSeconds(randomTime);
        }

    }
}
