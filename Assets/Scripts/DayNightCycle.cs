using UnityEngine;
using System.Collections;

public class DayNightCycle : MonoBehaviour
{
    public float transitionDuration = 5f;
    public bool isNight = false;
    public float delayBetweenChange = 10f;
    public float maxHeadLightIntensity = 8f;

    public Light carHeadLight;

    void Start()
    {
        carHeadLight.intensity = isNight ? maxHeadLightIntensity : 0;
        // Start the rotation coroutine
        StartCoroutine(transitionPeriodically());
    }

    IEnumerator transitionPeriodically()
    {
        while (true)
        {
            // Wait for the specified delay
            yield return new WaitForSeconds(delayBetweenChange);

            if(!isNight && ProbabilityFunctions.shouldChangeToNight(60)){
                isNight = true;
                yield return StartCoroutine(transition(-90f, maxHeadLightIntensity));
            }
            if(isNight && !ProbabilityFunctions.shouldChangeToNight(60)){
                isNight = false;
                yield return StartCoroutine(transition(90f, -maxHeadLightIntensity));
            }
            
        }
    }

    IEnumerator transition(float angle, float changeInLightIntensity)
    {
        float elapsedTime = 0f;
        float initialIntensity = carHeadLight.intensity;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(angle, transform.eulerAngles.y, transform.eulerAngles.z);

        while (elapsedTime < transitionDuration)
        {
            carHeadLight.intensity = (initialIntensity + changeInLightIntensity * (elapsedTime/transitionDuration));
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, elapsedTime / transitionDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final rotation is set exactly
        transform.rotation = endRotation;
        carHeadLight.intensity = initialIntensity + changeInLightIntensity;
    }
}
