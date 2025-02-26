using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] private float sensitivity = 1f;

    [SerializeField] private float verticalRotationMin = -45;
    [SerializeField] private float verticalRotationMax = 70;

    [SerializeField] private Transform playerTransform;

    [SerializeField] private float cameraZoomIdeal;

    [SerializeField] private LayerMask blockingLayer;

    private Transform pivotTransfom;

    private Transform cameraTransform;

    private float currentHorizontalRotation;
    private float currentVerticalRotation;

    private float cameraZoomCurrent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHorizontalRotation = transform.localEulerAngles.y;
        currentVerticalRotation = transform.localEulerAngles.x;

        pivotTransfom = transform.GetChild(0);

        cameraTransform = pivotTransfom.GetChild(0);

        cameraTransform.localPosition = new Vector3(0, 0, -cameraZoomIdeal);
    }

    // Update is called once per frame
    void Update()
    {
        currentHorizontalRotation += Input.GetAxis("Mouse X") * sensitivity;
        currentVerticalRotation -= Input.GetAxis("Mouse Y") * sensitivity;

        currentVerticalRotation = Mathf.Clamp(currentVerticalRotation, verticalRotationMin, verticalRotationMax);
        transform.localEulerAngles = new Vector3(0, currentHorizontalRotation);
        pivotTransfom.localEulerAngles = new Vector3(currentVerticalRotation, 0);

        transform.position = playerTransform.position;
        
        Vector3 direction = cameraTransform.position - transform.position;

        if (Physics.Raycast(transform.position, direction, out RaycastHit hit, cameraZoomIdeal, blockingLayer))
        {
            cameraZoomCurrent = hit.distance;
        }
        else 
        {
            cameraZoomCurrent = cameraZoomIdeal;
        }

        cameraTransform.localPosition = new Vector3(0, 0, -cameraZoomCurrent);
    }
}
