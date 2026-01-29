using UnityEngine;

public class MagnetController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 15f;
    public float jumpForce = 8f;
    
    [Header("Magnetic Settings")]
    public float magnetForce = 200f;
    public enum Polarity { Positive, Negative, Neutral }
    public Polarity currentPolarity = Polarity.Neutral;

    [Header("Visual Feedback")]
    public Material posMaterial;
    public Material negMaterial;
    public Material neutralMaterial;
    public LineRenderer lineRenderer;

    [Header("Advanced Mechanics")]
    public float slingshotBoost = 1.5f; // Fırlatma katsayısı

    private Rigidbody rb;
    private Renderer rend;
    private Vector3 moveInput;
    

    void Start() {
        rb = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();
        if (lineRenderer == null) lineRenderer = GetComponent<LineRenderer>();
        UpdateVisuals();
    }

    void Update() {
        // --- Giriş Kontrolleri ---
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        moveInput = new Vector3(x, 0, z).normalized;

        // Kutup Değiştirme
        if (Input.GetKeyDown(KeyCode.Q)) { currentPolarity = Polarity.Positive; UpdateVisuals(); }
        if (Input.GetKeyDown(KeyCode.E)) { currentPolarity = Polarity.Negative; UpdateVisuals(); }
        if (Input.GetKeyDown(KeyCode.Space) && currentPolarity != Polarity.Neutral) { currentPolarity = Polarity.Neutral; UpdateVisuals(); }

        // Zıplama (Sadece yerdeyken)
        if (Input.GetKeyDown(KeyCode.LeftShift) && IsGrounded()) {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.Space) && currentPolarity != Polarity.Neutral) {
            // Eğer bir yere çekiliyorsak ve hızımız yüksekse, ekstra bir itiş ver
            if (rb.linearVelocity.magnitude > 5f) {
                rb.AddForce(rb.linearVelocity.normalized * slingshotBoost, ForceMode.Impulse);
            }
    
            currentPolarity = Polarity.Neutral; 
                UpdateVisuals(); 
        }
    }

    void FixedUpdate() {
        // Hareket Uygula
        if (moveInput.magnitude >= 0.1f) {
            rb.AddForce(moveInput * moveSpeed, ForceMode.Force);
        }

        // Manyetik Etki
        HandleMagnets();
    }

    void HandleMagnets() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 15f);
        bool interacting = false;

        foreach (var hit in colliders) {
            if (hit.CompareTag("Positive") || hit.CompareTag("Negative")) {
                Polarity targetPol = hit.CompareTag("Positive") ? Polarity.Positive : Polarity.Negative;
                ApplyMagneticForce(hit.transform.position, targetPol);
                
                // Çizgiyi en yakın objeye bağla
                if (lineRenderer != null) {
                    lineRenderer.enabled = true;
                    lineRenderer.SetPosition(0, transform.position);
                    lineRenderer.SetPosition(1, hit.transform.position);
                }
                interacting = true;
                break; 
            }
        }

        if (!interacting && lineRenderer != null) lineRenderer.enabled = false;
    }

    void ApplyMagneticForce(Vector3 targetPos, Polarity targetPolarity) {
        Vector3 direction = targetPos - transform.position;
        float distance = direction.magnitude;
        if (distance < 0.5f || currentPolarity == Polarity.Neutral) return;

        float strength = magnetForce / Mathf.Max(distance, 1f);
        if (currentPolarity == targetPolarity) {
            rb.AddForce(-direction.normalized * strength, ForceMode.Acceleration);
        } else {
            rb.AddForce(direction.normalized * strength, ForceMode.Acceleration);
        }
    }

    void UpdateVisuals() {
        if (currentPolarity == Polarity.Positive) rend.material = posMaterial;
        else if (currentPolarity == Polarity.Negative) rend.material = negMaterial;
        else rend.material = neutralMaterial;
    }

    bool IsGrounded() {
        return Physics.Raycast(transform.position, Vector3.down, 1.2f);
    }
} 

//