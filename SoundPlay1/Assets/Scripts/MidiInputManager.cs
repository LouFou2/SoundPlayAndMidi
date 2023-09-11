using Minis;
using UnityEngine;
using UnityEngine.InputSystem;

public class MidiInputManager : MonoBehaviour
{
    // assign the actions asset to this field in the inspector:
    public InputActionAsset actions;

    //private InputAction midiKnob1; // *** dont use knob 1 or 9
    //private InputAction midiKnob9;

    private InputAction midiKnob2;
    private InputAction midiKnob3;
    private InputAction midiKnob4;
    private InputAction midiKnob5;
    private InputAction midiKnob6;
    private InputAction midiKnob7;
    private InputAction midiKnob8;
    // skip 9 for now
    private InputAction midiKnob10;
    private InputAction midiKnob11;
    private InputAction midiKnob12;
    private InputAction midiKnob13;
    private InputAction midiKnob14;
    private InputAction midiKnob15;
    private InputAction midiKnob16;
    private InputAction midiPad1;
    private InputAction midiPad2;
    private InputAction midiPad3;
    private InputAction midiPad4;
    private InputAction midiPad5;
    private InputAction midiPad6;
    private InputAction midiPad7;
    private InputAction midiPad8;

    public float _midiKnob2Value = 0.5f;
    public float _midiKnob3Value = 0.5f;
    public float _midiKnob4Value = 0.5f;
    public float _midiKnob5Value = 0.5f;
    public float _midiKnob6Value = 0.5f;
    public float _midiKnob7Value = 0.5f;
    public float _midiKnob8Value = 0.5f;
    public float _midiKnob10Value = 0.5f;
    public float _midiKnob11Value = 0.5f;
    public float _midiKnob12Value = 0.5f;
    public float _midiKnob13Value = 0.5f;
    public float _midiKnob14Value = 0.5f;
    public float _midiKnob15Value = 0.5f;
    public float _midiKnob16Value = 0.5f;
    public float _midiPad1Value = 0f;
    public float _midiPad2Value = 0f;
    public float _midiPad3Value = 0f;
    public float _midiPad4Value = 0f;
    public float _midiPad5Value = 0f;
    public float _midiPad6Value = 0f;
    public float _midiPad7Value = 0f;
    public float _midiPad8Value = 0f;

    void Awake()
    {
        midiKnob2 = actions.FindActionMap("Midi").FindAction("MidiKnob2");
        midiKnob3 = actions.FindActionMap("Midi").FindAction("MidiKnob3");
        midiKnob4 = actions.FindActionMap("Midi").FindAction("MidiKnob4");
        midiKnob5 = actions.FindActionMap("Midi").FindAction("MidiKnob5");
        midiKnob6 = actions.FindActionMap("Midi").FindAction("MidiKnob6");
        midiKnob7 = actions.FindActionMap("Midi").FindAction("MidiKnob7");
        midiKnob8 = actions.FindActionMap("Midi").FindAction("MidiKnob8");
        midiKnob10 = actions.FindActionMap("Midi").FindAction("MidiKnob10");
        midiKnob11 = actions.FindActionMap("Midi").FindAction("MidiKnob11");
        midiKnob12 = actions.FindActionMap("Midi").FindAction("MidiKnob12");
        midiKnob13 = actions.FindActionMap("Midi").FindAction("MidiKnob13");
        midiKnob14 = actions.FindActionMap("Midi").FindAction("MidiKnob14");
        midiKnob15 = actions.FindActionMap("Midi").FindAction("MidiKnob15");
        midiKnob16 = actions.FindActionMap("Midi").FindAction("MidiKnob16");

        midiPad1 = actions.FindActionMap("Midi").FindAction("MidiPad1");
        midiPad2 = actions.FindActionMap("Midi").FindAction("MidiPad2");
        midiPad3 = actions.FindActionMap("Midi").FindAction("MidiPad3");
        midiPad4 = actions.FindActionMap("Midi").FindAction("MidiPad4");
        midiPad5 = actions.FindActionMap("Midi").FindAction("MidiPad5");
        midiPad6 = actions.FindActionMap("Midi").FindAction("MidiPad6");
        midiPad7 = actions.FindActionMap("Midi").FindAction("MidiPad7");
        midiPad8 = actions.FindActionMap("Midi").FindAction("MidiPad8");

    }
    void OnEnable()
    {
        actions.FindActionMap("Midi").Enable();
    }
    void OnDisable()
    {
        actions.FindActionMap("Midi").Disable();
    }
    

    void Update()
    {
        //float currentknob1Value = midiKnob1.ReadValue<float>();
        float currentknob2Value = midiKnob2.ReadValue<float>();
        float currentknob3Value = midiKnob3.ReadValue<float>();
        float currentknob4Value = midiKnob4.ReadValue<float>();
        float currentknob5Value = midiKnob5.ReadValue<float>();
        float currentknob6Value = midiKnob6.ReadValue<float>();
        float currentknob7Value = midiKnob7.ReadValue<float>();
        float currentknob8Value = midiKnob8.ReadValue<float>();
        float currentknob10Value = midiKnob10.ReadValue<float>();
        float currentknob11Value = midiKnob11.ReadValue<float>();
        float currentknob12Value = midiKnob12.ReadValue<float>();
        float currentknob13Value = midiKnob13.ReadValue<float>();
        float currentknob14Value = midiKnob14.ReadValue<float>();
        float currentknob15Value = midiKnob15.ReadValue<float>();
        float currentknob16Value = midiKnob16.ReadValue<float>();

        float pad1Value = midiPad1.ReadValue<float>();
        float pad2Value = midiPad2.ReadValue<float>();
        float pad3Value = midiPad3.ReadValue<float>();
        float pad4Value = midiPad4.ReadValue<float>();
        float pad5Value = midiPad5.ReadValue<float>();
        float pad6Value = midiPad6.ReadValue<float>();
        float pad7Value = midiPad7.ReadValue<float>();
        float pad8Value = midiPad8.ReadValue<float>();

        _midiKnob2Value = currentknob2Value;
        _midiKnob3Value = currentknob3Value;
        _midiKnob4Value = currentknob4Value;
        _midiKnob5Value = currentknob5Value;
        _midiKnob6Value = currentknob6Value;
        _midiKnob7Value = currentknob7Value;
        _midiKnob8Value = currentknob8Value;
        _midiKnob10Value = currentknob10Value;
        _midiKnob11Value = currentknob11Value;
        _midiKnob12Value = currentknob12Value;
        _midiKnob13Value = currentknob13Value;
        _midiKnob14Value = currentknob14Value;
        _midiKnob15Value = currentknob15Value;
        _midiKnob16Value = currentknob16Value;
        _midiPad1Value = pad1Value;
        _midiPad2Value = pad2Value;
        _midiPad3Value = pad3Value;
        _midiPad4Value = pad4Value;
        _midiPad5Value = pad5Value;
        _midiPad6Value = pad6Value;
        _midiPad7Value = pad7Value;
        _midiPad8Value = pad8Value;
    }

}
