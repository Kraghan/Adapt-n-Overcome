using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CheckIfSaveExist : MonoBehaviour
{
    [SerializeField]
    bool m_checkAllSaves = false;
    [SerializeField]
    GameObject m_slotsMenu;

	// Use this for initialization
	void Start ()
    {
        Button button = GetComponent<Button>();
        if (!m_checkAllSaves)
        {
            SaveSlot saveSlot = GetComponent<SaveSlot>();
            button.interactable = saveSlot.IsUsed();
        }
        else
        {
            SaveSlot[] saveSlots = m_slotsMenu.GetComponentsInChildren<SaveSlot>();
            bool toUnlock = false;
            foreach (SaveSlot slot in saveSlots)
            {
                if(slot.IsUsed())
                {
                    toUnlock = true;
                    break;
                }
            }

            button.interactable = toUnlock;
        }
	}
}
