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
    }

    // Update is called once per frame
    void Update()
    {
        //if (blinkNum > 0) isBlinking = true;
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("WAIT"))
        {
            StartCoroutine(BlinkCoroutine());
        }
        else
        {
            animator.SetInteger("BlinkNum", 0);
            StopAllCoroutines();
        }

    }

    IEnumerator BlinkCoroutine()
    {
        float randomTime = Random.Range(10f, 30f);
        yield return new WaitForSeconds(randomTime);
        blinkNum = Random.Range(1, 4);
        animator.SetInteger("BlinkNum", blinkNum);

    }
}
