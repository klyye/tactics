using UnityEngine;

/// <summary>
///     https://www.youtube.com/watch?v=vmKxLibGrMo
/// </summary>
public class CanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject _starting;
    
    // Start is called before the first frame update
    private void Awake()
    {
        SwitchCanvas(_starting);
    }

    public void SwitchCanvas(GameObject state)
    {
        foreach (Transform controller in transform)
        {
            var child = controller.gameObject;
            child.SetActive(child == state || child == gameObject);
        }
    }
}