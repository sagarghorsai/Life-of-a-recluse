using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanicMeterUI : MonoBehaviour
{
    /*PhantomRealm Studio - Life of a Recluse
 * Austin Horn
 * CSCI 448, Davenport University
 * Instructor: David Kroggman
 * 
 * Title: PanicMeterUI
 * Summary: Controls the UI element for the Panic Meter, utilizing the panic value provided by the PanicMeter script.
 */

    public Slider PanicSlider; // Attach slider Parent
    public PanicMeter panicScript; //Attach game object containing the instance of script you want

    float panicSliderValue;
    float maxPanic = 10f;

    void Start()
    {
        
    }

    //Simply updates the slider value to the fetched panic Value
    private void Update()
    {
        panicSliderValue = panicScript.panicValue;
        PanicSlider.value = panicSliderValue; 
    }
}
