using System.Collections;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class ObjectMove : MonoBehaviour
{

    private Vector3 iniPosition;
    private Vector3 targetPosition;
    private float elapsedTime;
    [SerializeField] private float duration;
    [SerializeField] private float moveX;
    [SerializeField] private float moveY;
    [SerializeField] AnimationCurve curve;
    [SerializeField] private float waitTime;

    // Start is called before the first frame update
    void Awake()
    {
        iniPosition = transform.position;
        targetPosition = new Vector3(moveX, moveY, 0f);
    }

    // Update is called once per frame
    void Update()
    { 
        elapsedTime += Time.deltaTime;
        float percentageComplete = elapsedTime / duration;
        StartCoroutine(moving(percentageComplete));
    }

    private IEnumerator moving(float percentageComplete){
        if(transform.position == iniPosition){
            yield return new WaitForSeconds(waitTime);
            moveUp(percentageComplete);
            if(transform.position == targetPosition){
                elapsedTime = 0;
                StopAllCoroutines();
            }
        }
        if(transform.position == targetPosition){
            yield return new WaitForSeconds(waitTime);
            moveDown(percentageComplete);
            if(transform.position == iniPosition){
                elapsedTime = 0;
                StopAllCoroutines();
            }
        }
    }

    private void moveUp(float percent){
        transform.position = Vector3.Lerp(transform.position, targetPosition, curve.Evaluate(percent));
    }

    private void moveDown(float percent){
        transform.position = Vector3.Lerp(transform.position, iniPosition, curve.Evaluate(percent));
    }

}
