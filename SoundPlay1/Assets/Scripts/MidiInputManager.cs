using Minis;
using UnityEngine;
using UnityEngine.InputSystem;

public class MidiInputManager : MonoBehaviour
{
    // assign the actions asset to this field in the inspector:
    public InputActionAsset actions;

    //private InputAction midiKnob1; // *** dont use knob 1 or 9
    //private InputAction midiKnob9;

    private InputAction midiKnob2; // The knob inputs
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

    private InputAction midiPad1; // The pad inputs
    private InputAction midiPad2;
    private InputAction midiPad3;
    private InputAction midiPad4;
    private InputAction midiPad5;
    private InputAction midiPad6;
    private InputAction midiPad7;
    private InputAction midiPad8;

    private InputAction keyC3; // The key inputs
    private InputAction keyD3;
    private InputAction keyE3;
    private InputAction keyF3;
    private InputAction keyG3;
    private InputAction keyA3;
    private InputAction keyB3;
    private InputAction keyC4;
    private InputAction keyD4;
    private InputAction keyE4;
    private InputAction keyF4;
    private InputAction keyG4;
    private InputAction keyA4;
    private InputAction keyB4;
    private InputAction keyC5;


    //== Values of the inputs==//
    public float _midiKnob2Value = 0f;
    public float _midiKnob3Value = 0f;
    public float _midiKnob4Value = 0f;
    public float _midiKnob5Value = 0f;
    public float _midiKnob6Value = 0f;
    public float _midiKnob7Value = 0f;
    public float _midiKnob8Value = 0f;
    public float _midiKnob10Value = 0f;
    public float _midiKnob11Value = 0f;
    public float _midiKnob12Value = 0f;
    public float _midiKnob13Value = 0f;
    public float _midiKnob14Value = 0f;
    public float _midiKnob15Value = 0f;
    public float _midiKnob16Value = 0f;
    public float _midiPad1Value = 0f;
    public float _midiPad2Value = 0f;
    public float _midiPad3Value = 0f;
    public float _midiPad4Value = 0f;
    public float _midiPad5Value = 0f;
    public float _midiPad6Value = 0f;
    public float _midiPad7Value = 0f;
    public float _midiPad8Value = 0f;

    public float _keyC3_Value = 0f;
    public float _keyD3_Value = 0f;
    public float _keyE3_Value = 0f;
    public float _keyF3_Value = 0f;
    public float _keyG3_Value = 0f;
    public float _keyA3_Value = 0f;
    public float _keyB3_Value = 0f;
    public float _keyC4_Value = 0f;
    public float _keyD4_Value = 0f;
    public float _keyE4_Value = 0f;
    public float _keyF4_Value = 0f;
    public float _keyG4_Value = 0f;
    public float _keyA4_Value = 0f;
    public float _keyB4_Value = 0f;
    public float _keyC5_Value = 0f;

    public int previousKeyIndex = -1;
    public int currentKeyIndex = -1;

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

        keyC3 = actions.FindActionMap("Midi").FindAction("KeyC3");
        keyD3 = actions.FindActionMap("Midi").FindAction("KeyD3");
        keyE3 = actions.FindActionMap("Midi").FindAction("KeyE3");
        keyF3 = actions.FindActionMap("Midi").FindAction("KeyF3");
        keyG3 = actions.FindActionMap("Midi").FindAction("KeyG3");
        keyA3 = actions.FindActionMap("Midi").FindAction("KeyA3");
        keyB3 = actions.FindActionMap("Midi").FindAction("KeyB3");
        keyC4 = actions.FindActionMap("Midi").FindAction("KeyC4");
        keyD4 = actions.FindActionMap("Midi").FindAction("KeyD4");
        keyE4 = actions.FindActionMap("Midi").FindAction("KeyE4");
        keyF4 = actions.FindActionMap("Midi").FindAction("KeyF4");
        keyG4 = actions.FindActionMap("Midi").FindAction("KeyG4");
        keyA4 = actions.FindActionMap("Midi").FindAction("KeyA4");
        keyB4 = actions.FindActionMap("Midi").FindAction("KeyB4");
        keyC5 = actions.FindActionMap("Midi").FindAction("KeyC5");

        previousKeyIndex = -1;
        currentKeyIndex = -1;
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
        //float currentknob1Value = midiKnob1.ReadValue<float>(); // skip knob 1
        float currentknob2Value = midiKnob2.ReadValue<float>();
        float currentknob3Value = midiKnob3.ReadValue<float>();
        float currentknob4Value = midiKnob4.ReadValue<float>();
        float currentknob5Value = midiKnob5.ReadValue<float>();
        float currentknob6Value = midiKnob6.ReadValue<float>();
        float currentknob7Value = midiKnob7.ReadValue<float>();
        float currentknob8Value = midiKnob8.ReadValue<float>();
        //skip knob 9
        float currentknob10Value = midiKnob10.ReadValue<float>();
        float currentknob11Value = midiKnob11.ReadValue<float>();
        float currentknob12Value = midiKnob12.ReadValue<float>();
        float currentknob13Value = midiKnob13.ReadValue<float>();
        float currentknob14Value = midiKnob14.ReadValue<float>();
        float currentknob15Value = midiKnob15.ReadValue<float>();
        float currentknob16Value = midiKnob16.ReadValue<float>();

        float currentPad1Value = midiPad1.ReadValue<float>();
        float currentPad2Value = midiPad2.ReadValue<float>();
        float currentPad3Value = midiPad3.ReadValue<float>();
        float currentPad4Value = midiPad4.ReadValue<float>();
        float currentPad5Value = midiPad5.ReadValue<float>();
        float currentPad6Value = midiPad6.ReadValue<float>();
        float currentPad7Value = midiPad7.ReadValue<float>();
        float currentPad8Value = midiPad8.ReadValue<float>();

        float currentKeyC3Value = keyC3.ReadValue<float>();
        float currentKeyD3Value = keyD3.ReadValue<float>();
        float currentKeyE3Value = keyE3.ReadValue<float>();
        float currentKeyF3Value = keyF3.ReadValue<float>();
        float currentKeyG3Value = keyG3.ReadValue<float>();
        float currentKeyA3Value = keyA3.ReadValue<float>();
        float currentKeyB3Value = keyB3.ReadValue<float>();
        float currentKeyC4Value = keyC4.ReadValue<float>();
        float currentKeyD4Value = keyD4.ReadValue<float>();
        float currentKeyE4Value = keyE4.ReadValue<float>();
        float currentKeyF4Value = keyF4.ReadValue<float>();
        float currentKeyG4Value = keyG4.ReadValue<float>();
        float currentKeyA4Value = keyA4.ReadValue<float>();
        float currentKeyB4Value = keyB4.ReadValue<float>();
        float currentKeyC5Value = keyC5.ReadValue<float>();

        //== Assign an index value for currently pressed key ==//
        if (currentKeyC3Value > 0.05) { currentKeyIndex = 0; }
        else if (currentKeyD3Value > 0.05) { currentKeyIndex = 1; }
        else if (currentKeyE3Value > 0.05) { currentKeyIndex = 2; }
        else if (currentKeyF3Value > 0.05) { currentKeyIndex = 3; }
        else if (currentKeyG3Value > 0.05) { currentKeyIndex = 4; }
        else if (currentKeyA3Value > 0.05) { currentKeyIndex = 5; }
        else if (currentKeyB3Value > 0.05) { currentKeyIndex = 6; }
        else if (currentKeyC4Value > 0.05) { currentKeyIndex = 7; }
        else if (currentKeyD4Value > 0.05) { currentKeyIndex = 8; }
        else if (currentKeyE4Value > 0.05) { currentKeyIndex = 9; }
        else if (currentKeyF4Value > 0.05) { currentKeyIndex = 10; }
        else if (currentKeyG4Value > 0.05) { currentKeyIndex = 11; }
        else if (currentKeyA4Value > 0.05) { currentKeyIndex = 12; }
        else if (currentKeyB4Value > 0.05) { currentKeyIndex = 13; }
        else if (currentKeyC5Value > 0.05) { currentKeyIndex = 14; }

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

        _midiPad1Value = currentPad1Value;
        _midiPad2Value = currentPad2Value;
        _midiPad3Value = currentPad3Value;
        _midiPad4Value = currentPad4Value;
        _midiPad5Value = currentPad5Value;
        _midiPad6Value = currentPad6Value;
        _midiPad7Value = currentPad7Value;
        _midiPad8Value = currentPad8Value;

        _keyC3_Value = currentKeyC3Value;
        _keyD3_Value = currentKeyD3Value;
        _keyE3_Value = currentKeyE3Value;
        _keyF3_Value = currentKeyF3Value;
        _keyG3_Value = currentKeyG3Value;
        _keyA3_Value = currentKeyA3Value;
        _keyB3_Value = currentKeyB3Value;
        _keyC4_Value = currentKeyC4Value;
        _keyD4_Value = currentKeyD4Value;
        _keyE4_Value = currentKeyE4Value;
        _keyF4_Value = currentKeyF4Value;
        _keyG4_Value = currentKeyG4Value;
        _keyA4_Value = currentKeyA4Value;
        _keyB4_Value = currentKeyB4Value;
        _keyC5_Value = currentKeyC5Value;

        //== Update Key Index ==//
        previousKeyIndex = currentKeyIndex;
    }

}
