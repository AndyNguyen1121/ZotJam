using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
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
    IEnumerator UnlockCursor()
    {
        yield return new WaitForSeconds(1f);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    void OnEnable()
    {
        Item[] possibleItems = (PlayerManager.instance.currentLocation == PlayerLocation.Overworld) ? possibleOverworldItems : possibleHellItems;
        StartCoroutine(UnlockCursor());
        // Yes I know this is terribly programmed
        choices[0] = possibleItems[Random.Range(0, possibleItems.Length)];
        while (true)
        {
            choices[1] = possibleItems[Random.Range(0, possibleItems.Length)];
            WeaponEquip weapon = choices[1].GetComponent<WeaponEquip>();
            if (weapon == null)
            {
                break;
            }
            else if(weapon.prefab.name != PlayerManager.instance.currentWeapon.name)
            {
                break;
                
            }
            if(choices[1] != choices[0])
            {
                break;
            }
        }
        
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
