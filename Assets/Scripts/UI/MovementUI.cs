using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MovementUI : MonoBehaviour
{

    [SerializeField]
    GameObject MoveCircle;

    InputAction pressAction;
    InputAction pointAction;
    int moveTouchId = -1;
    Vector2 moveTouchPos;
    Vector3 direction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MoveCircle.SetActive(false);
        pressAction = InputSystem.actions.FindAction("UI/Press", throwIfNotFound: true);
        pointAction = InputSystem.actions.FindAction("UI/Point", throwIfNotFound: true);
    }

    void Update()
    {
        UpdateMoveFinger();

        bool isPressed = moveTouchId != -1;
        Vector3 position = moveTouchPos;

        if (!isPressed)
        {
            MoveCircle.SetActive(false);
            GameManager.Instance.Player.Stop();
            return;
        }

        if (!MoveCircle.activeSelf)
        {
            MoveCircle.SetActive(true);
            MoveCircle.transform.position = position;
            return;
        }

        direction = (position - MoveCircle.transform.position);
        direction = new Vector3(direction.x, 0, direction.y).normalized; // 2D plane movement
        ((CharacterStatus)GameManager.Instance.Player.GetStatus()).MoveDirection = direction;
    }

    void UpdateMoveFinger()
    {
        var ts = Touchscreen.current;

        if (ts != null)
        {
            // 1) 이동 손가락이 아직 없으면: UI가 아닌 곳에서 "시작된" 터치만 채택
            if (moveTouchId == -1)
            {
                foreach (var t in ts.touches)
                {
                    if (!t.press.wasPressedThisFrame) continue;

                    int id = t.touchId.ReadValue();
                    if (EventSystem.current != null &&
                        EventSystem.current.IsPointerOverGameObject(id))
                        continue; // UI에서 시작된 터치는 무시

                    moveTouchId = id;
                    moveTouchPos = t.position.ReadValue();
                    break;
                }
                return;
            }

            foreach (var t in ts.touches)
            {
                int id = t.touchId.ReadValue();
                if (id != moveTouchId) continue;

                moveTouchPos = t.position.ReadValue();

                var phase = t.phase.ReadValue();
                if (phase == UnityEngine.InputSystem.TouchPhase.Canceled ||
                    phase == UnityEngine.InputSystem.TouchPhase.Ended)
                {
                    //Logger.Instance.Log($"Move finger {moveTouchId} ended at {moveTouchPos} with phase {phase}");
                    moveTouchId = -1; // 손가락 뗌 → 이동 종료
                }
                break;
            }
            return;
        }

        var mouse = Mouse.current;
        if (mouse == null) return;

        // UI 위에서 "새로" 누른 경우만 시작 차단
        if (mouse.leftButton.wasPressedThisFrame)
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) return;
            moveTouchId = -2; // 마우스 전용 ID
        }

        // 누르고 있는 동안 계속 위치 업데이트
        if (mouse.leftButton.isPressed && moveTouchId == -2)
        {
            moveTouchPos = mouse.position.ReadValue();
            return;
        }

        // 릴리즈 프레임에 종료
        if (mouse.leftButton.wasReleasedThisFrame && moveTouchId == -2)
        {
            moveTouchId = -1;
        }
    }
}
