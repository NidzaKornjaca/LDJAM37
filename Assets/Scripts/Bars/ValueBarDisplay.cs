using UnityEngine;

/*
 * Basic bar - good for simple mini health bars over unit heads - scales foreground bar coresponding to fed value
 */
 namespace NidzaKornjaca.Bars.BasicBar {
    public class ValueBarDisplay : MonoBehaviour {
        [SerializeField]
        private Transform m_foregroundBar;
        [SerializeField]
        private Transform m_backgroundBar;
        [SerializeField]
        private float m_scaleSpeed = 1;
        private Vector3 m_baseScale;

        //private int test = 5;

        private float targetValue = 1f;
        private float currentValue = 1f;
	    // Use this for initialization
	    void Start () {
            m_baseScale = m_foregroundBar.localScale;
	    }
	
	    // Update is called once per frame
	    void Update () {
            /*
            if (Input.GetKeyDown(KeyCode.A)) {
                test++;
                Set(test / 5f);
            }
            if (Input.GetKeyDown(KeyCode.D)) {
                test--;
                Set(test / 5f);
            }
            */

            currentValue = Mathf.Lerp(currentValue, targetValue, m_scaleSpeed * Time.deltaTime);
            Rescale();
        }

        public void Set(float f) {
            targetValue = Mathf.Clamp(f, 0, 1);
            //m_foregroundBar.localScale = Vector3.Scale(m_baseScale, new Vector3(f, 1f, 1f));  
        }

        public void Rescale() {
            m_foregroundBar.localScale = Vector3.Scale(m_baseScale, new Vector3(currentValue, 1f, 1f));
        }
    }
}