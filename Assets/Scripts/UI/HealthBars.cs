using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using gm = GameManager;

public class HealthBars : MonoBehaviour
{
    /// <summary>
    ///     Maps each defender to the health bar that is supposed to follow them.
    /// </summary>
    private IDictionary<Defender, Slider> _sliders;

    [SerializeField] private Slider _healthBarPrefab;

    /// <summary>
    ///     The positional offset of the health bar from the center of the defender it follows.
    /// </summary>
    [SerializeField] private Vector3 _offset;

    /// <summary>
    ///     There should only be one instance of HealthBars. This is that instance.
    /// </summary>
    public static HealthBars inst;

    // Start is called before the first frame update
    void Awake()
    {
        _sliders = new Dictionary<Defender, Slider>();
        inst = this;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var pair in _sliders)
        {
            var bar = pair.Value;
            var unit = pair.Key;

            bar.value = (float) unit.hp / unit.maxHp;
            bar.transform.position = gm.cam.WorldToScreenPoint(unit.transform.position + _offset);
        }
    }

    /// <summary>
    ///     Adds a defender to the set of defenders who have health bars.
    /// </summary>
    /// <param name="def">The defender to add.</param>
    public void AddDefender(Defender def)
    {
        var bar = Instantiate(_healthBarPrefab, transform);
        _sliders.Add(def, bar);
        def.OnDeath += () =>
        {
            _sliders.Remove(def);
            Destroy(bar.gameObject);
        };
    }
}