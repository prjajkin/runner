using UnityEngine;

namespace Core
{
    /// <summary>
    /// Control Camera position by Player.
    /// </summary>
    public class CameraController
    {
        private const float ZDistance = 8;
        private const float YDistance = -4;

        private Transform _cameraTransform;
        private Transform _goalTransform;

        public CameraController(Transform cameraTransform, Transform goalTransform)
        {
            _cameraTransform = cameraTransform;
            _goalTransform = goalTransform;
        }

        public void ResetPosition()
        { 
            var goalPos = _goalTransform.position;
            _cameraTransform.position = new Vector3(0, goalPos.y + YDistance, goalPos.z + ZDistance);
        }
      
        public void Move()
        {
            var camPos = _cameraTransform.position;
            var goalPos = _goalTransform.position;

            var zDist = goalPos.z - camPos.z - ZDistance;
            float zStep = 0;
            if (Mathf.Abs(zDist) >0.1 )
            { 
                zStep = zDist * Time.deltaTime;
            }

            var yDist = goalPos.y - camPos.y - YDistance;
            float yStep = 0;
            if (Mathf.Abs(yDist) >0.1 )
            {
                yStep = yDist*0.5f * Time.deltaTime;
            }
             
            _cameraTransform.position = new Vector3(0, camPos.y +  yStep, camPos.z + zStep);
        }
    }
}
