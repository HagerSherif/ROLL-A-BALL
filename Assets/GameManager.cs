using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject startMenu;
    public TextController controller;
    public GameObject handModels;

    public KeyboardController keyboardController;
    public LeftHandController leftHandController;
    public RightHandController rightHandController;

    private Vector3 startPos;
    private MonoBehaviour activeController;

    void Start()
    {
        startPos = player.transform.position;
        startMenu.SetActive(true);

        DisableAllControllers();
    }

    private void DisableAllControllers()
    {
        keyboardController.enabled = false;
        leftHandController.enabled = false;
        rightHandController.enabled = false;
    }

    private void StartGame(MonoBehaviour controllerToEnable)
    {
        startMenu.SetActive(false);
        controller.ResetCollectibles();

        player.transform.position = startPos;
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;

        DisableAllControllers();
        controller.StartTutorial();
        controllerToEnable.enabled = true;
        activeController = controllerToEnable;
    }

    public void StartKeyboardMode() {
          handModels.SetActive(false); 
          StartGame(keyboardController);

    }
    public void StartLeftHandMode() 
     {
          handModels.SetActive(true); 
          StartGame(leftHandController); 

    }
    public void StartRightHandMode() 
        {
          handModels.SetActive(true); 
          StartGame(rightHandController); 
    }
}
