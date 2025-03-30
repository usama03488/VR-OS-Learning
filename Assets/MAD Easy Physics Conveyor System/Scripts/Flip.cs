using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
namespace MADFerret
{
    public class Flip : MonoBehaviour
    {
        public string thisKey;
        public bool Left;
        public Quaternion from;
        public Quaternion to;
        public float speed;
        public Text text;
        public float timeCount;
        private InputAction toggleLeftAction;
        // Start is called before the first frame update
        void Start()
        {
            text.text = thisKey;
        }
        private void OnEnable()
        {
            toggleLeftAction = new InputAction(binding: "<Keyboard>/space");
            toggleLeftAction.performed += ctx => ToggleRotation();
            toggleLeftAction.Enable();
            transform.rotation = Quaternion.Lerp(from, to, timeCount * speed);
            timeCount = timeCount + Time.deltaTime;
        }

        private void OnDisable()
        {
            toggleLeftAction.Disable();
        }

        private void ToggleRotation()
        {
            Left = !Left;
            timeCount = 0;
        }
        // Update is called once per frame
        void Update()
        {

        

            if (Left == true)
            {
                transform.rotation = Quaternion.Lerp(from, to, timeCount * speed);
                timeCount = timeCount + Time.deltaTime;
            }
            if (Left == false)
            {
                transform.rotation = Quaternion.Lerp(to, from, timeCount * speed);
                timeCount = timeCount + Time.deltaTime;
            }
        }
    }
}
