using UnityEngine;
using Dreamteck.Splines;

public class PlayerBehaviour : MonoBehaviour
{
    bool isDragging = false;
    Vector2 dragStartPos;
    Vector2 pointerMovement;
    
    float currentAxisMovement = 0;
    [Range(-1, 1)]
    [SerializeField] float horizontalPos;
    [SerializeField] float horizonAxisMovement;
    [SerializeField] float pointerMinMaxMove;
    [Range(1, 6)]
    [SerializeField] float horizontalMovementBounds;
    // lower is faster
    [Range(1, 8)]
    [SerializeField] float horizontalMovementSpeed;

    [Range(0.02f, 0.3f)]
    [SerializeField] float upwardBaseOffset;

    [Range(60f, 20f)]
    [SerializeField] float upwardDivider;

    private float mapResult;
    private float firstDegreeCalc;
    private Vector3 rightVector;

    float tapStartTime;
    float tapEndTime;

    public void SetPlayerSpeed(float spd)
    {
        gameObject.GetComponent<SplineFollower>().followSpeed = spd;
    }

    private void CustomMouseDown()
    {
        isDragging = true;
        dragStartPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    }

    private void CustomOnMouseUp()
    {
        isDragging = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            tapStartTime = Time.time;
            CustomMouseDown();
        }
        if (Input.GetMouseButtonUp(0))
        {
            tapEndTime = Time.time;
            if (tapEndTime - tapStartTime < 0.5f)
                Jump();
            CustomOnMouseUp();
        }
        if (isDragging)
        {
            pointerMovement = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - dragStartPos;
            horizonAxisMovement = pointerMovement.x;
            /*
            Vector3 tmp = gameObject.transform.position;
            tmp.x = (horizonAxisMovement / 100f) + tmp.x;
            tmp.y = (horizonAxisMovement / 100f) + tmp.y;
            gameObject.transform.position = tmp;
            */
            mapResult = Map(horizonAxisMovement, -pointerMinMaxMove, +pointerMinMaxMove, -1, +1);
            currentAxisMovement += mapResult / horizontalMovementSpeed;
            currentAxisMovement = Mathf.Clamp(currentAxisMovement,-horizontalMovementBounds,horizontalMovementBounds);
            //firstDegreeCalc = (mapResult / 22);
            rightVector = Vector3.right * (currentAxisMovement / 22);

        }
        transform.Translate(rightVector);
        gameObject.GetComponent<SplineFollower>().motion.offset = new Vector2(0, upwardBaseOffset+(Mathf.Abs(currentAxisMovement /upwardDivider))   );
    }

    void Jump()
    {

    }

    internal static float Map(float value, float fromLow, float fromHigh, float toLow, float toHigh)
    {
        if (value > fromHigh) return toHigh;
        if (value < fromLow) return toLow;
        return ((value - fromLow) * (toHigh - toLow) / (fromHigh - fromLow)) + toLow;
    }
}
