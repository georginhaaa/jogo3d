using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.SceneManagement;



public class Moeda : MonoBehaviour
{

    public static int pontucaoMoeda;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            pontucaoMoeda = pontucaoMoeda + 1;
            Destroy(gameObject);
            SceneManager.LoadScene(1);
        }
    }
}
