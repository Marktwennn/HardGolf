using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallController : MonoBehaviour
{
    public float maxPower = 100f; // ������������ ���� �����
    public float minPower = 0f;   // ����������� ���� �����
    public float powerMultiplier = 1f; // ��������� ���� �����
    public float stopThreshold = 0.05f; // ����� �������� ��� ����������� ��������� ����

    private Rigidbody rb;
    private Vector3 hitDirection;
    private float currentPower = 0f;
    private bool isAiming = false;

    public LineRenderer lineRenderer;

    public Attempts attempts;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lineRenderer.enabled = false; // ����� ������������ ���������� �� ������������
    }

    void Update()
    {
        // �������� �� ��������� ����
        if (rb.velocity.magnitude <= stopThreshold)
        {
            // ��������� ������� ����� ������ ���� ��� ������������ � �����
            if (Input.GetMouseButtonDown(0))
            {
                isAiming = true;
            }
            else if (Input.GetMouseButtonUp(0) && isAiming)
            {
                isAiming = false;
                Shoot();
                attempts.DecreaseAttempts();
            }
        }

        // ����������� ����� ������������ (��� �������)
        if (isAiming)
        {
            Aim();
        }
    }

    void Aim()
    {
        // ���������� ����� ������������ � ������� LineRenderer
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            hitDirection = (hit.point - transform.position).normalized;
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, hit.point);

            // ���������� ���� ����� �� ������ ����� �����
            float lineLength = Vector3.Distance(transform.position, hit.point);
            currentPower = Mathf.Lerp(minPower, maxPower, lineLength / lineRenderer.GetPosition(1).magnitude) * powerMultiplier;
        }
    }

    public void Shoot()
    {
        // ��������� ���� ����� � ����
        rb.AddForce(hitDirection * currentPower, ForceMode.Impulse);

        // ��������� ����� ������������
        lineRenderer.enabled = false;
    }
}

