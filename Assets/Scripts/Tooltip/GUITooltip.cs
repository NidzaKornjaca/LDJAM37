using UnityEngine;
using UnityEngine.UI;

namespace NidzaKornjaca.Tooltip.Basic { 
    public class GUITooltip : MonoBehaviour {
	    private static GUITooltip guiTooltip;
	    [SerializeField]private Text tooltipTextObject;

	    void Awake(){
		    guiTooltip = this;
		    tooltipTextObject.text = "";
		    gameObject.SetActive(false);
	    }
	
	    // Update is called once per frame
	    void Update () {
		    transform.position = Input.mousePosition + new Vector3(3, 3, 0);
	    }

	    public static void Show(string tooltip) {
		    guiTooltip.transform.position = Input.mousePosition + new Vector3(3, 3, 0);
		    guiTooltip.gameObject.SetActive(true);
		    guiTooltip.tooltipTextObject.text = tooltip;
	    }

	    public static void Hide() {
		    guiTooltip.gameObject.SetActive(false);
		    guiTooltip.tooltipTextObject.text = "";
	    }

        public static GUITooltip Tooltip {
            get {
                return guiTooltip;
            }
        }
    }
}