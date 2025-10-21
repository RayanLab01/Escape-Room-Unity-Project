using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Interactables;


public class CardReader : XRSocketInteractor
{
    public Vector3 firstPosition;
    public Vector3 lastPosition;
    public Transform cardTransform;

    public float readDistance;
    public float currentDistance;
    public bool validRead;

    public Vector3 diffVector;

    public float allowedRotationRange;

    public float dotUP;
    public float dotRiLef;

    public GameObject redLight;
    public GameObject greenLight;
    public GameObject doorLock;
    protected override void Awake()
    {
        readDistance = 0.15f;
        currentDistance = 0.0f;
        validRead = false;
        allowedRotationRange = 0.2f;
    }

    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        return false;
    }

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);
        if (greenLight.activeSelf) return;
        Debug.Log("Card Inserted");
        cardTransform = args.interactableObject.transform;
        firstPosition = args.interactableObject.transform.position;
        validRead = true;
    }

    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        base.OnHoverExited(args);
        if (greenLight.activeSelf) return;
        Debug.Log("Card Exited");
        lastPosition = args.interactableObject.transform.position;
        diffVector = lastPosition - firstPosition;
        currentDistance = diffVector.y;

        if (validRead & (currentDistance < -readDistance))
        {
            Debug.Log("Card Read");
            greenLight.SetActive(true);
            redLight.SetActive(false);
            doorLock.SetActive(false);
        }
        else if (!greenLight.activeSelf)
        {
            Debug.Log("Card Reading Failed");
            redLight.SetActive(true);
        }
        validRead = false;
        cardTransform = null;
    }

    private void Update()
    {
        if (greenLight.activeSelf) return;
        if (cardTransform != null && !greenLight.activeSelf) 
        {
            Vector3 keyCardUp = cardTransform.forward;
            Vector3 keyCardRiLef = cardTransform.right;

            dotUP = Vector3.Dot(keyCardUp, Vector3.up);
            dotRiLef = Vector3.Dot(keyCardRiLef, Vector3.left);
            if (dotUP < (1 - allowedRotationRange) || dotRiLef < (1 - allowedRotationRange))
            {
                validRead = false;
            }

        }
    }


}

