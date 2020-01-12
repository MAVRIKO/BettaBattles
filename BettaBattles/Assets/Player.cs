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
    public LayerMask enemies;
    public LayerMask eFins;
    public Vector2 attackPoint;


    private void Awake()
    {
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
        Move();
    }
    void Attack()
    {
        attackPoint = new Vector2(transform.position.x, transform.position.y) + movement * attackDistance;
        Debug.Log("attacked!");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint, attackRange, enemies);
        Collider2D[] hitFins = Physics2D.OverlapCircleAll(attackPoint, attackRange, eFins);

        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Stress>().stress = enemy.GetComponent<Stress>().stress - 12.5f;
            Debug.Log("Enemy stress " + enemy.GetComponent<Stress>().stress);
        }

        foreach(Collider2D fins in hitFins)
        {
            fins.GetComponentInParent<Stress>().finHealth = fins.GetComponentInParent<Stress>().finHealth - 25f;
            Debug.Log("Enemy fin health " + fins.GetComponentInParent<Stress>().finHealth);
        }
    }
    void Dash(float speed)
    {
        cSpeed = speed;
    }
    void Move()
    {
        Vector2 m = new Vector2(movement.x, movement.y) * Time.deltaTime * cSpeed;
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
