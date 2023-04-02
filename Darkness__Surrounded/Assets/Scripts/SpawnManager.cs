using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    public Queue<GameObject> _pool;
    [SerializeField] Transform EnemySpawnPoint;
    public Transform _enemyPatrolPoints;
    private Transform _randomSpawnPoint;
    public GameObject _canvas;
    public TMP_Text scoreText;
    public int score;

    public AudioClip _pointAddingSound;
    public AudioSource _audioSource;

    public static SpawnManager instance;
    
    [System.Serializable]
    public class Pool
    {
        public GameObject _object;
        public int _poolSize;
    }
    public Pool _enemyPool;

    public Transform minBound, maxBound;
    float minX, minZ, maxX, maxZ;

    public GameObject _player;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        if (_player != null)
        {
            minX = minBound.position.x;
            minZ = minBound.position.z;
            maxX = maxBound.position.x;
            maxZ = maxBound.position.z;
            minZ = minBound.position.z;
            Vector3 randomPos = new Vector3(Random.Range(minX, maxX), _player.transform.position.y, Random.Range(minZ, maxZ));
            PhotonNetwork.Instantiate(_player.name, randomPos, Quaternion.identity);
        }
        _pool = new Queue<GameObject>();
        for(int i = 0; i < _enemyPool._poolSize; i++)
        {
            GameObject _obj = Instantiate(_enemyPool._object);
            _obj.name = "Enemy " + (i + 1);
            _obj.SetActive(false);
            _pool.Enqueue(_obj);
        }
        InvokeRepeating("PopulateObject", 1, 1);
    }

    public void PopulateObject()
    {
        if(_pool.Count > 0)
        {
            Invoke("ShowBall", 5);
        }
    }

    public void ShowBall()
    {
        if (_pool.Count > 0)
        {
            GameObject a;
            for (int i = 0; i < _enemyPatrolPoints.childCount; i++)
            {
                a = _pool.Dequeue();
                _randomSpawnPoint = _enemyPatrolPoints.GetChild(i);
                a.transform.position = _randomSpawnPoint.position;
                a.transform.rotation = _randomSpawnPoint.rotation;
                a.SetActive(true);
            }
            //a.GetComponent<Rigidbody>().velocity = new Vector3(0.1f, 0.5f, 0.3f);
            
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
