using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CharacterStats))]
public class HealthUI : MonoBehaviour
{
    public GameObject uiPrefab;
    public Transform target;
    public RaycastHit raycastHit;
    //Transform cam;
    //float visibleTime = 5;
    //float lastMadeVisibleTime;

    Transform ui;
    Image healthSlider;
    
    public bool hoveredOver = false;
    GameObject player;
    GameObject enemy;
    
    void Start()
    {
        enemy = GameObject.FindWithTag("Enemy");
        player = GameObject.FindWithTag("Player");
        //Debug.Log(player.transform); //.position
        
        //cam = Camera.main.transform;
        foreach(Canvas c in FindObjectsOfType<Canvas>())
        {
            if(c.renderMode == RenderMode.ScreenSpaceOverlay)
            {
                ui = Instantiate(uiPrefab, c.transform).transform;
                healthSlider = ui.GetChild(0).GetComponent<Image>();
                //ui.gameObject.SetActive(false);
                break;
            }
        }
        GetComponent<CharacterStats>().OnHealthChanged += OnHealthChanged;
    }

    void OnHealthChanged(int maxHealth, int currentHealth)
    {
        if(ui != null)
        {
            ui.gameObject.SetActive(true);
            //lastMadeVisibleTime = Time.time;
            float healthPercent = (float)currentHealth / maxHealth;
            healthSlider.fillAmount = healthPercent;
            if (currentHealth <= 0)
            {
                Destroy(ui.gameObject);
            }
        }
    }
    /* void LateUpdate()
    { //if(ui != null)
      //{
      //ui.position = target.position;
      //ui.forward = -cam.forward;
      //}
      //if (Time.time - lastMadeVisibleTime > visibleTime)
      //{
      //ui.gameObject.SetActive(false);
      //}
    }*/
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit))
        {
            target = raycastHit.transform;

            if(hoveredOver == false)
            {
                enemy.GetComponent<HealthUI>().ui.GetComponent<Image>().enabled = false;
                enemy.GetComponent<HealthUI>().ui.GetChild(0).GetComponent<Image>().enabled = false;
                player.GetComponent<HealthUI>().ui.GetComponent<Image>().enabled = true;
                player.GetComponent<HealthUI>().ui.GetChild(0).GetComponent<Image>().enabled = true;
            }
                if (target.CompareTag("Enemy"))
                {
                    hoveredOver = true;
                    if(hoveredOver == true)
                    {
                    target.gameObject.GetComponent<HealthUI>().ui.GetChild(0).GetComponent<Image>().enabled = true;
                    target.gameObject.GetComponent<HealthUI>().ui.GetComponent<Image>().enabled = true;
                }
            }
            else
            {
                hoveredOver = false;
            }

            if (hoveredOver == false)
            {
                OnMouseExit();
                void OnMouseExit()
                {

                }
            }
            if (target.CompareTag("Player"))
            {
                
                hoveredOver= false;
            }    
        }
    }
    
}
