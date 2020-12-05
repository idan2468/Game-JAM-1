using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLoader : MonoBehaviour
{
    [System.Serializable]
    public struct Menu
    {
        public string name;
        public GameObject menuObject;
    }
    [SerializeField] Menu[] menus;
    GameObject activeMenu;
    Dictionary<string, GameObject> mapNameToMenu;
    // Start is called before the first frame update
    void Start()
    {
        activeMenu = menus[0].menuObject;
        mapNameToMenu = new Dictionary<string, GameObject>();
        foreach (var menu in menus)
        {
            mapNameToMenu.Add(menu.name, menu.menuObject);
        }
    }

    public void moveToMenu(string name)
    {
        activeMenu.SetActive(false);
        mapNameToMenu[name].SetActive(true);
        activeMenu = mapNameToMenu[name];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
