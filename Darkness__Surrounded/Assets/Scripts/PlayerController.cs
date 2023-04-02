using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject m_GotHitScreen;
    public GameObject FlashlightText;
    public GameObject TrapBlood;
    [SerializeField]
    float playerhealth = 100;
    float _attackIntensity = 10f;
    AudioSource _TrapaudioSrc;
    public AudioClip _trapAudio;
    // Start is called before the first frame update
    void Start()
    {
        _TrapaudioSrc = GameObject.Find("TrapAudio").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "enemy")
        {
            gethurt();
            m_GotHitScreen.SetActive(true);
        }
        if(other.tag == "Pickable")
        {
            FlashlightText.SetActive(true);
            _TrapaudioSrc.clip = _trapAudio;
            _TrapaudioSrc.Play();
        }
        else
        {
            FlashlightText.SetActive(false);
        }
        if(other.tag == "Trap")
        {
            TrapBlood.SetActive(true);
        }
        if(other.tag != "Trap")
        {
            TrapBlood.SetActive(false);
        }
    }

    

    void gethurt()
    {
        var color = m_GotHitScreen.GetComponent<Image>().color;
        if (playerhealth < .1f)
        {
            color.a = 1.0f;
            m_GotHitScreen.GetComponent<Image>().color = color;
        }
        else
        {
            color.a += _attackIntensity / playerhealth;
            Debug.Log("Screen color val : " + color.a);
            m_GotHitScreen.GetComponent<Image>().color = color;
            playerhealth -= _attackIntensity;
        }
    }
}
