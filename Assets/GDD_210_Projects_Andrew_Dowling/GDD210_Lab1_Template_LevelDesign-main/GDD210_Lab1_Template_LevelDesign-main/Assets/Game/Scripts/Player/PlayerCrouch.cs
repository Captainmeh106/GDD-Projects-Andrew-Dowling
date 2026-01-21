using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game
{
    public class PlayerCrouch : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Transform camTransform;
        [SerializeField] private CharacterController charController;

        [Header("Player Movement")]
        [SerializeField] private float mouseSensitivity = 3f;
        [SerializeField] private float moveSpeedStand = 6f;
        [SerializeField] private float moveSpeedCrouch = 3f;
        [SerializeField] private float sprintSpeedMultiplier = 1.5f;
        [SerializeField] private float sprintRampUpSpeed = 6f;
        [SerializeField] private float gravity = -9.8f;
        [SerializeField] private float jumpSpeed = 5f;
        [SerializeField] private float yReset = -10f;
        private Vector3 startPosition;
        private Quaternion startRotation;

        [Header("Camera Movement")]
        [SerializeField] private float camStandY = 0.8f;
        [SerializeField] private float camCrouchY = 0.4f;
        [SerializeField] private float camMoveSpeed = 6f;
        [SerializeField] private float camOffsetFromTop = 0.1f;

        [Header("UI")]
        [SerializeField] private Image damageFlashImage;
        [SerializeField] private Timer timer;

        [Header("Audio - Footsteps")]
        [SerializeField] private AudioSource footstepSource;
        [SerializeField] private List<AudioClip> footstepClips = new List<AudioClip>();
        [Range(0f, 0.3f)][SerializeField] private float footstepPitchVariation = 0.05f;
        [SerializeField] private float baseStepInterval = 0.5f;   // Step interval at normal speed
        [SerializeField] private float crouchStepInterval = 0.8f; // Step interval when crouched

        [Header("Sprint System")]
        [SerializeField] private float maxSprintTime = 5f;
        [SerializeField] private float sprintRecoveryDelay = 1.5f;
        [SerializeField] private float sprintRecoveryRate = 1f;
        private float currentSprintTime;
        private float sprintCooldownTimer;
        private bool isSprinting;
        private float currentSpeed;
        private float targetSpeed;

        [Header("Exhaustion Visual Effect")]
        [SerializeField] private Image exhaustionOverlay;
        [SerializeField, Range(0f, 1f)] private float maxDarknessAlpha = 0.6f;
        [SerializeField] private float darknessFadeSpeed = 3f;

        private float camOffset;
        private float camRotation = 0f;
        private float verticalSpeed;
        private bool isCrouching;
        private bool wasCrouching;
        private bool isMoving;
        private bool wasMoving;
        private float stepTimer = 0f;
        private int lastFootstepIndex = -1;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            wasCrouching = false;
            camOffset = camStandY;
            charController.height = 2;
            charController.center = new Vector3(0, 0f, 0);
            startPosition = transform.position;
            startRotation = transform.rotation;
            currentSprintTime = maxSprintTime;
            currentSpeed = moveSpeedStand;

            if (footstepSource == null)
            {
                footstepSource = gameObject.AddComponent<AudioSource>();
                footstepSource.playOnAwake = false;
                footstepSource.loop = false;
            }
        }

        private void Update()
        {
            if (transform.position.y <= yReset) ResetPlayer();

            // --- CAMERA ---
            float mouseInputY = Input.GetAxis("Mouse Y") * mouseSensitivity;
            camRotation -= mouseInputY;
            camRotation = Mathf.Clamp(camRotation, -90f, 90f);
            camTransform.localRotation = Quaternion.Euler(camRotation, 0f, 0f);

            float mouseInputX = Input.GetAxis("Mouse X") * mouseSensitivity;
            transform.rotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0f, mouseInputX));

            // --- PUSH OBJECTS ---
            if (Input.GetMouseButton(0))
            {
                if (Physics.Raycast(camTransform.position, camTransform.forward, out RaycastHit hit, 4f))
                {
                    Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
                    if (rb)
                        rb.AddForceAtPosition(-hit.normal * 1, hit.point);
                }
            }

            // --- CROUCH ---
            isCrouching = Input.GetKey(KeyCode.LeftControl);
            charController.height = camTransform.localPosition.y + camOffsetFromTop;
            charController.center = new Vector3(0, charController.height / 2, 0);

            if (isCrouching && !wasCrouching)
            {
                wasCrouching = true;
                camOffset = camCrouchY;
            }
            else if (wasCrouching && !isCrouching)
            {
                if (!Physics.SphereCast(camTransform.position, charController.radius / 2, Vector3.up,
                        out RaycastHit hit, camStandY + camOffsetFromTop - charController.radius * 2.5f))
                {
                    wasCrouching = false;
                    camOffset = camStandY;
                }
                else
                {
                    isCrouching = true;
                }
            }

            // --- HANDLE SPRINT ---
            HandleSprint();

            // --- MOVEMENT ---
            float forwardMovement = Input.GetAxis("Vertical") * currentSpeed * Time.deltaTime;
            float sideMovement = Input.GetAxis("Horizontal") * currentSpeed * Time.deltaTime;
            Vector3 movement = (transform.forward * forwardMovement) + (transform.right * sideMovement);

            // Jump & Gravity
            if (!isCrouching && (charController.isGrounded || Physics.Raycast(transform.position, Vector3.down, 0.1f)) && Input.GetKeyDown(KeyCode.Space))
            {
                verticalSpeed = jumpSpeed;
            }
            else if (charController.isGrounded)
            {
                verticalSpeed = 0f;
            }

            verticalSpeed += gravity * (verticalSpeed > 0f ? 1f : 1.5f) * Time.deltaTime;
            movement += transform.up * verticalSpeed * Time.deltaTime;

            // --- CAMERA OFFSET ---
            camTransform.localPosition = Vector3.Lerp(
                camTransform.localPosition,
                new Vector3(camTransform.localPosition.x, camOffset, camTransform.localPosition.z),
                Time.deltaTime * camMoveSpeed);

            charController.Move(movement);

            // --- FOOTSTEPS ---
            isMoving = (Mathf.Abs(forwardMovement) > 0.01f || Mathf.Abs(sideMovement) > 0.01f) && charController.isGrounded;

            if (isMoving)
            {
                // Adjust step timing based on speed and crouch state
                float speedRatio = currentSpeed / moveSpeedStand;
                float currentStepInterval = isCrouching ? crouchStepInterval : baseStepInterval / Mathf.Max(speedRatio, 0.1f);

                stepTimer += Time.deltaTime;

                if (stepTimer >= currentStepInterval)
                {
                    PlayRandomFootstep();
                    stepTimer = 0f;
                }
            }
            else
            {
                stepTimer = 0f;
            }

            wasMoving = isMoving;

            // --- EXHAUSTION VISUAL ---
            HandleExhaustionEffect();
        }

        private void HandleSprint()
        {
            bool wantsToSprint = Input.GetKey(KeyCode.LeftShift) && !isCrouching && currentSprintTime > 0f;
            float baseSpeed = isCrouching ? moveSpeedCrouch : moveSpeedStand;
            float sprintSpeed = baseSpeed * sprintSpeedMultiplier;

            if (wantsToSprint)
            {
                isSprinting = true;
                sprintCooldownTimer = 0f;
                currentSprintTime -= Time.deltaTime;
            }
            else
            {
                if (isSprinting && currentSprintTime <= 0f)
                {
                    sprintCooldownTimer = sprintRecoveryDelay;
                }
                isSprinting = false;
            }

            if (sprintCooldownTimer > 0f)
            {
                sprintCooldownTimer -= Time.deltaTime;
            }
            else if (!isSprinting && currentSprintTime < maxSprintTime)
            {
                currentSprintTime += Time.deltaTime * sprintRecoveryRate;
            }

            targetSpeed = wantsToSprint ? sprintSpeed : baseSpeed;
            currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * sprintRampUpSpeed);
        }

        private void HandleExhaustionEffect()
        {
            if (exhaustionOverlay == null) return;

            // Scale overlay alpha based on stamina left
            float staminaPercent = currentSprintTime / maxSprintTime;
            float targetAlpha = Mathf.Lerp(maxDarknessAlpha, 0f, staminaPercent);

            Color overlayColor = exhaustionOverlay.color;
            overlayColor.a = Mathf.Lerp(overlayColor.a, targetAlpha, Time.deltaTime * darknessFadeSpeed);
            exhaustionOverlay.color = overlayColor;
        }

        private void PlayRandomFootstep()
        {
            if (footstepClips.Count == 0) return;

            int index = Random.Range(0, footstepClips.Count);
            if (footstepClips.Count > 1)
            {
                while (index == lastFootstepIndex)
                    index = Random.Range(0, footstepClips.Count);
            }
            lastFootstepIndex = index;

            footstepSource.pitch = 1f + Random.Range(-footstepPitchVariation, footstepPitchVariation);
            footstepSource.clip = footstepClips[index];
            footstepSource.PlayOneShot(footstepSource.clip);
        }

        public void ResetPlayer()
        {
            verticalSpeed = 0;
            transform.position = startPosition;
            transform.rotation = startRotation;
            damageFlashImage.color = new Color(0.8f, 0f, 0f, 0.5f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Button button = hit.collider.GetComponent<Button>();
            if (button)
                button.Press();

            Goal goal = hit.collider.GetComponent<Goal>();
            if (goal)
                timer.Stop();
        }
    }
}
