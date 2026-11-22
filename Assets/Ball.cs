using UnityEngine;

public class Ball : MonoBehaviour
{
    public Transform target;              
    public ParticleSystem hitParticle;  
    public float startDelay = 2.0f;     
    public float flyTime = 1.0f;       
    public float destroyDelay = 1.5f;    

    Rigidbody rb;
    bool hit = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Invoke(nameof(Throw), startDelay);  
    }

    public void Throw()
    {
        transform.SetParent(null);
        rb.isKinematic = false;

        Vector3 start = transform.position;
        Vector3 end = target.position;
        Vector3 g = Physics.gravity;
        Vector3 v = (end - start - 0.5f * g * flyTime * flyTime) / flyTime;
        rb.linearVelocity = v;
    }

    void OnCollisionEnter(Collision c)
    {
        if (hit) return;                   

        if (c.gameObject.transform == target || c.transform.IsChildOf(target))
        {
            hit = true;

            if (hitParticle != null)
            {
                hitParticle.transform.position = c.contacts[0].point; 
                hitParticle.Play();
            }

            Destroy(target.gameObject, destroyDelay);
        }
    }
}