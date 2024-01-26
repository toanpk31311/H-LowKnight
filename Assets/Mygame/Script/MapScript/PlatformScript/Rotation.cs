using System.Collections;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;
using UnityEditor;

public class Rotation : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Vector3 iniRotation;
    private float elapsedTime;
    [SerializeField] private Vector3 targetRotation;
    [SerializeField] AnimationCurve curve;
    [SerializeField] private float duration;
    [SerializeField] private bool isTouched;
    [SerializeField] private float waitTime;

    private void Awake() {
        isTouched = false;
        iniRotation = transform.rotation.eulerAngles;
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isTouched){
            boxCollider.enabled = true;
        }
        else{
            boxCollider.enabled = false;
        }

        
        if(isTouched){
            elapsedTime += Time.deltaTime;
            float percentageComplete = elapsedTime / duration;
            StartCoroutine(rotating(percentageComplete));
        }
    }

    IEnumerator rotating(float percent){
        if(transform.rotation.eulerAngles == iniRotation){
            yield return new WaitForSeconds(waitTime);
            rotateDown(percent);
            if(transform.rotation.eulerAngles == targetRotation){
                elapsedTime = 0;
                StopAllCoroutines();
            }
        }
        if(transform.rotation.eulerAngles == targetRotation){
            yield return new WaitForSeconds(2);
            rotateUp(percent);
            if(transform.rotation.eulerAngles == iniRotation){
                isTouched = false;
                elapsedTime = 0;
                StopAllCoroutines();
            }
        }
    }

    void rotateDown(float percent){
        transform.rotation = Quaternion.Slerp(Quaternion.Euler(iniRotation), Quaternion.Euler(targetRotation), curve.Evaluate(percent));
    }

    void rotateUp(float percent){
        transform.rotation = Quaternion.Slerp(Quaternion.Euler(targetRotation), Quaternion.Euler(iniRotation), curve.Evaluate(percent));
    }
}
