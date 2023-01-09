using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Rigidbody playerRigidbody;

    public GameObject blastPrefab;
    public GameObject[] weaponPosition;

    // Cooldown del disparo
    private bool shootTrigger = true;
    private float shootCooldown = 0.05f;
    private bool sequence = true;

    [Header("Physics")]
    public float thrust = 100f;
    public Vector3 turnTorque = new Vector3(25f, 10f, 42f);
    public float forceMult = 100f;

    [Header("Input")]
    [SerializeField] [Range(-1f, 1f)] private float pitch = 0f;
    [SerializeField] [Range(-1f, 1f)] private float yaw = 0f;
    [SerializeField] [Range(-1f, 1f)] private float roll = 0f;

    private float horizontalInput, verticalInput;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        /*
        Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);

        Physics.Raycast(rayOrigin, out RaycastHit hitData);

        if(hitData.collider != null)
        {
            Vector3 direction = hitData.point - transform.position;
            transform.rotation = Quaternion.LookRotation(direction);
        }
        */

        horizontalInput = Input.GetAxis("Mouse X");
        verticalInput = -Input.GetAxis("Mouse Y");

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        pitch = verticalInput;
        roll = horizontalInput;

        yaw = 0;

        if (Input.GetKey(KeyCode.Q))
        {
            yaw = -1f;
        }

        if (Input.GetKey(KeyCode.E))
        {
            yaw = 1f;
        }

        playerRigidbody.AddRelativeForce(Vector3.forward * thrust * forceMult, ForceMode.Force);
        playerRigidbody.AddRelativeTorque(new Vector3(turnTorque.x * pitch, turnTorque.y * yaw, -turnTorque.z * roll) * forceMult, ForceMode.Force);

        // Si pulsas la tecla Espacio o Click Izquierdo y shootTrigger es True
        if (Input.GetKey(KeyCode.Space) && shootTrigger)
        {
            // Dispara
            WeaponShoot();
        }

    }

    private void WeaponShoot()
    {
        if(sequence)
        {
            Instantiate(blastPrefab, weaponPosition[0].transform.position, weaponPosition[0].transform.rotation);
            sequence = false;

        } else
        {
            Instantiate(blastPrefab, weaponPosition[1].transform.position, weaponPosition[1].transform.rotation);
            sequence = true;
        }

        StartCoroutine(WeaponCooldown());
    }

    private IEnumerator WeaponCooldown()
    {
        shootTrigger = false;
        yield return new WaitForSeconds(shootCooldown);
        shootTrigger = true;
    }
}
