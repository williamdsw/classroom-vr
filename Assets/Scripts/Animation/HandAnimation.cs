using UnityEngine;
using UnityEngine.XR;
using System.Linq;

namespace Animation
{
    // https://github.com/KAPastor/XRHandAnimations/blob/main/HandAnim.cs
    public class HandAnimation : MonoBehaviour
    {
        [SerializeField] private XRNode inputSource;
        [SerializeField] private Animator animator = null;

        public const string ANIM_LAYER_NAME_POINT = "Point Layer";
        public const string ANIM_LAYER_NAME_THUMB = "Thumb Layer";
        public const string ANIM_PARAM_NAME_FLEX = "Flex";
        public const string ANIM_PARAM_NAME_POSE = "Pose";

        private int animLayerIndexThumb = -1;
        private int animLayerIndexPoint = -1;
        private int animParamIndexFlex = -1;
        private int animParamIndexPose = -1;
        private Collider[] colliders = null;

        public float animationFrames = 4f;
        private float gripState = 0f;
        private float triggerState = 0f;
        private float triggerCapState = 0f;
        private float thumbCapState = 0f;

        private void Start()
        {
            colliders = this.GetComponentsInChildren<Collider>().Where(childCollider => !childCollider.isTrigger).ToArray();
            for (int i = 0; i < colliders.Length; ++i)
            {
                Collider collider = colliders[i];
                // collider.transform.localScale = new Vector3(COLLIDER_SCALE_MIN, COLLIDER_SCALE_MIN, COLLIDER_SCALE_MIN);
                collider.enabled = true;
            }

            animLayerIndexPoint = animator.GetLayerIndex(ANIM_LAYER_NAME_POINT);
            animLayerIndexThumb = animator.GetLayerIndex(ANIM_LAYER_NAME_THUMB);
            animParamIndexFlex = Animator.StringToHash(ANIM_PARAM_NAME_FLEX);
            animParamIndexPose = Animator.StringToHash(ANIM_PARAM_NAME_POSE);
        }

        private void Update()
        {
            InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
            CaptureGrip(device);
            CaptureTrigger(device);
            CaptureIndexTouch(device);
            CaptureThumbTouch(device);
        }

        private void CaptureThumbTouch(InputDevice device)
        {
            if (device.TryGetFeatureValue(CommonUsages.thumbTouch, out float thumbCapTarget))
            {
                float thumbCapStateDelta = thumbCapTarget - thumbCapState;
                if (thumbCapStateDelta > 0f)
                {
                    thumbCapState = Mathf.Clamp(thumbCapState + 1 / animationFrames, 0f, thumbCapTarget);
                }
                else if (thumbCapStateDelta < 0f)
                {
                    thumbCapState = Mathf.Clamp(thumbCapState - 1 / animationFrames, thumbCapTarget, 1f);
                }
                else
                {
                    thumbCapState = thumbCapTarget;
                }

                animator.SetLayerWeight(animLayerIndexThumb, 1f - thumbCapState);
            }
        }

        private void CaptureIndexTouch(InputDevice device)
        {
            if (device.TryGetFeatureValue(CommonUsages.indexTouch, out float triggerCapTarget))
            {
                float triggerCapStateDelta = triggerCapTarget - triggerCapState;
                if (triggerCapStateDelta > 0f)
                {
                    triggerCapState = Mathf.Clamp(triggerCapState + 1 / animationFrames, 0f, triggerCapTarget);
                }
                else if (triggerCapStateDelta < 0f)
                {
                    triggerCapState = Mathf.Clamp(triggerCapState - 1 / animationFrames, triggerCapTarget, 1f);
                }
                else
                {
                    triggerCapState = triggerCapTarget;
                }
                animator.SetLayerWeight(animLayerIndexPoint, 1f - triggerCapState);
            }
        }

        private void CaptureTrigger(InputDevice device)
        {
            if (device.TryGetFeatureValue(CommonUsages.trigger, out float triggerTarget))
            {
                float triggerStateDelta = triggerTarget - triggerState;
                if (triggerStateDelta > 0f)
                {
                    triggerState = Mathf.Clamp(triggerState + 1 / animationFrames, 0f, triggerTarget);
                }
                else if (triggerStateDelta < 0f)
                {
                    triggerState = Mathf.Clamp(triggerState - 1 / animationFrames, triggerTarget, 1f);
                }
                else
                {
                    triggerState = triggerTarget;
                }

                animator.SetFloat("Pinch", triggerState);
            }
        }

        private void CaptureGrip(InputDevice device)
        {
            if (device.TryGetFeatureValue(CommonUsages.grip, out float gripTarget))
            {
                float gripStateDelta = gripTarget - gripState;
                if (gripStateDelta > 0f)
                {
                    gripState = Mathf.Clamp(gripState + 1 / animationFrames, 0f, gripTarget);
                }
                else if (gripStateDelta < 0f)
                {
                    gripState = Mathf.Clamp(gripState - 1 / animationFrames, gripTarget, 1f);
                }
                else
                {
                    gripState = gripTarget;
                }

                animator.SetFloat(animParamIndexFlex, gripState);
            }
        }
    }
}
