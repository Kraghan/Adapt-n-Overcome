using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockButtonWithField : MonoBehaviour
{
    [SerializeField]
    private InputField m_linkedField;
    private Button m_buttonToLock;

	// Use this for initialization
	void Start ()
    {
        m_buttonToLock = GetComponent<Button>();
        m_buttonToLock.interactable = false;
    }
	
	public void CheckButton()
    {
        if (m_linkedField.text != "")
            m_buttonToLock.interactable = true;
        else
            m_buttonToLock.interactable = false;
    }
}
