using TMPro;
using UnityEngine;

public class TriggerDetection : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int powerChooser;
    public string final;
    public Object GUI;
    [SerializeField] TextMeshProUGUI collectionPopUp;
    [SerializeField] GameObject power;

    private void Start()
    {
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered by " + other.gameObject.name);

        powerChooser = Random.Range(1, 4);
        Debug.Log(powerChooser);

        string healthPassive = "health passive buff";
        string damagePassive = "damage passive buff";
        string speedPassive = "speed passive buff";

        if (powerChooser == 1)
        {
            Debug.Log(healthPassive);
            final = healthPassive;
        }
        else if (powerChooser == 2)
        {
            Debug.Log(damagePassive);
            final = damagePassive;
        }
        else
        {
            Debug.Log(speedPassive);
            final = speedPassive;
        }
        collectionPopUp.text = final;
        power.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
