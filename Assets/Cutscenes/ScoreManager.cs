using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI text;
    private void OnEnable()
    {
        WaveCounter.instance.myTMPText.enabled = false;
        text.text = "You Survived " + enemySpawner.Instance.waveCounter + " Waves";
    }
}
