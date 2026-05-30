using System.Collections; 
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : UnitMovement {

    public bool moveani;

    public bool dontmove;

    private Unit player;

    private List<Vector3> TargetPositions;

    private bool automatic_control;

    private bool stopmovement;

    private bool sss;
    
    public Vector2[] Pos;

    void Start () {
        automatic_control = true;
        TileManager.Instance.GetTilemap("Wall").GetTiles();
        TargetPositions = new List<Vector3>();
    }
    
    void Update () {

        if (Player == null || 20f < Vector2.Distance(Unit.transform.position, Player.transform.position))
            return;


        if (CursorManager.Instance.GameisRunning && !Unit.Stun && !stop && !dontmove)
        {
            if (automatic_control)
            {
                if (Unit.GetUnitAttack<EnemyAttack>().player_distance < 8f)
                {
                    if(sss)
                        while (0 < TargetPositions.Count)
                            TargetPositions.Remove(TargetPositions[0]);

                    Movement();
                    sss = false;
                }
                else
                {
                    if(!sss)
                        while (0 < TargetPositions.Count)
                            TargetPositions.Remove(TargetPositions[0]);

                    for (int i = 0; i < Pos.Length; i++)
                    {
                        TargetPositions.Add(Pos[i]);
                    }

                    sss = true;
                }

                MoveToPosition();
            }
            else
                ManualMovement();

#region TEST
            if (Input.GetKeyDown(KeyCode.C))        
            {
                automatic_control = !automatic_control;
            }
#endregion


            Unit.Rigid.velocity = Vector3.zero;
        }
	}

    private void OnDrawGizmosSelected()
    {

        if (Pos != null)
        {
            for (int i = 0; i < Pos.Length; i++)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawCube(Pos[i], Vector3.one);
            }
        }
    }

    protected override void Movement()
    {
        if (Unit.GetUnitAttack<EnemyAttack>().DetectPlayer && !stopmovement) 
        {


            #region 
            Vector2[] fourdirection = new Vector2[4]
            {
            transform.position.Floor() + Vector3.up,
            transform.position.Floor() + Vector3.down,
            transform.position.Floor() + Vector3.left,
            transform.position.Floor() + Vector3.right,
            };

            Vector2 startvec = transform.position;
            Vector2 closest = Vector2.one * -1;

            Vector2 pos = Vector2.zero;

            for (int i = 0; i < fourdirection.Length; i++)
            {
                if (TileManager.Instance.GetTilemap("Wall").GetTile(fourdirection[i]) != null)
                    fourdirection[i] = Vector2.one * -1;
            }

            for (int i = 0; i < fourdirection.Length; i++)
            {
                if (fourdirection[i] == Vector2.one * -1)
                    continue;

                if (closest == Vector2.one * -1 || Vector2.Distance(fourdirection[i], Player.transform.position) <= Vector2.Distance(closest, Player.transform.position))
                    closest = fourdirection[i];
            }

            if (closest != Vector2.one * -1 && (TargetPositions.Count == 0 || (0 < TargetPositions.Count && (Vector2)TargetPositions[TargetPositions.Count - 1] != closest + (Vector2.one * 0.5f))))
            {
                for (int i = TargetPositions.Count - 1; 0 <= i; i--)
                {
                    if (Vector3.Distance(closest + (Vector2.one * 0.5f), Player.transform.position) <= Vector3.Distance(TargetPositions[i], Player.transform.position))
                        TargetPositions.RemoveAt(i);
                }
                TargetPositions.Add(closest + (Vector2.one * 0.5f));
                //Debug.Log(closest + (Vector2.one * 0.5f));
            }
            #endregion
        }
    }
    
    private void MoveToPosition()       
    {
        if (0 < TargetPositions.Count && !stopmovement)
        {
            Vector3 vec = Vector3.MoveTowards(transform.position, TargetPositions[0], Unit.Status.Speed * 0.01f);

            if (moveani)
                Unit.Animator.AnimationTrigger = (((vec - transform.position) * 10).normalized).Direction2AnimationType().AnimationTypeChange();
            else
                Unit.Animator.AnimationTrigger = (((vec - transform.position) * 10).normalized).Direction2AnimationType();

            //Debug.Log((((vec - transform.position) * 10).normalized).Direction2AnimationType().AnimationTypeChange());

            transform.position = vec;


            if (Vector2.Distance(transform.position,TargetPositions[0]) < 0.1f)
            {
                TargetPositions.Remove(TargetPositions[0]);
            }
        }
    }

    private void ManualMovement()
    {
        Vector2 vel = Vector2.zero;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            vel.y += Unit.Status.Speed;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            vel.y -= Unit.Status.Speed;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            vel.x -= Unit.Status.Speed;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            vel.x += Unit.Status.Speed;
        }

        Unit.Rigid.velocity = vel;
    }

    #region Porperty

    private Unit Player
    {
        get
        {
            if (player == null)
                player = UnitManager.Instance.GetUnit("Player");

            return player;
        }
    }

    public bool StopMovemnet
    {
        get
        {
            return stopmovement;
        }
        set
        {
            if(value)
            {
                TargetPositions.RemoveRange(0, TargetPositions.Count);
            }

            stopmovement = value;
        }
    }
    #endregion
}
