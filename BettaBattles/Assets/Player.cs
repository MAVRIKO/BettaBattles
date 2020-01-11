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

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;


    Vector2 movement;
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
        Debug.Log("attacked!");

        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider enemy in hitEnemies)
        {
            Debug.Log("Hit " + enemy.name);
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
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
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
