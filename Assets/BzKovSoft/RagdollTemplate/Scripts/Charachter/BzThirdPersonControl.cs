using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace BzKovSoft.RagdollTemplate.Scripts.Charachter
{
    [RequireComponent(typeof(IBzThirdPerson))]
    public sealed class BzThirdPersonControl : MonoBehaviour
    {
        private IBzThirdPerson _character;
        private IBzRagdoll _ragdoll;
        private IBzDamageable _health;
        public Transform _camTransform;
        private bool _jumpPressed;
        private bool _fire;
        private bool _crouch;

        private void Start()
        {
            if (Camera.main == null)
                Debug.LogError("Error: no main camera found.");
            else
                _camTransform = Camera.main.transform;

            _character = GetComponent<IBzThirdPerson>();
            _health = GetComponent<IBzDamageable>();
            _ragdoll = GetComponent<IBzRagdoll>();
            StartCoroutine(MoveDrunkGuyRandomly());
        }

        void Update()
        {
            // read user input: jump, fire and crouch

            if (!_jumpPressed)
                _jumpPressed = Input.GetButtonDown("Jump");
            if (!_fire)
                _fire = Input.GetMouseButtonDown(0);

            _crouch = Input.GetKey(KeyCode.C);
        }

        public Slider Slider;
        private float Xvalue, Yvalue = 0;

        public float TakeUserControls()
        {
            return Slider.value;
        }

        IEnumerator MoveDrunkGuyRandomly()
        {
            yield return new WaitForSeconds(1f);
            int smallvalue = Random.Range(-8, 8);
            // Debug.Log("smallvalue = " + smallvalue);
            Xvalue = (float) smallvalue / 10;
            StartCoroutine(MoveDrunkGuyRandomly());
            // Debug.Log("value = " + value);
            // Debug.Log("Coroutine");
        }

        public bool Trigger1 = false, Trigger2 = false, Trigger3 = false, Trigger4 = false;

        public void MoveDrunkGuyOnYAxis()
        {
            // yield return new WaitForSeconds(1f);
            if (Trigger1)
            {
                Yvalue = 20;
            }

            else if (Trigger2)
            {
                Yvalue = 55;
            }

            else if (Trigger3)
            {
                Yvalue = 35;
            }

            else if (Trigger4)
            {
                Yvalue = 0;
            }
        }

        private void FixedUpdate()
        {
            // read user input: movement
            float h = TakeUserControls(); //Input.GetAxis("Horizontal");
            float v = 0.25f; //Input.GetAxis("Vertical");

            // calculate move direction and magnitude to pass to character
            Vector3
                camForward = new Vector3(Xvalue, 0, _camTransform.forward.z).normalized;


            // Debug.Log("Value =" + value);
            Vector3 move = v * camForward + h * _camTransform.right;
            if (move.magnitude > 1)
                move.Normalize();

            ProcessDamage();

            // pass all parameters to the character control script
            _character.Move(move, _crouch, _jumpPressed);
            _jumpPressed = false;
            MoveDrunkGuyOnYAxis();
            // if ragdolled, add a little move
            if (_ragdoll != null && _ragdoll.IsRagdolled)
                _ragdoll.AddExtraMove(move * 50 * Time.deltaTime);
        }

        /// <summary>
        /// if health script attached, shot the character
        /// </summary>
        private void ProcessDamage()
        {
            if (_health == null)
                return;

            if (_fire)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                _health.Shot(ray, 0.40f, 10000f);
                _fire = false;
            }

            if (_jumpPressed && _health.IsDead())
            {
                _health.Health = 1f;
                _jumpPressed = false;
            }
        }
        //
        // public BzRagdoll BzRagdoll;
        //
        // void OnCollisionEnter(Collider collision)
        // {
        //     Debug.Log("DamageObject  1  ");
        //     if (collision.gameObject.tag == "DamageObject")
        //     {
        //         Debug.Log("DamageObject  2  ");
        //         BzRagdoll.RagdollIn();
        //     }
        //
        // }
    }
}