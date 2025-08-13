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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MoveCircle.SetActive(false);
        pressAction = InputSystem.actions.FindAction("UI/Press", throwIfNotFound: true);
        pointAction = InputSystem.actions.FindAction("UI/Point", throwIfNotFound: true);
    }

    // Update is called once per frame
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

        Vector3 direction = (position - MoveCircle.transform.position).normalized;
        direction = new Vector3(direction.x, 0, direction.y); // 2D plane movement

        GameManager.Instance.Player.Move(direction, Time.deltaTime);
    }


    void UpdateMoveFinger()
    {
        var ts = Touchscreen.current;
        // 1) 이동 손가락이 아직 없으면: UI가 아닌 곳에서 "시작된" 터치만 채택
        if (moveTouchId == -1)
        {
            if (ts != null)
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
            }
            return;
        }

        // 2) 이동 손가락이 정해져 있으면: 그 손가락만 추적
        if (ts != null)
        {
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
        }
    }
}
