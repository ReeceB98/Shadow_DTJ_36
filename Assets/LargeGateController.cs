using UnityEngine;
using System.Collections;

public class LargeGateController : MonoBehaviour
{
    public float openAngle = 90f; // Angle to open the gate
    public float smooth = 2f; // Speed of gate movement
    public bool isOpen = false; // State of the gate

    private Quaternion closedRotation;
    private Quaternion openRotation;

    private void Start()
    {
        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, openAngle, 0));
    }

    public void OpenGate()
    {
        if (!isOpen)
        {
            StartCoroutine(OpenGateCoroutine());
        }
    }

    private IEnumerator OpenGateCoroutine()
    {
        float time = 0f;
        while (time < 1f)
        {
            transform.rotation = Quaternion.Slerp(closedRotation, openRotation, time);
            time += Time.deltaTime * smooth;
            yield return null;
        }
        transform.rotation = openRotation;
        isOpen = true;
    }

    public void CloseGate()
    {
        if (isOpen)
        {
            StartCoroutine(CloseGateCoroutine());
        }
    }

    private IEnumerator CloseGateCoroutine()
    {
        float time = 0f;
        while (time < 1f)
        {
            transform.rotation = Quaternion.Slerp(openRotation, closedRotation, time);
            time += Time.deltaTime * smooth;
            yield return null;
        }
        transform.rotation = closedRotation;
        isOpen = false;
    }
}
