using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightHandler : MonoBehaviour
{
    public ItemsHandler ItemsHandler;
    public float LightDimSpeed = 1f;

    public float OilAddSpeed = 2f;
    public float OilAddAmount = 0.25f;

    SpriteRenderer LightSpriteRenderer;
    public SpriteRenderer DarknessSpriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        LightSpriteRenderer = GetComponent<SpriteRenderer>(); // get the sprite renderer
    }

    IEnumerator RemoveDarkness()
    {
        float ElapsedTime = 0f; // Start at 0
        Color DarknessColour = DarknessSpriteRenderer.color; // get the current light colour

        float StartAlpha = DarknessColour.a; // get it's current transparency
        float EndAlpha = StartAlpha - OilAddAmount; // set the goal to reach

        while (ElapsedTime < OilAddSpeed) // Continue this loop while the elapsed time is lower than the time it needs to take for the animation to complete
        {
            ElapsedTime += Time.deltaTime; // Add the time difference between frames to the elapsed time
            float NewTransparency = Mathf.Lerp(StartAlpha, EndAlpha, ElapsedTime / OilAddSpeed); // This time we use lerp instead cause lerp is cool, lerps the alpha to the goal alpha

            if (NewTransparency <= 0f)
            {
                break;
            }

            DarknessColour.a = NewTransparency; // set the new transparency to the temporary light colour
            DarknessSpriteRenderer.color = DarknessColour; // set the temporary light colour to the sprite itself

            yield return null; // wait one frame
        }
    }

    IEnumerator AddLight()
    {
        float ElapsedTime = 0f; // Start at 0
        Color LightColour = LightSpriteRenderer.color; // get the current light colour

        float StartAlpha = LightColour.a; // get it's current transparency
        float EndAlpha = StartAlpha + OilAddAmount; // set the goal to reach

        while (ElapsedTime < OilAddSpeed) // Continue this loop while the elapsed time is lower than the time it needs to take for the animation to complete
        {
            ElapsedTime += Time.deltaTime; // Add the time difference between frames to the elapsed time
            float NewTransparency = Mathf.Lerp(StartAlpha, EndAlpha, ElapsedTime / OilAddSpeed); // This time we use lerp instead cause lerp is cool, lerps the alpha to the goal alpha

            if (NewTransparency >= 1f)
            {
                break;
            }

            LightColour.a = NewTransparency; // set the new transparency to the temporary light colour
            LightSpriteRenderer.color = LightColour; // set the temporary light colour to the sprite itself

            yield return null; // wait one frame
        }

        if (ItemsHandler.OilAmount == 0) // if statement to see whether the oil button can be pressed again based on if they have oil or not
        {
            ItemsHandler.OilButton.interactable = false;
        } else
        {
            ItemsHandler.OilButton.interactable = true;
        }
    }

    public void UseOil() // This function is being called by the button on the Items canvas with the OnClick() function
    {
        if (ItemsHandler.OilAmount >= 1) // Do we have more than 0 oil?
        {
            ItemsHandler.OilAmount -= 1; // Remove an oil if so
            ItemsHandler.OilText.text = $"OIL: {ItemsHandler.OilAmount}/10"; // Set the text to display correctly

            StartCoroutine(AddLight()); // Start the coroutine to do the animation
            StartCoroutine(RemoveDarkness());
            ItemsHandler.OilButton.interactable = false; // Disable the button while it adds light so they cant just spam
        }
    }

    // Update is called once per frame
    void Update()
    {
        Color LightColour = LightSpriteRenderer.color; // Get the current colour
        float NewTransparency = Mathf.MoveTowards(LightColour.a, 0f, LightDimSpeed * Time.deltaTime); // Using Mathf.MoveTowards to change a number to, well, go towards a goal, in our case its to always aim to go to 0 at a certain speed

        LightColour.a = NewTransparency; // Set the temporary colour value to have that transparency
        LightSpriteRenderer.color = LightColour; // Set it to the current light colour since it uses RGBA, A being alpha which is the transparency

        Color DarknessColour = DarknessSpriteRenderer.color; // Get the current colour
        float NewDarknessTransparency = Mathf.MoveTowards(DarknessColour.a, 1f, LightDimSpeed * Time.deltaTime); // Using Mathf.MoveTowards to change a number to, well, go towards a goal, in our case its to always aim to go to 0 at a certain speed

        DarknessColour.a = NewDarknessTransparency; // Set the temporary colour value to have that transparency
        DarknessSpriteRenderer.color = DarknessColour; // Set it to the current light colour since it uses RGBA, A being alpha which is the transparency
    }
}
