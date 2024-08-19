using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalManager : MonoBehaviour
{
    public float openAngle = 90f;
    public float openSpeed = 2f; 
    private Quaternion openRotation1;
    private Quaternion openRotation2;
    
    public GameObject FinalRival;
    public GameObject MaleProposal;

    public Transform lid_s_box;
    public Transform lid_s_box_2;

    private void Start()
    {
        
        openRotation1 = Quaternion.Euler(openAngle, lid_s_box.localRotation.eulerAngles.y, lid_s_box.localRotation.eulerAngles.z);
        openRotation2 = Quaternion.Euler(openAngle, lid_s_box_2.localRotation.eulerAngles.y, lid_s_box_2.localRotation.eulerAngles.z);

    }

    void Update()
    {
        if (!FinalRival && !MaleProposal.activeInHierarchy)
        {
            MaleProposal.SetActive(true);
            lid_s_box.localRotation = Quaternion.Slerp(lid_s_box.localRotation, openRotation1, Time.deltaTime * openSpeed);
            lid_s_box_2.localRotation = Quaternion.Slerp(lid_s_box_2.localRotation, openRotation2, Time.deltaTime * openSpeed);
        }
    }
}
