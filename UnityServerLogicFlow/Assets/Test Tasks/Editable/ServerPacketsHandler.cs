using TestTask.NonEditable;
using UnityEngine;

namespace TestTask.Editable
{
    public static class ServerPacketsHandler
    {
        #region Packet Handlers
        public static void LoginRequest(Packet packet)
        {
            LoginResponse response =
                ServerMock.Instance.TryConnectClient(
                    out int clientId);

            SendLoginResponse(
                response,
                clientId);

            if (response == LoginResponse.Success)
            {
                SendMonsterData();
            }
        }

        public static void MonsterDamageRequest(Packet packet)
        {
            float damage = packet.ReadFloat();

            ServerMock.Instance.ServerMobsManager.MonsterData.TakeDamage(damage);

            SendMonsterData();
        }

        public static void ColorListRequest(Packet packet)
        {
            SendColorListResponse();
        }

        #endregion

        #region Packet Senders
        public static void SendLoginResponse(LoginResponse response, int clientId)
        {
            using (Packet packet = new Packet(1))
            {
                packet.Write((int)response);
                packet.Write(clientId);

                ServerMock.Instance.PacketSenderServer.SendToClient(packet);
            }
        }


        public static void SendMonsterData()
        {
            var monsterData = ServerMock.Instance.ServerMobsManager.MonsterData;

            Packet packet = new Packet(2);
            packet.Write(monsterData.MonsterId);
            packet.Write((int)monsterData.MonsterType);
            packet.Write(monsterData.MonsterMaxHealth);
            packet.Write(monsterData.MonsterCurrentHealth);

            ServerMock.Instance.PacketSenderServer.SendToClient(packet);
        }

        public static void SendColorListResponse()
        {
            int colorCount = Random.Range(5, 11);

            Packet packet = new Packet(5);
            packet.Write(colorCount);

            for (int i = 0; i < colorCount; i++)
            {
                Color color = Random.ColorHSV();

                packet.Write(color.r);
                packet.Write(color.g);
                packet.Write(color.b);
                packet.Write(color.a);
            }

            ServerMock.Instance.PacketSenderServer.SendToClient(packet);
        }

        #endregion
    }
}

public enum LoginResponse
{
    Success = 0,
    Failure = 1,
}