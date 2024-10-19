using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casting : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GameObject VFX;
    [SerializeField] private GameObject dangerPrefab;
    private Camera mainCamera;
    private bool isCasting;
    private string currentSpell;
    
    private void Start()
    {
        mainCamera = Camera.main;

        currentSpell = "none";
        VFX.SetActive(false);

    }
    private void Update()
    {
        MousePosition();

        if (Input.GetMouseButtonDown(0) && currentSpell == "danger") {//Instantiate danger zone
            GameObject go = Instantiate(dangerPrefab);
            go.transform.position = VFX.transform.position;
        }
    }
    void MousePosition()
    {
        //Seeks for mouse position
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layerMask))
        {
            transform.position = raycastHit.point;
        }
    }

    public void CastDangerOnButton()//Called from click on button
    {
        if(currentSpell == "danger")
        {
            currentSpell = "none";
            VFX.SetActive(false);
            return;
        }

        currentSpell = "danger";
        VFX.SetActive(true);
    }
}