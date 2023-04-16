namespace Extension.Transform
{
    using UnityEngine;

    public static class TransformExtension
    {
        public static void LookAtLerp(this Transform self, Transform target, float t)
        {
            Vector3 relativePos = target.position - self.position;
            Quaternion toRotation = Quaternion.LookRotation(relativePos);
            self.rotation = Quaternion.Lerp(self.rotation, toRotation, t);
        }

        public static bool IsPathClear(this Transform source, Transform target, LayerMask mask, float maxDistance = Mathf.Infinity)
        {
            Vector3 direction = target.position - source.position;
            bool isClear = false;

            RaycastHit hit;
            if (Physics.Raycast(source.position, direction, out hit, maxDistance, mask))
            {
                isClear = (hit.transform == target);
            }
            else
            {
                isClear = true;
            }

            return isClear;
        }
    }
}