using TMPro;
using UnityEngine;

public class StatsUI : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI text;
    
    public void Start() {
        GameManager.instance.OnChangeLives += ChangeLives;
    }
    
    private void ChangeLives(int lives) {
        text.text = lives.ToString();
    }

    private void OnDestroy() {
        GameManager.instance.OnChangeLives -= ChangeLives;
    }
}
