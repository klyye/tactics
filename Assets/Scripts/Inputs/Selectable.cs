using UnityEngine;
using gm = GameManager;
using tm = TurnManager;
using im = InputManager;

/// <summary>
///     Something you can click on which tells the InputManager that it was selected.
/// </summary>
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Actor))]
public class Selectable : MonoBehaviour
{
    public Actor actor { get; private set; }

    // Start is called before the first frame update
    private void Start()
    {
        actor = GetComponent<Actor>();
    }
    
    private void OnMouseUpAsButton()
    {
        im.Select(this);
    }
}