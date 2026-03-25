using UnityEngine;
using UnityEngine.Rendering;

public class Bug : MonoBehaviour
{
    public float MoveRadius = 2f;
    Vector2 targetPos;
    Vector2 originPos;

    public float MoveSpeed = 0.02f;
    public float HoverTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originPos = transform.position;
        ChooseTargetPos();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentPosition = transform.localPosition;

        transform.localPosition = Vector2.MoveTowards(currentPosition, targetPos, MoveSpeed);
        if (Vector2.Distance(currentPosition, targetPos) < 0.05f)
        {
            ChooseTargetPos();
        }
    }

    void ChooseTargetPos()
    {
        float offsetX = UnityEngine.Random.Range(-MoveRadius, MoveRadius);
        float offsetY = UnityEngine.Random.Range(-MoveRadius, MoveRadius);
        targetPos = originPos + new Vector2(offsetX, offsetY);
    }
}
