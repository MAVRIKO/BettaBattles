using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    PlayerControls controls;

    public float mSpeed = 1f;
    public float dSpeed = 2f;
    float cSpeed = 1f;
    Vector2 movement;

    public float attackRange = 0.5f;
    public float attackDistance = 0.5f;
    public float attackPointResponciveness = 1f;
    public LayerMask enemies;
    public LayerMask eFins;
    Vector2 previouseAP;
    Vector2 attackPoint;
    Vector2 previousInput;


    private void Awake()
    {
        // input setup
        controls = new PlayerControls();

        controls.Player.Move.performed += ctx => movement = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => movement = Vector2.zero;

        controls.Player.Dash.performed += ctx => Dash(dSpeed);
        controls.Player.Dash.canceled += ctx => Dash(mSpeed);

        controls.Player.Attack.performed += ctx => Attack();

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // faces attackpoint in direction of input
        previouseAP = new Vector2(Mathf.Lerp(previousInput.x, movement.x, attackPointResponciveness), Mathf.Lerp(previousInput.y, movement.y, attackPointResponciveness));
        attackPoint = new Vector2(transform.position.x, transform.position.y) + previouseAP * attackDistance;
        previousInput = previouseAP;

        Move();
    }
    void Attack()
    {
        Debug.Log("attacked!");

        //grabs colliders of hit ditected enemies and their fins
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint, attackRange, enemies);
        Collider2D[] hitFins = Physics2D.OverlapCircleAll(attackPoint, attackRange, eFins);

        //deals stress damage to enemies
        foreach(Collider2D enemy in hitEnemies)
        {
            if (enemy.GetComponent<Stress>() == null)
            {
                Debug.Log("No Stress Component in " + enemy.name);
            }
            enemy.GetComponent<Stress>().stress = enemy.GetComponent<Stress>().stress - 12.5f;
            Debug.Log("Enemy stress " + enemy.GetComponent<Stress>().stress);
        }

        //deals damage to fins of enemies
        foreach(Collider2D fins in hitFins)
        {
            if (fins.GetComponentInParent<Stress>() == null)
            {
                Debug.Log("No Stress Component in parent of " + fins.name);
            }
            fins.GetComponentInParent<Stress>().finHealth = fins.GetComponentInParent<Stress>().finHealth - 25f;
            Debug.Log("Enemy fin health " + fins.GetComponentInParent<Stress>().finHealth);
        }
    }

    //sets current speed depending on if dash button is held or not
    void Dash(float speed)
    {
        cSpeed = speed;
    }

    //controls movement direction and speed
    void Move()
    {
        if(gameObject.GetComponent<Stress>() == null)
        {
            Debug.Log("No Stress Component");
            return;
        }
        Vector2 m = new Vector2(movement.x, movement.y) * Time.deltaTime * cSpeed * gameObject.GetComponent<Stress>().fSMultiplier;
        transform.Translate(m, Space.World);
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint, attackRange);
    }
    private void OnEnable()
    {
        controls.Player.Enable();
    }
    private void OnDisable()
    {
        controls.Player.Disable();
    }
}
