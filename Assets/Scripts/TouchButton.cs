using Mono.Cecil.Cil;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class TouchButton : XRBaseInteractable
{
    public Material hoverMaterial;
    private Material originalMaterial;
    public int buttonNumber;
    public NumberPad numberPad;


    protected override void Awake()
    {
        base.Awake();
        originalMaterial = GetComponent<Renderer>().material;
    }
    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);
        GetComponent<Renderer>().material = hoverMaterial;
        numberPad.screenText.text = "";
        numberPad.currentInput += buttonNumber.ToString();
    }
    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        base.OnHoverExited(args);
        GetComponent<Renderer>().material = originalMaterial;
    }

}
