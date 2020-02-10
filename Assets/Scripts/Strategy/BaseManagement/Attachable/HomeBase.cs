using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeBase : MonoBehaviour
{
    public BaseManager bm;

    public Canvas canvasObject;
    public int tier;

    public Text tierDisplayed;

    Renderer renderer;

    private void Awake()
    {
        renderer = gameObject.GetComponent<Renderer>();
    }

    void Start()
    {
        tier = 1;
        renderer.material.color = Color.gray;
        tierDisplayed.text = tier.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        Debug.Log("Enter");

        renderer.material.color = Color.yellow;

        if (Input.GetMouseButtonDown(0))
        {
            canvasObject.gameObject.SetActive(true);
            Debug.Log("I was clicked.");
            bm.GetComponent<BaseManager>().SetAllCanvasInactive();
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            UpgradeTier();
        }
    }

    private void OnMouseExit()
    {
        Debug.Log("Exit");
        renderer.material.color = Color.grey;
    }

    private void UpgradeTier()
    {
        Mathf.Clamp(++tier, 1, 3);
    }

    public void ExitCanvas()
    {
        canvasObject.gameObject.SetActive(false);
    }
}
