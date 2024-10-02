using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePower : MonoBehaviour
{
    public FaceRotator frontFaceRotator;
    public FaceRotator1 secondFaceRotator;
    public FaceRotator2 thirdFaceRotator;
    public FaceRotator3 fourthFaceRotator;

    public GMTeste gameManager;  // Reference to your game manager script (GMTeste)
    public GlobalPowerLimit globalPowerLimit;  // Reference to GlobalPowerLimit script

    // Method for rotating the front face
    public void ActivateFrontFacePower()
    {
        ActivateFaceRotationPower(1);  // Face index 1 for the front face
    }

    // Method for rotating the second face
    public void ActivateSecondFacePower()
    {
        ActivateFaceRotationPower(2);  // Face index 2 for the second face
    }

    // Method for rotating the third face
    public void ActivateThirdFacePower()
    {
        ActivateFaceRotationPower(3);  // Face index 3 for the third face
    }

    // Method for rotating the fourth face
    public void ActivateFourthFacePower()
    {
        ActivateFaceRotationPower(4);  // Face index 4 for the fourth face
    }

    // Function to activate the rotation power based on the current turn and face
    private void ActivateFaceRotationPower(int faceIndex)
    {
        // Check if the current player can still use their power
        if (!globalPowerLimit.CanUsePower(gameManager.currentTurn))
        {
            Debug.Log("Power limit reached for " + gameManager.currentTurn.ToString());
            globalPowerLimit.DisplayWarning();
            return;  // Exit if the player has reached their power limit
        }

        // Rotate the face for the current player
        RotateSelectedFace(faceIndex);

        // Mark the power as used for the current player
        globalPowerLimit.UsePower(gameManager.currentTurn);
    }

    // Helper function to rotate the selected face
    private void RotateSelectedFace(int faceIndex)
    {
        switch (faceIndex)
        {
            case 1:
                frontFaceRotator.RotateFace(Vector3.forward * 90f);
                break;
            case 2:
                secondFaceRotator.RotateFace(Vector3.forward * 90f);
                break;
            case 3:
                thirdFaceRotator.RotateFace(Vector3.forward * 90f);
                break;
            case 4:
                fourthFaceRotator.RotateFace(Vector3.forward * 90f);
                break;
            default:
                Debug.Log("Invalid face index.");
                break;
        }
    }
}
