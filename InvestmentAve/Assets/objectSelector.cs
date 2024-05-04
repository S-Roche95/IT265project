using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelector : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject selected;
    [SerializeField] private bool fixOffset = false;
    [SerializeField] private LayerMask layersToInteractWith;
    [SerializeField] private LayerMask placementLayers;
    [SerializeField]
    private Vector3 specificOffset;
    public bool allowMove = false;
    private void Awake()
    {
        var c = GetComponent<Camera>();
        if (c != null && cam == null)
        {
            cam = c;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("User clicked the left mouse button");
            Vector3 mouse = Vector3.zero;
            mouse.x = Input.mousePosition.x;
            mouse.y = Input.mousePosition.y;
            Ray ray = cam.ScreenPointToRay(mouse);
            //Vector3 origin = this.transform.position;
            //Vector3 direction = this.transform.forward;
            RaycastHit hitInfo;
            Debug.DrawLine(ray.origin, ray.direction * 100, Color.blue, 3);
            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
            {

                Debug.DrawLine(ray.origin, hitInfo.point, Color.green, 3);
                Debug.Log($"Hit Name: {hitInfo.collider.gameObject.name} " +
                          $"Tag: {hitInfo.collider.gameObject.tag} " +
                          $"Layer: {LayerMask.LayerToName(hitInfo.collider.gameObject.layer)}");
                if (selected != null && allowMove)
                {
                    Debug.Log("Placed item");
                    int layer = hitInfo.collider.gameObject.layer;
                    if ((layersToInteractWith & (1 << layer)) != 0)
                    {
                        //layer exists in mask
                        return;// don't place items if the object is in the mask
                    }
                    else
                    {
                        //layer doesn't exist in mask
                    }
                    if (fixOffset)
                    {
                        Vector3 pos = hitInfo.point;
                        var offset = hitInfo.transform.localScale / 2;
                        var tile = hitInfo.collider.gameObject.GetComponent<piece1>();
                        if (tile != null)
                        {
                            //this item is a tile
                            pos = tile.GetPointPosition();
                        }
                        selected.transform.position = pos + specificOffset; //pos + offset;

                    }
                    else
                    {
                        selected.transform.position = hitInfo.point;
                    }

                }
                else
                {
                    Debug.Log("Selected new item");
                    int layer = hitInfo.collider.gameObject.layer;
                    if ((layersToInteractWith & (1 << layer)) != 0)
                    {
                        //layer exists in mask
                        selected = hitInfo.collider.gameObject;
                    }
                    else
                    {
                        //layer doesn't exist in mask
                    }

                }
            }
            else
            {
                selected = null;
            }
        }
    }
}