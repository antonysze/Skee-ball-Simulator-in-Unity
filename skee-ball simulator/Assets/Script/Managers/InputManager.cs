using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public static InputManager _instance;
    private RuntimePlatform platform = Application.platform;
    public int inputState; //0=none, 1=down
    public bool autoUpdate = false;
    public bool useDragEvent = false;
    public Vector2 inputPosition;
    public Vector2 inputPositionRate;

    // Use this for initialization
    void Awake () {
		_instance = this;
	}

    // Update is called once per frame
    void Update()
    {
        if (autoUpdate)
            InputUpdate();
    }

    public void InputUpdateManually() {
        if (autoUpdate) return;
        InputUpdate();
    }

    void InputUpdate() {
        if (platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer)
        {
            if (Input.touchCount > 0)
            {
                inputPosition = Input.GetTouch(0).position;
                Transform target = checkTouch(inputPosition);
                clickEvent(target);
                inputState = 1;

                caclucalateInputPositionRate(inputPosition);
            } else {
                inputState = 0;
                inputPositionRate = new Vector3(0.5f,0.5f,0.5f);
            }
            dragEvent();
        }
        else if (platform == RuntimePlatform.WindowsEditor || platform == RuntimePlatform.WindowsPlayer ||
            platform == RuntimePlatform.OSXEditor || platform == RuntimePlatform.OSXPlayer)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Transform target = checkTouch(Input.mousePosition);
                clickEvent(target);
                
            }
            inputState = Input.GetMouseButton(0)?1:0;
            inputPosition = Input.mousePosition;
            caclucalateInputPositionRate(inputPosition);
            dragEventMouse();
        }
    }

    public void caclucalateInputPositionRate(Vector2 pos) {
        pos.x /= Screen.width;
        pos.y /= Screen.height;
        inputPositionRate = pos;
    }

    Transform checkTouch(Vector3 pos)
    {
        Vector3 wp = Camera.main.ScreenToWorldPoint(pos);
        Vector2 touchPos = new Vector2(wp.x, wp.y);
        Collider2D hit = Physics2D.OverlapPoint(touchPos);

        if (hit)
        {
            return hit.transform;
        }
        return null;
    }

    void clickEvent(Transform target)
    {

    }


    private Vector2 touchPos;
    ArrayList lastPos = new ArrayList();
    public static float dragSpeed = 0;
    public static float uncontrolDragSpeed = 0;
    public static float moveRate = 0;
    public static bool canDrag = true;
    public static bool holdDown = false;
    void dragEvent() {
        if (!useDragEvent) return;

        holdDown = Input.touchCount > 0;
        if (!holdDown) {
            dragSpeed = Mathf.SmoothStep(dragSpeed, 0, Time.deltaTime*10);
            uncontrolDragSpeed = Mathf.SmoothStep(uncontrolDragSpeed, 0, Time.deltaTime*10);
        }
        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            touchPos = Input.GetTouch(0).position;
            lastPos.Clear();
            lastPos.Add(touchPos);
            dragSpeed = 0;
        }
        if (Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            touchPos = Input.GetTouch(0).position;
            Vector2 last = touchPos;
            if (lastPos.Count > 0)
            last = (Vector2)lastPos[lastPos.Count - 1];
            
            lastPos.Add(touchPos);
            if (lastPos.Count > 5)
                lastPos.RemoveAt(0);

            float offset = touchPos.x - last.x;
            dragSpeed = (offset)*0.3f;
            moveRate += offset/Screen.width;
        }
        if (Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            if (lastPos.Count < 1) return;
            float speed = ((Vector2)lastPos[lastPos.Count - 1]).x - ((Vector2)lastPos[0]).x;
            dragSpeed = speed * 0.1f;
            moveRate = 0;
        }
        if (canDrag) uncontrolDragSpeed = dragSpeed;    
    }

    void dragEventMouse() {
        holdDown = Input.GetMouseButton(0);
        if (!holdDown) {
            dragSpeed = Mathf.SmoothStep(dragSpeed, 0, Time.deltaTime*15);
            uncontrolDragSpeed = Mathf.SmoothStep(uncontrolDragSpeed, 0, Time.deltaTime*15);
        }

        if (Input.GetMouseButtonDown(0))
        {
            touchPos = Input.mousePosition;
            lastPos.Clear();
            lastPos.Add(touchPos);
            dragSpeed = 0;
        }
        if (Input.GetMouseButton(0))
        {
            touchPos = Input.mousePosition;
            Vector2 last = touchPos;
            if (lastPos.Count > 0)
                last = (Vector2)lastPos[lastPos.Count - 1];
            
            lastPos.Add(touchPos);
            if (lastPos.Count > 5)
                lastPos.RemoveAt(0);

            float offset = touchPos.x - last.x;
            dragSpeed = offset;
            moveRate += offset/Screen.width;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (lastPos.Count < 1) return;
            float speed = ((Vector2)lastPos[lastPos.Count - 1]).x - ((Vector2)lastPos[0]).x;
            dragSpeed = speed*0.2f;
            moveRate = 0;
        }
        if (canDrag) uncontrolDragSpeed = dragSpeed; 
    }
}
