
using UnityEngine;


public class Ball : MonoBehaviour
{
    //Config Parameters
    [SerializeField] Paddle paddle1;
    [SerializeField] float pushX;
    [SerializeField] float pushY;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomFactor = 0.2f;

    //state
    Vector2 paddleToBallVector;
    bool hasStarted = false;


    // Cached Component References
    AudioSource myAudioSource;
    Rigidbody2D myRigidBody2D;


    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = transform.position - paddle1.transform.position;
        myAudioSource = GetComponent<AudioSource>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    // ! means not
    void Update()
    {
        if(!hasStarted)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }
    }


    private void LaunchOnMouseClick()
    {
        if(Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
            myRigidBody2D.velocity = new Vector2(pushX, pushY);
        }
    }


    private void LockBallToPaddle()
    {
        Vector2 paddlePosition = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePosition + paddleToBallVector;
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityTweak = new Vector2
            (Random.Range(0f, randomFactor), // x value
            Random.Range(0f, randomFactor));// y value

    
        if(hasStarted)
        {
            AudioClip clip = ballSounds[Random.Range(0, ballSounds.Length)];
            myAudioSource.PlayOneShot(clip); //PlayOneshot means to play the audio clip all the way through without being interrupted
            //myRigidBody2D.velocity += velocityTweak; // adds random factor of velocityTweak and velocity of ball once the game has started 
        }
    }
}
