using System;
using System.Collections.Generic;
using TestTask.NonEditable;
using UnityEngine;

namespace TestTask.Editable
{
    public static class ClientPacketsHandler
    {
        #region Packet Handlers
        public static void LoginDataReceived(Packet packet)
        {
            int responseCode = packet.ReadInt();
            int clientId = packet.ReadInt();

            ClientManager.Instance.SetClientLogInStatus(responseCode, clientId);
        }

        public static void MonsterDataReceived(Packet packet)
        {
            int monsterId = packet.ReadInt();

            MonsterNames monsterType =
                (MonsterNames)packet.ReadInt();

            float monsterMaxHealth =
                packet.ReadFloat();

            float monsterCurrentHealth =
                packet.ReadFloat();

            MonsterData monsterData = new MonsterData(
                monsterId,
                monsterType,
                monsterMaxHealth,
                monsterCurrentHealth);

            ClientMobsManager.Instance.SpawnMonster(
                monsterData);
        }

        public static void ColorListReceived(Packet packet)
        {
            int colorCount = packet.ReadInt();

            List<Color> colors = new List<Color>();

            for (int i = 0; i < colorCount; i++)
            {
                float r = packet.ReadFloat();
                float g = packet.ReadFloat();
                float b = packet.ReadFloat();
                float a = packet.ReadFloat();

                colors.Add(new Color(r, g, b, a));
            }

            ClientColors.Instance.SpawnColors(colors);
        }

        #endregion

        #region Packet Senders
        public static void SendLoginRequest()
        {
            Packet packet = new Packet(1);
            ClientManager.Instance.PacketSenderClient.SendToServer(packet);
        }

        public static void SendMonsterDamageRequest(float damage)
        {
            Packet packet = new Packet(3);

            packet.Write(damage);

            ClientManager.Instance.PacketSenderClient.SendToServer(packet);
        }

        public static void SendColorListRequest()
        {
            Packet packet = new Packet(4);

            ClientManager.Instance.PacketSenderClient.SendToServer(packet);
        }
        #endregion
    }
}
