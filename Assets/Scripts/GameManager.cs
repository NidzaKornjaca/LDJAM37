using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{

    public TriggerArea ta;
    public GameObject[] namestaj;
    public List<GameObject> trenutni;

    // Use this for initialization
    void Start()
    {

        NextTarget();

        ta.Subscribe(Calculate);
    }

    void NextTarget()
    {
        trenutni.Clear();
        GameObject pom = namestaj[Random.Range(0, namestaj.Length)];
        if (pom.transform.childCount > 0)
        {
            for (int i = 0; i < pom.transform.childCount; i++)
                trenutni.Add(pom.transform.GetChild(i).gameObject);
        }
        else {
            trenutni.Add(pom);
        }
        Debug.Log(pom.name);
    }

    void Calculate(Collider other)
    {
        for (int i = 0; i < trenutni.Count; i++)
        {
            if (other.gameObject == trenutni[i])
            {
                trenutni.RemoveAt(i);
                if (trenutni.Count == 0)
                    NextTarget();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }




}