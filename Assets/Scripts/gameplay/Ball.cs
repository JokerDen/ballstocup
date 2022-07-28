using UnityEngine;

namespace gameplay
{
    /**
     * Should be more abstract name but for test task OK
     */
    public class Ball : MonoBehaviour
    {
        [SerializeField] bool overwritePhysicMaterial;
        [SerializeField] float bounce;
        [SerializeField] float friction;

        private void Start()
        {
            if (!overwritePhysicMaterial) return;
            
            var cols = GetComponentsInChildren<Collider>();
            var mat = new PhysicMaterial();
            mat.bounciness = bounce;
            mat.dynamicFriction = friction;
            mat.staticFriction = friction;
            
            foreach (var col in cols)
                col.material = mat;
        }
    }
}