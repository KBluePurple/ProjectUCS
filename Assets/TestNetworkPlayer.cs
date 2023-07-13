using System;
using UnityEngine;

public class TestNetworkPlayer : MonoBehaviour
{
    public int UserId { get; set; }
    public bool IsLocalPlayer { get; set; }
    
    private void Update()
    {
        if (!IsLocalPlayer) return;
        
        var horizontal = Mathf.Sin(Time.time);
        var vertical = Mathf.Cos(Time.time);
        
        var position = transform.position;
        position.x += horizontal * Time.deltaTime * 5f;
        position.y += vertical * Time.deltaTime * 5f;
        transform.position = position;
    }
}