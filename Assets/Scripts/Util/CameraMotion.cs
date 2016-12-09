using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMotion : MonoBehaviour {
	public float speed = 2f;
    public float maxCameraSize = 10f;
    public float minCameraSize = 2f;
    public float cameraResizeSpeed = 0.5f;
    public float smoothFactor = 0.5f;

    public GameObject boundsObject;

    private Camera attachedCamera;
    private float minX = -999f, maxX = 999f, minY = -999f, maxY = 999f;
    private Vector3 targetPosition;

    void Start() {
        attachedCamera = GetComponent<Camera>();
        targetPosition = transform.position;
        UpdateBounds();
    }
	// Update is called once per frame
	void Update () {
		Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		targetPosition = targetPosition + (Vector3)direction * speed * Time.deltaTime;
        targetPosition = new Vector3(Mathf.Clamp(targetPosition.x, minX, maxX), Mathf.Clamp(targetPosition.y, minY, maxY), transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * speed * smoothFactor);

        if (Input.GetAxis("Mouse ScrollWheel") < 0) IncreaseCameraSize();
        else if (Input.GetAxis("Mouse ScrollWheel") > 0) DecreaseCameraSize();

    }

    public void SetTargetPosition(Vector3 targetPosition) {
        this.targetPosition = targetPosition;
    }

    void UpdateBounds() {
        if (boundsObject != null) {

            Vector3 cameraMax = (new Vector3(attachedCamera.aspect, 1f)) * attachedCamera.orthographicSize;
            Renderer r = boundsObject.GetComponent<Renderer>();
            Vector3 minBounds = r.bounds.min;
            Vector3 maxBounds = r.bounds.max;
            Vector3 centerBounds = r.bounds.center; 
            minX = minBounds.x + cameraMax.x;
            maxX = maxBounds.x - cameraMax.x;
            if (minX > maxX) {
                minX = centerBounds.x;
                maxX = minX;
            }
            minY = minBounds.y + cameraMax.y;
            maxY = maxBounds.y - cameraMax.y;
            if(minY > maxY) {
                minY = centerBounds.y;
                maxY = minY;
            }
        }
    }

    void IncreaseCameraSize() {
        if (attachedCamera.orthographicSize < maxCameraSize) {
            attachedCamera.orthographicSize = attachedCamera.orthographicSize + cameraResizeSpeed;
            UpdateBounds();
        }
    }

    void DecreaseCameraSize() {
        if (attachedCamera.orthographicSize > minCameraSize) {
            attachedCamera.orthographicSize = attachedCamera.orthographicSize - cameraResizeSpeed;
            UpdateBounds();
        }
    }
}
