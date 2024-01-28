using UnityEngine;

namespace Infrastructure
{
    public class CameraPositionService
    {
        private Transform _cameraTransform;
        private static Vector3 _battlefieldCamPos = new Vector3(18f, 17f, -10f);
        private static Vector3 _mapCamPos = new Vector3(50f, 50f, -10f);

        public CameraPositionService(MainCameraMarker cameraTransform)
        {
            _cameraTransform = cameraTransform.transform;
        }

        public void SetCamToBattlefield() => 
            _cameraTransform.position = _battlefieldCamPos;
        public void SetCamToMap() =>
            _cameraTransform.position = _mapCamPos;
    }
}