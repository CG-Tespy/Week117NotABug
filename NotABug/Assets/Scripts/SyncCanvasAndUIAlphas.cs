using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// When an object's Canvas Group alpha changes, any alpha values on other UI components
/// (like Images) are made to be the same.
/// </summary>
public class SyncCanvasAndUIAlphas : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] GameObject hasUIElements;
    Graphic[] uiElements;

    // Update is called once per frame
    void Update()
    {
        
    }

    void SyncAlphas()
    {

    }
}
