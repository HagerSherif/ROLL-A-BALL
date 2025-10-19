

using UnityEngine;
using TMPro;

public class TextController : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_Text collectiblesText;
    public TMP_Text victoryText;
    public GameObject startMenu;
    public GameObject nextPhaseButton;

    [Header("Collectible Sequences")]
    public Transform[] tutorialCollectibles;  // 4 corners
    public Transform[] studyCollectibles;     // 12 experiment cubes

    private int currentTutorialIndex = 0;
    private int currentStudyIndex = 0;
    private bool inTutorial = true;

    private float startTime;
    private float endTime;

    void Start()
    {
        // Disable all collectibles at start
        foreach (Transform c in tutorialCollectibles) c.gameObject.SetActive(false);
        foreach (Transform c in studyCollectibles) c.gameObject.SetActive(false);

        victoryText.gameObject.SetActive(false);
        nextPhaseButton.SetActive(false);
    }

    public void StartTutorial()
    {   UpdateCollectiblesText();
        inTutorial = true;
        currentTutorialIndex = 0;
        tutorialCollectibles[0].gameObject.SetActive(true);
        startTime = Time.time;
    }

    public void OnCollectibleCaptured()
    {
        if (inTutorial)
        {    
 
            currentTutorialIndex++;
            UpdateCollectiblesText();

            if (currentTutorialIndex < tutorialCollectibles.Length)
                tutorialCollectibles[currentTutorialIndex].gameObject.SetActive(true);
            else
                nextPhaseButton.SetActive(true);  // Wait for user to start study
        }
        else
        {
  
            currentStudyIndex++;
            UpdateCollectiblesText();

            if (currentStudyIndex < studyCollectibles.Length)
                studyCollectibles[currentStudyIndex].gameObject.SetActive(true);
            else
                EndExperiment();
        }
    }

    public void StartStudy()
    {
        inTutorial = false;
        nextPhaseButton.SetActive(false);
        currentStudyIndex = 0;
        studyCollectibles[0].gameObject.SetActive(true);
        startTime = Time.time;
    }

    private void EndExperiment()
    {
        endTime = Time.time;
        float duration = endTime - startTime;
        victoryText.gameObject.SetActive(true);
        victoryText.text = $"Study complete!  Time: {duration:F2} seconds";
        startMenu.SetActive(true);
        ResetCollectibles();
    }

     private void UpdateCollectiblesText()
    {   if (inTutorial)
                collectiblesText.text = $"Collectibles left: {tutorialCollectibles.Length-currentTutorialIndex}";
        else
                collectiblesText.text = $"Collectibles left: {studyCollectibles.Length-currentStudyIndex}";

    }

    public void ResetCollectibles()
    {   
        currentTutorialIndex = 0;
        currentStudyIndex = 0;
    }

}
