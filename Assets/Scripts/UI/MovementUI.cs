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
        // 1) �̵� �հ����� ���� ������: UI�� �ƴ� ������ "���۵�" ��ġ�� ä��
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
                        continue; // UI���� ���۵� ��ġ�� ����

                    moveTouchId = id;
                    moveTouchPos = t.position.ReadValue();
                    break;
                }
            }
            return;
        }

        // 2) �̵� �հ����� ������ ������: �� �հ����� ����
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
                    moveTouchId = -1; // �հ��� �� �� �̵� ����
                }
                break;
            }
        }
    }
}
