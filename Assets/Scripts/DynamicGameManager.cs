using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DynamicGameManager : MonoBehaviour {

    public float forceMultiplier = 200f;
    public Rect dimenzijaSobe;

    public TriggerArea taPrefab;
    public TriggerArea ta = null;

    public GameObject[] spawnPoints;

    public GameObject[] namestajPrefabsH;
    public GameObject[] namestajPrefabsV;

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
        if (namestajNaSceni.Count == 0)
            return;
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

        return new Vector3(x,0,y);
    }


    void UbaciUSobu() {
        Vector3 prozor = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
        Vector3 tackaUSobi = GetRandomPoint();
        Vector3 point = prozor - tackaUSobi;
        point = point.normalized;
        point = prozor + point;

        GameObject inst;

        if (Random.Range(0, 2) == 1)
        {
            inst = namestajPrefabsH[Random.Range(0, namestajPrefabsH.Length)];
            inst = Instantiate(inst, point, Quaternion.identity);
            Vector3 ugao = Vector3.RotateTowards(inst.transform.forward, prozor, 2 * Mathf.PI, 0.0f);
      //      inst.transform.rotation = Quaternion.LookRotation(ugao);
        }
        else {
            inst = namestajPrefabsV[Random.Range(0, namestajPrefabsV.Length)];
            inst = Instantiate(inst, point, Quaternion.identity);
            Vector3 ugao = Vector3.RotateTowards(inst.transform.up, prozor, 2 * Mathf.PI, 0.0f);
       //     inst.transform.rotation = Quaternion.LookRotation(ugao);
        }

        Vector3 normVect = (tackaUSobi - point).normalized;


        inst.GetComponent<Rigidbody>().AddForce(normVect*forceMultiplier,ForceMode.Acceleration);
    }

    void OnDrawGizmos()
    {
        Vector3 pos = new Vector3(dimenzijaSobe.x, 0, dimenzijaSobe.y);
        Gizmos.DrawWireSphere(pos, 0.5f);
        Gizmos.DrawWireSphere(pos+ new Vector3(dimenzijaSobe.width, 0), 0.5f);
        Gizmos.DrawWireSphere(pos + new Vector3(dimenzijaSobe.width, 0, dimenzijaSobe.height), 0.5f);
        Gizmos.DrawWireSphere(pos + new Vector3(0, 0, dimenzijaSobe.height), 0.5f);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UbaciUSobu();
        }
    }

}
