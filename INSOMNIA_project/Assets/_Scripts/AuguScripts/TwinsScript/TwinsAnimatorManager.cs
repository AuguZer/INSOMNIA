using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TwinsAnimatorManager : MonoBehaviour
{
    Animator animator;
    NavMeshAgent navMeshAgent;
    [SerializeField] float agentSpeed;
    [SerializeField] float agentSpeedOnRun;
    [SerializeField] float animatorSpeed;

    [SerializeField] public bool isRunning;

    [SerializeField] AudioClip[] footSteps;
    [SerializeField] AudioClip[] voices;

    [SerializeField] AudioSource footStepsAudioSource;
    [SerializeField] AudioSource voicesAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            animator.speed = animatorSpeed;
            agentSpeed = agentSpeedOnRun;
        }
    }

    private void Steps()
    {
        Debug.Log("step");
        AudioClip clip = RandomStepsClip(footSteps);
        footStepsAudioSource.PlayOneShot(clip);
    }

    private void Voices()
    {
        AudioClip clip = RandomStepsClip(voices);
        voicesAudioSource.PlayOneShot(clip);
    }

    private AudioClip RandomStepsClip(AudioClip[] clips)
    {
        return clips[Random.Range(0, clips.Length)];
    }


    public void Stop()
    {
        navMeshAgent.speed = 0f;
    }
    public void Go()
    {

        navMeshAgent.speed = agentSpeed;

    }
}
