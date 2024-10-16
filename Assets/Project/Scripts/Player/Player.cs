using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direct { foward = 0, back = 1, left = 2, right = 3}

public class Player : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private Animator anim;
    [SerializeField] private ParticleSystem effect;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool isMoving;
    [SerializeField] private LayerMask wallLayer;
    public Direct chooseDir; // Khai bao enum huong di
    private Vector2 startSwipePos;
    private Vector2 endSwipePos;
    private Vector2 moveDirect;
    private Vector3 moveTarget;

    [Header("Gameplay")]
    [SerializeField] private GameObject brickHolder;
    [SerializeField] private CharacterBrick brickPrefab;
    [SerializeField] private List<CharacterBrick> collectedBrick = new();
    [SerializeField] private Vector3 offset = new( 0, 0.3f, 0);
    public int collectedGem;
    public int collectedPoint;

    // Start is called before the first frame update
    void Start()
    {
        Oninit();
    }


    // Update is called once per frame
    void Update()
    {
        HandleInput();
        // Neu dich den chua duoc xac dinh, nhan vat khong di chuyen
        if(moveTarget != Vector3.zero && moveTarget != transform.position)
        {
            ControllCharacter(moveTarget);
        }
        else
        {
            isMoving = false;
            effect.Stop();
        }
    }

    #region State

    public void Oninit()
    {
        isMoving = false;
    }

    public void OnFinish()
    {
        ClearBrick();
        anim.gameObject.transform.Rotate(0, 210, 0);
        anim.SetInteger("renwu",2);
    }

    #endregion

    #region Movement Control

    public void HandleInput()
    {
        if (isMoving || LevelManager.Ins.status == GameState.Finish)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            startSwipePos = Input.mousePosition;
        }

        else if (Input.GetMouseButtonUp(0))
        {
            endSwipePos = Input.mousePosition;
            // Lay khoang cach giua hai diem va chuyen no ve Vector3 co ban
            Vector3 direcSwipe = (endSwipePos - startSwipePos).normalized;

            if (Mathf.Abs(direcSwipe.x) > Mathf.Abs(direcSwipe.y))
            {
                // Gan gia tri di chuyen vao, neu gia tri x > 0, di sang phai. Nguoc lai di sang trai
                moveDirect = new Vector2(direcSwipe.x > 0 ? 1 : -1, 0);
            }
            else if (Mathf.Abs(direcSwipe.y) > Mathf.Abs(direcSwipe.x))
            {
                // Neu gia tri z > 0, di len tren. Nguoc lai di xuong duoi
                moveDirect = new Vector2(0, direcSwipe.y > 0 ? 1 : -1);
            }
        }

        // Chuyen tu moveDir sang enum
        if(moveDirect.x != 0 && moveDirect.y == 0)
        {
            chooseDir = moveDirect.x > 0 ? Direct.right : Direct.left;
        }
        else if (moveDirect.x == 0 && moveDirect.y != 0)
        {
            chooseDir = moveDirect.y > 0 ? Direct.foward : Direct.back;
        }
        else //Khi gia tri moveDir chua cap nhat hoac bi loi, bo qua cho nguoi choi keo lai
        {
            return;
        }

        // Reset sau khi keo tha xong
        startSwipePos = Vector3.zero;
        endSwipePos = Vector3.zero;
        moveDirect = Vector2.zero;

        SearchDirection(chooseDir);
    }

    private RaycastHit IsCheckRaycast(Ray raycast)
    {
        Physics.Raycast(raycast, out RaycastHit hit, 1000f, wallLayer);
        return hit;
    }

    public void SearchDirection(Direct direct)
    {
        Ray raycast;
        RaycastHit hitInfo;
        float distance;

        isMoving = true;    // Xet bien de ngan nguoi choi dieu khien khi dang di chuyen
        effect.Play();      // Play hieu ung di chuyen

        switch (direct)
        {
            case Direct.foward:
                raycast = new Ray(transform.position, Vector3.forward);                   // Thiet lap tia raycast theo huong vuot vua duoc xac dinh
                hitInfo = IsCheckRaycast(raycast);                                          // Ban tia ray va tra ve vi tri cua wall gan nhat
                distance = Mathf.Abs(hitInfo.transform.position.z - transform.position.z);  // Tinh toan khoang cach cua nhan vat toi wall
                moveTarget = transform.position + new Vector3( 0, 0, distance - 1);         // Thiet lap dich den dua vao khoang cach, chieu ( giam di 1 ) 
                break;
            case Direct.back:
                raycast = new Ray(transform.position, Vector3.back);
                hitInfo = IsCheckRaycast(raycast);
                distance = Mathf.Abs(hitInfo.transform.position.z - transform.position.z);
                moveTarget = transform.position + new Vector3(0, 0, 1 - distance);
                break;
            case Direct.left:
                raycast = new Ray(transform.position, Vector3.left);
                hitInfo = IsCheckRaycast(raycast);
                distance = Mathf.Abs(hitInfo.transform.position.x - transform.position.x);
                moveTarget = transform.position + new Vector3(1 - distance, 0, 0);
                break;
            case Direct.right:
                raycast = new Ray(transform.position, Vector3.right);
                hitInfo = IsCheckRaycast(raycast);
                distance = Mathf.Abs(hitInfo.transform.position.x - transform.position.x);
                moveTarget = transform.position + new Vector3(distance - 1, 0, 0);
                break;
            default:
                break;
        }
    }

    public void ControllCharacter(Vector3 target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
    }

    #endregion

    #region Brick Control

    public void AddBrick()
    {
        collectedBrick.Add(brickPrefab);

        GameObject oj = Instantiate(brickPrefab.gameObject, transform.position, transform.rotation);    // Tao ra gach duoi chan nguoi choi
        anim.gameObject.transform.position += offset;                                                   // Dich hinh anh nhan vat len tren
        brickHolder.transform.position += offset;                                                       // Dich khoi gach len tren
        oj.transform.SetParent(brickHolder.transform);                                                  // Dat khoi gach la cha cua vien gach moi

        collectedPoint += 10;   //Cong them diem cho nguoi choi

    }

    public void RemoveBrick()
    {
        collectedBrick.Remove(brickPrefab);

        anim.gameObject.transform.position -= offset;
        brickHolder.transform.position -= offset;
    }

    public void ClearBrick()
    {
        collectedBrick.Clear();
        anim.gameObject.transform.position = gameObject.transform.position - new Vector3(0,0.2f,0);
        Destroy(brickHolder);
    }

    #endregion

    #region Interact

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gem"))
        {
            collectedGem++;
            collectedPoint += 100;
            Destroy(other.gameObject);
        }
    }

    #endregion
}
