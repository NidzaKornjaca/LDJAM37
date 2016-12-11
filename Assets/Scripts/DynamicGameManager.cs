using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DynamicGameManager : Singleton<GameManager> {

    public Rect dimenzijaSobe;

    public TriggerArea taPrefab;
    public TriggerArea ta = null;

    public GameObject[] spawnPoints;

    public GameObject[] namestajPrefabs;
    public List<GameObject> namestajNaSceni;
    public List<GameObject> trenutni;

	// Use this for initialization
	void Start () {
        NextTarget();
        
        ta.Subscribe(Calculate);
	}

    void NextTarget() {
        MakeTriggerArea();
        trenutni.Clear();
        GameObject pom = namestajNaSceni[Random.Range(0, namestajNaSceni.Count)];
        if (pom.transform.childCount > 0)
        {
            for (int i = 0; i < pom.transform.childCount; i++)
                trenutni.Add(pom.transform.GetChild(i).gameObject);
        }
        else {
            trenutni.Add(pom);
        }        
    }

    void Calculate(Collider other) {
        for (int i = 0; i < trenutni.Count; i++)
        {
            if (other.gameObject == trenutni[i])
            {
                trenutni.RemoveAt(i);
                if(trenutni.Count == 0)
                    NextTarget();
            }
        }
    }


    void MakeTriggerArea() {
        if (ta != null) {
            Destroy(ta);    
        }
        Vector3 point = GetRandomPoint();
        ta = Instantiate(taPrefab, point, Quaternion.identity);
    }

    Vector3 GetRandomPoint() {

        float x = Random.Range(dimenzijaSobe.xMin, dimenzijaSobe.xMax);
        float y = Random.Range(dimenzijaSobe.yMin, dimenzijaSobe.yMax);

        return new Vector3(x,y,0);
    }


    void UbaciUSobu() {
        Vector3 prozor = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
        Vector3 tackaUSobi = GetRandomPoint();
        Vector3 point = prozor - tackaUSobi;
        point = point.normalized;
        point = prozor + point;





    }


}
