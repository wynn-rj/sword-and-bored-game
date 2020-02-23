using UnityEngine;
using UnityEngine.UI;

public class AbilityButtonHighlight : MonoBehaviour
{
    [HideInInspector]
    public bool isSelected;
    // Start is called before the first frame update
    void Start()
    {
        isSelected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isSelected)
        {
            GetComponent<Image>().color = Color.green;
        } else
        {
            GetComponent<Image>().color = Color.white;
        }
    }
}
