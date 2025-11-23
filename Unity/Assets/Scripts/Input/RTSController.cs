using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class RTSController : MonoBehaviour
{
    [SerializeField]
    private Transform eye;
    [SerializeField]
    private CinemachineCamera cinemachineCamera;
    [SerializeField]
    private float edgeThreshold = 20f;
    [SerializeField]
    private float panningSpeed = 10f;
    [SerializeField]
    private float zoomSpeed = 30f;
    [SerializeField]
    private float minimumOrthographicSize = 3f;
    [SerializeField]
    private float maximumOrthographicSize = 12f;
    [SerializeField]
    private LayerMask selectMask;

    private PlayerInputActions inputActions;
    private InputManager inputManager;
    private Collider2D selected;
    
    private void Start()
    {
        inputManager = GetComponent<InputManager>();
        inputActions = inputManager.InputActions;
        inputActions.RTSControl.Enable();
        inputActions.RTSControl.Select.performed += OnSelectPerformed;
    }

    private void OnEnable() => inputActions?.RTSControl.Enable();
    private void OnDisable() => inputActions.RTSControl.Disable();

    private void Update()
    {
        Vector2 mousePosition = inputActions.RTSControl.MousePosition.ReadValue<Vector2>();
        HandleEdgeScrolling(mousePosition);
        Vector2 zoom = inputActions.RTSControl.Zoom.ReadValue<Vector2>();
        HandleZoom(zoom.y);
    }

    private void HandleEdgeScrolling(Vector2 mousePosition)
    {
        Vector3 inputDirection = Vector3.zero;

        if (mousePosition.x < edgeThreshold)
            inputDirection.x = -1f;
        if (mousePosition.x > Screen.width - edgeThreshold)
            inputDirection.x = 1f;

        if (mousePosition.y < edgeThreshold)
            inputDirection.y = -1f;
        if (mousePosition.y > Screen.height - edgeThreshold)
            inputDirection.y = 1f;

        if (inputDirection != Vector3.zero)
            eye.position += inputDirection.normalized * panningSpeed * Time.deltaTime;
    }

    private void HandleZoom(float zoom)
    {
        if (zoom == 0f) return;
        
        float target = cinemachineCamera.Lens.OrthographicSize;

        if (zoom > 0f) target += 1;
        if (zoom < 0f) target -= 1;

        target = Mathf.Clamp(target, minimumOrthographicSize, maximumOrthographicSize);
        cinemachineCamera.Lens.OrthographicSize = Mathf.Lerp(cinemachineCamera.Lens.OrthographicSize, target, Time.deltaTime * zoomSpeed);
    }

    private void OnSelectPerformed(InputAction.CallbackContext context)
    {
        if (selected != null)
        {
            inputManager.OnReleaseAgentControl();
            selected = null;
        }

        Vector2 mousePosition = inputActions.RTSControl.MousePosition.ReadValue<Vector2>();
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(mousePosition);
        worldPoint.z = 0f;

        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero, 0f, selectMask);

        if (hit.collider != null)
        {
            selected = hit.collider;
            AgentContext agent = hit.collider.GetComponent<AgentContext>();
            if (agent != null) inputManager.OnTakeAgentControl(agent);
        }
    }
}
