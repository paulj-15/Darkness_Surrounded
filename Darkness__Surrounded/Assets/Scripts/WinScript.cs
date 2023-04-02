using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScript : MonoBehaviour
{
    [SerializeField] private GameObject bloodImg;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Win")
        {
            Debug.Log("You Win!");
            bloodImg.SetActive(true);
        }
    }
}
