using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorManager : MonoBehaviour
{
    Animator animator;
    EnemyStateManager enemyStateManager;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        enemyStateManager = GetComponentInParent<EnemyStateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("IsInIdle", enemyStateManager.isInIdle);
        animator.SetBool("IsInPatrol", enemyStateManager.isInPatrol);
    }
}
