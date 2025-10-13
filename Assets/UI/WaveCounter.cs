using UnityEngine;
using TMPro;
public class WaveCounter : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TMP_Text myTMPText;

    public static WaveCounter instance;
    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        myTMPText.text = "Wave: " + enemySpawner.Instance.waveCounter;
    }
}
