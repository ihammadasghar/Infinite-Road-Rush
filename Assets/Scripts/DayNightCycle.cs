using UnityEngine;
using System.Collections;

public class DayNightCycle : MonoBehaviour
{
    public float rotationDuration = 5f;
    public bool isNight = false;
    public float delayBetweenChange = 10f;

    void Start()
    {
        // Start the rotation coroutine
        StartCoroutine(RotatePeriodically());
    }

    IEnumerator RotatePeriodically()
    {
        while (true)
        {
            if(!isNight && ProbabilityFunctions.shouldChangeToNight(30)){
                isNight = true;
                yield return StartCoroutine(RotateXAxis(-90f));
            }
            if(isNight && !ProbabilityFunctions.shouldChangeToNight(30)){
                isNight = false;
                yield return StartCoroutine(RotateXAxis(90f));
            }

            // Wait for the specified delay
            yield return new WaitForSeconds(delayBetweenChange);
        }
    }

    IEnumerator RotateXAxis(float angle)
    {
        float elapsedTime = 0f;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(angle, transform.eulerAngles.y, transform.eulerAngles.z);

        while (elapsedTime < rotationDuration)
        {
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, elapsedTime / rotationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final rotation is set exactly
        transform.rotation = endRotation;
    }
}
