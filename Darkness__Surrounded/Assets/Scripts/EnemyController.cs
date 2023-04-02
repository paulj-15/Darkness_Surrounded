using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public AudioClip _trapClip;
    public float lookRadius = 10f;
    public float _attackRadius = 2f;
    Transform target;
    NavMeshAgent agent;
    Transform _currentPatrolPoint;
    [SerializeField]
    AudioSource _SFXaudioSrc;
    //[SerializeField]
    //SpawnManager _test;
    // Start is called before the first frame update
    void Start()
    {
        //target = GameObject.FindObjectOfType<FirstPersonController>().transform;
        agent = GetComponent<NavMeshAgent>();
        _SFXaudioSrc = GameObject.Find("SFXAudioSource").GetComponent<AudioSource>();
        //InvokeRepeating("ObtainPlayerOnRunTime", 1, 0.1f);
        //Debug.Log("enem name " + this.name + " enem destn pos " + _currentPatrolPoint);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Checking on distance for enemy " + this.name + "Dist as " + Vector3.Distance(target.position, transform.position));
        if (target != null)
        {
            if (Vector3.Distance(target.position, transform.position) <= _attackRadius && !_SFXaudioSrc.isPlaying)
            {
                FaceTarget();
                agent.speed = 4f;
                agent.SetDestination(target.position);
                

            }
            else if(Vector3.Distance(target.position, transform.position) > _attackRadius && !_SFXaudioSrc.isPlaying)
            {
                Debug.Log("Stand ideally");
                agent.speed = 0;
            }
        }
        else
        {
            Debug.Log("Stand ideally");
            agent.speed = 0;
        }
    }
    private void OnEnable()
    {
    }
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        _SFXaudioSrc.clip = _trapClip;
        _SFXaudioSrc.Play();
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<FirstPersonController>())
        {
            Debug.Log("Setting target as : " + other.name);
            target = other.transform;
        }
        else
        {
            target = null;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (target != null)
            target = null;
    }

    void ObtainPlayerOnRunTime()
    {
        if (target == null)
            target = GameObject.FindObjectOfType<FirstPersonController>().transform;
        else
            CancelInvoke();
    }
}