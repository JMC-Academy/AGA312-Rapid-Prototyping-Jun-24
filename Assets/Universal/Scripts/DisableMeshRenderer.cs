using UnityEngine;

public class DisableMeshRenderer : MonoBehaviour
{
    private void Awake()
    {
        if(GetComponent<MeshRenderer>() != null)
            GetComponent<MeshRenderer>().enabled = false;
    }
}
