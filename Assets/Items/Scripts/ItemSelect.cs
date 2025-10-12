using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ItemSelect : MonoBehaviour
{
    Item[] choices = new Item[2];
    public Item[] possibleOverworldItems;
    public Item[] possibleHellItems;
    public Transform[] choice_buttons;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        

    }
    void OnEnable()
    {
        Item[] possibleItems = (PlayerManager.instance.currentLocation == PlayerLocation.Overworld) ? possibleOverworldItems : possibleHellItems;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        // Yes I know this is terribly programmed
        choices[0] = possibleItems[Random.Range(0, possibleItems.Length)];
        choices[1] = possibleItems[Random.Range(0, possibleItems.Length)];
        choice_buttons[0].GetChild(0).GetComponent<Image>().sprite = choices[0].icon;
        choice_buttons[0].GetChild(1).GetComponent<TMP_Text>().text = choices[0].name;
        choice_buttons[0].GetChild(2).GetComponent<TMP_Text>().text = choices[0].description;

        choice_buttons[1].GetChild(0).GetComponent<Image>().sprite = choices[1].icon;
        choice_buttons[1].GetChild(1).GetComponent<TMP_Text>().text = choices[1].name;
        choice_buttons[1].GetChild(2).GetComponent<TMP_Text>().text = choices[1].description;
    }


    public void ChooseItem(int n)
    {
        ItemManager.instance.AddItem(choices[n].gameObject);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameObject.active = false;
        if(PlayerManager.instance.currentLocation == PlayerLocation.Hell)
        {
            PlayerManager.instance.TransportToOverworld();
        }

        enemySpawner.Instance.NextWave();
    }
   
}
