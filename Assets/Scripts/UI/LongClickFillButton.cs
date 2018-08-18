using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace UI {
    [RequireComponent(typeof(Image))]
    public class LongClickFillButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler{

        public float longClickTime;
        public UnityEvent onLongClick;

        public Image fillImage;

        private float clickTime;
        private bool isPressing;

        private void Update() {
            if (isPressing) {
                clickTime += Time.deltaTime;
                if (clickTime >= longClickTime) {
                    onLongClick.Invoke();
                    clickTime = 0f;
                    isPressing = false;
                }
                fillImage.fillAmount = clickTime / longClickTime;
            }
            else {
                fillImage.fillAmount = 0f;
            }

        }


        public void OnPointerDown(PointerEventData eventData) {
            isPressing = true;
            clickTime = 0f;
        }

        public void OnPointerUp(PointerEventData eventData) {
            isPressing = false;
            clickTime = 0f;
        }

    }
}
