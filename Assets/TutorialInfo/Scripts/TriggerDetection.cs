using UnityEngine;

public class TriggerDetection : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Start()
    {
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered by " + other.gameObject.name);

        int powerChooser = Random.Range(1, 4);
        Debug.Log(powerChooser);

        string healthPassive = "health passive buff";
        string damagePassive = "damage passive buff";
        string speedPassive = "speed passive buff";

        if (powerChooser == 1)
        {
            Debug.Log(healthPassive);
        }
        else if (powerChooser == 2)
        {
            Debug.Log(damagePassive);
        }
        else
        {
            Debug.Log(speedPassive);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
