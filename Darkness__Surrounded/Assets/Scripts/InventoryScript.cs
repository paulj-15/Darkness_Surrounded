using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
    public int _inventoryCapacity;

    public Transform _objPos;
    [SerializeField]
    List<GameObject> _invetoryItems;
    GameObject _selectedItem;
    string _objName = "";
    GameObject _object;
    bool pickAvailable;
    string _objectToPick;
    GameObject pickObject;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Used to pick object from invemtory and add objects to the inventory
        InventoryManagement();

        if (pickAvailable)
        {
            Debug.Log("pick " + _objectToPick);
        }
    }

    void InventoryManagement()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _object = _invetoryItems[0];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _object = _invetoryItems[1];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _object = _invetoryItems[2];
        }
        Debug.Log("Selected item " + _objName);
        if (_object != null)
        {
            if (_selectedItem != null)
            {
                _selectedItem.SetActive(false);
                //_selectedItem.GetComponent<GunScript>().enabled = false;
                _selectedItem = null;
            }
            _selectedItem = _object;//Instantiate(_object, _objPos.parent);
            _selectedItem.SetActive(true);
            //_selectedItem.GetComponent<GunScript>().enabled = true;
            _selectedItem.transform.localPosition = _objPos.localPosition;
            _selectedItem.transform.localRotation = _objPos.localRotation;
            _object = null;
        }
        if (Input.GetKeyDown(KeyCode.F) && pickAvailable)
        {
            if (_invetoryItems.Count < _inventoryCapacity)
            {
                pickObject.transform.parent = _objPos.parent;
                pickObject.SetActive(false);
                _invetoryItems.Add(pickObject);

            }
            else
            {
                Debug.Log("Your inventory is full");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pickable")
        {
            pickAvailable = true;
            _objectToPick = other.name;
            pickObject = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Pickable")
        {
            pickAvailable = false;
        }
    }
}