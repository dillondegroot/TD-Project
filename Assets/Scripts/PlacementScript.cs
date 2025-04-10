using JetBrains.Annotations;
using Mono.Cecil.Cil;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlacementScript : MonoBehaviour
{
    public static PlacementScript Instance;

    [SerializeField] GameObject circle;
    [SerializeField] GameObject enemySpawner;

    [Header ("Towers")]
    [SerializeField] GameObject tower1;
    [SerializeField] GameObject tower2;
    [SerializeField] GameObject tower3;
    [SerializeField] GameObject tower4;

    [Header ("Prices")]
    [SerializeField] int tower1Price;
    [SerializeField] int tower2Price;
    [SerializeField] int tower3Price;
    [SerializeField] int tower4Price;

    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text errorText;

    public int money;

    bool isPlacing;
    int currentPrice;
    float debounce;
    GameObject currentTower;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isPlacing = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveCircle();
        OnClick();
        PlacingToggle();
        MoneyHandler();
    }

    void MoveCircle()
    {
        // Move the circle to the mouse position
        Vector3 mousePos = Input.mousePosition;
        circle.transform.position = mousePos;
    }

    void MoneyHandler()
    {
        moneyText.text = "$ " + money.ToString();

        if (debounce > 0)
        {
            debounce -= Time.deltaTime;
        }
        else
        {
            errorText.text = "";
        }
    }

    void PlacingToggle()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (money >= tower1Price)
            {
                currentPrice = tower1Price;
                currentTower = tower1;
                if (isPlacing) isPlacing = false;
                else isPlacing = true;
            }
            else
            {
                errorText.text = "Not enough money!";
                debounce = 3;
                return;
            }
        } 

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (money >= tower2Price)
            {
                currentPrice = tower2Price;
                currentTower = tower2;
                if (isPlacing) isPlacing = false;
                else isPlacing = true;
            }
            else
            {
                errorText.text = "Not enough money!";
                debounce = 3;
                return;
            }
        }

        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (money >= tower3Price)
            {
                currentPrice = tower3Price;
                currentTower = tower3;
                if (isPlacing) isPlacing = false;
                else isPlacing = true;
            }
            else
            {
                errorText.text = "Not enough money!";
                debounce = 3;
                return;
            }
        }

        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (money >= tower4Price)
            {
                currentPrice = tower4Price;
                currentTower = tower4;
                if (isPlacing) isPlacing = false;
                else isPlacing = true;
            }
            else
            {
                errorText.text = "Not enough money!";
                debounce = 3;
                return;
            }
        }

        if (isPlacing)
        {
            circle.GetComponent<Image>().enabled = true;
        }

        else
        {
            circle.GetComponent<Image>().enabled = false;
        }
    }

    public void AddMoney(int amount)
    {
        money += amount;
    }

    void OnClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isPlacing)
            {
                Vector3 mousePos = Input.mousePosition;
                Ray ray = Camera.main.ScreenPointToRay(mousePos);

                Physics.Raycast(ray, out RaycastHit hit, 100);

                if (hit.transform.tag == "Ground")
                {
                    GameObject tower = Instantiate(currentTower);
                    tower.transform.position = new Vector3(hit.point.x, hit.point.y + 1.5f, hit.point.z);
                    money -= currentPrice;
                }
                else
                {
                    Debug.Log("Can't place tower on something that isn't ground!");
                }
                isPlacing = false;
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mousePos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);

            Physics.Raycast(ray, out RaycastHit hit, 100);

            if (hit.transform)
            {
                if (hit.transform.tag == "Tower") Destroy(hit.transform.gameObject);
                isPlacing = false;
            }
        }
    }
}
