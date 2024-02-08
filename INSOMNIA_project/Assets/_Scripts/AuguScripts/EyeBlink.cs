using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBlink : MonoBehaviour
{
    Animator animator;
    int blinkNum;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        blinkNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
      if (blinkNum == 0) StartCoroutine(BlinkCoroutine());
    }

    IEnumerator BlinkCoroutine()
    {
        float randomTime = Random.Range(3f, 10f);
        yield return new WaitForSeconds(randomTime);
        blinkNum = (int)Random.Range(0f, 2f);
        animator.SetFloat("BlinkNum", blinkNum);
        yield return new WaitForSeconds(.1f);
        blinkNum = 0;
    }
}
