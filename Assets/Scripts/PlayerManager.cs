using UnityEngine;
using tm = TurnManager;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private string[] playerNames;

    // Start is called before the first frame update
    private void Start()
    {
        for (var i = 0; i < playerNames.Length; i++)
        {
            var p = new Player(playerNames[i]);
            // todo: placeholder stuff
            var lightman = GameObject.Find("Light Elemental").GetComponent<Selectable>();
            var darkman = GameObject.Find("Dark Elemental").GetComponent<Selectable>();
            var earthman = GameObject.Find("Earth Elemental").GetComponent<Selectable>();
            var plantman = GameObject.Find("Plant Elemental").GetComponent<Selectable>();
            p.units.Add(i % 2 == 0 ? lightman : darkman);
            p.units.Add(i % 2 == 0 ? earthman : plantman);
            tm.AddPlayer(p, i);
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }
}