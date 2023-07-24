using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public struct PlayerData : IEquatable<PlayerData>, INetworkSerializable
{
  public ulong Id;
  public Color color;
  public string playerName;
  public int playerWeapon;

  public void SetWeapon(int weapon)
  {
    playerWeapon = weapon;
  }
  public bool Equals(PlayerData other)
  {
    return
        Id == other.Id &&
        color == other.color &&
        playerName == other.playerName &&
        playerWeapon == other.playerWeapon;
  }

  public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
  {
    serializer.SerializeValue(ref Id);
    serializer.SerializeValue(ref color);
    serializer.SerializeValue(ref playerName);
    serializer.SerializeValue(ref playerWeapon);
  }

}