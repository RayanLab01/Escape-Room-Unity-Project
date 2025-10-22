using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class DoorHandle : XRBaseInteractable
{
    public bool doorUnlocked = false;
    private IXRSelectInteractor currentInteractor;
    public Transform doorTransform;
    [SerializeField]
    private Vector3 initialDoorPosition;
    [SerializeField]
    private Vector3 maxDoorPosition;

    public float maxSlideDistance;
    public int DoorWeight;
    private Vector3 worldDragDirection;
    private Vector3 localDragDirection = Vector3.left;
    [SerializeField]
    private float slideSpeed;


    private void Start()
    {
        maxSlideDistance = 0.8f;
        DoorWeight = 4;
        worldDragDirection = transform.TransformDirection(localDragDirection).normalized;
        initialDoorPosition = doorTransform.position; 
        maxDoorPosition = initialDoorPosition + worldDragDirection * maxSlideDistance;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        currentInteractor = args.interactorObject;
        Debug.Log(currentInteractor);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        currentInteractor = null;
        Debug.Log("Handle released");
    }

    

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if (doorUnlocked && isSelected && currentInteractor != null)
        {
            Vector3 controllerPosition = currentInteractor.transform.position;
            
            float dotProduct = Vector3.Dot(controllerPosition - transform.position, worldDragDirection);
            slideSpeed = Mathf.Abs(dotProduct) * Time.deltaTime / DoorWeight;
            if (transform.position.z > controllerPosition.z)
            {
                doorTransform.position = Vector3.MoveTowards(doorTransform.position, maxDoorPosition, slideSpeed);
            }
            else if (transform.position.z < controllerPosition.z)
            {
                doorTransform.position = Vector3.MoveTowards(doorTransform.position, initialDoorPosition, slideSpeed);
            }
            

        }
    }
}
