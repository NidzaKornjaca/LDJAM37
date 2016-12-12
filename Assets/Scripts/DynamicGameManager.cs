using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityStandardAssets.Characters.FirstPerson;
public class DynamicGameManager : MonoBehaviour {

    private bool isGameOver = false;

    private int currentHighScore = 0;
    public int pointsPerObject = 20;

    public float secsPerRound = 30f;
    private float currenSecsPerRound;
    public Timer timer;
    [SerializeField]
    private Timer spawnTimer;
    public float initialSpawnTime = 30f;
    private static DynamicGameManager instance = null;

    public float forceMultiplier = 200f;
    public Rect dimenzijaSobe;

    public TriggerArea taPrefab;
    public TriggerArea ta = null;

    public GameObject[] spawnPoints;

    public GameObject[] namestajPrefabsH;
    public GameObject[] namestajPrefabsV;

    public List<GameObject> namestajNaSceni;
    public GameObject trenutni;

    public GameObject highlightParticlePrefab;

    private Shader previousShader;
    private GameObject highlightParticles;

    private UnityEvent gameOver;

    void Awake() {
        if (instance == null)
            instance = this;
        timer = GetComponent<Timer>();
        gameOver = new UnityEvent();
    }

	// Use this for initialization
	void Start () {

        PauseManager.setGameOver(false);

        ta = Instantiate(taPrefab, Vector3.zero, Quaternion.identity);

        NextTarget();
        
        ta.Subscribe(Calculate);

        if(timer)
            timer.Subscribe(GameOver);
        if (spawnTimer)
        {
            spawnTimer.Subscribe(UbaciUSobu);
            spawnTimer.StartTimer(initialSpawnTime);
        }
	} 


    void NextTarget() {
        _NextTarget();
        currenSecsPerRound = secsPerRound - currentHighScore / 30;
        if(timer)
            timer.StartTimer((currenSecsPerRound < 6)? 6 : currenSecsPerRound);
    }

    void _NextTarget() {
        MakeTriggerArea();
        UnhighlightObject(trenutni);
        if (namestajNaSceni.Count == 0)
            return;
        trenutni = namestajNaSceni[Random.Range(0, namestajNaSceni.Count)];
        HighlightObject(trenutni);
    }

    void HighlightObject(GameObject target) {
        if (!target) return;
        previousShader = target.GetComponent<Renderer>().material.shader;
        HoverHighlighter hh = target.GetComponent<HoverHighlighter>();
        if (highlightParticlePrefab) {
            highlightParticles = Instantiate(highlightParticlePrefab, target.transform.position, Quaternion.identity);
            highlightParticles.transform.SetParent(target.transform);
            highlightParticles.transform.localScale = new Vector3(1, 1, 1);
        }
        if (hh) hh.nonStandard = true;
        Renderer ren = target.GetComponent<Renderer>();
        if (ren)
            ren.material.shader = Shader.Find("GUI/Text Shader");
    }

    void UnhighlightObject(GameObject target) {
        if (!target) return;
        HoverHighlighter hh = target.GetComponent<HoverHighlighter>();
        if (highlightParticles) Destroy(highlightParticles);
        if (hh) hh.nonStandard = false;
        Renderer ren = target.GetComponent<Renderer>();
        if (ren)
            ren.material.shader = previousShader;
    }

    void Calculate(Collider other) {
        if (other.gameObject == trenutni)
        {
            if (timer)
            {
                currentHighScore += Mathf.FloorToInt(pointsPerObject * (timer.TimeLeft() / currenSecsPerRound));
                ta.PlayEfx();
            }
            NextTarget();
        }   
    }


    void MakeTriggerArea() {
        Vector3 point = GetRandomPoint();
        if(ta != null)
            ta.transform.position = point; 
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
      //      Vector3 ugao = Vector3.RotateTowards(inst.transform.forward, prozor, 2 * Mathf.PI, 0.0f);
      //      inst.transform.rotation = Quaternion.LookRotation(ugao);
        }
        else {
            inst = namestajPrefabsV[Random.Range(0, namestajPrefabsV.Length)];
            inst = Instantiate(inst, point, Quaternion.identity);
       //     Vector3 ugao = Vector3.RotateTowards(inst.transform.up, prozor, 2 * Mathf.PI, 0.0f);
       //     inst.transform.rotation = Quaternion.LookRotation(ugao);
        }

        Vector3 normVect = (tackaUSobi - point).normalized;

        inst.GetComponent<Rigidbody>().AddForce(normVect * forceMultiplier, ForceMode.Acceleration);

        for (int i = 0; i < inst.transform.childCount; i++) {
            namestajNaSceni.Add(inst.transform.GetChild(i).gameObject);
        }

        float newTime = initialSpawnTime - currentHighScore / 15;
        newTime = (newTime < 3f) ? 3f : newTime;
        
        spawnTimer.StartTimer( newTime);
        
    }

    void OnDrawGizmos()
    {
        Vector3 pos = new Vector3(dimenzijaSobe.x, 0, dimenzijaSobe.y);
        Gizmos.DrawWireSphere(pos, 0.5f);
        Gizmos.DrawWireSphere(pos+ new Vector3(dimenzijaSobe.width, 0), 0.5f);
        Gizmos.DrawWireSphere(pos + new Vector3(dimenzijaSobe.width, 0, dimenzijaSobe.height), 0.5f);
        Gizmos.DrawWireSphere(pos + new Vector3(0, 0, dimenzijaSobe.height), 0.5f);
    }



    void OnTriggerExit(Collider other) {
        Destroy(other.gameObject);
    }


    public static void UpisiSe(GameObject other) {
        other.transform.DetachChildren();
        instance.namestajNaSceni.Add(other);
    }

    public static void IspisiSe(GameObject other) {
        Debug.Log("Trenutno se unistava");
        for (int i = 0; i < instance.namestajNaSceni.Count; i++)
        {
            if (other == instance.namestajNaSceni[i])
            {
                instance.namestajNaSceni.RemoveAt(i);
                break;
            }
        }
        if (instance.trenutni == other) {
            instance.trenutni = null;
            instance._NextTarget();
        }
    }

    public static Timer getTimer() {
        return instance.timer;
    }

    public void GameOver() {
        isGameOver = true;
        Debug.Log(currentHighScore);
        FindObjectOfType<FirstPersonController>().GetComponent<FirstPersonController>().enabled = false;
        gameOver.Invoke();
        PauseManager.setGameOver(true);
        Time.timeScale = 0;
        return;
    }

    public static void Subscribe(UnityAction action) {
        instance.gameOver.AddListener(action);
    }

    public static bool IsGameOver() {
        return instance.isGameOver;
    }

    public static int Score() {
        return instance.currentHighScore;
    }

}
